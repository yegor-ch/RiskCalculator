using RiskCalculator.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace RiskCalculator.API
{
    class ApiProcessor
    {
        public static async Task<CveModel> LoadCveInfo(string query)
        {
            string url = $"https://services.nvd.nist.gov/rest/json/cves/1.0?{query}";

            using (HttpResponseMessage response = await ApiHelper.ApiClient.GetAsync(url))
            {
                if (response.IsSuccessStatusCode)
                {
                    var result = await response.Content.ReadAsAsync<CveModel>();

                    return result;
                }
                else
                {
                    throw new Exception(response.ReasonPhrase);
                }
            }
        }
    }
}
