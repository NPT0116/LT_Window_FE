using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Navigation.Converters
{
    using Microsoft.UI.Xaml.Data;
    using CommunityToolkit.Mvvm.ComponentModel;
    using System;
    using System.Linq;
    using System.ComponentModel.DataAnnotations;
    using System.Diagnostics;
    using System.Reflection;

    public class ValidationErrorConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parentObject, string language)
        {
            Debug.WriteLine("Converter is called");
            if (value == null)
            {
                return string.Empty;
            }
            Debug.WriteLine("Value is not null");
            if (parentObject is ObservableValidator observableValidator)
            {
                Debug.WriteLine("Inside if condition");
                    var propertyName = GetPropertyName(value, observableValidator);
                    Debug.WriteLine(propertyName);
                    var errors = observableValidator.GetErrors(propertyName)?.Cast<ValidationResult>();
                    return errors?.FirstOrDefault()?.ErrorMessage ?? string.Empty;
                
            }

            return string.Empty;
        }

        public object ConvertBack(object value, Type targetType, object parameter, string language)
        {
            throw new NotImplementedException();
        }

        // 🔹 Helper method to get the parent object (Customer) of a property (Email)
        private object GetParentObject(object value)
        {
            return value?.GetType().DeclaringType != null ? value : null;
        }

        // 🔹 Helper method to infer property name dynamically
        private string GetPropertyName(object value, ObservableValidator parentObject)
        {
            return parentObject.GetType()
                .GetProperties(BindingFlags.Public | BindingFlags.Instance)
                .FirstOrDefault(prop => prop.GetValue(parentObject)?.Equals(value) == true)
                ?.Name;
        }
    }

}
