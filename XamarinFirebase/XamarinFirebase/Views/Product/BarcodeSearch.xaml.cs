using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using ZXing.Net.Mobile.Forms;

namespace XamarinFirebase.Views.Product
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class BarcodeSearch : ContentPage
    {
        public BarcodeSearch(ProductModel product)
        {
            InitializeComponent();

            LabelPicture.Source = product.Image;
            LabelImage.Text = product.Image;
            LabelName.Text = product.ProductName;
            LabelMarket.Text = product.NameOfMarket;
            LabelPrice.Text = product.Price;
            LabelBarcode.Text = product.Barcode;
            LabelPriceDate.Text = product.PriceDate.ToString();
        }
            
    }
}