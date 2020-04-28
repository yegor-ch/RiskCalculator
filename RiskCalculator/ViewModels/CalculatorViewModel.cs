using Caliburn.Micro;
using RiskCalculator.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;

namespace RiskCalculator.ViewModels
{
    class CvssMetrics
    {
        private Dictionary<string, double> AV { get; set; }
        private Dictionary<string, double> AC { get; set; }
        private Dictionary<string, double> PRU { get; set; }
        private Dictionary<string, double> PRC { get; set; }
        private Dictionary<string, double> UI { get; set; }
        private Dictionary<string, double> S { get; set; }
        private Dictionary<string, double> CIA { get; set; }
        private Dictionary<string, double> E { get; set; }
        private Dictionary<string, double> RL { get; set; }
        private Dictionary<string, double> RC { get; set; }
        private Dictionary<string, double> CIAR { get; set; }

        #region METRIC WEIGHT
        public double AV_Weight { get; set; }
        public double AC_Weight { get; set; }
        public double PR_Weight { get; set; }
        public double UI_Weight { get; set; }
        public double S_Weight { get; set; }
        public double C_Weight { get; set; }
        public double I_Weight { get; set; }
        public double A_Weight { get; set; }
        public double E_Weight { get; set; }
        public double RL_Weight { get; set; }
        public double RC_Weight { get; set; }
        public double CR_Weight { get; set; }
        public double IR_Weight { get; set; }
        public double AR_Weight { get; set; }
        public double MAV_Weight { get; set; }
        public double MAC_Weight { get; set; }
        public double MPR_Weight { get; set; }
        public double MUI_Weight { get; set; }
        public double MS_Weight { get; set; }
        public double MC_Weight { get; set; }
        public double MI_Weight { get; set; }
        public double MA_Weight { get; set; }
        #endregion

        public Dictionary<string, Dictionary<string, double>> WeightDict { get; set; }

        public Dictionary<string, string> MetricsKeys { get; set; }
        public Dictionary<string, double> MetricsWeight { get; set; }

        

        public string PR_Key { get; set; }

        public CvssMetrics()
        {
            AV = new Dictionary<string, double>
            {
                { "N", 0.85 },
                { "A", 0.62 },
                { "L", 0.55 },
                { "P", 0.2 }
            };

            AC = new Dictionary<string, double>
            {
                { "H", 0.44 },
                { "L", 0.77 }
            };

            PRU = new Dictionary<string, double>
            {
                { "N", 0.85 },
                { "L", 0.62 },
                { "H", 0.27 },

            };

            PRC = new Dictionary<string, double>
            {
                { "N", 0.85 },
                { "L", 0.68 },
                { "H", 0.5 },

            };

            UI = new Dictionary<string, double>
            {
                { "N", 0.85 },
                { "R", 0.62 }
            };

            S = new Dictionary<string, double>
            {
                { "U", 6.42 },
                { "C", 7.52 }
            };

            CIA = new Dictionary<string, double>
            {
                { "N", 0.0 },
                { "L", 0.22 },
                { "H", 0.56 },

            };

            E = new Dictionary<string, double>
            {
                { "X", 1.0 },
                { "U", 0.91 },
                { "P", 0.94 },
                { "F", 0.97 },
                { "H", 1.0 },
            };

            RL = new Dictionary<string, double>
            {
                { "X", 1.0 },
                { "O", 0.95 },
                { "T", 0.96 },
                { "W", 0.97 },
                { "U", 1.0 },
            };

            RC = new Dictionary<string, double>
            {
                { "X", 1.0 },
                { "U", 0.92 },
                { "R", 0.96 },
                { "C", 1.0 }
            };

            CIAR = new Dictionary<string, double>
            {
                { "X", 1.0 },
                { "L", 0.5 },
                { "M", 1.0 },
                { "H", 1.5 }
            };

            WeightDict = new Dictionary<string, Dictionary<string, double>>
            {
                { "AV",   AV },
                { "AC",   AC },
                { "PRU",  PRU },
                { "PRC",  PRC },
                { "UI",   UI },
                { "S",    S },
                { "E",    E },
                { "RL",   RL },
                { "RC",   RC },
                { "CIA",  CIA },
                { "CIAR", CIAR }
            };

            // Метрики относящиеся к Temporal, Envirmental должны быть изначально Not defined.
            MetricsKeys = new Dictionary<string, string>
            {
                { "E", "X" },
                { "RL", "X" },
                { "RC", "X" },
                { "MAV", "X" },
                { "MAC", "X" },
                { "MPR", "X" },
                { "MUI", "X" },
                { "MS",  "X" },
                { "MC",  "X" },
                { "MI",  "X" },
                { "MA",  "X" },
                { "CR",  "X" },
                { "IR",  "X" },
                { "AR",  "X" }
                
            };

            MetricsWeight = new Dictionary<string, double>();
        }
    }

