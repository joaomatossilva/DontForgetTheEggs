using System.Threading.Tasks;
using DontForgetTheEggs.Model;

namespace DontForgetTheEggs.Business.Categories
{
    internal static class CategorySharedActions
    {
        internal static Category CreateCategory(EggsContext context, string name)
        {
            return context.Categories.Add(new Category {Name = name});
        }

        internal static async Task<Category> GetCategoryAsync(EggsContext context, int id)
        {
            return await context.Categories.FindAsync(id);
        }
    }
}
