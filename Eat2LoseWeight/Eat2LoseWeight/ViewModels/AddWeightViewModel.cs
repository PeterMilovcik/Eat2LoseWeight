using Eat2LoseWeight.DataAccess.Entities;
using System;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace Eat2LoseWeight.ViewModels
{
    public class AddWeightViewModel : ViewModel
    {
        private string myWeight;

        public AddWeightViewModel()
        {
            SubmitCommand = new Command(async () => await SubmitAsync());
        }

        public string Weight
        {
            get => myWeight;
            set
            {
                if (Equals(myWeight, value)) return;
                myWeight = value;
                OnPropertyChanged();
            }
        }

        public Command SubmitCommand { get; }

        private async Task SubmitAsync()
        {
            if (CanSubmit())
            {
                await SaveToDatabaseAsync(double.Parse(Weight));
                await Shell.Current.Navigation.PopAsync();
            }
        }

        private async Task SaveToDatabaseAsync(double weight) =>
            await App.Database.SaveWeightRecordAsync(
                new WeightRecord
                {
                    Value = weight,
                    MeasuredAt = DateTime.Now
                });

        private bool CanSubmit() => double.TryParse(Weight, out var value) && value > 0;
    }
}
