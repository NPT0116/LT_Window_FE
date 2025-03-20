using CommunityToolkit.Mvvm.ComponentModel;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PhoneSelling.Data.Models
{
    public class BaseEntity : ObservableValidator
    {
        public Guid Id { get; set; }

        protected string GetFirstError(string propertyName)
        {
            var errors = GetErrors(propertyName)?.Cast<ValidationResult>(); // Convert to ValidationResult
            return errors?.FirstOrDefault()?.ErrorMessage ?? string.Empty; // Return the first validation error message
        }
    }
}
