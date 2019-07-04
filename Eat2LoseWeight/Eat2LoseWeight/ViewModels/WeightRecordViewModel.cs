using Eat2LoseWeight.DataAccess.Entities;
using System;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace Eat2LoseWeight.ViewModels
{
    public class WeightRecordViewModel : ViewModel
    {
        private string myWeight;
        private DateTime myDate;
        private TimeSpan myTime;
        protected WeightRecord WeightRecord { get; set; }

        public WeightRecordViewModel()
        {
            var now = DateTime.Now;
            Date = now.Date;
            Time = now.TimeOfDay;
            Weight = null;
            SubmitCommand = new Command(async () => await SubmitAsync());
        }

        public WeightRecordViewModel(WeightRecord weightRecord)
        {
            WeightRecord = weightRecord;
            Date = WeightRecord.MeasuredAt.Date;
            Time = WeightRecord.MeasuredAt.TimeOfDay;
            Weight = WeightRecord.Value.ToString("F1");
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

        public DateTime Date
        {
            get => myDate;
            set
            {
                if (Equals(myDate, value)) return;
                myDate = value;
                OnPropertyChanged();
            }
        }

        public TimeSpan Time
        {
            get => myTime;
            set
            {
                if (Equals(myTime, value)) return;
                myTime = value;
                OnPropertyChanged();
            }
        }

        public Command SubmitCommand { get; }

        private async Task SubmitAsync()
        {
            if (CanSubmit())
            {
                await SaveToDatabaseAsync();
                await Shell.Current.Navigation.PopAsync();
            }
        }

        private async Task SaveToDatabaseAsync()
        {
            bool isNew = false;
            if (WeightRecord == null)
            {
                WeightRecord = new WeightRecord();
                isNew = true;
            }
            WeightRecord.Value = double.Parse(Weight);
            WeightRecord.MeasuredAt = Date.Add(Time);
            if (isNew)
            {
                await App.Database.InsertWeightRecordAsync(WeightRecord);
            }
            else
            {
                await App.Database.UpdateWeightRecordAsync(WeightRecord);
            }
        }

        private bool CanSubmit() => double.TryParse(Weight, out var value) && value > 0;
    }
}