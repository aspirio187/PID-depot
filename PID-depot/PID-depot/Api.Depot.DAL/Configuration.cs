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
            var connection = new Connection(connectionString, MySqlConnectorFactory.Instance);
            services.AddSingleton(sp => connection);

            InitializeRoles(connection);

            services.AddScoped<ILessonDetailRepository, LessonDetailRepository>();
            services.AddScoped<ILessonFileRepository, LessonFileRepository>();
            services.AddScoped<ILessonRepository, LessonRepository>();
            services.AddScoped<ILessonTimetableRepository, LessonTimetableRepository>();
            services.AddScoped<IRoleRepository, RoleRepository>();
            services.AddScoped<IUserRepository, UserRepository>();
            services.AddScoped<IUserTokenRepository, UserTokenRepository>();

            return services;
        }

        public static void InitializeRoles(Connection connection)
        {
            IRoleRepository roleRepository = new RoleRepository(connection);

            var adminRole = roleRepository.GetRole("Admin");
            if (adminRole is null)
            {
                var adminRoleId = roleRepository.Create(new Entities.RoleEntity() { Name = "Admin" });
                if (adminRoleId == Guid.Empty) throw new Exception("Admin role couldn't be created!");
            }
            var userRole = roleRepository.GetRole("User");
            if (userRole is null)
            {
                var userRoleId = roleRepository.Create(new Entities.RoleEntity() { Name = "User" });
                if (userRoleId == Guid.Empty) throw new Exception("User role couldn't be created!");
            }

            var studentRole = roleRepository.GetRole("Student");
            if (studentRole is null)
            {
                var studentRoleId = roleRepository.Create(new Entities.RoleEntity() { Name = "Student" });
                if (studentRoleId == Guid.Empty) throw new Exception("Student role couldn't be created!");
            }

            var teacherRole = roleRepository.GetRole("Teacher");
            if (teacherRole is null)
            {
                var teacherRoleId = roleRepository.Create(new Entities.RoleEntity() { Name = "Teacher" });
                if (teacherRoleId == Guid.Empty) throw new Exception("Teacher role couldn't be created!");
            }

            roleRepository = null;
        }
    }
}
