using DAL.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Registration.ViewModel
{
    public class UserAddress
    {
        public User User { get; set; }
        public Address Address { get; set; }
    }
}
