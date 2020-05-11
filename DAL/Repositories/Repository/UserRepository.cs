using DAL.DataContext;
using DAL.Entities;
using DAL.Repositories.IRepository;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.Repositories.Repository
{
    public class UserRepository : IUserRepository
    {
        
        public async Task<User> Get(int id)
        {
            using (var context = new DatabaseContext(DatabaseContext.ops.dbOption))
            {   
                return await context.Users.FindAsync(id); 
            }
            
        }

        public IQueryable<User> GetAll()
        {
            var context = new DatabaseContext(DatabaseContext.ops.dbOption);
            return context.Users.AsQueryable();
        }

        public async Task<User> AddUser(User user)
        {
            using (var context = new DatabaseContext(DatabaseContext.ops.dbOption)) {
                await context.Users.AddAsync(user);
                await context.SaveChangesAsync();
            }
            return user;
        }

        public async Task<User> UpdateUser(User user)
        {
            using (var context = new DatabaseContext(DatabaseContext.ops.dbOption))
            {
                context.Users.Update(user);
                await context.SaveChangesAsync();
            }
            return user;
        }
    }
}
