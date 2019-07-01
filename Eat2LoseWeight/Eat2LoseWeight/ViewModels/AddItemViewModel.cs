using Eat2LoseWeight.DataAccess.Entities;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace Eat2LoseWeight.ViewModels
{
    public class AddItemViewModel : ViewModel
    {
        private string mySearchText;
        private DateTime myDate;
        private TimeSpan myTime;
        private ObservableCollection<Model> myDisplayedItems;
        private List<Item> AllItems { get; set; }
        private List<ItemRecord> AllItemRecords { get; set; }
        private Model mySelectedItem;


        public AddItemViewModel()
        {
            AddItemCommand = new Command(async () => await AddItemAsync());
            SelectionChangedCommand = new Command(async () => await SelectionChangedAsync());
        }

        private async Task SelectionChangedAsync()
        {
            if (SelectedItem != null)
            {
                await AddItemAsync(SelectedItem.Name);
            }
        }

        public Command SelectionChangedCommand { get; }


        public Model SelectedItem
        {
            get => mySelectedItem;
            set
            {
                if (Equals(value, mySelectedItem)) return;
                mySelectedItem = value;
                OnPropertyChanged();
            }
        }

        public ObservableCollection<Model> DisplayedItems
        {
            get => myDisplayedItems;
            private set
            {
                if (Equals(value, myDisplayedItems)) return;
                myDisplayedItems = value;
                OnPropertyChanged();
            }
        }

        public void InitializeControls()
        {
            var now = DateTime.Now;
            Date = new DateTime(now.Year, now.Month, now.Day);
            Time = new TimeSpan(now.Hour, now.Minute, now.Second);
        }

        public async Task LoadAsync()
        {
            AllItems = await App.Database.GetItemsAsync();
            AllItemRecords = await App.Database.GetItemRecordsAsync();
            DisplayedItems = ToModels(AllItemRecords);
        }

        public string SearchText
        {
            get => mySearchText;
            set
            {
                if (value == mySearchText) return;
                mySearchText = value;
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

        public Command AddItemCommand { get; }

        private ObservableCollection<Model> ToModels(IEnumerable<ItemRecord> itemRecords) =>
            new ObservableCollection<Model>(
                itemRecords
                    .GroupBy(record => record.ItemId)
                    .OrderByDescending(g => g.Count())
                    .Select(g =>
                    {
                        var itemId = g.Key;
                        var name = AllItems.Single(item => item.Id == itemId).Name;
                        return new Model(itemId, name);
                    }));

        private async Task AddItemAsync(string text)
        {
            if (string.IsNullOrWhiteSpace(text)) return;
            if (AllItems.All(i => i.Name != text))
            {
                await App.Database.SaveItemAsync(new Item { Name = text });
                await LoadAsync();
            }

            var item = AllItems.Single(i => i.Name == text);
            var itemRecord = new ItemRecord
            {
                ItemId = item.Id,
                At = Date.Add(Time)
            };
            await App.Database.SaveItemRecordAsync(itemRecord);
            SearchText = null;
            await Shell.Current.Navigation.PopAsync();
        }

        private async Task AddItemAsync() => await AddItemAsync(SearchText);

        public void Search() =>
            DisplayedItems = string.IsNullOrWhiteSpace(SearchText)
                ? ToModels(AllItemRecords)
                : new ObservableCollection<Model>(ToModels(AllItemRecords).Where(m => m.Name.Contains(SearchText)));

        public class Model
        {
            public Model(int id, string name)
            {
                Id = id;
                Name = name;
            }

            public int Id { get; }
            public string Name { get; }
        }
    }
}
