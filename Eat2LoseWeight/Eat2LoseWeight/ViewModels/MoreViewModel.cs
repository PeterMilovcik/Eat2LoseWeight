using Eat2LoseWeight.Views;
using Xamarin.Forms;

namespace Eat2LoseWeight.ViewModels
{
    public class MoreViewModel
    {
        public MoreViewModel(INavigation navigation)
        {
            WeightHistoryCommand = new Command(
                async () => await navigation.PushAsync(new WeightHistoryPage()));
            FoodHistoryCommand = new Command(
                async () => await navigation.PushAsync(new FoodHistoryPage()));
        }

        public Command WeightHistoryCommand { get; }
        public Command FoodHistoryCommand { get; }
    }
}