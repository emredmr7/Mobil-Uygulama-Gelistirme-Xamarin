using Plugin.Media;
using Plugin.Media.Abstractions;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Essentials;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace XamarinFirebase.Views.Product
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class RegisterUser : ContentPage
    {
        MediaFile file;
        UserRepository userRepository = new UserRepository();
        PictureRepository pictureRepository = new PictureRepository();
        public RegisterUser()
        {
            InitializeComponent();
        }

        private async void RegisterButton_Clicked(object sender, EventArgs e)
        {
            try
            {
                string name = TxtName.Text;
                string email = TxtEmail.Text;
                string password = TxtPassword.Text;
                string confirmPassword = TxtConfirmPass.Text;

                if (String.IsNullOrEmpty(name))
                {
                    await DisplayAlert("UYARI!", "Ad Alanı Boş Olamaz!", "Tamam");
                    return;
                }
                if (String.IsNullOrEmpty(email))
                {
                    await DisplayAlert("UYARI!", "E-mail Alanı Boş Olamaz!", "Tamam");
                    return;
                }
                if (String.IsNullOrEmpty(password))
                {
                    await DisplayAlert("UYARI!", "Şifre Alanı Boş Olamaz!", "Tamam");
                    return;
                }
                if (String.IsNullOrEmpty(confirmPassword))
                {
                    await DisplayAlert("UYARI!", "Şifre Tekrar Alanı Boş Olamaz!", "Tamam");
                    return;
                }
                if (password != confirmPassword)
                {
                    await DisplayAlert("UYARI!", "Şifre ve Şifre Tekrarı Eşleşmiyor\nLütfen Dikkat Ediniz", "Tamam");
                    return;
                }

                bool isSave = await userRepository.Register(email, name, password);
                if (isSave)
                {
                    await DisplayAlert("Yeni Kullanıcı!", "Tebrikler! Kayıt Oldunuz", "Tamam");
                    await Navigation.PopModalAsync();
                }
                else
                {
                    await DisplayAlert("Başarısız", "Kayıt İşleminiz Başarısız", "Tamam");
                }

                ProfilePicModel profilePicModel = new ProfilePicModel();
                if (file != null)
                {
                    string image = await pictureRepository.Upload(file.GetStream(), Path.GetFileName(file.Path));
                    profilePicModel.EMail = email;
                    profilePicModel.ProfilePicture = image;
                }
                await pictureRepository.Save(profilePicModel);

            }
            catch(Exception ex)
            {
                if (ex.Message.Contains("EMAIL_EXISTS"))
                {
                    await DisplayAlert("UYARI!", "E-mail adresi zaten kullanılıyor!\nFarklı mail adresi deneyiniz.", "Tamam");
                }
                else
                {
                    await DisplayAlert("HATA!", ex.Message, "Tamam");
                }
            }
            
        }

        //MediaPicker kullanmak zorunda kaldım. CrossMedia ile fotoğraf çektirmeyi bir türlü başaramadım. Permission'lar sorun çıkardı.
        private async void TakePhoto_Clicked(object sender, EventArgs e)
        {
            var result = await MediaPicker.CapturePhotoAsync();

            if (result != null)
            {
                var stream = await result.OpenReadAsync();

                FileImage.Source = ImageSource.FromStream(() => stream);

            }
        }

        private async void ChoosePhoto_Clicked(object sender, EventArgs e)
        {
            await CrossMedia.Current.Initialize();

            if (!CrossMedia.Current.IsPickPhotoSupported)
            {
                await DisplayAlert("Uyarı", "Fotoğraf Seçilmedi", "Tamam");
                return;
            }

            file = await CrossMedia.Current.PickPhotoAsync();

            if (file == null)
                return;
            FileImage.Source = ImageSource.FromStream(() =>
            {
                return file.GetStream();
            });
        }
    }
}