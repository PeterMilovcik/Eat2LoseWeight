using Eat2LoseWeight.DataAccess.Entities;
using Eat2LoseWeight.Dialogs;
using Eat2LoseWeight.Views;
using System;
using System.Collections.ObjectModel;
using System.Linq;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace Eat2LoseWeight.ViewModels
{
    public class EditWeightHistoryViewModel : ViewModel
    {
        private IConfirmationDialog ConfirmationDialog { get; }
        private Model mySelectedItem;

        public EditWeightHistoryViewModel(
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
                var weightRecords = await App.Database.GetWeightRecordsAsync();
                var orderedWeightRecords = weightRecords.OrderBy(r => r.MeasuredAt);
                var models = orderedWeightRecords.Select(
                    r => new Model(r, Items, ConfirmationDialog)).ToList();
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
                await Shell.Current.Navigation.PushAsync(
                    new EditWeightRecordPage(
                        new WeightRecordViewModel(
                            SelectedItem.WeightRecord)));
                SelectedItem = null;
            }
        }

        public class Model
        {
            private ObservableCollection<Model> Models { get; }
            private IConfirmationDialog ConfirmationDialog { get; }

            public Model(
                WeightRecord weightRecord,
                ObservableCollection<Model> models,
                IConfirmationDialog confirmationDialog)
            {
                WeightRecord = weightRecord;
                Models = models;
                ConfirmationDialog = confirmationDialog;
                DeleteCommand = new Command(async () => await DeleteAsync());
            }

            public WeightRecord WeightRecord { get; }

            public Command DeleteCommand { get; }

            private async Task DeleteAsync()
            {
                if (await ConfirmationDialog.ShowDialog())
                {
                    await App.Database.RemoveWeightAsync(WeightRecord);
                    Models.Remove(this);
                }
            }
        }
    }
}
