using Eat2LoseWeight.DataAccess.Entities;
using Eat2LoseWeight.Views;
using System;
using System.Collections.ObjectModel;
using System.Linq;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace Eat2LoseWeight.ViewModels
{
    public class WeightHistoryViewModel : ViewModel
    {
        private ObservableCollection<WeightRecord> myItems;

        public WeightHistoryViewModel()
        {
            EditModeCommand = new Command(
                async () => await Shell.Current.GoToAsync(
                    nameof(EditWeightHistoryPage), false));
        }

        public Command EditModeCommand { get; }

        public ObservableCollection<WeightRecord> Items
        {
            get => myItems;
            private set
            {
                if (Equals(myItems, value)) return;
                myItems = value;
                OnPropertyChanged();
            }
        }

        public async Task LoadAsync()
        {
            try
            {
                var weightRecords = await App.Database.GetWeightRecordsAsync();
                var orderedWeightRecords = weightRecords.OrderBy(r => r.MeasuredAt);
                Items = new ObservableCollection<WeightRecord>(orderedWeightRecords);
            }
            catch (Exception exception)
            {
                Console.WriteLine(exception);
            }
        }
    }
}