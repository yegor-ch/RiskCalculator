using Caliburn.Micro;
using RiskCalculator.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RiskCalculator.ViewModels
{
    class CalculatorViewModel : Screen
    {
        public BindableCollection<VulnerabilityModel> Vulnerabilities { get; set; }
        public CalculatorViewModel(BindableCollection<VulnerabilityModel> Vulnerabilities)
        {
            this.Vulnerabilities = Vulnerabilities;
        }
    }
}
