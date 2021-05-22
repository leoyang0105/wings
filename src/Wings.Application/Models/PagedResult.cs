using System.Collections.Generic;
using System.Linq;

namespace Wings.Application.Models
{
    public class PagedResult<T> : IPagedResult<T> where T : class
    {
        public int PageIndex { get; private set; }
        public int PageSize { get; private set; }
        public int TotalCount { get; private set; }
        public int TotalPages { get; private set; }
        public IEnumerable<T> Items { get; private set; }

        public PagedResult(IQueryable<T> source, int pageIndex, int pageSize)
        {
            if (pageIndex > 0)
                pageIndex--;
            int totalCount = source.Count();
            TotalCount = totalCount;
            TotalPages = totalCount / pageSize;

            if (totalCount % pageSize > 0)
                TotalPages++;
            PageSize = pageSize;
            PageIndex = pageIndex + 1;
            Items = source.Skip(pageIndex * pageSize).Take(pageSize);
        }

        public PagedResult(IEnumerable<T> source, int pageIndex, int pageSize)
        {
            if (pageIndex > 0)
                pageIndex--;
            int totalCount = source.Count();
            TotalCount = totalCount;
            TotalPages = totalCount / pageSize;

            if (totalCount % pageSize > 0)
                TotalPages++;
            PageSize = pageSize;
            PageIndex = pageIndex + 1;
            Items = source.Skip(pageIndex * pageSize).Take(pageSize);
        }
    }
}
