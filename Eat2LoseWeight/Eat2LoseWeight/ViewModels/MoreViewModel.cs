using Eat2LoseWeight.Views;
using Xamarin.Forms;

namespace Eat2LoseWeight.ViewModels
{
    public class MoreViewModel
    {
        public MoreViewModel()
        {
            WeightHistoryCommand = new Command(
                async () => await Shell.Current.GoToAsync(nameof(WeightHistoryPage)));
            FoodHistoryCommand = new Command(
                async () => await Shell.Current.GoToAsync(nameof(FoodHistoryPage)));
        }

        public Command WeightHistoryCommand { get; }
        public Command FoodHistoryCommand { get; }
    }
}