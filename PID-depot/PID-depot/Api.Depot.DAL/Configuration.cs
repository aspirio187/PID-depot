using Api.Depot.DAL.IRepositories;
using Api.Depot.DAL.Repositories;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Api.Depot.DAL
{
    public static class Configuration
    {
        public static IServiceCollection InjectDAL(this IServiceCollection services)
        {
            services.AddScoped<IUserRepository, UserRepository>();

            return services;
        }
    }
}
