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
    class PictureRepository
    {
        FirebaseClient firebaseClient = new FirebaseClient("https://xamarinfirebase-87bc6-default-rtdb.firebaseio.com/");
        FirebaseStorage firebaseStorage = new FirebaseStorage("xamarinfirebase-87bc6.appspot.com");

        public async Task<bool> Save(ProfilePicModel profilePicModel)
        {
            var data = await firebaseClient.Child(nameof(ProfilePicModel)).PostAsync(JsonConvert.SerializeObject(profilePicModel));
            if (!string.IsNullOrEmpty(data.Key))
            {
                return true;
            }
            return false;
        }
        public async Task<ProfilePicModel> GetById(string id)
        {
            return (await firebaseClient.Child(nameof(ProfilePicModel) + "/" + id).OnceSingleAsync<ProfilePicModel>());
        }
        public async Task<string> Upload(Stream img, string fileName)
        {
            var image = await firebaseStorage.Child("Images").Child(fileName).PutAsync(img);
            return image;
        }

    }
}
