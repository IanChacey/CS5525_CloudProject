using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace TimeKeepingApp.Models
{
    public class Employee
    {
        public int Id { get; set; }

        [Display(Name = "Employee ID Number")]
        public string EmployeeID { get; set; }

        public string? Department { get; set; }

        public string Role { get; set; }

        [Display(Name = "Employee Name")]
        public string EmployeeName { get; set; }

        public float HourlyWage { get; set; }
    }
}
