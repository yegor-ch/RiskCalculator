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

        public bool IsActive { get; set; } = true;
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

        public AddCveViewModel()
        {
            SearchResultList = new BindableCollection<VulnerabilityModel>();
            SelectedVulnerabilities = new BindableCollection<VulnerabilityModel>();

            // Создаем свой кастомный экземпляр SnackbarMessageQueue и устанавливаем задержку, сколько будет видно Snackbar.
            MessageQueue = new SnackbarMessageQueue(TimeSpan.FromSeconds(1));
  
            Maximum = 20;
           
            ApiHelper.InitializeClient();
        }


        public async void SearchRequest()
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
                SearchResultList.Add(new VulnerabilityModel
                {
                    Id = item.Cve.Meta.Id,
                    Description = item.Cve.Description.description_data[0].Value,
                    //Severity = item.Impact.baseMetricV3.cvssV3.baseSeverity                  
                });
            }
        }

        /// <summary>
        /// Метод-обработчик события изменения выделенного ListItem в ListView.
        /// </summary>
        /// <param name="e"></param>
        public void NewVulnerabilityAdded(SelectionChangedEventArgs e)
        {
            // Сообщение которое будет выводиться в Snackbar.
            string message = "";

            foreach (var v in e.AddedItems)
            {
                SelectedVulnerabilities.Add(v as VulnerabilityModel);

                message += $"{(v as VulnerabilityModel).Id} додано до списку.";
            }

            foreach (var v in e.RemovedItems)
            {
                SelectedVulnerabilities.Remove(v as VulnerabilityModel);

                message += $"{(v as VulnerabilityModel).Id} видалено зі списку.";
            }

            message += Environment.NewLine;
            message += $"Всього додано вразливостей: {SelectedVulnerabilities.Count}";

            MessageQueue.Enqueue(message);

        }

        public void ClearSearchParams()
        {
            SearchParams = "";
            SearchResultList.Clear();
        }
    }
}
