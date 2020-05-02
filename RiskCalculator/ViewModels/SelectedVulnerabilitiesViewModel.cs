using Caliburn.Micro;
using RiskCalculator.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RiskCalculator.ViewModels
{
    class SelectedVulnerabilitiesViewModel : Screen
    {
        private VulnerabilityModel _selectedVulnerability;

        public SelectedVulnerabilitiesViewModel(BindableCollection<VulnerabilityModel> vulnerabilities)
        {
            VulnerabilitiesList = vulnerabilities;
        }

        public BindableCollection<VulnerabilityModel> VulnerabilitiesList { get; set; }

        public VulnerabilityModel SelectedVulnerability
        {
            get => _selectedVulnerability;
            set
            {
                if (value != null)
                {
                    _selectedVulnerability = value;
                }

                NotifyOfPropertyChange(() => SelectedVulnerability);
            }
        }


        public void Edit()
        {
        }

        public void Delete()
        {
            if(VulnerabilitiesList.Count > 0)
            {
                VulnerabilitiesList.Remove(SelectedVulnerability);
            }
        }

        //protected override void OnActivate()
        //{
        //}
    }
}
