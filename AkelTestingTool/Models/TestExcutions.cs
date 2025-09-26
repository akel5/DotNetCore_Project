using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace AkelTestingTool.Models
{
    public class TestExcutions
    {
        [Key]
        public int TEId { get; set; }


        public int ProjectTestsTId { get; set; }
        public ProjectTests ProjectTests { get; set; }

        [DataType(DataType.Date)]
        public DateTime PPublicationDate { get; set; }

        

        [Display(Name = "TestEx Status")]
        public int StatusSTTEID { get; set; }
        public StatusTE StatusTE { get; set; }

        [Display(Name = "TestEx AssignedTo")]
        public int AssignedToAssignedToID { get; set; }
        public AssignedTo AssignedTo { get; set; }
    }
}
