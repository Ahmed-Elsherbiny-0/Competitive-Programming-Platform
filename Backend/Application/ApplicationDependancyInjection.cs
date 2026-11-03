using Application.Auth.Commands.CreateRegister;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application
{
    public static  class ApplicationDependancyInjection
    {
        public static IServiceCollection AddDependanciesFromApplication(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddMediatR(x => x.RegisterServicesFromAssembly(typeof(CreateRegisterCommand).Assembly));
            return services;
        }
    }
}
