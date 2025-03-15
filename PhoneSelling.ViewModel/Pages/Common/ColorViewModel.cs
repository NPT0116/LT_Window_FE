using PhoneSelling.ViewModel.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PhoneSelling.ViewModel.Pages.Common
{
    public class ColorViewModel
    {
        public string ColorName { get; set; }
        public string ColorImageUrl { get; set; }

        public ColorViewModel(string colorName, string colorImageUrl)
        {
            ColorName = colorName;
            ColorImageUrl = colorImageUrl;
        }
    }

}
