using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace AkelTestingTool.Models
{
    public class StatusTER
    {
        [Key]
        public int STTERID { get; set; }
        public string Name { get; set; }
    }
}