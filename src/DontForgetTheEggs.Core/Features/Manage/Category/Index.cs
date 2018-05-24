using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using DontForgetTheEggs.Core.Common.Extensions;
using DontForgetTheEggs.Core.Data;
using MediatR;
using PagedList;

namespace DontForgetTheEggs.Core.Features.Manage.Category
{
    public class Index
    {
        public class Query : IRequest<IPagedList<Model>>
        {
            public int? Page { get; set; }
        }

        public class Model
        {
            public int Id { get; set; }
            public string Name { get; set; }
            public int GroceryCount { get; set; }
        }

        public class Mappings : Profile
        {
            public Mappings()
            {
                CreateMap<Data.Category, Model>()
                    .ForMember(x => x.GroceryCount, opt => opt.MapFrom(src => src.Groceries.Count()));
            }
        }

        public class QueryHandler : IRequestHandler<Query, IPagedList<Model>>
        {
            private readonly EggsDbContext _dbContext;

            public QueryHandler(EggsDbContext dbContext)
            {
                _dbContext = dbContext;
            }

            public async Task<IPagedList<Model>> Handle(Query request, CancellationToken cancellationToken)
            {
                var query = _dbContext.Categories
                    .OrderBy(x => x.Id);

                var page = request.Page ?? 1;
                var pageSize = 10; // add settings here

                return await query.ProjectToPagedListAsync<Model>(page, pageSize);
            }
        }
    }
}
