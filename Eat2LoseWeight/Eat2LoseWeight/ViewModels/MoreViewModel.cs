using Eat2LoseWeight.Views;
using Xamarin.Forms;

namespace Eat2LoseWeight.ViewModels
{
    public class MoreViewModel
    {
        private INavigation Navigation { get; }

        public MoreViewModel(INavigation navigation)
        {
            Navigation = navigation;
            WeightHistoryCommand = new Command(
                async () => await Navigation.PushAsync(new WeightHistoryPage()));
        }

        public Command WeightHistoryCommand { get; }
    }
}