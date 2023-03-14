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

        [Required]
        [Display(Name = "Shift Starting Time")]
        public DateTime ShiftStart { get; set; }

        [ShiftEnd]
        [Display(Name = "Shift Ending Time")]
        public DateTime? ShiftEnd { get; set; }

        [Required]
        [StringLength(15, MinimumLength = 1, ErrorMessage = "Location must be between 1 and 15 characters")]
        public string Location { get; set; }

        public ShiftStatus Status { get; set; }
    }

    public enum ShiftStatus
    {
        Pending,
        Approved,
        Rejected,
        Ongoing
    }
}
