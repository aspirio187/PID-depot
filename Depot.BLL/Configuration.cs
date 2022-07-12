using Depot.BLL.IServices;
using Depot.BLL.Services;
using Depot.DAL;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Depot.BLL
{
    public static class Configuration
    {
        public static IServiceCollection InjectBLL(this IServiceCollection services, string connectionString)
        {
            services.InjectDAL(connectionString);

            services.AddScoped<ILessonDetailService, LessonDetailService>();
            services.AddScoped<ILessonFileService, LessonFileService>();
            services.AddScoped<ILessonService, LessonService>();
            services.AddScoped<ILessonTimetableService, LessonTimetableService>();
            services.AddScoped<IRoleService, RoleService>();
            services.AddScoped<IUserService, UserService>();
            services.AddScoped<IUserTokenService, UserTokenService>();

            return services;
        }
    }
}
