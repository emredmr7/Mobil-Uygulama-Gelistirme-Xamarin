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
    public partial class SearchPage : ContentPage
    {
        ProductRepository productRepository = new ProductRepository();
        public SearchPage()
        {
            InitializeComponent();

            ProductListView.RefreshCommand = new Command(() =>
            {
                OnAppearing();
            });
        }

        protected override async void OnAppearing()
        {
            var products = await productRepository.GetAll();
            ProductListView.ItemsSource = null;
            ProductListView.ItemsSource = products;
            ProductListView.IsRefreshing = false;
        }

        private void AddToolBarItem_Clicked(object sender, EventArgs e)
        {
            Navigation.PushAsync(new ProductEntry());
        }

        private void ProductListView_ItemTapped(object sender, ItemTappedEventArgs e)
        {
            if (e.Item == null)
            {
                return;
            }

            var producttt = e.Item as ProductModel;
            Navigation.PushAsync(new ProductDetails(producttt));
            ((ListView)sender).SelectedItem = null;

        }
        private async void TxtSearch_SearchButtonPressed(object sender, EventArgs e)
        {
            string searchValue = TxtSearch.Text;
            if (!String.IsNullOrEmpty(searchValue))
            {
                var products = await productRepository.GetAllByName(searchValue);
                ProductListView.ItemsSource = null;
                ProductListView.ItemsSource = products;          
            }
            else if(!String.IsNullOrEmpty(searchValue))
            {
                var products = await productRepository.GetAllByName(searchValue);
                if (products == null)
                {                    
                    await DisplayAlert("Bildirim", "Ürün Bulunamadı", "Tamam");
                }
            }
            else
            {
                OnAppearing();
            }
        }

        private async void TxtSearch_TextChanged(object sender, TextChangedEventArgs e)
        {
            string searchValue = TxtSearch.Text;
            if (!String.IsNullOrEmpty(searchValue))
            {
                var products = await productRepository.GetAllByName(searchValue);
                ProductListView.ItemsSource = null;
                ProductListView.ItemsSource = products;
            }
            else
            {
                OnAppearing();
            }
        }

        private async void EditSwipeItem_Invoked(object sender, EventArgs e)
        {
            string id = ((MenuItem)sender).CommandParameter.ToString();
            var product = await productRepository.GetById(id);
            if (product == null)
            {
                await DisplayAlert("HATA", "Bilgi Bulunamadı!", "Tamam");
            }
            product.Id = id;
            await Navigation.PushAsync(new ProductEdit(product));
        }

        private async void DeleteSwipeItem_Invoked(object sender, EventArgs e)
        {
            var response = await DisplayAlert("SİL", "Silmek İstediğinize Emin Misiniz?", "Evet", "Hayır");
            if (response)
            {
                string id = ((MenuItem)sender).CommandParameter.ToString();
                bool isDelete = await productRepository.Delete(id);
                if (isDelete)
                {
                    await DisplayAlert("Bilgilendirme", "Ürün Silindi", "Tamam");
                    OnAppearing();
                }
                else
                {
                    await DisplayAlert("HATA", "Ürün Silinemedi!", "Tamam");
                }
            }
        }
    }
}