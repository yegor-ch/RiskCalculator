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
        public SelectedVulnerabilitiesViewModel(BindableCollection<VulnerabilityModel> vulnerabilities)
        {
            VulnerabilitiesList = vulnerabilities;
        }

        public BindableCollection<VulnerabilityModel> VulnerabilitiesList { get; set; } 


        public void Edit()
        {
        }

        //protected override void OnActivate()
        //{
        //}
    }
}
