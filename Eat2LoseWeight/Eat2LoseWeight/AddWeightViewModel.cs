using System.Threading.Tasks;
using Xamarin.Forms;

namespace Eat2LoseWeight
{
    public class AddWeightViewModel : ViewModel
    {
        private double myWeight;
        private INavigation Navigation { get; }

        public AddWeightViewModel(INavigation navigation)
        {
            Navigation = navigation;
            SubmitCommand = new Command(async () => await SubmitAsync());
        }

        public double Weight
        {
            get => myWeight;
            set
            {
                if (value.Equals(myWeight)) return;
                myWeight = value;
                OnPropertyChanged();
            }
        }

        public Command SubmitCommand { get; }

        private async Task SubmitAsync()
        {
            if (CanSubmit())
            {
                SaveToDatabase(Weight);
                await Navigation.PopAsync();
            }
        }

        private void SaveToDatabase(double weight)
        {
            // Coming soon...
        }

        private bool CanSubmit() => Weight > 0;
    }
}
