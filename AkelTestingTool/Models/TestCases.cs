using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace AkelTestingTool.Models
{
    public class TestCases
    {

        [Key]
        public int TCId { get; set; }


        public int ProjectTestsTId { get; set; }
        public ProjectTests ProjectTests { get; set; }

        [Required]
        public string TestCaseNum { get; set; }

        [Required]
        [StringLength(5000)]
        public string TestCase { get; set; }

        [Required]
        [StringLength(5000)]
        public string ExpectedResult { get; set; }

        

      
        [DataType(DataType.Date)]
        public DateTime PPublicationDate { get; set; }

        

    }
}
