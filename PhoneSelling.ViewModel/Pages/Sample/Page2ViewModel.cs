﻿using CommunityToolkit.Mvvm.Input;
using PhoneSelling.ViewModel.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PhoneSelling.ViewModel.Pages.Sample
{
    public class Page2ViewModel : BasePageViewModel
    {
        public Page2ViewModel()
        {
            GoToPage1_1Command = new RelayCommand(
                () => ParentPageNavigation.ViewModel = new Page1ViewModel(new Page1_1ViewModel()));
            //GoToPage1_2Command = new RelayCommand(
            //    () => ParentPageNavigation.ViewModel = new Page1ViewModel(new Page1_2ViewModel()));
        }
        public RelayCommand GoToPage1_1Command { get; set; }
        public RelayCommand GoToPage1_2Command { get; set; }
    }
}
