using AutoMapper;
using System.Linq;
using Wings.Application.Models;

namespace Wings.Application.Extensions
{
    public static class IMapperExtension
    {
        public static IPagedResult<TDestination> ProjectToPaged<TDestination>(this IMapper mapper, IQueryable source, int pageIndex, int pageSize)
            where TDestination : class
        {
            return new PagedResult<TDestination>(mapper.ProjectTo<TDestination>(source), pageIndex, pageSize);
        }
    }
}
