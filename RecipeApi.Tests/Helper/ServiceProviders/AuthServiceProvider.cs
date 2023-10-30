using RecipeApi.Tests.Helper.AuthenticationServices;

namespace RecipeApi.Tests.Helper.ServiceProviders
{
    internal class AuthServiceProvider : IServiceProvider
    {
        public object? GetService(Type serviceType)
        {
            return new StubAuthService();
        }
    }
}
