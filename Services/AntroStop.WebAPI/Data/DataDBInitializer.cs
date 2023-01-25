using AntroStop.DAL.Context;
using AntroStop.DAL.Entities;
using AntroStop.Domain;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;

namespace AntroStop.WebAPI.Data
{
    public class DataDBInitializer
    {
        private readonly DataDB db;
        public DataDBInitializer(DataDB db) => this.db = db;

        public void Initialize()
        {
            db.Database.Migrate();

            HasAnyRoles();
            HasAnyUsers();
        }

        private void HasAnyRoles()
        {
            if (!db.Roles.Any())
            {
                List<Role> roles = new List<Role>();

                roles.Add(new Role() { Name = "Главный администратор", Description = "Администратор управления всей системой", CreatedAt = DateTime.UtcNow, UpdatedAt = DateTime.UtcNow });
                roles.Add(new Role() { Name = "Администратор организации", Description = "Администратор организации", CreatedAt = DateTime.UtcNow, UpdatedAt = DateTime.UtcNow });
                roles.Add(new Role() { Name = "Пользователь", Description = "Пользователь системы", CreatedAt = DateTime.UtcNow, UpdatedAt = DateTime.UtcNow });

                db.AddRange(roles);
                db.SaveChanges();
            }
        }

        private void HasAnyUsers()
        {
            Role role = db.Roles.Where(x => x.ID == 1).Where(n => n.Name == "Главный администратор").FirstOrDefault();

            if(!db.Users.Any())
            {
                List<User> users = new List<User>();

                users.Add(new User() { ID = "admin@admin.local", Status = true, Role = role, Name = "Администратор", Password = Hasher.GetHashPassword("Admin"), CreatedAt = DateTime.UtcNow, UpdatedAt = DateTime.UtcNow });
                
                db.AddRange(users);
                db.SaveChanges();
            }
        }
    }
}
