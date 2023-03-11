using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace TimeKeepingApp.Models
{
    public class Shift
    {
        public int Id { get; set; }

        [Display(Name = "Employee ID Number")]
        public string EmployeeID { get; set; }

        [Display(Name = "Shift Starting Time")]
        public DateTime ShiftStart { get; set; }

        [Display(Name = "Shift Ending Time")]
        public DateTime ShiftEnd { get; set; }

        public string Location { get; set; }

    }
}