    class CalculatorViewModel : Screen
    {

        #region BOOLEAN RADIO PROPERTIES

        #region Base Score Metrics
        // Attack Vector (AV)
        public bool AV_N { get; set; }
        public bool AV_A { get; set; }
        public bool AV_L { get; set; }
        public bool AV_P { get; set; }

        // Attack Complexity (AC)
        public bool AC_L { get; set; }
        public bool AC_H { get; set; }

        // Privileges Required (PR)
        public bool PR_N { get; set; }
        public bool PR_L { get; set; }
        public bool PR_H { get; set; }

        // User Interaction (UI)
        public bool UI_N { get; set; }
        public bool UI_R { get; set; }

        // Scope (S)
        public bool S_U { get; set; }
        public bool S_C { get; set; }

        // Confidentiality Impact (C)
        public bool C_N { get; set; }
        public bool C_L { get; set; }
        public bool C_H { get; set; }

        // Confidentiality Impact (C)
        public bool I_N { get; set; }
        public bool I_L { get; set; }
        public bool I_H { get; set; }

        // Availability Impact (A)
        public bool A_N { get; set; }
        public bool A_L { get; set; }
        public bool A_H { get; set; }
        #endregion

        #region Temporal Score Metrics
        // Remediation Level (RL)
        public bool E_X { get; set; } = true;
        public bool E_U { get; set; }
        public bool E_P { get; set; }
        public bool E_F { get; set; }
        public bool E_H { get; set; }

        // Exploitability (E)
        public bool RL_X { get; set; } = true;
        public bool RL_O { get; set; }
        public bool RL_T { get; set; }
        public bool RL_W { get; set; }
        public bool RL_U { get; set; }

        // Report Confidence (RC)
        public bool RC_X { get; set; } = true;
        public bool RC_U { get; set; }
        public bool RC_R { get; set; }
        public bool RC_C { get; set; }

        #endregion

        #region Environmental Score Metrics
        // Attack Vector (AV)
        public bool MAV_X { get; set; } = true;
        public bool MAV_N { get; set; }
        public bool MAV_A { get; set; }
        public bool MAV_L { get; set; }
        public bool MAV_P { get; set; }

        // Attack Complexity (AC)
        public bool MAC_X { get; set; } = true;
        public bool MAC_L { get; set; }
        public bool MAC_H { get; set; }

        // Privileges Required (PR)
        public bool MPR_X { get; set; } = true;
        public bool MPR_N { get; set; }
        public bool MPR_L { get; set; }
        public bool MPR_H { get; set; }

        // User Interaction (UI)
        public bool MUI_X { get; set; } = true;
        public bool MUI_N { get; set; }
        public bool MUI_R { get; set; }

        // Scope (S)
        public bool MS_X { get; set; } = true;
        public bool MS_U { get; set; }
        public bool MS_C { get; set; }

