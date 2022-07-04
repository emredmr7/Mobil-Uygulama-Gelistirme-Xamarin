using Plugin.Media;
using Plugin.Media.Abstractions;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace XamarinFirebase.Views.Product
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class ProductEntry : ContentPage
    {
        MediaFile file;
        ProductRepository productRepository = new ProductRepository();
        public ProductEntry()
        {
            InitializeComponent();
        }

        private async void ButtonSave_Clicked(object sender, EventArgs e)
        {
            string name = TxtName.Text;
            string market = TxtMarket.Text;
            string price = TxtPrice.Text;
            DateTime priceDate = DateTime.Now;
            string barcode = TxtBarcode.Text;
            priceDate.ToString("MM/dd/yyyy H:mm");
            string favori = "0";

            if (string.IsNullOrEmpty(name))
            {
                await DisplayAlert("UYARI", "İsim Alanı Boş Olamaz", "Tamam");                
            }
            if (string.IsNullOrEmpty(market))
            {
                await DisplayAlert("UYARI", "Market Alanı Boş Olamaz", "Tamam");
            }
            if (string.IsNullOrEmpty(price))
            {
                await DisplayAlert("UYARI", "Market Alanı Boş Olamaz", "Tamam");
            }

            ProductModel product = new ProductModel();
            product.ProductName = name;
            product.NameOfMarket = market;
            product.Price = price;
            product.PriceDate = priceDate;
            product.Barcode = barcode;
            product.Favorite = favori;

            if (file != null)
            {
                string image = await productRepository.Upload(file.GetStream(), Path.GetFileName(file.Path));
                product.Image = image;
            }
            var isSaved = await productRepository.Save(product);
            if (isSaved)
            {
                await DisplayAlert("Bilgilendirme", "Ürün eklendi!", "Tamam");
                Clear();
                await Navigation.PushAsync(new BottomTabbedPage());
            }
            else
            {
                await DisplayAlert("Hata", "Ürün Eklenmedi!", "Tamam");
            }
        }
        private async void ImageTap_Tapped(object sender, EventArgs e)
        {
            await CrossMedia.Current.Initialize();
            try
            {
                file = await CrossMedia.Current.PickPhotoAsync(new PickMediaOptions
                {
                    PhotoSize = PhotoSize.Custom
                });
                if (file == null)
                {
                    return;
                }
                ProductImage.Source = ImageSource.FromStream(() =>
                {
                    return file.GetStream();
                });
            }
            catch (Exception ex)
            {

            }
        }

        public void Clear()
        {
            TxtName.Text = string.Empty;
            TxtMarket.Text = string.Empty;
            TxtPrice.Text = string.Empty;
            TxtBarcode.Text = string.Empty;
        }       
    }
}