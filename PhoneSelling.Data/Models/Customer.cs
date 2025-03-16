using CommunityToolkit.Mvvm.ComponentModel;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Net;
using System.Xml.Linq;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace PhoneSelling.Data.Models
{
    public partial class Customer : BaseEntity, IValidatableObject
    {
        [ObservableProperty] private string name = string.Empty;
        [ObservableProperty] private string email = string.Empty;
        [ObservableProperty] private string phone = string.Empty;
        [ObservableProperty] private string address = string.Empty;
        [ObservableProperty] private List<Invoice> invoices = new();

        public IEnumerable<ValidationResult> ValidateName()
        {
            var errors = new List<ValidationResult>();

            // Validate Name: required, not empty
            if (string.IsNullOrWhiteSpace(Name))
            {
                errors.Add(new ValidationResult("Customer name cannot be empty.", new[] { nameof(Name) }));
            }

            return errors;
        }

        public IEnumerable<ValidationResult> ValidateEmail()
        {
            var errors = new List<ValidationResult>();
            if (string.IsNullOrWhiteSpace(Email))
            {
                errors.Add(new ValidationResult("Customer email cannot be empty.", new[] { nameof(Email) }));
            }
            else
            {
                // Using EmailAddressAttribute to validate email format
                var emailAttribute = new EmailAddressAttribute();
                if (!emailAttribute.IsValid(Email))
                {
                    errors.Add(new ValidationResult("Customer email is not valid.", new[] { nameof(Email) }));
                }
            }
            return errors;
        }

        public IEnumerable<ValidationResult> ValidatePhone()
        {
            var errors = new List<ValidationResult>();
            if (string.IsNullOrWhiteSpace(Phone))
            {
                errors.Add(new ValidationResult("Customer phone cannot be empty.", new[] { nameof(Phone) }));
            }
            return errors;
        }

        public IEnumerable<ValidationResult> ValidateAddress()
        {
            var errors = new List<ValidationResult>();
            if (string.IsNullOrWhiteSpace(Address))
            {
                errors.Add(new ValidationResult("Customer address cannot be empty.", new[] { nameof(Address) }));
            }

            return errors;
        }



        public IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
        {
            var errors = new List<ValidationResult>();

            // Validate Name: required, not empty
            var nameErrors = ValidateName();
            if (nameErrors.Any()) errors.AddRange(nameErrors);

            // Validate Email: required and must be in valid format
            var emailErrors = ValidateEmail();
            if(emailErrors.Any()) errors.AddRange(emailErrors);

            // Validate Phone: required (you can add more complex phone validation if needed)
            var phoneErrors = ValidatePhone();
            if(phoneErrors.Any()) errors.AddRange(phoneErrors);

            // Validate Address: required
            var addressErrors = ValidateAddress();
            if(addressErrors.Any()) errors.AddRange(addressErrors);

            return errors;
        }
    }
}
