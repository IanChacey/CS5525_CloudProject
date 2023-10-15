using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace TimeKeepingApp.Models
{
    public class ShiftManagerShiftsViewModel
    {
        public List<Shift> sList { get; set; }
        public List<Employee> employeeList { get; set; }
        public List<string> nameList { get; set; }
        public List<string> lastNameList { get; set; }

        [Required]
        public DateTime shift { get; set; }
        public DateTime toDate { get; set; }

        public ShiftManagerShiftsViewModel(List<Employee> employees, List<Shift> shifts, List<string> firstName, List<string> lastName)//DateTime start, DateTime end, int page = 1, string acct = "all")
        {
            employeeList = employees;
            sList = shifts;
            nameList = firstName;
            lastNameList = lastName;
        }
    }
}
