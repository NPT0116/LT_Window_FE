using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.UI.Xaml.Controls;

namespace Navigation.Views
{
    public sealed partial class ReportPage : Page
    {
        public ReportPage()
        {
            Debug.WriteLine("Report Page Initialization !");
            InitializeComponent();
        }
    }
}
