using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DontForgetTheEggs.Core.Commands;
using DontForgetTheEggs.Model;
using ShortBus;

namespace DontForgetTheEggs.Data.CommandHandlers
{
    public class RenameCategoryHandler : IAsyncRequestHandler<RenameCategory, UnitType>
    {
        private readonly EggsContext _context;

        public RenameCategoryHandler(EggsContext context)
        {
            _context = context;
        }

        public async Task<UnitType> HandleAsync(RenameCategory request)
        {
            var category = await _context.Categories.FindAsync(request.Id);
            category.Name = request.Name;
            await _context.SaveChangesAsync();
            return UnitType.Default;
        }
    }
}
