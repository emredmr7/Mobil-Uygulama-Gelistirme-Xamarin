using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace XamarinFirebase.Views.Product
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class ProductDetails : ContentPage
    {
        ProductRepository productRepository = new ProductRepository();
        
        public ProductDetails(ProductModel product)
        {
            InitializeComponent();

            LabelPicture.Source = product.Image;
            LabelImage.Text = product.Image;
            LabelName.Text = product.ProductName;
            LabelMarket.Text = product.NameOfMarket;
            LabelPrice.Text = product.Price;
            TxtBarcode.Text = product.Barcode;
            LabelPriceDate.Text = product.PriceDate.ToString();

            TxtId.Text = product.Id;
            TxtName.Text = product.ProductName;
            TxtMarket.Text = product.NameOfMarket;
            TxtPrice.Text = product.Price;
            TxtBarcode.Text = product.Barcode;
            TxtPriceDate.Date = product.PriceDate;
        }
        private async void AddFavoriteButton_Clicked(object sender, EventArgs e)
        {
            string favorite = "1";

            ProductModel productModel = new ProductModel();
            productModel.Id = TxtId.Text;
            productModel.ProductName = TxtName.Text;
            productModel.NameOfMarket = TxtMarket.Text;
            productModel.Price = TxtPrice.Text;
            productModel.Barcode = TxtBarcode.Text;
            productModel.PriceDate = TxtPriceDate.Date;
            productModel.Favorite = favorite;
            productModel.Image = LabelImage.Text;

            var isUpdated = await productRepository.Update(productModel);
            if (isUpdated)
            {
                await DisplayAlert("Bildirim", "Ürünü Favorilerine Ekledin!", "Tamam");
                await Navigation.PopAsync();
            }
            else
            {
                await DisplayAlert("Bildirim", "Ürün Favorilere Eklenemedi!", "Tamam");
                return;
            }
        }
    }
}