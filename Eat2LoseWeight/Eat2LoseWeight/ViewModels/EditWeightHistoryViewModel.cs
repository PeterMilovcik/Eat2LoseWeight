using Eat2LoseWeight.DataAccess.Entities;
using System.Collections.ObjectModel;
using System.Linq;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace Eat2LoseWeight.ViewModels
{
    public class EditWeightHistoryViewModel : ViewModel
    {
        private ObservableCollection<Model> myItems;
        private Model mySelectedItem;

        public EditWeightHistoryViewModel()
        {
            SelectionChangedCommand = new Command(async () => await SelectionChangedAsync());
            SubmitCommand = new Command(async () => await Shell.Current.Navigation.PopAsync(false));
        }

        public Command SubmitCommand { get; }

        public Model SelectedItem
        {
            get => mySelectedItem;
            set
            {
                if (Equals(mySelectedItem, value)) return;
                mySelectedItem = value;
                OnPropertyChanged();
            }
        }

        public Command SelectionChangedCommand { get; }

        public ObservableCollection<Model> Items
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
            var models = orderedWeightRecords.Select(r => new Model(r));
            Items = new ObservableCollection<Model>(models);
        }

        private async Task SelectionChangedAsync()
        {

        }

        public class Model
        {
            public Model(WeightRecord weightRecord)
            {
                WeightRecord = weightRecord;
            }

            public WeightRecord WeightRecord { get; }

            public Command RemoveCommand { get; }
        }
    }
}
