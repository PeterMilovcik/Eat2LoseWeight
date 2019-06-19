using System;
using System.Linq;
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
                await UpdateItemRecordsAsync(Weight);
                await SaveToDatabaseAsync(Weight);
                await Navigation.PopAsync();
            }
        }

        private async Task UpdateItemRecordsAsync(double weight)
        {
            var weightRecords = await App.Database.GetWeightRecordsAsync();
            var previousWeightRecord = weightRecords.OrderByDescending(w => w.MeasuredAt).First();
            var delta = weight - previousWeightRecord.Value;
            var itemRecords = await App.Database.GetItemRecordsAsync();
            var itemsToUpdate = itemRecords.Where(i => !i.HasDelta).ToList();
            itemsToUpdate.ForEach(i =>
            {
                i.HasDelta = true;
                i.Delta = delta;
            });
            await App.Database.UpdateItemRecordsAsync(itemsToUpdate);
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
