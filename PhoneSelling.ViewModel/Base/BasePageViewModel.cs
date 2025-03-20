using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CommunityToolkit.Mvvm.ComponentModel;
using PhoneSelling.ViewModel.Navigation;
namespace PhoneSelling.ViewModel.Base
{
    public class BasePageViewModel : ObservableValidator
    {
        public PageNavigation ParentPageNavigation { get; set; }
    }
}
