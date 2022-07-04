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
    public partial class ProductListPage : ContentPage
    {
        ProductRepository productRepository = new ProductRepository();

        public ProductListPage()
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
        private void ProductListView_ItemTapped(object sender, ItemTappedEventArgs e)
        {
            if(e.Item == null)
            {
                return;
            }

            var producttt = e.Item as ProductModel;
            Navigation.PushAsync(new ProductDetails(producttt));
            ((ListView)sender).SelectedItem = null;
                
        }
    }
}