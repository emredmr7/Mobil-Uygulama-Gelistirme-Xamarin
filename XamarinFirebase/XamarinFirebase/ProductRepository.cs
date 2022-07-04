using Firebase.Database;
using Firebase.Storage;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace XamarinFirebase
{
    public class ProductRepository
    {
        FirebaseClient firebaseClient = new FirebaseClient("https://xamarinfirebase-87bc6-default-rtdb.firebaseio.com/");
        FirebaseStorage firebaseStorage = new FirebaseStorage("xamarinfirebase-87bc6.appspot.com");
        public async Task<bool> Save(ProductModel product)
        {
            var data = await firebaseClient.Child(nameof(ProductModel)).PostAsync(JsonConvert.SerializeObject(product));
            if (!string.IsNullOrEmpty(data.Key))
            {
                return true;
            }
            return false;
        }
        public async Task<List<ProductModel>> GetAll()
        {
            return (await firebaseClient.Child(nameof(ProductModel)).OnceAsync<ProductModel>()).Select(item => new ProductModel
            {
                Id = item.Key,
                ProductName = item.Object.ProductName,
                NameOfMarket = item.Object.NameOfMarket,
                Price = item.Object.Price,
                PriceDate = item.Object.PriceDate,
                Barcode = item.Object.Barcode,
                Image = item.Object.Image,
                Favorite = item.Object.Favorite
            }).ToList();
        }

        public async Task<List<ProductModel>>GetAllByName(string name)
        {
            return (await firebaseClient.Child(nameof(ProductModel)).OnceAsync<ProductModel>()).Select(item => new ProductModel
            {
                Id = item.Key,
                ProductName = item.Object.ProductName,
                NameOfMarket = item.Object.NameOfMarket,
                Price = item.Object.Price,
                PriceDate = item.Object.PriceDate,
                Barcode = item.Object.Barcode,
                Image = item.Object.Image,
                Favorite = item.Object.Favorite

            }).Where(c=>c.ProductName.ToLower().Contains(name.ToLower())).ToList();
        }

        public async Task<ProductModel>GetById(string id)
        {
            return (await firebaseClient.Child(nameof(ProductModel) + "/" + id).OnceSingleAsync<ProductModel>());
        }
        public async Task<bool> Update(ProductModel product)
        {
            await firebaseClient.Child(nameof(ProductModel) + "/" + product.Id).PutAsync(JsonConvert.SerializeObject(product));
            return true;
        }

        public async Task<bool>Delete(string id)
        {
            await firebaseClient.Child(nameof(ProductModel) + "/" + id).DeleteAsync();
            return true;
        }

        public async Task<string>Upload(Stream img, string fileName)
        {
            var image = await firebaseStorage.Child("Images").Child(fileName).PutAsync(img);
            return image;
        }
    }
}
