using App.core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace App.Application
{
    public interface IUserTypeService
    {
         Task CreateUserType(UserType userType);
        Task RemoveUserType(int id);
        Task UpdateUserType(UserType userType);
        Task<IEnumerable<UserType>> ListUserTypes();
        Task<UserType> GetUserTypeById(int id);
    }
}
