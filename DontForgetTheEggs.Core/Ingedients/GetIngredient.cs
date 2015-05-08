using DontForgetTheEggs.Core.Ingedients.ViewModels;
using ShortBus;

namespace DontForgetTheEggs.Core.Ingedients
{
    public class GetIngredient : IAsyncRequest<IngredientViewModel>
    {
        public int Id { get; set; }
    }
}
