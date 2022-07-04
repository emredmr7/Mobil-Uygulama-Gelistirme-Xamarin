using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Essentials;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using XamarinFirebase.Preview;

namespace XamarinFirebase.Views.Product
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class LoginPage : ContentPage
    {
        UserRepository userRepository = new UserRepository();
        public LoginPage()
        {
            InitializeComponent();

            //still login
            bool hasKey = Preferences.ContainsKey("token");
            if (hasKey)
            { 
                string token = Preferences.Get("token", "");
                if (!string.IsNullOrEmpty(token))
                {
                    Navigation.PushAsync(new BottomTabbedPage());
                }
            }
        }

        private async void SignInButton_Clicked(object sender, EventArgs e)
        {
            try
            {
                string email = TxtEmail.Text;
                string password = TxtPassword.Text;
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

                string token = await userRepository.SignIn(email, password);
                if (!string.IsNullOrEmpty(token))
                {
                    Preferences.Set("token", token);
                    Preferences.Set("userEmail", email);
                    await Navigation.PushAsync(new BottomTabbedPage());
                }
                else
                {
                    await DisplayAlert("Başarısız Giriş!", "Giriş Yapılmadı, bilgileri kontrol edip tekrar deneyin.", "Tamam");
                }
            }
            catch(Exception ex)
            {
                if (ex.Message.Contains("EMAIL_NOT_FOUND"))
                {
                    await DisplayAlert("HATA!", "E-mail adresini yanlış girdiniz.", "Tamam");
                }
                else if (ex.Message.Contains("INVALID_PASSWORD"))
                {
                    await DisplayAlert("HATA!", "Şifrenizi yanlış girdiniz.", "Tamam");
                }
                else if (ex.Message.Contains("TOO_MANY_ATTEMPTS_TRY_LATER"))
                {
                    await DisplayAlert("HATA!", "Çok sayıda yanlış bilgi girdiniz!\nBir süre sonra tekrar deneyin.", "Tamam");
                }
                else
                {
                    await DisplayAlert("HATA!", ex.Message, "Tamam");
                }
            }
            
        }

        private async void RegisterTap_Tapped(object sender, EventArgs e)
        {
            await Navigation.PushModalAsync(new RegisterUser());
        }

        private async void PreviewTap_Tapped(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new PreviewTabbed());
        }
    }
}