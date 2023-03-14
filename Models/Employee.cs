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

        [Required]
        [StringLength(15, MinimumLength = 1, ErrorMessage = "Department must be between 1 and 15 characters")]
        public string? Department { get; set; }

        [Required]
        public string Role { get; set; }

        public string RoleName { get; set; }

        [Required]
        [Display(Name = "First Name")]
        [StringLength(20, MinimumLength = 1, ErrorMessage = "First Name must be between 1 and 20 characters")]
        public string EmployeeName { get; set; }

        [Required]
        [Display(Name = "Last Name")]
        [StringLength(20, MinimumLength = 1, ErrorMessage = "Last Name must be between 1 and 20 characters")]
        public string EmployeeLastName { get; set; }

        [Required]
        [Display(Name = "Hourly Wage")]
        public float HourlyWage { get; set; }
    }
}
