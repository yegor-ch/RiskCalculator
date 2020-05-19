using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RiskCalculator.Models
{
    static class SeverityModel
    {
        public static string [] GetSevetity(int count)
        {
            switch (count)
            {
                case 3: return new string [] { "Низький", "Середній", "Високий" };
                case 4: return new string[] { "Низький", "Середній", "Високий", "Критичний" };
                case 5: return new string[] { "Низький", "Нижче середнього", "Середній", "Вижче середнього", "Високий" };
                default: return new string[] { };
            }
        }
    }
}
