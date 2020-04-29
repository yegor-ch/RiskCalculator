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

        public Dictionary<string, string> MetricsIdentifiers { get; set; }

        public Dictionary<string, string> ConstMetricsIdentifiers { get; private set; }

        public Dictionary<string, double> MetricsWeight { get; set; }

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

            // Определим константные метрики, которые будут использоваться, когда полученный вектор не будет иметь значений Temporal, Envirmental.
            ConstMetricsIdentifiers = new Dictionary<string, string>
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

            // Метрики относящиеся к Temporal, Envirmental должны быть изначально Not defined.
            MetricsIdentifiers = new Dictionary<string, string>(ConstMetricsIdentifiers);

            MetricsWeight = new Dictionary<string, double>();
        }
    }

    class CalculatorViewModel : Screen
    {

        #region BOOLEAN RADIO PROPERTIES

        #region Base Score Metrics
        
        // Attack Vector (AV)
        private bool _av_n;
        public bool AV_N
        {
            get => _av_n;
            set
            {
                _av_n = value;
                NotifyOfPropertyChange(() => AV_N);
            }
        }

        private bool _av_a;
        public bool AV_A
        {
            get => _av_a;
            set
            {
                _av_a = value;
                NotifyOfPropertyChange(() => AV_A);
            }
        }

        private bool _av_l;
        public bool AV_L
        {
            get => _av_l;
            set
            {
                _av_l = value;
                NotifyOfPropertyChange(() => AV_L);
            }
        }

        private bool _av_p;
        public bool AV_P
        {
            get => _av_p;
            set
            {
                _av_p = value;
                NotifyOfPropertyChange(() => AV_P);
            }
        }

        // Attack Complexity (AC)
        private bool _ac_l;
        public bool AC_L
        {
            get => _ac_l;
            set
            {
                _ac_l = value;
                NotifyOfPropertyChange(() => AC_L);
            }
        }

        private bool _ac_h;
        public bool AC_H
        {
            get => _ac_h;
            set
            {
                _ac_h = value;
                NotifyOfPropertyChange(() => AC_H);
            }
        }

        // Privileges Required (PR)
        private bool _pr_n;
        public bool PR_N
        {
            get => _pr_n;
            set
            {
                _pr_n = value;
                NotifyOfPropertyChange(() => PR_N);
            }
        }

        private bool _pr_l;
        public bool PR_L
        {
            get => _pr_l;
            set
            {
                _pr_l = value;
                NotifyOfPropertyChange(() => PR_L);
            }
        }

        private bool _pr_h;
        public bool PR_H
        {
            get => _pr_h;
            set
            {
                _pr_h = value;
                NotifyOfPropertyChange(() => PR_H);
            }
        }

        // User Interaction (UI)
        private bool _ui_n;
        public bool UI_N
        {
            get => _ui_n;
            set
            {
                _ui_n = value;
                NotifyOfPropertyChange(() => UI_N);
            }
        }

        private bool _ui_r;
        public bool UI_R
        {
            get => _ui_r;
            set
            {
                _ui_r = value;
                NotifyOfPropertyChange(() => UI_R);
            }
        }
        
        // Scope (S)
        private bool _s_u;
        public bool S_U
        {
            get => _s_u;
            set
            {
                _s_u = value;
                NotifyOfPropertyChange(() => S_U);
            }
        }

        private bool _s_c;
        public bool S_C
        {
            get => _s_c;
            set
            {
                _s_c = value;
                NotifyOfPropertyChange(() => S_C);
            }
        }
        
        // Confidentiality Impact (C)
        private bool _c_n;
        public bool C_N
        {
            get => _c_n;
            set
            {
                _c_n = value;
                NotifyOfPropertyChange(() => C_N);
            }
        }

        private bool _c_l;
        public bool C_L
        {
            get => _c_l;
            set
            {
                _c_l = value;
                NotifyOfPropertyChange(() => C_L);
            }
        }

        private bool _c_h;
        public bool C_H
        {
            get => _c_h;
            set
            {
                _c_h = value;
                NotifyOfPropertyChange(() => C_H);
            }
        }

        // Integrity Impact (I)
        private bool _i_n;
        public bool I_N
        {
            get => _i_n;
            set
            {
                _i_n = value;
                NotifyOfPropertyChange(() => I_N);
            }
        }

        private bool _i_l;
        public bool I_L
        {
            get => _i_l;
            set
            {
                _i_l = value;
                NotifyOfPropertyChange(() => I_L);
            }
        }

        private bool _i_h;
        public bool I_H
        {
            get => _i_h;
            set
            {
                _i_h = value;
                NotifyOfPropertyChange(() => I_H);
            }
        }

        // Availability Impact (A)
        private bool _a_n;
        public bool A_N
        {
            get => _a_n;
            set
            {
                _a_n = value;
                NotifyOfPropertyChange(() => A_N);
            }
        }

        private bool _a_l;
        public bool A_L
        {
            get => _a_l;
            set
            {
                _a_l = value;
                NotifyOfPropertyChange(() => A_L);
            }
        }

        private bool _a_h;
        public bool A_H
        {
            get => _a_h;
            set
            {
                _a_h = value;
                NotifyOfPropertyChange(() => A_H);
            }
        }
        #endregion

        #region Temporal Score Metrics
        
        // Exploitability (E)
        private bool _e_x;
        public bool E_X
        {
            get => _e_x;
            set
            {
                _e_x = value;
                NotifyOfPropertyChange(() => E_X);
            }
        }

        private bool _e_u;
        public bool E_U
        {
            get => _e_u;
            set
            {
                _e_u = value;
                NotifyOfPropertyChange(() => E_U);
            }
        }

        private bool _e_p;
        public bool E_P
        {
            get => _e_p;
            set
            {
                _e_p = value;
                NotifyOfPropertyChange(() => E_P);
            }
        }

        private bool _e_f;
        public bool E_F
        {
            get => _e_f;
            set
            {
                _e_f = value;
                NotifyOfPropertyChange(() => E_F);
            }
        }

        private bool _e_h;
        public bool E_H
        {
            get => _e_h;
            set
            {
                _e_h = value;
                NotifyOfPropertyChange(() => E_H);
            }
        }

        // Remediation Level (RL)
        private bool _rl_x;
        public bool RL_X
        {
            get => _rl_x;
            set
            {
                _rl_x = value;
                NotifyOfPropertyChange(() => RL_X);
            }
        }

        private bool _rl_o;
        public bool RL_O
        {
            get => _rl_o;
            set
            {
                _rl_o = value;
                NotifyOfPropertyChange(() => RL_O);
            }
        }

        private bool _rl_t;
        public bool RL_T
        {
            get => _rl_t;
            set
            {
                _rl_t = value;
                NotifyOfPropertyChange(() => RL_T);
            }
        }

        private bool _rl_w;
        public bool RL_W
        {
            get => _rl_w;
            set
            {
                _rl_w = value;
                NotifyOfPropertyChange(() => RL_W);
            }
        }

        private bool _rl_u;
        public bool RL_U
        {
            get => _rl_u;
            set
            {
                _rl_u = value;
                NotifyOfPropertyChange(() => RL_U);
            }
        }

        // Report Confidence (RC)
        private bool _rc_x;
        public bool RC_X
        {
            get => _rc_x;
            set
            {
                _rc_x = value;
                NotifyOfPropertyChange(() => RC_X);
            }
        }

        private bool _rc_u;
        public bool RC_U
        {
            get => _rc_u;
            set
            {
                _rc_u = value;
                NotifyOfPropertyChange(() => RC_U);
            }
        }

        private bool _rc_r;
        public bool RC_R
        {
            get => _rc_r;
            set
            {
                _rc_r = value;
                NotifyOfPropertyChange(() => RC_R);
            }
        }

        private bool _rc_c;
        public bool RC_C
        {
            get => _rc_c;
            set
            {
                _rc_c = value;
                NotifyOfPropertyChange(() => RC_C);
            }
        }
        #endregion

        #region Environmental Score Metrics
        // Attack Vector (AV)
        private bool _mav_x;
        public bool MAV_X
        {
            get => _mav_x;
            set
            {
                _mav_x = value;
                NotifyOfPropertyChange(() => MAV_X);
            }
        }

        private bool _mav_n;
        public bool MAV_N
        {
            get => _mav_n;
            set
            {
                _mav_n = value;
                NotifyOfPropertyChange(() => MAV_N);
            }
        }

        private bool _mav_a;
        public bool MAV_A
        {
            get => _mav_a;
            set
            {
                _mav_a = value;
                NotifyOfPropertyChange(() => MAV_A);
            }
        }

        private bool _mav_l;
        public bool MAV_L
        {
            get => _mav_l;
            set
            {
                _mav_l = value;
                NotifyOfPropertyChange(() => MAV_L);
            }
        }

        private bool _mav_p;
        public bool MAV_P
        {
            get => _mav_p;
            set
            {
                _mav_p = value;
                NotifyOfPropertyChange(() => MAV_P);
            }
        }

        // Attack Complexity(AC)
        private bool _mac_x;
        public bool MAC_X
        {
            get => _mac_x;
            set
            {
                _mac_x = value;
                NotifyOfPropertyChange(() => MAC_X);
            }
        }

        private bool _mac_l;
        public bool MAC_L
        {
            get => _mac_l;
            set
            {
                _mac_l = value;
                NotifyOfPropertyChange(() => MAC_L);
            }
        }

        private bool _mac_h;
        public bool MAC_H
        {
            get => _mac_h;
            set
            {
                _mac_h = value;
                NotifyOfPropertyChange(() => MAC_H);
            }
        }

        // Privileges Required (PR)
        private bool _mpr_x;
        public bool MPR_X
        {
            get => _mpr_x;
            set
            {
                _mpr_x = value;
                NotifyOfPropertyChange(() => MPR_X);
            }
        }

        private bool _mpr_n;
        public bool MPR_N
        {
            get => _mpr_n;
            set
            {
                _mpr_n = value;
                NotifyOfPropertyChange(() => MPR_N);
            }
        }

        private bool _mpr_l;
        public bool MPR_L
        {
            get => _mpr_l;
            set
            {
                _mpr_l = value;
                NotifyOfPropertyChange(() => MPR_L);
            }
        }

        private bool _mpr_h;
        public bool MPR_H
        {
            get => _mpr_h;
            set
            {
                _mpr_h = value;
                NotifyOfPropertyChange(() => MPR_H);
            }
        }

        // User Interaction (UI)
        private bool _mui_x;
        public bool MUI_X
        {
            get => _mui_x;
            set
            {
                _mui_x = value;
                NotifyOfPropertyChange(() => MUI_X);
            }
        }

        private bool _mui_n;
        public bool MUI_N
        {
            get => _mui_n;
            set
            {
                _mui_n = value;
                NotifyOfPropertyChange(() => MUI_N);
            }
        }

        private bool _mui_r;
        public bool MUI_R
        {
            get => _mui_r;
            set
            {
                _mui_r = value;
                NotifyOfPropertyChange(() => MUI_R);
            }
        }

        // Scope (S)
        private bool _ms_x;
        public bool MS_X
        {
            get => _ms_x;
            set
            {
                _ms_x = value;
                NotifyOfPropertyChange(() => MS_X);
            }
        }

        private bool _ms_u;
        public bool MS_U
        {
            get => _ms_u;
            set
            {
                _ms_u = value;
                NotifyOfPropertyChange(() => MS_U);
            }
        }

        // Impact Metrics
        // Confidentiality Impact(C)
        private bool _ms_c;
        public bool MS_C
        {
            get => _ms_c;
            set
            {
                _ms_c = value;
                NotifyOfPropertyChange(() => MS_C);
            }
        }

        private bool _mc_x;
        public bool MC_X
        {
            get => _mc_x;
            set
            {
                _mc_x = value;
                NotifyOfPropertyChange(() => MC_X);
            }
        }

        private bool _mc_n;
        public bool MC_N
        {
            get => _mc_n;
            set
            {
                _mc_n = value;
                NotifyOfPropertyChange(() => MC_N);
            }
        }

        private bool _mc_l;
        public bool MC_L
        {
            get => _mc_l;
            set
            {
                _mc_l = value;
                NotifyOfPropertyChange(() => MC_L);
            }
        }

        private bool _mc_h;
        public bool MC_H
        {
            get => _mc_h;
            set
            {
                _mc_h = value;
                NotifyOfPropertyChange(() => MC_H);
            }
        }
        
        // Integrity Impact (I)
        private bool _mi_x;
        public bool MI_X
        {
            get => _mi_x;
            set
            {
                _mi_x = value;
                NotifyOfPropertyChange(() => MI_X);
            }
        }

        private bool _mi_n;
        public bool MI_N
        {
            get => _mi_n;
            set
            {
                _mi_n = value;
                NotifyOfPropertyChange(() => MI_N);
            }
        }

        private bool _mi_l;
        public bool MI_L
        {
            get => _mi_l;
            set
            {
                _mi_l = value;
                NotifyOfPropertyChange(() => MI_L);
            }
        }

        private bool _mi_h;
        public bool MI_H
        {
            get => _mi_h;
            set
            {
                _mi_h = value;
                NotifyOfPropertyChange(() => MI_H);
            }
        }

        // Availability Impact(A)
        private bool _ma_x;
        public bool MA_X
        {
            get => _ma_x;
            set
            {
                _ma_x = value;
                NotifyOfPropertyChange(() => MA_X);
            }
        }

        private bool _ma_n;
        public bool MA_N
        {
            get => _ma_n;
            set
            {
                _ma_n = value;
                NotifyOfPropertyChange(() => MA_N);
            }
        }

        private bool _ma_l;
        public bool MA_L
        {
            get => _ma_l;
            set
            {
                _ma_l = value;
                NotifyOfPropertyChange(() => MA_L);
            }
        }

        private bool _ma_h;
        public bool MA_H
        {
            get => _ma_h;
            set
            {
                _ma_h = value;
                NotifyOfPropertyChange(() => MA_H);
            }
        }

        // Impact Subscore Modifiers
        // Confidentiality Requirement(CR)
        private bool _cr_x;
        public bool CR_X
        {
            get => _cr_x;
            set
            {
                _cr_x = value;
                NotifyOfPropertyChange(() => CR_X);
            }
        }

        private bool _cr_l;
        public bool CR_L
        {
            get => _cr_l;
            set
            {
                _cr_l = value;
                NotifyOfPropertyChange(() => CR_L);
            }
        }

        private bool _cr_m;
        public bool CR_M
        {
            get => _cr_m;
            set
            {
                _cr_m = value;
                NotifyOfPropertyChange(() => CR_M);
            }
        }

        private bool _cr_h;
        public bool CR_H
        {
            get => _cr_h;
            set
            {
                _cr_h = value;
                NotifyOfPropertyChange(() => CR_H);
            }
        }

        // Integrity Requirement (IR)
        private bool _ir_x;
        public bool IR_X
        {
            get => _ir_x;
            set
            {
                _ir_x = value;
                NotifyOfPropertyChange(() => IR_X);
            }
        }

        private bool _ir_l;
        public bool IR_L
        {
            get => _ir_l;
            set
            {
                _ir_l = value;
                NotifyOfPropertyChange(() => IR_L);
            }
        }

        private bool _ir_m;
        public bool IR_M
        {
            get => _ir_m;
            set
            {
                _ir_m = value;
                NotifyOfPropertyChange(() => IR_M);
            }
        }

        private bool _ir_h;
        public bool IR_H
        {
            get => _ir_h;
            set
            {
                _ir_h = value;
                NotifyOfPropertyChange(() => IR_H);
            }
        }

        // Availability Requirement (AR)
        private bool _ar_x;
        public bool AR_X
        {
            get => _ar_x;
            set
            {
                _ar_x = value;
                NotifyOfPropertyChange(() => AR_X);
            }
        }

        private bool _ar_l;
        public bool AR_L
        {
            get => _ar_l;
            set
            {
                _ar_l = value;
                NotifyOfPropertyChange(() => AR_L);
            }
        }

        private bool _ar_m;
        public bool AR_M
        {
            get => _ar_m;
            set
            {
                _ar_m = value;
                NotifyOfPropertyChange(() => AR_M);
            }
        }

        private bool _ar_h;
        public bool AR_H
        {
            get => _ar_h;
            set
            {
                _ar_h = value;
                NotifyOfPropertyChange(() => AR_H);
            }
        }
        #endregion

        #endregion BOOLEAN RADIO PROPERTIES

        private double _baseScore;
        private double _impactSubScore;
        private double _exploitabalitySubScore;
        private double _temporalScore;
        private double _envScore;
        private double _envModifiedImpactSubScore;
        private string _cvssVectorString;
        private VulnerabilityModel _selectedVulnerability;

        public double ExploitabilityCoefficient { get; set; } = 8.22;
        public double ScopeCoefficient { get; set; } = 1.08;
        public double BaseScore
        {
            get => _baseScore;
            set
            {
                _baseScore = value;
                NotifyOfPropertyChange(() => BaseScore);
            }
        }
        public double ImpactSubScore
        {
            get => _impactSubScore;
            set
            {
                _impactSubScore = value;
                NotifyOfPropertyChange(() => ImpactSubScore);
            }
        }
        public double ExploitabalitySubScore
        {
            get => _exploitabalitySubScore;
            set
            {
                _exploitabalitySubScore = value;
                NotifyOfPropertyChange(() => ExploitabalitySubScore);
            }
        }
        public double TemporalScore
        {
            get => _temporalScore;
            set
            {
                _temporalScore = value;
                NotifyOfPropertyChange(() => TemporalScore);
            }
        }
        public double EnvScore
        {
            get => _envScore;
            set
            {
                _envScore = value;
                NotifyOfPropertyChange(() => EnvScore);
            }
        }
        public double EnvModifiedImpactSubScore
        {
            get => _envModifiedImpactSubScore;
            set
            {
                _envModifiedImpactSubScore = value;
                NotifyOfPropertyChange(() => EnvModifiedImpactSubScore);
            }
        }
        public double OverallScore { get; set; }
        public string CvssVectorString 
        { 
            get => _cvssVectorString;
            set 
            {
                _cvssVectorString = value; 
                NotifyOfPropertyChange(() => CvssVectorString); 
            } 
        }
        public string CvssVersionIdentifier { get; set; } = "CVSS:3.1";
        public CvssMetrics Metrics { get; set; }
        public BindableCollection<VulnerabilityModel> Vulnerabilities { get; set; }
        public VulnerabilityModel SelectedVulnerability  
        { 
            get => _selectedVulnerability;
            set 
            { 
                if(value != null)
                {
                    _selectedVulnerability = value;
                }
                
                NotifyOfPropertyChange(() => SelectedVulnerability);

                InitializeCalculator(SelectedVulnerability);
             }
        }

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

            InitializeMetrics(name, value);

            CvssVectorString = ConstructVectorString(Metrics.MetricsIdentifiers);

            CalculateCvss3(Metrics.MetricsWeight);
        }

        public void SetMetricKey(string keyName, string value)
        {
            if (Metrics.MetricsIdentifiers.ContainsKey(keyName))
            {
                Metrics.MetricsIdentifiers.Remove(keyName);
            }

            Metrics.MetricsIdentifiers.Add(keyName, value);
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
                    correctedName += Metrics.MetricsIdentifiers["S"];
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
                    InitializeMetrics("PR", Metrics.MetricsIdentifiers["PR"]);
                    InitializeMetrics("MPR", Metrics.MetricsIdentifiers["MPR"]);
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
                    scopeState = Metrics.MetricsIdentifiers["MS"] == "X" ? Metrics.MetricsIdentifiers["S"] : Metrics.MetricsIdentifiers["MS"];
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
                        value = Metrics.MetricsIdentifiers["PR"];
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
                    InitializeMetrics("MPR", Metrics.MetricsIdentifiers["MPR"]);
                }

                // Если значение метрики Not defined - X, метрика берется из уже определенных базовых метрик.
                if (value.Equals("X"))
                {
                    value = Metrics.MetricsIdentifiers[metricName.Substring(1)];
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

            if(Metrics.MetricsIdentifiers["S"] == "U")
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
                if (Metrics.MetricsIdentifiers["S"] == "U")
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
            var notDefValues = Metrics.MetricsIdentifiers.Where(v => v.Value == "X");

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

            if(Metrics.MetricsIdentifiers["MS"] == "U" || 
              (Metrics.MetricsIdentifiers["MS"] == "X" && 
               Metrics.MetricsIdentifiers["S"] == "U"))
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
                    CvssVersionIdentifier +
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
        /// Произваодит парсинг CVSS вектора в словарь идентификаторов.
        /// </summary>
        /// <param name="vector">Входной CVSS вектор.</param>
        /// <param name="cvssVersion">CVSS версия вектора.</param>
        /// <returns>Словарь идентификаторов.</returns>
        public Dictionary<string, string> ParseVectorString(string vector, string cvssVersion)
        {
            var metricNameValue = vector.Substring(cvssVersion.Length + 1).Split('/');
            Dictionary<string, string> identifires = new Dictionary<string, string>();

            for (int i = 0; i < metricNameValue.Length; i++)
            {
                var singleMetric = metricNameValue[i].Split(':');

                identifires.Add(singleMetric[0], singleMetric[1]);
            }

            return identifires;
        }

        /// <summary>
        /// Инициализирует указанную метрику, получая соответствующие значение из словаря.
        /// </summary>
        /// <param name="metricName">Имя свойства метрики которое нужно инициализировать.</param>
        /// <param name="key">Ключ по-которому будет осуществляться доступ значения в словаре.</param>
        //public void InitializeMetricWeight(string metricName, string key)
        //{
        //    string correctedName = metricName;

        //    if (correctedName.StartsWith("M") && !key.Equals("X"))
        //    {
        //        // Получаем имя метрики без М.
        //        // MAV - AV, MAC - AC ...
        //        correctedName = metricName.Substring(1);
        //    }
        //    else if (key.Equals("X"))
        //    {

        //    }

        //    // При установке PR нужно проверить установлено ли Scope.
        //    if (correctedName.Equals("PR"))
        //    {
        //        // Запоминаем ключ PR - пригодится, когда будет изменено Scope.
        //        Metrics.PR_Key = key;

        //        correctedName += S_U ? "U" : S_C ? "C" : "";

        //        if (metricName.Equals(correctedName)) return;
        //    }

        //    if(correctedName.Equals("S"))
        //    {
        //        // Если было изменено S нужно пересчитать PR.
        //        InitializeMetricWeight("PR", Metrics.PR_Key);
        //    }

        //    if(correctedName.Equals("C") || correctedName.Equals("I") || correctedName.Equals("A"))
        //    {
        //        correctedName = "CIA";
        //    }

        //    if (correctedName.Equals("CR") || correctedName.Equals("IR") || correctedName.Equals("AR"))
        //    {
        //        correctedName = "CIAR";
        //    }
        
        //    // Нашли нужный словарь.
        //    var metricProperty = Metrics.GetType().GetProperty(correctedName);

        //    // Получили значение из словаря по ключу.
        //    double metricWeight = (metricProperty.GetValue(Metrics) as Dictionary<string, double>)[key];

        //    // Если correctedName изменялся, значит надо установить его назад, чтобы свойства *_Weight записывались без ошибок.
        //    // Потому что словарь может быть один на несколько метрик, но сами значения метрик должы записываться в свои поля.
        //    if (!correctedName.Equals(metricName))
        //    {
        //        correctedName = metricName;
        //    }

        //    // Установили значение метрики *_Weight в значение полученное из словаря.
        //    Metrics.GetType().GetProperty(correctedName + "_Weight").SetValue(Metrics, metricWeight);
        //}

        public void InitializeCalculator(VulnerabilityModel selectedVulnerability)
        {
            string vector = selectedVulnerability.MetricV3.cvssV3.vectorString;
            string cvssVersion = vector.Substring(0, vector.IndexOf('/'));

            // Для начала скинем идентификаторы метрик к дефолтному состоянию.
            Metrics.MetricsIdentifiers = new Dictionary<string, string>(Metrics.ConstMetricsIdentifiers);

            // Получим идентификаторы метрик из вектора.
            foreach (var i in ParseVectorString(vector, cvssVersion))
            {
                SetMetricKey(i.Key, i.Value);
            }

            // Инициализируем численное значения метрик в соответсвие с их строковыми идентификаторами.
            foreach (var i in Metrics.MetricsIdentifiers)
            {
                InitializeMetrics(i.Key, i.Value);
            }


            // Нужно установить интерфейс калькулятора (radio-buttons) в значение идентификаторов выбранной уязвимости.
            InitializeRadioButtons(Metrics.MetricsIdentifiers, true);

            // Расчитаем оценки уязвимости.
            CalculateCvss3(Metrics.MetricsWeight);
        }

        private void InitializeRadioButtons(Dictionary<string, string> identifiers, bool flag)
        {
            foreach (var i in identifiers.ToList())
            {
                this.GetType().GetProperty($"{i.Key}_{i.Value}").SetValue(this, flag);                
            }           
        }
    }
}
