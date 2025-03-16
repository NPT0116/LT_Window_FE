using PhoneSelling.ViewModel.Pages.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PhoneSelling.ViewModel.Pages.Items
{
    public class ItemColorViewModel : ColorViewModel
    {
        public static int CURRENT_TEMP_ID = 0;
        public ItemColorViewModel(string colorName, string colorImageUrl) : base(colorName, colorImageUrl)
        {
        }
    }
}
