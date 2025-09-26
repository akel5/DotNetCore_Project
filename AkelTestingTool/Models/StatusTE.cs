using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace AkelTestingTool.Models
{
    public class StatusTE
    {
        [Key]
        public int STTEID { get; set; }
        public string Name { get; set; }
    }
}