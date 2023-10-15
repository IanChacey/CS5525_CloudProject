using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace TimeKeepingApp.Models
{
    public class ShiftEmployeeJoin
    {
        [Display(Name = "First Name")]
        public string first { get; set; }

        [Display(Name = "Last Name")]
        public string last { get; set; }

        [Display(Name = "Shift Start")]
        public DateTime start { get; set; }

        [Display(Name = "Shift End")]
        public DateTime? end { get; set; }

        [Display(Name = "Location")]
        public string loc { get; set; }

        [Display(Name = "Status")]
        public ShiftStatus stat { get; set; }

        public int id { get; set; }

    }
}
