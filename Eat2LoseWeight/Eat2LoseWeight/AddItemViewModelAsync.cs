using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace Eat2LoseWeight
{
    public class AddItemViewModel : ViewModel
    {
        private string mySearchText;
        private List<Item> AllItems { get; set; }

        public AddItemViewModel()
        {
            AddItemCommand = new Command(async () => await AddItemAsync());
        }

        public async Task LoadAsync() => AllItems = await App.Database.GetItemsAsync();

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

        public Command AddItemCommand { get; }

        private async Task AddItemAsync()
        {
            if (string.IsNullOrWhiteSpace(SearchText)) return;
            if (AllItems.All(i => i.Name != SearchText))
            {
                await App.Database.SaveItemAsync(new Item { Name = SearchText });
                await LoadAsync();
            }
        }
    }
}
