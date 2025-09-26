using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace AkelTestingTool.Models
{
    public class Projects
    {
        [Key]
        public int PId { get; set; }

       [Required(ErrorMessage = "תכניס שם פרוייקט בבקשה")] 
        public string PName { get; set; }

        [StringLength(100)]
        public string PDescription { get; set; }

    }
}
