using DAL.DataContext;
using DAL.Entities;
using DAL.Repositories.IRepository;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace DAL.Repositories.Repository
{
    public class AddressRepository :IAddressRepository
    {
        public async Task<Address> AddAddress(Address address)
        {
            using (var context = new DatabaseContext(DatabaseContext.ops.dbOption))
            {
                await context.Address.AddAsync(address);
                await context.SaveChangesAsync();
            }
            return address;
        }
    }
}
