using DataAccess.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.Interfaces
{
    public interface IUserDAO
    {
        public Task<int> RegisterUser(Register userRegistration);

        public User GetUser(User toSearch);
    }
}
