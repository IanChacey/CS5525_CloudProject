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
        //public string descSearch { get; set; }
        //public string debug { get; set; }
        //public int pageNumber { get; set; }
        //public string account { get; set; }

        public int pageSize { get; set; } = 10;

        //public IEnumerator res;

        //public IEnumerator<ShiftManagerShiftsViewModel> GetEnumerator()
        //{
        //    return new IEnumerator<ShiftManagerShiftsViewModel>(this);
        //}

        public ShiftManagerShiftsViewModel(List<Shift> shifts, List<string> firstName, List<string> lastName)//DateTime start, DateTime end, int page = 1, string acct = "all")
        {
            //employeeList = employees;
            sList = shifts;
            nameList = firstName;
            lastNameList = lastName;
            //var names = nameList.Zip(lastNameList, (f, l) => new { First = f, Last = l });
            //var res = sList.Zip(names, (s, n) => new { shif = s, nam = n });
            //descSearch = desc;
            //fromDate = start;
            //toDate = end;
            //pageNumber = page;
            //account = acct;

        }
    }
}
