using Eat2LoseWeight.ViewModels;
using Xamarin.Forms;

namespace Eat2LoseWeight.Views
{
    public partial class MainPage : ContentPage
    {
        private MainViewModel ViewModel { get; }

        public MainPage()
        {
            InitializeComponent();
            ViewModel = new MainViewModel();
            BindingContext = ViewModel;
        }

        protected override async void OnAppearing()
        {
            base.OnAppearing();
            // await ViewModel.CheckInitialWeightAsync();
            await ViewModel.LoadAsync();
        }
    }
}
