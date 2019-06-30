using System;
using System.Collections.ObjectModel;
using System.Linq;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace Eat2LoseWeight.ViewModels
{
    public class TodayViewModel : ViewModel
    {
        private INavigation Navigation { get; }
        private bool CanSortAscending { get; set; }

        private ObservableCollection<Model> myItems;

        public TodayViewModel(INavigation navigation)
        {
            Navigation = navigation;
            ToggleSortCommand = new Command(ToggleSort);
            CanSortAscending = true;
        }

        private void ToggleSort()
        {
            SortItems();
            CanSortAscending = !CanSortAscending;
        }

        private void SortItems() =>
            Items = CanSortAscending
                ? new ObservableCollection<Model>(Items.OrderBy(i => i.At))
                : new ObservableCollection<Model>(Items.OrderByDescending(i => i.At));

        public Command ToggleSortCommand { get; }


        public ObservableCollection<Model> Items
        {
            get => myItems;
            set
            {
                if (Equals(myItems, value)) return;
                myItems = value;
                OnPropertyChanged();
            }
        }

        public async Task LoadAsync()
        {
            var itemRecords = await App.Database.GetItemRecordsAsync();
            var todayRecords = itemRecords.Where(ir => ir.At.Date == DateTime.Now.Date).ToList();
            var items = await App.Database.GetItemsAsync();
            var models = todayRecords.Select(
                record =>
                {
                    var name = items.Single(item => item.Id == record.ItemId).Name;
                    return new Model(record.Id, name, record.At);
                });
            Items = new ObservableCollection<Model>(models);
            SortItems();
        }

        public class Model
        {
            public Model(int id, string name, DateTime at)
            {
                Id = id;
                Name = name;
                At = at;
                AtFormatted = at.TimeOfDay.ToString(@"hh\:mm");
            }

            public int Id { get; }
            public string Name { get; }
            public string AtFormatted { get; }
            public DateTime At { get; }
        }
    }
}