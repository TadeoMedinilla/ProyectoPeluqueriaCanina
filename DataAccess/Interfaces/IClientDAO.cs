using DataAccess.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.Interfaces
{
    public interface IClientDAO
    {
        public Task<int> RegisterClient(Register clientRegistration);
    }
}
