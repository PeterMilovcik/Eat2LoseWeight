﻿using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace Eat2LoseWeight
{
    public class AddItemViewModel : ViewModel
    {
        private string mySearchText;
        private DateTime myDate;
        private TimeSpan myTime;
        private ObservableCollection<Item> myDisplayedItems;
        private List<Item> AllItems { get; set; }

        public AddItemViewModel()
        {
            AddItemCommand = new Command(async () => await AddItemAsync());
        }

        public ObservableCollection<Item> DisplayedItems
        {
            get => myDisplayedItems;
            private set
            {
                if (Equals(value, myDisplayedItems)) return;
                myDisplayedItems = value;
                OnPropertyChanged();
            }
        }

        public async Task LoadAsync()
        {
            AllItems = await App.Database.GetItemsAsync();
            var now = DateTime.Now;
            Date = new DateTime(now.Year, now.Month, now.Day);
            Time = new TimeSpan(now.Hour, now.Minute, now.Second);
            DisplayedItems = new ObservableCollection<Item>(AllItems);
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
                if (value.Equals(myDate)) return;
                myDate = value;
                OnPropertyChanged();
            }
        }

        public TimeSpan Time
        {
            get => myTime;
            set
            {
                if (value.Equals(myTime)) return;
                myTime = value;
                OnPropertyChanged();
            }
        }

        public Command AddItemCommand { get; }

        private async Task AddItemAsync()
        {
            if (string.IsNullOrWhiteSpace(SearchText)) return;
            if (AllItems.All(i => i.Name != SearchText))
            {
                await App.Database.SaveItemAsync(new Item { Name = SearchText });
                await LoadAsync();
            }

            var item = AllItems.Single(i => i.Name == SearchText);
            var itemRecord = new ItemRecord
            {
                ItemId = item.Id,
                At = Date.Add(Time)
            };
            await App.Database.SaveItemRecordAsync(itemRecord);
            SearchText = null;
        }

        public void Search()
        {
            DisplayedItems = string.IsNullOrWhiteSpace(SearchText)
                ? new ObservableCollection<Item>(AllItems)
                : new ObservableCollection<Item>(AllItems.Where(i => i.Name.Contains(SearchText)));
        }
    }
}