        // Impact Metrics
        // Confidentiality Impact(C)
        public bool MC_X { get; set; } = true;
        public bool MC_N { get; set; }
        public bool MC_L { get; set; }
        public bool MC_H { get; set; }

        // Integrity Impact (I)
        public bool MI_X { get; set; } = true;
        public bool MI_N { get; set; }
        public bool MI_L { get; set; }
        public bool MI_H { get; set; }

        // Availability Impact (A)
        public bool MA_X { get; set; } = true;
        public bool MA_N { get; set; }
        public bool MA_L { get; set; }
        public bool MA_H { get; set; }

        // Impact Subscore Modifiers
        // Confidentiality Requirement (CR)
        public bool CR_X { get; set; } = true;
        public bool CR_L { get; set; }
        public bool CR_M { get; set; }
        public bool CR_H { get; set; }

        // Integrity Requirement (IR)
        public bool IR_X { get; set; } = true;
        public bool IR_L { get; set; }
        public bool IR_M { get; set; }
        public bool IR_H { get; set; }

        // Integrity Requirement (IR)
        public bool AR_X { get; set; } = true;
        public bool AR_L { get; set; }
        public bool AR_M { get; set; }
        public bool AR_H { get; set; }
        #endregion

        #endregion

        public double ExploitabilityCoefficient { get; set; } = 8.22;
        public double ScopeCoefficient { get; set; } = 1.08;

        public double BaseScore { get; set; }
        public double ImpactSubScore { get; set; }
        public double ExploitabalitySubScore { get; set; }

        public double TemporalScore { get; set; }

        public double EnvScore { get; set; }
        public double EnvModifiedImpactSubScore { get; set; }

        public double OverallScore { get; set; }

        public string CvssVectorString { get; set; }

        public CvssMetrics Metrics { get; set; }


        public BindableCollection<VulnerabilityModel> Vulnerabilities { get; set; }
        public CalculatorViewModel(BindableCollection<VulnerabilityModel> Vulnerabilities)
        {
            this.Vulnerabilities = Vulnerabilities;

            Metrics = new CvssMetrics();

        }

        public void OnCheckedChanged(RoutedEventArgs e)
        {
            string rbName = (e.OriginalSource as RadioButton).Name;

            int divider = rbName.IndexOf('_');

            string name = rbName.Substring(0, divider);
            string value = rbName.Substring(divider + 1);

            SetMetricKey(name, value);

            //InitializeMetricWeight(name, value);

            InitializeMetrics(name, value);

            CvssVectorString = ConstructVectorString(Metrics.MetricsKeys);

            CalculateCvss3(Metrics.MetricsWeight);
        }

        public void SetMetricKey(string keyName, string value)
        {
            if (Metrics.MetricsKeys.ContainsKey(keyName))
            {
                Metrics.MetricsKeys.Remove(keyName);
            }

            Metrics.MetricsKeys.Add(keyName, value);
        }

        public void SetMetricWeight(string metricName, double value)
        {
            if (Metrics.MetricsWeight.ContainsKey(metricName))
            {
                Metrics.MetricsWeight.Remove(metricName);
            }

            Metrics.MetricsWeight.Add(metricName, value);
        }

