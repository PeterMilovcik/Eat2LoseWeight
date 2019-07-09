using System;
using System.Collections.ObjectModel;
using System.Linq;
using System.Threading.Tasks;
using Eat2LoseWeight.DataAccess.Entities;
using Eat2LoseWeight.Dialogs;
using Xamarin.Forms;

namespace Eat2LoseWeight.ViewModels
{
    public class EditFoodHistoryViewModel : ViewModel
    {
        private IConfirmationDialog ConfirmationDialog { get; }
        private Model mySelectedItem;

        public EditFoodHistoryViewModel(
            IConfirmationDialog confirmationDialog)
        {
            ConfirmationDialog = confirmationDialog;
            SelectionChangedCommand = new Command(
                async () => await SelectionChangedAsync());
            SubmitCommand = new Command(
                async () => await Shell.Current.Navigation.PopAsync(false));
            Items = new ObservableCollection<Model>();
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

        public ObservableCollection<Model> Items { get; }

        public async Task LoadAsync()
        {
            try
            {
                var items = await App.Database.GetItemsAsync();
                var itemRecords = await App.Database.GetItemRecordsAsync();
                var orderedItemRecords = itemRecords.OrderByDescending(r => r.At);
                var models = orderedItemRecords.Select(
                    foodRecord =>
                    {
                        var item = items.Single(i => i.Id == foodRecord.ItemId);
                        return new Model(foodRecord, item, Items, ConfirmationDialog);
                    }).ToList();
                Items.Clear();
                models.ForEach(model => Items.Add(model));
            }
            catch (Exception exception)
            {
                Console.WriteLine(exception);
            }
        }

        private async Task SelectionChangedAsync()
        {
            if (SelectedItem != null)
            {
                // TODO: Create EditFoodRecordPage & FoodRecordViewModel
                //await Shell.Current.Navigation.PushAsync(
                //    new EditFoodRecordPage(
                //        new FoodRecordViewModel(
                //            SelectedItem.FoodRecord)));
                SelectedItem = null;
            }
        }

        public class Model
        {
            private ObservableCollection<Model> Models { get; }
            private IConfirmationDialog ConfirmationDialog { get; }

            public Model(
                ItemRecord foodRecord,
                Item food,
                ObservableCollection<Model> models,
                IConfirmationDialog confirmationDialog)
            {
                FoodRecord = foodRecord;
                Food = food;
                Models = models;
                ConfirmationDialog = confirmationDialog;
                DeleteCommand = new Command(async () => await DeleteAsync());
            }

            public ItemRecord FoodRecord { get; }
            public Item Food { get; }

            public Command DeleteCommand { get; }

            private async Task DeleteAsync()
            {
                if (await ConfirmationDialog.ShowDialog())
                {
                    await App.Database.RemoveFoodRecordAsync(FoodRecord);
                    Models.Remove(this);
                }
            }
        }
    }
}
