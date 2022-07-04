using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace XamarinFirebase.Preview
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class PreviewTabbed : TabbedPage
    {
        public PreviewTabbed()
        {
            InitializeComponent();
        }
    }
}