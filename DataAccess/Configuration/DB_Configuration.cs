using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.Configuration
{
    internal class DB_Connection
    {
        public string ConnectionString { get; private set; }

        public DB_Connection()
        {
            this.ConnectionString = "Data Source=TADEO\\SQLEXPRESS;Integrated Security=True;Connect Timeout=30;Encrypt=False;Trust Server Certificate=False;Application Intent=ReadWrite;Multi Subnet Failover=False";
        }
    }
}
