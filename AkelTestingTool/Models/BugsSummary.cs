using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace AkelTestingTool.Models
{
    public class BugsSummary
    {
       

        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        //[Required]
        //[Display(Name = "Project")]
        
        public int ProjectsPId { get; set; }
        public Projects Projects { get; set; }

        
        [ScaffoldColumn(false)]
        public string UserId { get; set; }
      //  [ScaffoldColumn(false)]
        public ApplicationUser User { get; set; }


        [Required]
        [StringLength(100)]
        public string Bug { get; set; }

        [Required]
        [StringLength(5000)]
        public string BugSummary { get; set; }

        [ScaffoldColumn(false)]
        [DataType(DataType.Date)]
        public DateTime PublicationDate { get; set; }


        [Display(Name = "Tester Name")]
        [StringLength(150)]
        public string TesterName { get; set; }

        [ScaffoldColumn(false)]
        public int Readers { get; set; }

        [Display(Name = "Bug Image")]
        public string ImageUrl { get; set; }

        [Display(Name = "Bug Status")]
        public int StatusSTID { get; set; }
        public Status Status { get; set; }

        [ForeignKey("TestsExeResults")]
        public int TestsExeResultsTERID { get; set; }
        public TestsExeResults TestsExeResults { get; set; }
        
    }
}
