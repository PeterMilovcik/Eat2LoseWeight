using System.Threading.Tasks;
using Xamarin.Forms;

namespace Eat2LoseWeight
{
    public class MainViewModel
    {
        private INavigation Navigation { get; }

        public MainViewModel(INavigation navigation)
        {
            Navigation = navigation;
            AddWeightCommand = new Command(async () => await AddWeight());
            AddFoodCommand = new Command(async () => await AddFood());
        }

        public Command AddWeightCommand { get; }

        public Command AddFoodCommand { get; }

        private async Task AddWeight() => await Navigation.PushAsync(new AddWeightPage());

        private async Task AddFood() => await Navigation.PushAsync(new AddFoodPage());
    }
}
