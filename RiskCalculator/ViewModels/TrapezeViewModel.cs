using Caliburn.Micro;
using LiveCharts;
using LiveCharts.Defaults;
using LiveCharts.Wpf;
using RiskCalculator.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RiskCalculator.ViewModels
{
    class TrapezeViewModel: Screen
    {        
        private SeriesCollection _metricsSeriesCollection;
        private BindableCollection<IntervalModel> _intervalList;
        private IntervalModel _selectedInterval;
        private string _intervalA;
        private string _intervalB;
        private bool _isUserInterval;

        private List<TrapezeModel> testList;

        public List<TrapezeModel> TrapList { get; set; }
        public BindableCollection<IntervalModel> IntervalList 
        { 
            get => _intervalList;
            set
            {
                _intervalList = value;
                NotifyOfPropertyChange(() => IntervalList);
            }
        }
        public IntervalModel SelectedInterval 
        {
            get { return _selectedInterval; }
            set
            {
                _selectedInterval = value;
                NotifyOfPropertyChange(() => SelectedInterval);
            }
        }
        public string IntervalA { get => _intervalA; set { _intervalA = value; NotifyOfPropertyChange(() => IntervalA); } }
        public string IntervalB { get => _intervalB; set { _intervalB = value; NotifyOfPropertyChange(() => IntervalB); } }

        public SeriesCollection MetricsSeriesCollection 
        { 
            get => _metricsSeriesCollection;
            set
            {
                _metricsSeriesCollection = value;
                NotifyOfPropertyChange(() => MetricsSeriesCollection);
            }
        }

        //public bool IsUserInterval { get => _isUserInterval; set { _isUserInterval = value; NotifyOfPropertyChange(() => IsUserInterval); } }

        public TrapezeViewModel(ref List<TrapezeModel> trapList, SeriesCollection metricsSeriesCollection)
        {
            TrapList = trapList;
            MetricsSeriesCollection = metricsSeriesCollection;
            //MetricsSeriesCollection = new SeriesCollection();

            IntervalList = new BindableCollection<IntervalModel>
            {
                new IntervalModel { a = 0, b = 20 },
                new IntervalModel { a = 20, b = 40 },
                new IntervalModel { a = 40, b = 60 },
                new IntervalModel { a = 60, b = 80 },
                new IntervalModel { a = 80, b = 100 }
            };

            TrapList = BuildChart();
            trapList = TrapList;
            DrawChart();

        }

        public void AddInterval()
        {
            var interval = new IntervalModel();

            double a, b;

            try
            {
                a = double.Parse(IntervalA);
                b = double.Parse(IntervalB);
            }
            catch
            {
                return;
            }

            interval.a = a;
            interval.b = b;

            if (IntervalList.Contains(interval))
            {
                return;
            }

            IntervalList.Add(interval);
        }

        public void RemoveInterval()
        {
            if(IntervalList.Contains(SelectedInterval))
            {
                IntervalList.Remove(SelectedInterval);
            }
        }

        public List<TrapezeModel> BuildChart()
        {
            double[] interval = new double[IntervalList.Count * 2];

            for (int i = 0, k = 0; i < interval.Length; i++, k++)
            {
                interval[i] = IntervalList[k].a;
                interval[++i] = IntervalList[k].b;
            }

            return TrapezeCreator.CreateTrapezeList(10.0, IntervalList.Count, interval);            
        }

        public void DrawChart()
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
                        new ObservablePoint(trap.a, 0),
                        new ObservablePoint(trap.b11, 1),
                        new ObservablePoint(trap.b21, 1),
                        new ObservablePoint(trap.c, 0),
                    },
                    LineSmoothness = 0,
                    //TODO: Добавить проверку на выход из-за пределов массива.
                    Title = severityTrap[j++]
                });                
            }
        }

        public void Inctement()
        {
            var temp = TrapezeCreator.IncrementTrapezeList(TrapList, 10.0);
            TrapList.Clear();
            TrapList.AddRange(temp);
            DrawChart();
        }

        public void Decrement()
        {
            //TODO: Добавить декрементирование для неравноменрых интервалов.
            var temp = TrapezeCreator.DecrementTrapezeList(TrapList, 10.0);
            TrapList.Clear();
            TrapList.AddRange(temp);
            DrawChart();
        }
    }
}
