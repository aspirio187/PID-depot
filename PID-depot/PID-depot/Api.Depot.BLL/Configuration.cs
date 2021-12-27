using Api.Depot.BLL.IServices;
using Api.Depot.BLL.Services;
using Api.Depot.DAL;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Api.Depot.BLL
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

            return services;
        }
    }
}
