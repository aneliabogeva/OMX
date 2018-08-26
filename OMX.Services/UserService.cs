using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using AutoMapper;
using Microsoft.EntityFrameworkCore;
using OMX.Data;
using OMX.Models;
using OMX.Services.Contracts;

namespace OMX.Services
{
    public class UserService : BaseService, IUserService
    {
        public UserService(OmxDbContext dbContext, IMapper mapper) : base(dbContext, mapper)
        {
        }

        public IEnumerable<User> GetAllUsers()
        {
            var users = this.DbContext.Users.ToList();

            return users;

        }
        public User GetUserById(string id)
        {
            var user = DbContext.Users
                .Include(e=> e.Properties).FirstOrDefault(e=> e.Id == id);

            return user;

        }

    }
}
