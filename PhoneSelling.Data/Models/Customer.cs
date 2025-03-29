using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Diagnostics;
using System.Net;
using System.Xml.Linq;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace PhoneSelling.Data.Models
{
    public partial class Customer : BaseEntity
    {
        [ObservableProperty] private Guid customerID;

        [ObservableProperty]
        [Required(ErrorMessage = "Tên khách hàng không được để trống.")]
        [NotifyPropertyChangedFor(nameof(HasErrors))]
        private string name = string.Empty;

        [ObservableProperty]
        [EmailAddress(ErrorMessage = "Email sai định dạng.")]
        [Required(ErrorMessage = "Email không được để trống.")]
        [NotifyPropertyChangedFor(nameof(HasErrors))] // Ensure UI updates
        private string email = string.Empty;

        [ObservableProperty]
        [Phone(ErrorMessage = "Số điện thoại không hợp lệ.")]
        [NotifyPropertyChangedFor(nameof(HasErrors))]
        private string phone = string.Empty;
        [ObservableProperty] private string address = string.Empty;
        [ObservableProperty] private List<Invoice> invoices = new();

        [ObservableProperty] private string emailError = string.Empty;
        [ObservableProperty] private string nameError = string.Empty;
        [ObservableProperty] private string phoneError = string.Empty;

        partial void OnEmailChanged(string newValue)
        {
            ValidateProperty(newValue, nameof(Email));
            EmailError = GetFirstError(nameof(Email));
        }

        partial void OnNameChanged(string newValue)
        {
            ValidateProperty(newValue, nameof(Name));
            NameError = GetFirstError(nameof(Name));
        }

        partial void OnPhoneChanged(string newValue)
        {
            ValidateProperty(newValue, nameof(Phone));
            PhoneError = GetFirstError(nameof(Phone));
        }

        public bool ValidateAllProperties()
        {
            base.ValidateAllProperties();
            EmailError = GetFirstError(nameof(Email));
            NameError = GetFirstError(nameof(Name));
            PhoneError = GetFirstError(nameof(Phone));
            return HasErrors;
        }

        public IEnumerable<ValidationResult> ValidateName()
        {
            var errors = new List<ValidationResult>();

            // Validate Name: required, not empty
            if (string.IsNullOrWhiteSpace(Name))
            {
                errors.Add(new ValidationResult("Tên khách hàng không được để trống.", new[] { nameof(Name) }));
            }

            return errors;
        }

        public IEnumerable<ValidationResult> ValidateEmail()
        {
            var errors = new List<ValidationResult>();
            if (string.IsNullOrWhiteSpace(Email))
            {
                errors.Add(new ValidationResult("Email không được để trống.", new[] { nameof(Email) }));
            }
            else
            {
                // Using EmailAddressAttribute to validate email format
                var emailAttribute = new EmailAddressAttribute();
                if (!emailAttribute.IsValid(Email))
                {
                    errors.Add(new ValidationResult("Email sai định dạng.", new[] { nameof(Email) }));
                }
            }
            return errors;
        }

        public IEnumerable<ValidationResult> ValidatePhone()
        {
            var errors = new List<ValidationResult>();
            if (string.IsNullOrWhiteSpace(Phone))
            {
                errors.Add(new ValidationResult("Số điện thoại không được để trống.", new[] { nameof(Phone) }));
            }
            return errors;
        }

        public IEnumerable<ValidationResult> ValidateAddress()
        {
            var errors = new List<ValidationResult>();
            if (string.IsNullOrWhiteSpace(Address))
            {
                errors.Add(new ValidationResult("Địa chỉ không được để trống.", new[] { nameof(Address) }));
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
