using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace TimeKeepingApp.Models
{
    public class ShiftIndexViewModel
    {
        public List<Shift> sList { get; set; }
        public List<Employee> employeeList { get; set; }

        [Required]
        public DateTime fromDate { get; set; }
        public DateTime toDate { get; set; }

        public int pageSize { get; set; } = 10;

        public ShiftIndexViewModel(List<Shift> shifts)//,  List<Employee> employees = null)//DateTime start, DateTime end, int page = 1, string acct = "all")
        {
            sList = shifts;
        }
    }
}
