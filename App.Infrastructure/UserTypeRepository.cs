using App.core;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace App.Infrastructure
{
    public class UserTypeRepository
    {
        public async Task InsertUserTypeAsync(string userType)
        {
            using (var context = new AppDBContext())
            {
                var newUserType = new UserType
                {
                    Usertype = userType
                };
                await context.UserTypes.AddAsync(newUserType);
                context.SaveChanges();
            }
        }

        public async Task DeleteUserTypeAsync(int userTypeId)
        {
            using (var context = new AppDBContext())
            {
                var userType = context.UserTypes.FirstOrDefault(u => u.UserTypeId == userTypeId);
                if (userType != null)
                {
                    context.UserTypes.Remove(userType);
                    context.SaveChanges();
                }
            }
        }

        public async Task UpdateUserTypeAsync(UserType userType)
        {
            using (var context = new AppDBContext())
            {
                var usertype = context.UserTypes.FirstOrDefault(u => u.UserTypeId == userType.UserTypeId);
                if (usertype != null) 
                {
                    usertype.Usertype=userType.Usertype;
                    context.SaveChanges();
                }
            }
        }

        public async Task<IEnumerable<UserType>> ListUserTypes()
        {
            using (var context = new AppDBContext())
            {
                return context.UserTypes.ToList();
            }
        }

        public async Task<UserType> GetUserTypeByIdAsync(int userTypeId)
        {
            using (var context = new AppDBContext())
            {
                var userType = await context.UserTypes
                                     .FirstOrDefaultAsync(u => u.UserTypeId == userTypeId);
                return userType;
            }
        }
    }
}
