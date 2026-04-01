using App.core;
using App.Infrastructure;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace App.Application
{
    public class UserService : IUserService
    {
        private static UserRepository userRepository = new UserRepository();
        static UserTypeService userTypeService = new UserTypeService();
        public async Task<User?> CreateUser(User user)
        {
            string firstName = user.FirstName;
            user.FirstName = char.ToUpper(firstName[0]) + firstName.Substring(1);
            string lastName = user.LastName;
            user.LastName = char.ToUpper(lastName[0]) + lastName.Substring(1);
            bool exist = false;
            var userTypes = await userTypeService.ListUserTypes();
            foreach (var userType in userTypes)
            {
                if (userType.UserTypeId == user.UserTypeId)
                {
                    exist = true;
                }
            }
            if (exist == false)
            {
                user.UserTypeId = -1;
                return null;
            }
            await userRepository.InsertUserAsync(user);
            return user;
        }
        public async Task RemoveUser(int id)
        {
            await userRepository.DeleteUserAsync(id);
        }
        public async Task<User?> UpdateUser(User user)
        {
            string firstName = user.FirstName;
            user.FirstName = char.ToUpper(firstName[0]) + firstName.Substring(1);
            string lastName = user.LastName;
            user.LastName = char.ToUpper(lastName[0]) + lastName.Substring(1);
            bool exist = false;
            var userTypes = await userTypeService.ListUserTypes();
            foreach (var userType in userTypes)
            {
                if (userType.UserTypeId == user.UserTypeId)
                {
                    exist = true;
                }
            }
            if (exist == false)
            {
                user.UserTypeId = -1;
                return null;
            }
            await userRepository.UpdateUserAsync(user);
            return user;
        }

        public async Task<IEnumerable<User>> ListUsers()
        {
            var users = await userRepository.ListUsers();
            return users;
        }
    }
}
