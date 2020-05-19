using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RiskCalculator.Models
{
    class TrapezeModel
    {
        public string degreeRisk;
        public double a { get; set; }
        public double b11 { get; set; }
        public double b21 { get; set; }
        public double c { get; set; }
    }
}
