using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace TimeKeepingApp.Models
{
    public class ShiftEndAttribute : ValidationAttribute
    {
        //public ShiftEndAttribute(DateTime date)
        //{
        //    ShiftEnd
        //}

        public string GetErrorMessage()
        {
            return "Shift End cannot be before Shift Start";
        }

        protected override ValidationResult IsValid(object value, ValidationContext validationContext)
        {
            var shift = (Shift)validationContext.ObjectInstance;
            var end = (DateTime)value!;
            var start = shift.ShiftStart;

            if (end <= start)
            {
                return new ValidationResult(GetErrorMessage());
            }

            return ValidationResult.Success;//base.IsValid(value, validationContext);
        }
    }
}
