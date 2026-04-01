using App.core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace App.Application
{
    public interface IUserService
    {
        Task<User?> CreateUser(User user);
        Task RemoveUser(int id);
        Task<User?> UpdateUser(User user);
        Task<IEnumerable<User>> ListUsers();
    }
}
