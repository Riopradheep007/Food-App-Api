using DataAccess.Interfaces;
using DataAccess.Queries;
using Microsoft.Extensions.DependencyInjection;

namespace DataAccess
{
    public static class DataAccessRegistrations
    {
        public static void RegisterDataAccess(IServiceCollection services)
        {

            services.AddTransient<IAuthenticationDataAccess, AuthenticationDataAccess>();
            services.AddTransient(provider => new Lazy<IAuthenticationDataAccess>(provider.GetService<IAuthenticationDataAccess>));


            services.AddTransient<ISignupDataAccess, SignupDataAccess>();
            services.AddTransient(provider => new Lazy<ISignupDataAccess>(provider.GetService<ISignupDataAccess>));

            services.AddTransient<IRestaurentDataAccess, RestaurentDataAccess>();
            services.AddTransient(provider => new Lazy<IRestaurentDataAccess>(provider.GetService<IRestaurentDataAccess>));
            
            services.AddTransient<ICommonDataAccess, CommonDataAccess>();
            services.AddTransient(provider => new Lazy<ICommonDataAccess>(provider.GetService<ICommonDataAccess>));

            services.AddTransient<ICustomerDataAccess, CustomerDataAccess>();
            services.AddTransient(provider => new Lazy<ICustomerDataAccess>(provider.GetService<ICustomerDataAccess>));
        }

    }
}