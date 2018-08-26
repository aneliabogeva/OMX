using OMX.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace OMX.Services.Contracts
{
    public interface IUserService
    {
        IEnumerable<User> GetAllUsers();
        User GetUserById(string id);
    }
}
