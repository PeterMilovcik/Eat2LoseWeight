using Eat2LoseWeight.ViewModels;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace Eat2LoseWeight.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class AddFoodPage : ContentPage
    {
        private AddItemViewModel ViewModel { get; }

        public AddFoodPage()
        {
            InitializeComponent();
            ViewModel = new AddItemViewModel();
            BindingContext = ViewModel;
        }

        protected override async void OnAppearing()
        {
            base.OnAppearing();
            ViewModel.InitializeControls();
            await ViewModel.LoadAsync();
        }

        private void SearchBar_OnTextChanged(object sender, TextChangedEventArgs e) =>
            ViewModel.Search();
    }
}