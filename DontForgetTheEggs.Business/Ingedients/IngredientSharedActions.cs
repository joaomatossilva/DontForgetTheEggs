using DontForgetTheEggs.Model;

namespace DontForgetTheEggs.Business.Ingedients
{
    internal static class IngredientSharedActions
    {
        internal static Ingredient CreateIngredient(EggsContext context, string name, Category category)
        {
            var newIngredient = new Ingredient
                                {
                                    Name = name,
                                    Category = category
                                };
            context.Ingredients.Add(newIngredient);
            return newIngredient;
        }
    }
}
