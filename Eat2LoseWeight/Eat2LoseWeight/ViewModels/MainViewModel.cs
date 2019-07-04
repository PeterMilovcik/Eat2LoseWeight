using Eat2LoseWeight.Models;
using Eat2LoseWeight.Views;
using System.Collections.ObjectModel;
using System.Linq;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace Eat2LoseWeight.ViewModels
{
    public class MainViewModel : ViewModel
    {
        private ObservableCollection<MealItemViewModel> myItems;
        private IWeightChangeDistributionStrategy DistributionStrategy { get; }
        private bool CanSortAscending { get; set; }

        public MainViewModel()
        {
            AddWeightCommand = new Command(async () => await AddWeight());
            AddFoodCommand = new Command(async () => await AddFood());
            ToggleSortCommand = new Command(ToggleSort);
            DistributionStrategy = new UniformWeightChangeDistributionStrategy();
            CanSortAscending = true;
        }

        private void ToggleSort()
        {
            SortItems();
            CanSortAscending = !CanSortAscending;
        }

        private void SortItems() =>
            Items = CanSortAscending
                ? new ObservableCollection<MealItemViewModel>(Items.OrderBy(i => i.Sum))
                : new ObservableCollection<MealItemViewModel>(Items.OrderByDescending(i => i.Sum));

        public Command ToggleSortCommand { get; }

        public async Task LoadAsync()
        {
            var weightRecords = await App.Database.GetWeightRecordsAsync();
            var count = weightRecords.Count();
            if (count > 1)
            {
                weightRecords = weightRecords.OrderBy(wr => wr.MeasuredAt).ToList();
                var itemRecords = await App.Database.GetItemRecordsAsync();
                var distribution = new WeightChangeDistribution();
                for (int i = 0; i < count - 1; i++)
                {
                    var record1 = weightRecords[i];
                    var record2 = weightRecords[i + 1];
                    var weightSpan = new WeightSpan(record1, record2);
                    var spanItemRecords = itemRecords.Where(ir => ir.At > weightSpan.From && ir.At <= weightSpan.To);
                    distribution.Add(DistributionStrategy.Distribute(weightSpan, spanItemRecords.ToList()));
                }
                var items = await App.Database.GetItemsAsync();
                Items = new ObservableCollection<MealItemViewModel>(
                    distribution.Select(pair =>
                            new MealItemViewModel
                            {
                                Id = pair.Key,
                                Name = items.Single(i => i.Id == pair.Key).Name,
                                Count = pair.Value.Count,
                                Average = pair.Value.Average(),
                                Sum = pair.Value.Sum()
                            })
                        .OrderByDescending(i => i.Sum));
            }
        }

        public ObservableCollection<MealItemViewModel> Items
        {
            get => myItems;
            private set
            {
                if (Equals(value, myItems)) return;
                myItems = value;
                OnPropertyChanged();
            }
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

        private async Task AddWeight() => await Shell.Current.GoToAsync(nameof(AddWeightPage));

        private async Task AddFood() => await Shell.Current.GoToAsync(nameof(AddFoodPage));
    }
}
