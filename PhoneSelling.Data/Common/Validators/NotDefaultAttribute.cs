using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PhoneSelling.Data.Common.Validators
{
    [AttributeUsage(AttributeTargets.Property, AllowMultiple = false)]
    public class NotDefaultAttribute : ValidationAttribute
    {
        public override bool IsValid(object value)
        {
            if (value is DateTime dateTime)
            {
                return dateTime != default; // ✅ Ensure it's not DateTime.MinValue
            }
            if (value is Guid guid)
            {
                return guid != Guid.Empty; // ✅ Prevents empty GUID
            }
            return true; // If value is null or another type, ignore validation
        }
    }
}
