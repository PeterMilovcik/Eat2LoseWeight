using Xamarin.Forms;

namespace Eat2LoseWeight
{
    public partial class MainPage : ContentPage
    {
        private MainViewModel ViewModel { get; }

        public MainPage()
        {
            InitializeComponent();
            ViewModel = new MainViewModel(Navigation);
            BindingContext = ViewModel;
        }

        protected override async void OnAppearing()
        {
            base.OnAppearing();
            await ViewModel.CheckInitialWeightAsync();
        }
    }
}
