using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;
using AkelTestingTool.Controllers;
using Newtonsoft.Json;

namespace AkelTestingTool.Models
{
    public class TestsExeResults
    {
        

        [Key]
        [JsonProperty(NullValueHandling = NullValueHandling.Ignore, PropertyName = "TERId2")]
        public int TERId2 { get; set; }

        [JsonProperty(NullValueHandling = NullValueHandling.Ignore, PropertyName = "TestExcutionsTEId")]
        [ForeignKey("TestExcutionsTEId")]
        public int? TestExcutionsTEId { get; set; }
        public TestExcutions TestExcutions { get; set; }

        [JsonProperty(NullValueHandling = NullValueHandling.Ignore, PropertyName = "TestCasesTCId")]
        [ForeignKey("TestCasesTCId")]
        public int? TestCasesTCId { get; set; }
        public TestCases TestCases { get; set; }

        [JsonProperty(NullValueHandling = NullValueHandling.Ignore, PropertyName = "ProjectTestsTId")]
        [ForeignKey("ProjectTestsTId")]
        public int? ProjectTestsTId { get; set; }
        public ProjectTests ProjectTests { get; set; }


        [Required]
        [JsonProperty(NullValueHandling = NullValueHandling.Ignore, PropertyName = "Result")]
        [StringLength(5000)]
        public string Result { get; set; }

        [Required]
        [JsonProperty(NullValueHandling = NullValueHandling.Ignore, PropertyName = "Status")]
        [StringLength(5000)]
        public string Status { get; set; }

      

        [DataType(DataType.Date)]
        [JsonProperty(NullValueHandling = NullValueHandling.Ignore, PropertyName = "PPublicationDate")]
        public DateTime PPublicationDate { get; set; }

        [Display(Name = "Test Image")]
        public string ImageUrl2 { get; set; }

        [Display(Name = "TestExRe Status")]
        public int StatusSTTERID { get; set; }
        public StatusTER StatusTER { get; set; }

    }
}
