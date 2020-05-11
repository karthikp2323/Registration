using DAL.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.Repositories.IRepository
{
    public interface IUserRepository
    {
        public Task<User> Get(int id);
        public IQueryable<User> GetAll();
        public Task<User> AddUser(User user);
        public Task<User> UpdateUser(User user);
    }
}
