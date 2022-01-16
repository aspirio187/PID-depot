using Api.Depot.DAL.IRepositories;
using Api.Depot.DAL.Repositories;
using Api.Depot.DAL.Tools;
using Microsoft.Extensions.DependencyInjection;
using MySqlConnector;
using System;
using System.Collections.Generic;
using System.Data.Common;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Api.Depot.DAL
{
    public static class Configuration
    {
        public static IServiceCollection InjectDAL(this IServiceCollection services, string connectionString)
        {
            services.AddSingleton(sp => new Connection(connectionString, MySqlConnectorFactory.Instance));

            services.AddScoped<ILessonDetailRepository, LessonDetailRepository>();
            services.AddScoped<ILessonFileRepository, LessonFileRepository>();
            services.AddScoped<ILessonRepository, LessonRepository>();
            services.AddScoped<ILessonTimetableRepository, LessonTimetableRepository>();
            services.AddScoped<IRoleRepository, RoleRepository>();
            services.AddScoped<IUserRepository, UserRepository>();
            services.AddScoped<IUserTokenRepository, UserTokenRepository>();

            return services;
        }
    }
}
