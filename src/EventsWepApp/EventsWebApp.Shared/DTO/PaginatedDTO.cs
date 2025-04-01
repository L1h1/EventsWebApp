using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EventsWebApp.Shared.DTO
{
    public class PaginatedDTO<T>
    {
        public IEnumerable<T> Items { get; set; }
        public int TotalCount { get; set; }
        public int PageSize { get; set; }
        public int PageCount => (int)Math.Ceiling(TotalCount / (double)PageSize);
        public int PageNumber { get; set; }

    }
}
