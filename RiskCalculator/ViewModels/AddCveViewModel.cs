using Caliburn.Micro;
using RiskCalculator.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RiskCalculator.ViewModels
{
    class AddCveViewModel : Screen
    {
        public BindableCollection<VulnerabilityModel> SearchResult { get; set; }

        public AddCveViewModel()
        {
            SearchResult = new BindableCollection<VulnerabilityModel>();
        }
    }
}
