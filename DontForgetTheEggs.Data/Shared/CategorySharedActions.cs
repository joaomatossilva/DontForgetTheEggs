using DontForgetTheEggs.Model;

namespace DontForgetTheEggs.Data.Shared
{
    internal static class CategorySharedActions
    {
        internal static Category CreateCategory(EggsContext context, string name)
        {
            return context.Categories.Add(new Category {Name = name});
        }

        internal static Category GetCategory(EggsContext context, int id)
        {
            return context.Categories.Find(id);
        }
    }
}
