using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using Eat2LoseWeight.DataAccess.Entities;
using Eat2LoseWeight.Models;

namespace Eat2LoseWeight.ViewModels
{
    public class LoseWeightFoodViewModel : WeightFoodViewModel
    {
        protected override void SortItems() =>
            Items = new ObservableCollection<MealItemViewModel>(Items.OrderBy(i => i.Sum));

        protected override ObservableCollection<MealItemViewModel> GetMealItemViewModels(WeightChangeDistribution distribution, List<Item> items) =>
            new ObservableCollection<MealItemViewModel>(
                base.GetMealItemViewModels(distribution, items).Where(vm => vm.Sum < 0));
    }
}