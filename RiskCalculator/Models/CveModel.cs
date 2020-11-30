using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RiskCalculator.Models
{
    public class CveModel
    {
        public Result Result { get; set; }

    }

    public class Result
    {
        public CVE_Items[] CVE_Items { get; set; }

    }

    public class CVE_Items
    {
        public Impact Impact { get; set; }
        public Cve Cve { get; set; }
    }

    public class Cve
    {
        [JsonProperty(PropertyName = "CVE_data_meta")]
        public CVE_data_meta Meta { get; set; }

        public Description Description { get; set; }
    }

    public class Description
    {
        public Description_Data[] description_data { get; set; }
    }

    public class Description_Data
    {
        public string Value { get; set; }
    }

    public class CVE_data_meta
    {
        public string Id { get; set; }
    }

    public class Impact
    {
        [JsonProperty("Basemetricv2")]
        public BaseMetricV2 baseMetricV2 { get; set; }
        public BaseMetricV3 baseMetricV3 { get; set; }
    }

    [JsonObject("Basemetricv2")]
    public class BaseMetricV2
    {
        public Cvssv2 cvssV2 { get; set; }
        public string severity { get; set; }
        public string exploitabilityScore { get; set; }
        public string impactScore { get; set; }
        public bool obtainAllPrivilege { get; set; }
        public bool obtainUserPrivilege { get; set; }
        public bool obtainOtherPrivilege { get; set; }
        public bool userInteractionRequired { get; set; }
    }

    public class Cvssv2
    {
        public string version { get; set; }
        public string vectorString { get; set; }
        public string accessVector { get; set; }
        public string accessComplexity { get; set; }
        public string authentication { get; set; }
        public string confidentialityImpact { get; set; }
        public string integrityImpact { get; set; }
        public string availabilityImpact { get; set; }
        public double baseScore { get; set; }
    }

    public class BaseMetricV3
    {
        public Cvssv3 cvssV3 { get; set; }
        public string exploitabilityScore { get; set; }
        public string impactScore { get; set; }

        // Заполнятся в калькуляторе.

        public double BaseScore { get; set; }
        public double TemporalScore { get; set; }
        public double EnvScore { get; set; }

    }

    public class Cvssv3
    {
        public string version { get; set; }
        public string vectorString { get; set; }
        public string attackVector { get; set; }
        public string attackComplexity { get; set; }
        public string privilegesRequired { get; set; }
        public string userInteraction { get; set; }
        public string scope { get; set; }
        public string confidentialityImpact { get; set; }
        public string integrityImpact { get; set; }
        public string availabilityImpact { get; set; }
        public string baseScore { get; set; }
        public string baseSeverity { get; set; }
    }

}
