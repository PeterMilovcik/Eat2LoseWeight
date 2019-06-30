using System;
using System.Threading.Tasks;
using Eat2LoseWeight.DataAccess.Entities;
using Xamarin.Forms;

namespace Eat2LoseWeight.ViewModels
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
                await SaveToDatabaseAsync(Weight);
                await Navigation.PopAsync();
            }
        }

        private async Task SaveToDatabaseAsync(double weight) =>
            await App.Database.SaveWeightRecordAsync(
                new WeightRecord
                {
                    Value = weight,
                    MeasuredAt = DateTime.Now
                });

        private bool CanSubmit() => Weight > 0;
    }
}
