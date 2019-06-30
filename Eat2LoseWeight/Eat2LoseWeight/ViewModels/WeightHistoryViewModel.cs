using Eat2LoseWeight.DataAccess.Entities;
using System.Collections.ObjectModel;
using System.Linq;
using System.Threading.Tasks;

namespace Eat2LoseWeight.ViewModels
{
    public class WeightHistoryViewModel : ViewModel
    {
        private ObservableCollection<WeightRecord> myItems;

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
            var weightRecords = await App.Database.GetWeightRecordsAsync();
            var orderedWeightRecords = weightRecords.OrderBy(r => r.MeasuredAt);
            Items = new ObservableCollection<WeightRecord>(orderedWeightRecords);
        }
    }
}