using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using DontForgetTheEggs.Core.Common.Exceptions;
using DontForgetTheEggs.Core.Data;
using MediatR;
using AutoMapper;

namespace DontForgetTheEggs.Core.Features.Manage.Grocery
{
    public class Detail
    {
        public class Query : IRequest<Model>
        {
            public int Id { get; set; }
        }

        public class Model
        {
            public int Id { get; set; }
            public string Name { get; set; }
            public string CategoryName { get; set; }
            public string CategoryId { get; set; }
            public DateTime? LastTimeBoughtUtc { get; set; }
        }

        public class ModelMappingProfile : Profile
        {
            public ModelMappingProfile()
            {
                CreateMap<Data.Grocery, Model>()
                    .ForMember(x => x.LastTimeBoughtUtc, opt => opt.MapFrom(src =>
                        src.GroceryListItems
                            .Where(i => i.Checked)
                            .OrderByDescending(i => i.CheckedDatedUtc)
                            .Select(i => i.CheckedDatedUtc)
                            .FirstOrDefault()));
            }
        }

        public class QueryHandler : IRequestHandler<Query, Model>
        {
            private readonly EggsDbContext _dbContext;

            public QueryHandler(EggsDbContext dbContext)
            {
                _dbContext = dbContext;
            }

            public async Task<Model> Handle(Query request, CancellationToken cancellationToken)
            {
                var grocery = await  _dbContext.Groceries
                    .Where(x => x.Id == request.Id)
                   .ProjectToFirstOrDefaultAsync<Model>();

                if(grocery == null)
                {
                    throw new EggsDataException("Grocery not found");
                }

                return grocery;
            }
        }
        
    }
}
