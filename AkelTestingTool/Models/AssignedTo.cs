using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;


namespace AkelTestingTool.Models
{
    public class AssignedTo
    {
        [Key]
        public int AssignedToID { get; set; }   //STTEID
        public string Name { get; set; }        //Name
    }
}