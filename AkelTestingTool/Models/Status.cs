using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace AkelTestingTool.Models
{
    public class Status
    {
        [Key]
        public int STID { get; set; }
        public string Name { get; set; }
    }
}
