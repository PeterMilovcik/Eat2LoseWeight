using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace Eat2LoseWeight
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
            await ViewModel.LoadAsync();
        }
    }
}