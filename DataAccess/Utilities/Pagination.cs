using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.Utilities
{
    public class Pagination
    {
        public int PageNumber { get; set; }
        public int PageSize { get; set; }
        public int Offset { get; set; }
        public int maxResults { get; set; } = 30;
        public string OrderBy { get; set; }
        public int TotalPages { get; set; }

        public Pagination(string orderBy, int pageSize = 10, int pageNumber = 1)
        {
            OrderBy = orderBy;
            if (pageSize < maxResults)
            {
                PageSize = pageSize;
            }
            Offset = (pageNumber - 1) * PageSize;
        }
    }
}
