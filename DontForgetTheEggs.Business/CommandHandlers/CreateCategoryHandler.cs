using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DontForgetTheEggs.Core.Commands;
using DontForgetTheEggs.Business.Shared;
using DontForgetTheEggs.Model;
using ShortBus;

namespace DontForgetTheEggs.Business.CommandHandlers
{
    public class CreateCategoryHandler : IAsyncRequestHandler<CreateCategory, int>
    {
        private readonly EggsContext _context;

        public CreateCategoryHandler(EggsContext context)
        {
            _context = context;
        }

        public async Task<int> HandleAsync(CreateCategory request)
        {
            var newCategory = CategorySharedActions.CreateCategory(_context, request.Name);
            await _context.SaveChangesAsync();
            return newCategory.Id;
        }
    }
}
