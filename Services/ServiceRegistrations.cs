using DataAccess.Interfaces;
using DataAccess.Queries;
using Microsoft.Extensions.DependencyInjection;
using Services.Functional;
using Services.Interfaces;

namespace Services
{
    public static class ServiceRegistrations
    {

        public static void RegisterServices(IServiceCollection services)
        {
            services.AddTransient<IAuthenticationsService, AuthenticationsService>();
            services.AddTransient(provider => new Lazy<IAuthenticationsService>(provider.GetService<IAuthenticationsService>));


            services.AddTransient<ISignupService, SignupService>();
            services.AddTransient(provider => new Lazy<ISignupService>(provider.GetService<ISignupService>));

            services.AddTransient<IRestaurentService, RestaurentService>();
            services.AddTransient(provider => new Lazy<IRestaurentService>(provider.GetService<IRestaurentService>));

            services.AddTransient<ICommonService, CommonService> ();
            services.AddTransient(provider => new Lazy<ICommonService>(provider.GetService<ICommonService>));

            services.AddTransient<ICustomerService, CustomerService>();
            services.AddTransient(provider => new Lazy<ICustomerService>(provider.GetService<ICustomerService>));
        }
    }
}