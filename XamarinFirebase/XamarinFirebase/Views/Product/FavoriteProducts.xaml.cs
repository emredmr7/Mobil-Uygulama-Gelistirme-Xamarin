using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Internals;
using Xamarin.Forms.Xaml;

namespace XamarinFirebase.Views.Product
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class FavoriteProducts : ContentPage
    {
        ProductRepository productRepository = new ProductRepository();
        public FavoriteProducts()
        {
            InitializeComponent();
        }

        protected override async void OnAppearing()
        {
            var products = await productRepository.GetAll();

            List<ProductModel> newList = new List<ProductModel>();
            foreach (var item in products)
            {
                if (item.Favorite == "1")
                {
                    var newAdd = new ProductModel{Favorite = item.Favorite, Id = item.Id, Image = item.Image, NameOfMarket = item.NameOfMarket, Price = item.Price, ProductName = item.ProductName, Barcode = item.Barcode };
                    newList.Add(newAdd);
                }
            }

            ProductListView.ItemsSource = null;
            ProductListView.ItemsSource = newList;
            ProductListView.IsRefreshing = false;

        }
        private void ProductListView_ItemTapped(object sender, ItemTappedEventArgs e)
        {
            if (e.Item == null)
            {
                return;
            }

            var producttt = e.Item as ProductModel;
            Navigation.PushAsync(new ProductDetailsRemove(producttt));
            ((ListView)sender).SelectedItem = null;

        }
    }
}