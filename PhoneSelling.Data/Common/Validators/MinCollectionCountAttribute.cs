using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PhoneSelling.Data.Common.Validators
{
    [AttributeUsage(AttributeTargets.All, AllowMultiple = false)]
    public class MinCollectionCountAttribute : ValidationAttribute
    {
        private readonly int _minCount;

        public MinCollectionCountAttribute(int minCount)
        {
            _minCount = minCount;
            ErrorMessage = $"Danh sách phải chứa ít nhất {minCount} phần tử.";
        }

        protected override ValidationResult IsValid(object value, ValidationContext validationContext)
        {
            if (value is ICollection collection && collection.Count < _minCount)
            {
                return new ValidationResult(ErrorMessage);
            }
            return ValidationResult.Success;
        }
    }
}
