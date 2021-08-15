using System.Collections.Generic;
using System.Linq;

namespace Project.Framework.Extensions
{
    public static class LinqExtensions
    {
        public static IEnumerable<T> Paging<T>(this IEnumerable<T> query, int pageSize = 1, int pageNumber = 10)
        {
            return query.Skip((pageNumber - 1) * pageSize).Take(pageSize);
        }
    }
}
