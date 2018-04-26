using System.Linq;
using System.Threading.Tasks;
using AutoMapper.QueryableExtensions;
using DelegateDecompiler;
using PagedList;
using PagedList.EntityFramework;

namespace DontForgetTheEggs.Core.Common.Extensions
{
    public static class PagedListExtensions
    {
        public static IPagedList<TDestination> ProjectToPagedList<TDestination>(this IQueryable queryable, int pageNumber, int pageSize, object parameters = null)
        {
            return queryable.ProjectTo<TDestination>(parameters).Decompile().ToPagedList(pageNumber, pageSize);
        }

        public static Task<IPagedList<TDestination>> ProjectToPagedListAsync<TDestination>(this IQueryable queryable, int pageNumber, int pageSize, object parameters = null)
        {
            return queryable.ProjectTo<TDestination>(parameters).ToPagedListAsync(pageNumber, pageSize);
        }
    }
}
