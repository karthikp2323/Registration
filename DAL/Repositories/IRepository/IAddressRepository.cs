using DAL.Entities;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace DAL.Repositories.IRepository
{
    public interface IAddressRepository
    {
        public Task<Address> AddAddress(Address address);
    }
}
