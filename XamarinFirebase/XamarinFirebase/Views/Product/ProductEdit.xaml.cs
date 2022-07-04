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
    public partial class ProductEdit : ContentPage
    {
        MediaFile file;
        ProductRepository productRepository = new ProductRepository();
        public ProductEdit(ProductModel product)
        {
            InitializeComponent();

            TxtId.Text = product.Id;
            ProductImage.Source = product.Image; //BURADA KALDIM. Favorilere fotoğraf gitmiyor.
            TxtName.Text = product.ProductName;
            TxtMarket.Text = product.NameOfMarket;
            TxtPrice.Text = product.Price;
            TxtPriceDate.Date = product.PriceDate.Date;
            TxtBarcode.Text = product.Barcode;

        }

        private async void ButtonUpdate_Clicked(object sender, EventArgs e)
        {
            string name = TxtName.Text;
            string market = TxtMarket.Text;
            string price = TxtPrice.Text;
            string barcode = TxtBarcode.Text;
            DateTime priceDate = DateTime.Now;
            priceDate.ToString("MM/dd/yyyy H:mm");

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
                await DisplayAlert("UYARI", "Fiyat Alanı Boş Olamaz", "Tamam");
            }

            ProductModel product = new ProductModel();
            product.Id = TxtId.Text;
            product.ProductName = name;
            product.NameOfMarket = market;
            product.Price = price;
            product.Barcode = barcode;
            product.PriceDate = priceDate;

            if (file!=null)
            {
                string image = await productRepository.Upload(file.GetStream(), Path.GetFileName(file.Path));
                product.Image = image;
            }
            bool isUpdated = await productRepository.Update(product);
            if (isUpdated)
            {
                await Navigation.PopModalAsync();
            }
            else
            {
                await DisplayAlert("HATA", "Güncelleme Yapılamadı", "Tamam");
            }
        }

        private async void ImageTap_Tapped(object sender, EventArgs e)
        {
            await CrossMedia.Current.Initialize();
            try
            {
                file = await CrossMedia.Current.PickPhotoAsync(new PickMediaOptions 
                {
                    PhotoSize = PhotoSize.Medium
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
            catch(Exception ex)
            {

            }
        }
    }
}