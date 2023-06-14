using DataAccess.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.Interfaces
{
    public interface ISQL_Methods<T> where T : class
    {
        //1. Queryables 

        public T SQL_QueryFirstOrDefault(string query, T parameters);

        public List<T> SQL_Query(string query, T parameters);

        public List<T> SQL_Query(string query);

        //2. Executables 

        public Task<int> SQL_Executable(string query, T parameters);

    }
}
