using Eat2LoseWeight.ViewModels;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace Eat2LoseWeight.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class FoodHistoryPage : ContentPage
    {
        private FoodHistoryViewModel ViewModel { get; }

        public FoodHistoryPage()
        {
            InitializeComponent();
            ViewModel = new FoodHistoryViewModel();
            BindingContext = ViewModel;
        }

        protected override async void OnAppearing()
        {
            base.OnAppearing();
            await ViewModel.LoadAsync();
        }
    }
}