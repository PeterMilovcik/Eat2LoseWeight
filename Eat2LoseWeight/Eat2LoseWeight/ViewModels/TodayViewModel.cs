﻿using Eat2LoseWeight.Views;
using System;
using System.Collections.ObjectModel;
using System.Linq;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace Eat2LoseWeight.ViewModels
{
    public class TodayViewModel : ViewModel
    {
        private bool CanSortAscending { get; set; }

        private ObservableCollection<Model> myItems;
        private string myTitle;

        public TodayViewModel()
        {
            ToggleSortCommand = new Command(ToggleSort);
            AddWeightCommand = new Command(async () => await AddWeight());
            AddFoodCommand = new Command(async () => await AddFood());
            Title = "Today";
        }

        public string Title
        {
            get => myTitle;
            set
            {
                if (Equals(myTitle, value)) return;
                myTitle = value;
                OnPropertyChanged();
            }
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
            try
            {
                var weightRecords = await App.Database.GetWeightRecordsAsync();
                var weightRecord = weightRecords.LastOrDefault(r => r.MeasuredAt.Date == DateTime.Now.Date);
                if (weightRecord != null)
                {
                    Title = $"Today {weightRecord.Value}";
                }

                var itemRecords = await App.Database.GetItemRecordsAsync();
                var todayRecords = itemRecords.Where(ir => ir.At.Date == DateTime.Now.Date).ToList();
                var items = await App.Database.GetItemsAsync();
                var models = todayRecords.Select(
                    record =>
                    {
                        var name = items.Single(item => item.Id == record.ItemId).Name;
                        return new Model(record.Id, name, record.At);
                    }).OrderBy(m => m.At);
                Items = new ObservableCollection<Model>(models);
            }
            catch (Exception exception)
            {
                Console.WriteLine(exception);
            }
        }

        public Command AddWeightCommand { get; }

        public Command AddFoodCommand { get; }

        private async Task AddWeight() =>
            await Shell.Current.Navigation.PushAsync(
                new AddWeightPage(
                    new WeightRecordViewModel()));

        private async Task AddFood() => await Shell.Current.GoToAsync(nameof(AddFoodPage));

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