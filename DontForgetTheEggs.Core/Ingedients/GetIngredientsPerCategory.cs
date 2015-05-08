using System.Collections.Generic;
using DontForgetTheEggs.Core.Ingedients.ViewModels;
using ShortBus;

namespace DontForgetTheEggs.Core.Ingedients
{
    public class GetIngredientsPerCategory : IAsyncRequest<IEnumerable<IngredientsCategoryViewModel>>
    {
    }
}
