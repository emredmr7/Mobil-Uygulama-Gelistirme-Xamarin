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
    public partial class Search : ContentPage
    {
        ProductRepository productRepository = new ProductRepository();
        public Search()
        {
            InitializeComponent();
        }

        private async void SearchWithName_Clicked(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new SearchPage());
        }

        private async void Barcode_Clicked(object sender, EventArgs e)
        {
            var scanner = new ZXingScannerPage();
            Navigation.PushAsync(scanner);
            scanner.OnScanResult += (result) =>
            {
                Device.BeginInvokeOnMainThread(async () =>
                {
                    await Navigation.PopAsync();

                    var products = await productRepository.GetAll();

                    List<ProductModel> newList = new List<ProductModel>();
                    var get = products.SingleOrDefault(x => x.Barcode == result.Text);
                    if (get != null )
                    {
                        await DisplayAlert("Bilgilendirme", "Taranan Ürün Kayıtlı", "Tamam");
                        await Navigation.PushAsync(new BarcodeSearch(get));
                    }
                    else
                    {
                        await DisplayAlert("Bilgilendirme", "Ürün Bulunamadı!\nÜrün Ekleme Sayfasına Yönlendiriliyorsunuz...", "Tamam");
                        await Navigation.PushAsync(new ProductEntry());
                    }
                });
            };
        }
    }
}