using App.core;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace App.Infrastructure
{
    public class UserRepository
    {
        public async Task InsertUserAsync(User user)
        {
            using (var context = new AppDBContext())
            {
                var newUser = new User
                {
                    FirstName=user.FirstName,
                    LastName=user.LastName,
                    Email=user.Email,
                    Password=user.Password,
                    UserTypeId=user.UserTypeId,
                };
                await context.Users.AddAsync(newUser);
                context.SaveChanges();
            }
        }

        public async Task DeleteUserAsync(int userId)
        {
            using (var context = new AppDBContext())
            {
                var user = context.Users.FirstOrDefault(x => x.UserId == userId);
                if (user != null)
                {
                    context.Users.Remove(user);
                    context.SaveChanges();
                }
            }
        }

        public async Task UpdateUserAsync(User user)
        {
            using (var context = new AppDBContext())
            {
                var User = context.Users.FirstOrDefault(u => u.UserId == user.UserId);
                if (User != null) 
                {
                    User.FirstName = user.FirstName;
                    User.LastName = user.LastName;
                    User.Email = user.Email;
                    User.Password = user.Password;
                    User.UserTypeId = user.UserTypeId;
                   context.SaveChanges();
                }
            }
        }

        public async Task<IEnumerable<User>> ListUsers()
        {
            using (var context = new AppDBContext())
            {
                return context.Users.ToList(); 
               
            }
        }
    }
}
