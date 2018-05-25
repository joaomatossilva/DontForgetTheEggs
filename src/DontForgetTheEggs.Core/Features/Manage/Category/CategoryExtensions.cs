using System.Data.Entity;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using DontForgetTheEggs.Core.Common.Exceptions;
using DontForgetTheEggs.Core.Data;

namespace DontForgetTheEggs.Core.Features.Manage.Category
{
    public static class CategoryExtensions
    {
        public static async Task<Data.Category> FindOrCreateCategory(this IDbSet<Data.Category> categories, int? id, string name, CancellationToken cancellationToken)
        {
            Data.Category category;

            //find by id
            if (id != null)
            {
                category = await categories
                    .FirstOrDefaultAsync(x => x.Id == id, cancellationToken)
                    .ConfigureAwait(false);

                if (category == null)
                {
                    throw new EggsDataException("Unable to get the category");
                }

                return category;
            }

            //try to find by name first before creating one
            category = await categories
                    .FirstOrDefaultAsync(x => x.Name == name, cancellationToken)
                    .ConfigureAwait(false);

            if (category == null)
            {
                category = new Data.Category
                {
                    Name = name
                };
                categories.Add(category);
            }

            return category;
        }

        public static async Task<bool> HasGroceries(this IDbSet<Data.Category> categories, int id, CancellationToken cancellationToken = default(CancellationToken))
        {
            var hasCategories = await categories.AnyAsync(x => x.Id == id && x.Groceries.Any(), cancellationToken)
                .ConfigureAwait(false);
            return hasCategories;
        }
    }
}
