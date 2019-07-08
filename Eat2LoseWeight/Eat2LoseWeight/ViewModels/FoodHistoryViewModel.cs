using System;
using System.Collections.ObjectModel;
using System.Linq;
using System.Threading.Tasks;

namespace Eat2LoseWeight.ViewModels
{
    public class FoodHistoryViewModel : ViewModel
    {
        private ObservableCollection<Model> myItems;

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
            try
            {
                var itemRecords = await App.Database.GetItemRecordsAsync();
                var orderedItemRecords = itemRecords.OrderBy(r => r.At);
                var items = await App.Database.GetItemsAsync();
                Items = new ObservableCollection<Model>(
                    orderedItemRecords.Select(r =>
                    {
                        var name = items.Single(i => i.Id == r.ItemId).Name;
                        return new Model(r.Id, name, r.At);
                    }));
            }
            catch (Exception exception)
            {
                Console.WriteLine(exception);
            }
        }

        public class Model
        {
            public Model(int id, string name, DateTime at)
            {
                Id = id;
                Name = name;
                At = at;
            }

            public int Id { get; }
            public string Name { get; }
            public DateTime At { get; }
        }
    }
}