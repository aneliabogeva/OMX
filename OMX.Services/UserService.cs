using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using OMX.Data;
using OMX.Models;
using OMX.Services.Contracts;

namespace OMX.Services
{
    public class UserService : BaseService, IUserService
    {
        private readonly UserManager<User> userManager;
        public UserService(OmxDbContext dbContext, IMapper mapper, UserManager<User> userManager) : base(dbContext, mapper)
        {
            if (userManager == null)
            {
                throw new ArgumentNullException();
            }
            this.userManager = userManager;
        }

        public async Task<IEnumerable<User>> GetAllUsers()
        {
            var users = new List<User>();

            foreach (var user in this.DbContext.Users)
            {
                var isAdmin = await this.userManager.IsInRoleAsync(user, "Administrator");
                if (!isAdmin)
                {
                    users.Add(user);
                }
            }

            return users;

        }
        public User GetUserById(string id)
        {
            var user = DbContext.Users
                .Include(e=> e.Properties).FirstOrDefault(e=> e.Id == id);

            return user;

        }
        public User GetUserByEmail(string email)
        {
            var user = DbContext.Users
                .Include(e => e.Properties).FirstOrDefault(e => e.Email == email);

            return user;

        }

    }
}
