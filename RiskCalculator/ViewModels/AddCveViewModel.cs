using Caliburn.Micro;
using MaterialDesignThemes.Wpf;
using RiskCalculator.API;
using RiskCalculator.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Threading;

namespace RiskCalculator.ViewModels
{
    class AddCveViewModel : Screen
    {
        private string _searchParams = "adobe";
        public string SearchParams 
        { 
            get { return _searchParams; } 
            set 
            {
                _searchParams = value;
                NotifyOfPropertyChange(() => SearchParams);
            } 
        }

        private bool _isIndeterminate;
        public bool IsIndeterminate 
        {
            get { return _isIndeterminate; }
            set
            {
                _isIndeterminate = value;
                NotifyOfPropertyChange(() => IsIndeterminate);
            }
        }

        private double _maximum;
        public double Maximum
        {
            get { return _maximum; }
            set
            {
                _maximum = value;
                NotifyOfPropertyChange(() => Maximum);
            }
        }
        public string AddedVulCount { get; set; }
        public SnackbarMessageQueue MessageQueue { get; set; }

        private VulnerabilityModel _selectedVulnerability;
        public VulnerabilityModel SelectedVulnerability
        {
            get { return _selectedVulnerability; }
            set
            {
                _selectedVulnerability = value;
                NotifyOfPropertyChange(() => SelectedVulnerability);
            }
        }
        public BindableCollection<VulnerabilityModel> SelectedVulnerabilities { get; set; }

        public BindableCollection<VulnerabilityModel> SearchResultList { get; set; }

        public AddCveViewModel(BindableCollection<VulnerabilityModel> vulnerabilities)
        {
            SearchResultList = new BindableCollection<VulnerabilityModel>();
            SelectedVulnerabilities = vulnerabilities;

            // Создаем свой кастомный экземпляр SnackbarMessageQueue и устанавливаем задержку, сколько будет видно Snackbar.
            MessageQueue = new SnackbarMessageQueue(TimeSpan.FromSeconds(1.2));
  
            Maximum = 20;
           
            ApiHelper.InitializeClient();
        }


        public void SearchRequest()
        {
            Thread thread = new Thread(GetCveData);
            thread.Start();
        }

        private async void GetCveData()
        {     
            string query = "keyword=" + SearchParams.TrimEnd(' ').Replace(" ", "+");

            // ProgressBar.
            IsIndeterminate = true;

            try
            {
                // Request to NVD API.
                var result = await ApiProcessor.LoadCveInfo(query);
                GetVulnerabilities(result);
                IsIndeterminate = false;
            }
            catch (Exception)
            {
                MessageBox.Show("Помилка отримання даних, спробуйте ще раз!", "Помилка");
            }
        }

        /// <summary>
        /// Конвертация полученного JSON объекта CVE в более удобный для работы список VulnerabilityModel.
        /// </summary>
        public void GetVulnerabilities(CveModel cve)
        {
            SearchResultList.Clear();

            foreach (var item in cve.Result.CVE_Items)
            {              
                string severity = "";
                BaseMetricV2 metricV2 = null;
                BaseMetricV3 metricV3 = null;

                if (item.Impact != null)
                {
                    if (item.Impact.baseMetricV2 != null)
                    {
                        severity += $"V2: {item.Impact.baseMetricV2.cvssV2.baseScore} {item.Impact.baseMetricV2.severity}";
                        metricV2 = item.Impact.baseMetricV2;
                    }
                    else
                    {
                        severity += "V2: none";
                    }

                    severity += Environment.NewLine;

                    if (item.Impact.baseMetricV3 != null)
                    {
                        metricV3 = item.Impact.baseMetricV3;

                        if(item.Impact.baseMetricV3.cvssV3 != null)
                        {                            
                            severity += $"V3.1: {item.Impact.baseMetricV3.cvssV3.baseScore} {item.Impact.baseMetricV3.cvssV3.baseSeverity}";
                        }                    
                    }
                    else
                    {
                        severity += "V3.1: none";
                    }
                }
                else
                {
                    severity += "none";
                }

                SearchResultList.Add(new VulnerabilityModel
                {
                    Id = item.Cve.Meta.Id,
                    Description = item.Cve.Description.description_data[0].Value,
                    Severity = severity,
                    MetricV2 = metricV2,
                    MetricV3 = metricV3
                });
            }
        }

        /// <summary>
        /// Метод-обработчик события изменения выделенного ListItem в ListView.
        /// </summary>
        /// <param name="e"></param>
        public void NewVulnerabilityAdded(SelectionChangedEventArgs e)
        {
            foreach (var v in e.AddedItems)
            {
                // Сообщение которое будет выводиться в Snackbar.
                string message = "";

                SelectedVulnerabilities.Add(v as VulnerabilityModel);
                SearchResultList.Remove(v as VulnerabilityModel);

                message += $"{(v as VulnerabilityModel).Id} додано до списку.";
                message += Environment.NewLine;
                message += $"Всього додано вразливостей: {SelectedVulnerabilities.Count}";

                MessageQueue.Enqueue(message);
            }
        }

        public void ClearSearchParams()
        {
            SearchParams = "";
            SearchResultList.Clear();
        }
    }
}
