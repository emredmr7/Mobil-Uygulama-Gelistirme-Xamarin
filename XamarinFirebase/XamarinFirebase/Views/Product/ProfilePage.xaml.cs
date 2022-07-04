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
    public partial class ProfilePage : ContentPage
    {
        UserRepository _userRepository = new UserRepository();
        
        public ProfilePage()
        {
            InitializeComponent();

            UserLabel.Text = Preferences.Get("userEmail", "default");
        }

        private void ChangePasswordButton_Clicked(object sender, EventArgs e)
        {
            Navigation.PushAsync(new ChangePassword());
        }

        private void LogOut_Clicked(object sender, EventArgs e)
        {
            Preferences.Remove("token");
            App.Current.MainPage = new NavigationPage(new LoginPage());
        }

        [Obsolete]
        private async void DeleteAccount_Clicked(object sender, EventArgs e)
        {
            string token = Preferences.Get("token", "");
            bool isChanged = await _userRepository.DeleteUser(token);
            if (isChanged)
            {
                await DisplayAlert("BİLGiLENDİRME", "Hesabınızı Başarıyla Sildiniz", "Tamam");
                KickUser();
            }
        }

        public void KickUser()
        {
            Preferences.Remove("token");
            App.Current.MainPage = new NavigationPage(new LoginPage());

        }
    }
}