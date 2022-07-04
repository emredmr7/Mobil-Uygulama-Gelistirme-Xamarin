using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Essentials;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace XamarinFirebase.Views.Product
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class ChangePassword : ContentPage
    {
        UserRepository _userRepository = new UserRepository();
        public ChangePassword()
        {
            InitializeComponent();
        }

        private async void ChangePasswordButton_Clicked(object sender, EventArgs e)
        {
            try
            {
                string password = TxtPassword.Text;
                string confirmPassword = TxtConfirmPassword.Text;
                if (string.IsNullOrEmpty(password))
                {
                    await DisplayAlert("Şifre Değişikliği!", "Yeni bir şifre giriniz.","Tamam");
                }
                if (string.IsNullOrEmpty(confirmPassword))
                {
                    await DisplayAlert("Şifre Değişikliği!", "Yeni şifreyi tekrar giriniz.", "Tamam");
                    return;
                }
                if (password!=confirmPassword)
                {
                    await DisplayAlert("Şifre Değişikliği!", "Şifreler eşleşmiyor!", "Tamam");
                    return;
                }
                string token = Preferences.Get("token","");
                bool isChanged = await _userRepository.ChangePassword(token, password);
                if (isChanged)
                {
                    await DisplayAlert("Şifre Değişikliği","Şifre Başarıyla Güncellendi!","Tamam");
                    KickUser();
                }
                else
                {
                    await DisplayAlert("Şifre Değişikliği", "Şifre Güncellenemedi!", "Tamam");
                }
            }
            catch(Exception ex)
            {

            }
        }

        public void KickUser()
        {
            Preferences.Remove("token");
            App.Current.MainPage = new NavigationPage(new LoginPage());

        }
    }
}