        public void InitializeMetrics(string metricName, string value)
        {
            string correctedName = metricName;

            // При установке PR нужно проверить установлено ли Scope.
            if (metricName.Equals("PR"))
            {
                try
                {
                    // Получаем значение Scope.
                    // Сhanged or Unchanged (U,C).
                    // PRU, PRC.
                    correctedName += Metrics.MetricsKeys["S"];
                }
                catch(KeyNotFoundException)
                {
                    // Если не нашло ключ, просто заканчиваем выполнение.
                    // В будущем, при указании Scope, оно всё-равно пересчитает PR.
                    return;
                }
            }
            // Если было изменено S нужно пересчитать PR и MPR.
            if (metricName.Equals("S"))
            {
                try
                {
                    InitializeMetrics("PR", Metrics.MetricsKeys["PR"]);
                    InitializeMetrics("MPR", Metrics.MetricsKeys["MPR"]);
                }
                catch(KeyNotFoundException)
                {
                    // Может не найти какой-то из ключей. Это не критично, продолжаем выполнение и устанавливаем Weiht для Scope.
                    //... 
                }
            }

            // При расчете MPR нужно учитывать MS, S, MPR, PR.
            if (metricName.Equals("MPR"))
            {
                //PRU, PRC 
                string scopeState;
                try
                {
                    scopeState = Metrics.MetricsKeys["MS"] == "X" ? Metrics.MetricsKeys["S"] : Metrics.MetricsKeys["MS"];
                }
                catch(KeyNotFoundException)
                {
                    return;
                }

                correctedName = scopeState == "U" ? "PRU" : "PRC";

                // Если значение метрики MPR Not defined - X, MPR берем из базовой метрики PR.
                if (value.Equals("X"))
                {
                    try
                    {
                        value = Metrics.MetricsKeys["PR"];
                    }
                    catch(KeyNotFoundException)
                    {
                        return;
                    }
                    
                }
            }
            // Все Environmental метрики, которые начинаются на М расчитытываются из БАЗОВЫХ метрик.
            else if (correctedName.StartsWith("M"))
            {
                // MS вляет на MPR, поэтому перерасчитываем его.
                if (metricName.Equals("MS"))
                {
                    InitializeMetrics("MPR", Metrics.MetricsKeys["MPR"]);
                }

                // Если значение метрики Not defined - X, метрика берется из уже определенных базовых метрик.
                if (value.Equals("X"))
                {
                    value = Metrics.MetricsKeys[metricName.Substring(1)];
                }

                // Получаем имя метрики без М.
                // MAV - AV, MAC - AC ...
                correctedName = metricName.Substring(1);
            }

            if (correctedName.Equals("C") || correctedName.Equals("I") || correctedName.Equals("A"))
            {
                correctedName = "CIA";
            }

            if (correctedName.Equals("CR") || correctedName.Equals("IR") || correctedName.Equals("AR"))
            {
                correctedName = "CIAR";
            }

            double weight = Metrics.WeightDict[correctedName][value];

            SetMetricWeight(metricName, weight);
        }

