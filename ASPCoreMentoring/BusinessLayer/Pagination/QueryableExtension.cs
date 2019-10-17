using System;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace BusinessLayer.Pagination
{
    public static class QueryableExtension
    {
        public static async Task<PagedResult<T>> GetPagedAsync<T>(this IQueryable<T> query, int pageSize = 0, int page = 1) where T : class
        {
            var result = new PagedResult<T>
            {
                CurrentPage = page,
                PageSize = pageSize,
                RowCount = query.Count()
            };

            if (pageSize != 0)
            {
                var pageCount = (double)result.RowCount / pageSize;
                result.PageCount = (int)Math.Ceiling(pageCount);

                var skip = (page - 1) * pageSize;
                result.Results = await query.Skip(skip).Take(pageSize).ToListAsync();
            }
            else
            {
                result.Results = await query.ToListAsync();
            }

            return result;
        }
    }
}
