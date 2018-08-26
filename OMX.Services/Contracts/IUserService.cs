using OMX.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace OMX.Services.Contracts
{
    public interface IUserService
    {
        Task<IEnumerable<User>> GetAllUsers();
        User GetUserById(string id);
        User GetUserByEmail(string email);
    }
}
