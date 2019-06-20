using System.Collections.ObjectModel;
using System.Linq;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace Eat2LoseWeight
{
    public class MainViewModel : ViewModel
    {
        private ObservableCollection<Meal> myItems;
        private INavigation Navigation { get; }

        public MainViewModel(INavigation navigation)
        {
            Navigation = navigation;
            AddWeightCommand = new Command(async () => await AddWeight());
            AddFoodCommand = new Command(async () => await AddFood());
            Items = new ObservableCollection<Meal>();
        }

        public ObservableCollection<Meal> Items
        {
            get => myItems;
            set
            {
                if (Equals(value, myItems)) return;
                myItems = value;
                OnPropertyChanged();
            }
        }

        public async Task LoadAsync()
        {
            Items.Clear();
            var items = await App.Database.GetItemsAsync();
            var itemRecords = await App.Database.GetItemRecordsAsync();
            var itemsWithDeltas = itemRecords.Where(i => i.HasDelta);
            var groupedItems = itemsWithDeltas.GroupBy(i => i.ItemId);
            foreach (var groupedItem in groupedItems)
            {
                var item = items.Single(i => i.Id == groupedItem.Key);
                var meal = new Meal
                {
                    Name = item.Name,
                    Delta = groupedItem.Sum(i => i.Delta)
                };
                Items.Add(meal);
            }
            Items = new ObservableCollection<Meal>(Items.OrderByDescending(i => i.Delta));
        }

        public async Task CheckInitialWeightAsync()
        {
            var weightRecords = await App.Database.GetWeightRecordsAsync();
            if (!weightRecords.Any())
            {
                await AddWeight();
            }
        }

        public Command AddWeightCommand { get; }

        public Command AddFoodCommand { get; }

        private async Task AddWeight() => await Navigation.PushAsync(new AddWeightPage());

        private async Task AddFood() => await Navigation.PushAsync(new AddFoodPage());
    }
}
