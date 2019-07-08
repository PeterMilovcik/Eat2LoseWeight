using Eat2LoseWeight.DataAccess.Entities;
using Eat2LoseWeight.Models;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Threading.Tasks;

namespace Eat2LoseWeight.ViewModels
{
    public abstract class WeightFoodViewModel : ViewModel
    {
        private ObservableCollection<MealItemViewModel> myItems;
        private IWeightChangeDistributionStrategy DistributionStrategy { get; }

        protected WeightFoodViewModel()
        {
            DistributionStrategy = new UniformWeightChangeDistributionStrategy();
        }

        protected abstract void SortItems();

        public async Task LoadAsync()
        {
            try
            {
                var weightRecords = await App.Database.GetWeightRecordsAsync();
                var count = weightRecords.Count;
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
                    Items = GetMealItemViewModels(distribution, items);
                    SortItems();
                }
            }
            catch (Exception exception)
            {
                Console.WriteLine(exception);
            }
        }

        protected virtual ObservableCollection<MealItemViewModel> GetMealItemViewModels(
            WeightChangeDistribution distribution,
            List<Item> items) =>
            new ObservableCollection<MealItemViewModel>(
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

        public ObservableCollection<MealItemViewModel> Items
        {
            get => myItems;
            protected set
            {
                if (Equals(value, myItems)) return;
                myItems = value;
                OnPropertyChanged();
            }
        }
    }
}
