using Caliburn.Micro;
using LiveCharts;
using LiveCharts.Defaults;
using LiveCharts.Wpf;
using RiskCalculator.Models;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RiskCalculator.ViewModels
{
    class RiskCalculationViewModel : Screen
    {
        private SeriesCollection _metricsSeriesCollection;

        public List<TrapezeModel> TrapList { get; set; }
        public BindableCollection<VulnerabilityModel> Vulnerabilities { get; set; }
        public BindableCollection<VulnerabilityModel> ResultVulnerabilities { get; set; }

        public SeriesCollection MetricsSeriesCollection
        {
            get => _metricsSeriesCollection;
            set
            {
                _metricsSeriesCollection = value;
                NotifyOfPropertyChange(() => MetricsSeriesCollection);
            }
        }

        public RiskCalculationViewModel(BindableCollection<VulnerabilityModel> vulnerabilities, List<TrapezeModel> trapList)
        {
            Vulnerabilities = vulnerabilities;
            TrapList = trapList;
            MetricsSeriesCollection = new SeriesCollection();
            ResultVulnerabilities = new BindableCollection<VulnerabilityModel>();
        }

        public void CalculateRisk()
        {
            ResultVulnerabilities.Clear();
          
            foreach (var v in Vulnerabilities)
            {
                v.metrics.baseV.uzB = new double[TrapList.Count];
                v.metrics.tempV.uzT = new double[TrapList.Count];
                v.metrics.envV.uzE = new double[TrapList.Count];

                // Если повторно нажали "Расчитать риск" нужно очистить словарь. Будет ругаться из-за однаковых ключей.
                v.SP.Clear();
            }

            double[,] arr = new double[3, Vulnerabilities.Count * TrapList.Count];

            int j = 0, k = 0;

            foreach (var v in Vulnerabilities)
            {
                double b = v.metrics.baseV.Score;
                double t = v.metrics.tempV.Score;
                double e = v.metrics.envV.Score;

                for (int i = 0; i < TrapList.Count; i++)
                {
                    v.metrics.baseV.uzB[i] = ClassifyMetrics(b, i);
                    v.metrics.tempV.uzT[i] = ClassifyMetrics(t, i);
                    v.metrics.envV.uzE[i] = ClassifyMetrics(e, i);

                    arr[j, k] = v.metrics.baseV.uzB[i];
                    arr[++j, k] = v.metrics.tempV.uzT[i];
                    arr[++j, k] = v.metrics.envV.uzE[i];

                    j = 0;
                    k++;
                }
                //  (Vulnerabilities.Count * TrapList.Count) - 1 == 24
                if (k == (Vulnerabilities.Count * TrapList.Count) - 1) k = 0;
            }

            double[] kLr = new double[TrapList.Count];

            for (int i = 0; i < kLr.Length; i++)
            {
                double t = TrapList.Count - (i + 1);
                kLr[i] = 90 - 20 * t;
            }

            int g = 3;

            double[] ls = new double[g];


            for (int i = 0; i < ls.Length; i++)
            {
                double t = 2 * (g - (i + 1) + 1);

                ls[i] = t / ((g - 1) * g);
            }

            // Коэффициет нормирования. // Определить переменную отвечающую за кол-во метрик.
            double ks = 1 / ls.Aggregate((x, y) => x + y);

            List<double> sumList = new List<double>();

            foreach (var v in Vulnerabilities)
            {
                double sum = 0;
                for (int i = 0; i < TrapList.Count; i++)
                {
                    double s = 0;
                    for (j = 0; j < g; j++)
                    {
                        s += ks * ls[j] * (j == 0 ? v.metrics.baseV.uzB[i] :
                            j == 1 ? v.metrics.tempV.uzT[i] : v.metrics.envV.uzE[i]);
                    }
                    sum += kLr[i] * s;
                }
                v.Lrv = sum;
                sumList.Add(sum);
            }         

            for (int i = 0; i < Vulnerabilities.Count; i++)
            {
                Vulnerabilities[i].uLrv = new double[TrapList.Count];
            }

            foreach (var v in Vulnerabilities)
            {
                for (int i = 0; i < TrapList.Count; i++)
                {
                    v.uLrv[i] = SubClassifyMetricsTest(v.Lrv, i);

                    if (v.uLrv[i] != 0)
                    {
                        v.SP.Add(v.uLrv[i], TrapList[i].degreeRisk);
                    }
                }
            }

            foreach (var v in Vulnerabilities)
            {
                ResultVulnerabilities.Add(v);
            }

            DrawRiskDegree();
        }

        public void DrawRiskDegree()
        {
            MetricsSeriesCollection.Clear();

            string[] severityTrap = SeverityModel.GetSevetity(TrapList.Count);

            int j = 0;

            foreach (var trap in TrapList)
            {
                TrapList[j].degreeRisk = severityTrap[j];
                MetricsSeriesCollection.Add(new LineSeries()
                {
                    Values = new ChartValues<ObservablePoint>()
                    {
                        new ObservablePoint(trap.a * 10, 0),
                        new ObservablePoint(trap.b11 * 10, 1),
                        new ObservablePoint(trap.b21 * 10, 1),
                        new ObservablePoint(trap.c * 10, 0),
                    },
                    LineSmoothness = 0,
                    //TODO: Добавить проверку на выход из-за пределов массива.
                    Title = severityTrap[j++]
                });
            }

       
            foreach (var v in Vulnerabilities)
            {
                MetricsSeriesCollection.Add(new LineSeries()
                {
                    Values = new ChartValues<ObservablePoint>()
                    {
                        new ObservablePoint(v.Lrv, 0),
                        new ObservablePoint(v.Lrv, 1)
                    },
                    Title = v.Id
                });
            }
        }

        /// <summary>
        /// Классификация текущих значений (Шаг 9).
        /// </summary>
        /// <param name="v">Value - значение оценки метрики (B || T || E)</param>
        private double ClassifyMetrics(double v, int curTrap)
        {
            for (int i = curTrap; i < TrapList.Count; i++)
            {
                if (i == 0)
                {
                    if (v >= TrapList[i].b11 && v < TrapList[i].b21)
                    {
                        return 1.0;
                    }
                    else if (!(v >= TrapList[i].b11 && v < TrapList[i].c))
                    {
                        return 0;
                    }
                    else if (v >= TrapList[i].b21 && v < TrapList[i].c)
                    {
                        return SubClassifyMetrics(v, i);
                    }
                }
                else if (i != TrapList.Count - 1)
                {
                    if (v >= TrapList[i].a && v < TrapList[i].b11)
                    {
                        return SubClassifyMetrics(v, i);
                    }
                    else if (v >= TrapList[i].b11 && v < TrapList[i].b21)
                    {
                        return 1.0;
                    }
                    else if (v >= TrapList[i].b21 && v < TrapList[i].c)
                    {
                        return SubClassifyMetrics(v, i);
                    }
                    else if (!(v >= TrapList[i].a && v < TrapList[i].c))
                    {
                        return 0;
                    }
                }
                else
                {
                    if (v >= TrapList[i].a && v < TrapList[i].b11)
                    {
                        return SubClassifyMetrics(v, i);
                    }
                    else if (v >= TrapList[i].b11 && v < TrapList[i].b21)
                    {
                        return 1.0;
                    }
                    else if (!(v >= TrapList[i].a && v < TrapList[i].b21))
                    {
                        return 0;
                    }
                }
            }

            return -98;
        }

        private double SubClassifyMetrics(double v, int curTrap)
        {
            int i = curTrap;

            if (v >= TrapList[i].a && v <= TrapList[i].b11)
            {
                return Math.Round((TrapList[i].a - v) / (TrapList[i].a - TrapList[i].b11), 2);
            }
            else if (v >= TrapList[i].b11 && v <= TrapList[i].b21)
            {
                return 1.0;
            }
            else if (v >= TrapList[i].b21 && v <= TrapList[i].c)
            {
                return Math.Round((v - TrapList[i].c) / (TrapList[i].b21 - TrapList[i].c), 2);
            }

            // For error.
            return -99;
        }

        private double SubClassifyMetricsTest(double v, int curTrap)
        {
            int i = curTrap;

            if (v >= TrapList[i].a * 10 && v <= TrapList[i].b11 * 10)
            {
                return Math.Round((TrapList[i].a * 10 - v) / (TrapList[i].a * 10 - TrapList[i].b11 * 10), 2);
            }
            else if (v >= TrapList[i].b11 * 10 && v <= TrapList[i].b21 * 10)
            {
                return 1.0;
            }
            else if (v >= TrapList[i].b21 * 10 && v <= TrapList[i].c * 10)
            {
                return Math.Round((v - TrapList[i].c * 10) / (TrapList[i].b21 * 10 - TrapList[i].c * 10), 2);
            }
            else return 0;
        }

    }
}
