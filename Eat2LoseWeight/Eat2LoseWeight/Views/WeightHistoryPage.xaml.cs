using Eat2LoseWeight.ViewModels;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace Eat2LoseWeight.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class WeightHistoryPage : ContentPage
    {
        private WeightHistoryViewModel ViewModel { get; }

        public WeightHistoryPage()
        {
            InitializeComponent();
            ViewModel = new WeightHistoryViewModel();
            BindingContext = ViewModel;
        }

        protected override async void OnAppearing()
        {
            base.OnAppearing();
            await ViewModel.LoadAsync();
        }
    }
}