        public void CalculateCvss3(Dictionary<string, double> weights)
        {
            double metricWeightAV;
            double metricWeightAC;
            double metricWeightPR;
            double metricWeightUI;
            double metricWeightS;
            double metricWeightC;
            double metricWeightI;
            double metricWeightA;


            // Для начала инициализируем базовые метрики.
            try
            {
                metricWeightAV = weights["AV"];
                metricWeightAC = weights["AC"];
                metricWeightPR = weights["PR"];
                metricWeightUI = weights["UI"];
                metricWeightS  = weights["S"];
                metricWeightC  = weights["C"];
                metricWeightI  = weights["I"];
                metricWeightA  = weights["A"];
            }
            // Если хотя-бы одна метрика не была указанна из базовых, выходим.
            catch(KeyNotFoundException)
            {
                return;
            }

            #region BASE SCORE CALCULATION

            ExploitabalitySubScore = ExploitabilityCoefficient * metricWeightAV * metricWeightAC * metricWeightPR * metricWeightUI;

            double impactSubScoreMultiplier = 1 - (1 - metricWeightC) * (1 - metricWeightI) * (1 - metricWeightA);

            if(Metrics.MetricsKeys["S"] == "U")
            {
                ImpactSubScore = metricWeightS * impactSubScoreMultiplier;
            }
            else
            {
                ImpactSubScore = metricWeightS * (impactSubScoreMultiplier - 0.029) - 3.25 * Math.Pow(impactSubScoreMultiplier - 0.02, 15);
            }

            if (ImpactSubScore <= 0)
            {
                BaseScore = 0;
            }
            else
            {
                if (Metrics.MetricsKeys["S"] == "U")
                {
                    BaseScore = RaundUp(Math.Min(ExploitabalitySubScore + ImpactSubScore, 10));
                }
                else
                {
                    BaseScore = RaundUp(Math.Min((ExploitabalitySubScore + ImpactSubScore) * ScopeCoefficient, 10));
                }
            }
            #endregion


            // Пост-инициализация метрик Temporal, Environmental.
            // Пройдемся по всем необязательным метрикам и если там имеются Not defined (X) метрики,
            // возьмем их
            var notDefValues = Metrics.MetricsKeys.Where(v => v.Value == "X");

            foreach (var item in notDefValues)
            {
                InitializeMetrics(item.Key, item.Value);
            }

            #region TEMPORAL SCORE CALCULATION

            double metricWeightE  = weights["E"];
            double metricWeightRL = weights["RL"];
            double metricWeightRC = weights["RC"];

            TemporalScore = RaundUp(BaseScore * metricWeightE * metricWeightRL * metricWeightRC);

            #endregion

            #region ENVIRONMENTAL SCORE CALCULATION

            double metricWeightCR  = weights["CR"];
            double metricWeightIR  = weights["IR"];
            double metricWeightAR  = weights["AR"];
            double metricWeightMAV = weights["MAV"];
            double metricWeightMAC = weights["MAC"];
            double metricWeightMPR = weights["MPR"];
            double metricWeightMUI = weights["MUI"];
            double metricWeightMS  = weights["MS"];
            double metricWeightMC  = weights["MC"];
            double metricWeightMI  = weights["MI"];
            double metricWeightMA  = weights["MA"];

            double envModifiedExploitabalitySubScore = ExploitabilityCoefficient * metricWeightMAV * metricWeightMAC * metricWeightMPR * metricWeightMUI;

            double envImpactSubScoreMultiplier = Math.Min(1 - (1 - metricWeightMC * metricWeightCR) * 
                (1 - metricWeightMI * metricWeightIR) * (1 - metricWeightMA * metricWeightAR), 0.915);

            if(Metrics.MetricsKeys["MS"] == "U" || 
              (Metrics.MetricsKeys["MS"] == "X" && 
               Metrics.MetricsKeys["S"] == "U"))
            {
                EnvModifiedImpactSubScore = metricWeightMS * envImpactSubScoreMultiplier;
                EnvScore = RaundUp(RaundUp(Math.Min(EnvModifiedImpactSubScore + envModifiedExploitabalitySubScore, 10)) * metricWeightE * metricWeightRL * metricWeightRC);
            }
            else
            {
                EnvModifiedImpactSubScore = metricWeightMS * (envImpactSubScoreMultiplier - 0.029) - 3.25 * Math.Pow(envImpactSubScoreMultiplier - 0.02, 15);
                EnvScore = RaundUp(RaundUp(Math.Min(ScopeCoefficient * (EnvModifiedImpactSubScore + envModifiedExploitabalitySubScore), 10)) * metricWeightE * metricWeightRL * metricWeightRC);
            }

            if(EnvModifiedImpactSubScore <= 0)
            {
                EnvScore = 0;
            }

            #endregion
        }

        public double RaundUp(double d)
        {
            return Math.Ceiling(d * 10) / 10;
        }

