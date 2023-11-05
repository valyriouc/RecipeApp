using RecipeClient.Command.Authentication;
using RecipeClient.Database.InMemory;

using System.Windows.Input;

namespace RecipeClient.ViewModel.Login
{
    internal class LoginViewModel : ViewModelBase
    {
        private string email;

        private string password;

        public string Email
        {
            get
            {
                return email;
            }

            set
            {
                email = value;
                OnPropertyChanged(nameof(Email));
            }
        }

        public string Password
        {
            get
            {                 
                return password;
            }
            set
            {
                password = value;
                OnPropertyChanged(nameof(Password));
            }
        }

        public ICommand LoginCommand { get; }

        public LoginViewModel()
        {
            LoginCommand = new LoginCommand(this, new InMemUserRepository());
        }
    }
}
