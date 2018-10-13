using AutoMapper;
using DontForgetTheEggs.Core.Common.Extensions;
using DontForgetTheEggs.Core.Data;
using MediatR;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace DontForgetTheEggs.Core.Features.GroceryList
{
    public static class SearchIngredient
    {
        public class Query : IRequest<IEnumerable<Model>>
        {
            public string SearchText { get; set; }
            public int? Page { get; set; }
        }

        public class Model 
        {
            public int Id { get; set; }
            public string Name { get; set; }
            public string CategoryName { get; set; }
        }

        public class ModelMapping : Profile
        {
            public ModelMapping()
            {
                CreateMap<Grocery, Model>();
            }
        }

        public class QueryHandler : IRequestHandler<Query, IEnumerable<Model>>
        {
            private readonly EggsDbContext _eggsDbContext;

            public QueryHandler(EggsDbContext eggsDbContext)
            {
                _eggsDbContext = eggsDbContext;
            }

            public async Task<IEnumerable<Model>> Handle(Query request, CancellationToken cancellationToken)
            {
                IQueryable<Grocery> query = _eggsDbContext.Groceries;

                if(!string.IsNullOrEmpty(request.SearchText))
                {
                    query = query.Where(x => 
                        x.Name.Contains(request.SearchText) ||
                        x.Category.Name.Contains(request.SearchText));
                }

                var page = request.Page ?? 1;
                var pageSize = 10; // add settings here

                return await query
                    .OrderBy(x => x.Name)
                    .ProjectToPagedListAsync<Model>(page, pageSize)
                    .ConfigureAwait(false);
            }
        }
    }
}
