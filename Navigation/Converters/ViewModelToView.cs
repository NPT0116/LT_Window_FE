﻿using Microsoft.UI.Xaml.Controls;
using Microsoft.UI.Xaml.Data;
using Navigation.Views;
using PhoneSelling.ViewModel.Pages.Authentication;
using PhoneSelling.ViewModel.Pages.Sample;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Navigation.Converters
{
    internal class ViewModelToView : IValueConverter
    {
        private static readonly Dictionary<Type, Type> pairs = new Dictionary<Type, Type>()
        {
            {typeof(Page1ViewModel),typeof(Page1)},
            {typeof(Page1_1ViewModel),typeof(Page1_1)},
            {typeof(Page1_2ViewModel),typeof(Page1_2)},
            //{typeof(Page1_3ViewModel),typeof(Page1_3)},
            //{typeof(Page1_4ViewModel),typeof(Page1_4)},
            {typeof(Page2ViewModel),typeof(Page2)},
            {typeof(LoginPageViewModel),typeof(LoginPage)},
            {typeof(PhonePageViewModel),typeof(PhonePage)}
        };

        public object Convert(object value, Type targetType, object parameter, string language)
        {
            pairs.TryGetValue(value.GetType(), out var page);
            Page x = (Page)Activator.CreateInstance(page);
            x.DataContext = value;
            return x;
        }

        public object ConvertBack(object value, Type targetType, object parameter, string language)
        {
            throw new NotImplementedException();
        }
    }
}
