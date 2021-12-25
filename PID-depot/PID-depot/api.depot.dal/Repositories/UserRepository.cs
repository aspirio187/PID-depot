﻿using Api.Depot.DAL.Entities;
using Api.Depot.DAL.IRepositories;
using Api.Depot.DAL.Tools;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Api.Depot.DAL.Repositories
{
    public class UserRepository : IUserRepository
    {
        private readonly Connection _connection;

        public UserRepository(Connection connection)
        {
            _connection = connection ??
                throw new ArgumentNullException(nameof(connection));
        }

        public Guid Create(UserEntity data)
        {
            Command command = new Command("spCreateUser", true);
            command.AddParameter("email", data.Email);
            command.AddParameter("password", data.Password);
            command.AddParameter("firstname", data.Firstname);
            command.AddParameter("lastname", data.Lastname);
            command.AddParameter("birth_date", data.Birthdate.ToString("yyyy-MM-dd"));
            command.AddParameter("registration_number", data.RegistrationNumber);

            return Guid.Parse(_connection.ExecuteScalar(command).ToString());
        }

        public bool Delete(Guid key)
        {
            Command command = new Command("spDeleteUser", true);
            command.AddParameter("user_id", key);
            return _connection.ExecuteNonQuery(command) > 0;
        }

        public IEnumerable<UserEntity> GetAll()
        {
            Command command = new Command("spGetUsers", true);
            return _connection.ExecuteReader(command, r => new UserEntity(r));
        }

        public UserEntity GetById(Guid key)
        {
            Command command = new Command("spGetUser", true);
            return _connection.ExecuteReader(command, r => new UserEntity(r)).FirstOrDefault();
        }

        public bool Update(Guid key, UserEntity data)
        {
            Command command = new Command("spUpdateUser", true);
            command.AddParameter("user_id", key);
            command.AddParameter("firstname", data.Firstname);
            command.AddParameter("lastname", data.Lastname);
            command.AddParameter("birth_date", data.Birthdate.ToString("yyyy-MM-dd"));
            return _connection.ExecuteNonQuery(command) > 0;
        }
    }
}
