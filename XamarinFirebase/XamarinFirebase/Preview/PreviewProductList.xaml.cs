using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using XamarinFirebase.Views.Product;

namespace XamarinFirebase.Preview
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class PreviewProductList : ContentPage
    {
        public PreviewProductList()
        {
            InitializeComponent();
        }

        private void ForSignIn_Clicked(object sender, EventArgs e)
        {
            Navigation.PushAsync(new LoginPage());
        }
    }
}