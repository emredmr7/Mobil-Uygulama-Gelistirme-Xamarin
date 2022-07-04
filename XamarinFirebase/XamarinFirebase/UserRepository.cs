using Firebase.Auth;
using Firebase.Database;
using Firebase.Storage;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace XamarinFirebase
{
    public class UserRepository
    {
        static string webAPIKey = "AIzaSyC7I8ou1EcA2O136LCq-E0uConyHwYhpRU";
        FirebaseAuthProvider authProvider;
        FirebaseStorage firebaseStorage = new FirebaseStorage("xamarinfirebase-87bc6.appspot.com");

        public UserRepository()
        {
            authProvider = new FirebaseAuthProvider(new FirebaseConfig(webAPIKey));
        }

        public async Task<bool>Register(string email, string name,string password)
        {
            var token = await authProvider.CreateUserWithEmailAndPasswordAsync(email, password, name);

            if (!string.IsNullOrEmpty(token.FirebaseToken))
            {
                return true;
            }
            return false;
        }

        public async Task<string>SignIn(string email, string password)
        {
            var token = await authProvider.SignInWithEmailAndPasswordAsync(email, password);
            if (!string.IsNullOrEmpty(token.FirebaseToken))
            {
                return token.FirebaseToken;
            }
            return "";
        }

        public async Task<bool>ChangePassword(string token, string password)
        {
            await authProvider.ChangeUserPassword(token, password);
            return true;
        }

        [Obsolete]
        public async Task<bool>DeleteUser(string id)
        {
            await authProvider.DeleteUser(id);
            return true;
        }

    }
}