        public string ConstructVectorString(Dictionary<string, string> idenifiers)
        {
            string vector;
            try
            {
                vector =
                    "CVSS:3.0" +
                    "/AV:" + idenifiers["AV"] +
                    "/AC:" + idenifiers["AC"] +
                    "/PR:" + idenifiers["PR"] +
                    "/UI:" + idenifiers["UI"] +
                    "/S:" + idenifiers["S"] +
                    "/C:" + idenifiers["C"] +
                    "/I:" + idenifiers["I"] +
                    "/A:" + idenifiers["A"];
            }
            catch(KeyNotFoundException)
            {
                return "";
            }

            vector += idenifiers["E"] != "X" ? "/E:" + idenifiers["E"] : "";
            vector += idenifiers["RL"] != "X" ? "/RL:" + idenifiers["RL"] : "";
            vector += idenifiers["RC"] != "X" ? "/RC:" + idenifiers["RC"] : "";

            vector += idenifiers["CR"] != "X" ? "/CR:" + idenifiers["CR"] : "";
            vector += idenifiers["IR"] != "X" ? "/IR:" + idenifiers["IR"] : "";
            vector += idenifiers["AR"] != "X" ? "/AR:" + idenifiers["AR"] : "";
            vector += idenifiers["MAV"] != "X" ? "/MAV:" + idenifiers["MAV"] : "";
            vector += idenifiers["MAC"] != "X" ? "/MAC:" + idenifiers["MAC"] : "";
            vector += idenifiers["MPR"] != "X" ? "/MPR:" + idenifiers["MPR"] : "";
            vector += idenifiers["MUI"] != "X" ? "/MUI:" + idenifiers["MUI"] : "";
            vector += idenifiers["MS"] != "X" ? "/MS:" + idenifiers["MS"] : "";
            vector += idenifiers["MC"] != "X" ? "/MC:" + idenifiers["MC"] : "";
            vector += idenifiers["MI"] != "X" ? "/MI:" + idenifiers["MI"] : "";
            vector += idenifiers["MA"] != "X" ? "/MA:" + idenifiers["MA"] : "";

            return vector;
        }

        /// <summary>
        /// Инициализирует указанную метрику, получая соответствующие значение из словаря.
        /// </summary>
        /// <param name="metricName">Имя свойства метрики которое нужно инициализировать.</param>
        /// <param name="key">Ключ по-которому будет осуществляться доступ значения в словаре.</param>
        public void InitializeMetricWeight(string metricName, string key)
        {
            string correctedName = metricName;

            if (correctedName.StartsWith("M") && !key.Equals("X"))
            {
                // Получаем имя метрики без М.
                // MAV - AV, MAC - AC ...
                correctedName = metricName.Substring(1);
            }
            else if (key.Equals("X"))
            {

            }

            // При установке PR нужно проверить установлено ли Scope.
            if (correctedName.Equals("PR"))
            {
                // Запоминаем ключ PR - пригодится, когда будет изменено Scope.
                Metrics.PR_Key = key;

                correctedName += S_U ? "U" : S_C ? "C" : "";

                if (metricName.Equals(correctedName)) return;
            }

            if(correctedName.Equals("S"))
            {
                // Если было изменено S нужно пересчитать PR.
                InitializeMetricWeight("PR", Metrics.PR_Key);
            }

            if(correctedName.Equals("C") || correctedName.Equals("I") || correctedName.Equals("A"))
            {
                correctedName = "CIA";
            }

            if (correctedName.Equals("CR") || correctedName.Equals("IR") || correctedName.Equals("AR"))
            {
                correctedName = "CIAR";
            }
        
            // Нашли нужный словарь.
            var metricProperty = Metrics.GetType().GetProperty(correctedName);

            // Получили значение из словаря по ключу.
            double metricWeight = (metricProperty.GetValue(Metrics) as Dictionary<string, double>)[key];

            // Если correctedName изменялся, значит надо установить его назад, чтобы свойства *_Weight записывались без ошибок.
            // Потому что словарь может быть один на несколько метрик, но сами значения метрик должы записываться в свои поля.
            if (!correctedName.Equals(metricName))
            {
                correctedName = metricName;
            }

            // Установили значение метрики *_Weight в значение полученное из словаря.
            Metrics.GetType().GetProperty(correctedName + "_Weight").SetValue(Metrics, metricWeight);
        }
    }
}
;