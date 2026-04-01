using App.core;
using App.Infrastructure;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace App.Application
{
    public class UserTypeService : IUserTypeService
    {
        private  UserTypeRepository userTypeRepository = new UserTypeRepository();

        public  async Task CreateUserType(UserType userType)
        {
            await userTypeRepository.InsertUserTypeAsync(userType.Usertype);
            Console.WriteLine("UserType Added");
        }
        public  async Task RemoveUserType(int id)
        {
            await userTypeRepository.DeleteUserTypeAsync(id);
            Console.WriteLine("Deleted");
        }
        public  async Task UpdateUserType(UserType userType)
        {
            await userTypeRepository.UpdateUserTypeAsync(userType);
            Console.WriteLine("Updated");
        }
        public async Task<IEnumerable<UserType>> ListUserTypes()
        {
            var userTypes = await userTypeRepository.ListUserTypes();
            return userTypes;
        }
        public async Task<UserType> GetUserTypeById(int id)
        {
            var userType = await userTypeRepository.GetUserTypeByIdAsync(id);
            return userType;
        }
    }
}
