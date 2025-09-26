using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace AkelTestingTool.Models
{
    public class ProjectTests
    {
        [Key]
        public int TId { get; set; }

        
        public int ProjectsPId { get; set; }
        public Projects Projects { get; set; }

        [Required]
        [StringLength(5000)]
        public string Test { get; set; }

        [Required]
        [StringLength(5000)]
        public string TStatus { get; set; }

        [ScaffoldColumn(false)]
        [DataType(DataType.Date)]
        public DateTime PPublicationDate { get; set; }

        [Required]
        [StringLength(5000)]
        public string TestedBy { get; set; }



        
    }
}
