using RecipeClient.Auth;
using RecipeClient.Database.Core;
using RecipeClient.Model;
using RecipeClient.ViewModel.Login;

using System;

namespace RecipeClient.Command.Authentication
{
    internal class LoginCommand : CommandBase
    {
        public LoginViewModel LoginModel { get; }

        public IRepository<User> Repository { get; }

        public LoginCommand(LoginViewModel loginModel, IRepository<User> repository)
        {
            LoginModel = loginModel;
            Repository = repository;
        }

        public override void Execute(object? parameter)
        {

            if (!User.IsValidEmail(LoginModel.Email) || !User.IsValidPassword(LoginModel.Password))
            {
                throw new Exception(
                    "Email or password are incorrect");
            }

            User user = Repository.GetSingle(user => user.Email == LoginModel.Email);

            string hashedPassword = User.GenerateSha512PasswordHash(LoginModel.Password);

            if (user.Password != hashedPassword)
            {
                throw new Exception(
                    "Email or password are incorrect");
            }

            // We need some authentication mechanism 
            AuthenticationStore.Add(user);
        }
    }
}
