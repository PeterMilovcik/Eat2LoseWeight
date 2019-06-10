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
        }

        public Command AddWeightCommand { get; }

        private async Task AddWeight() => await Navigation.PushAsync(new AddWeightPage());
    }
}
