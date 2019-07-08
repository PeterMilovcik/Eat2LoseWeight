using Eat2LoseWeight.ViewModels;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace Eat2LoseWeight.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class GainWeightFoodPage : ContentPage
    {
        private WeightFoodViewModel ViewModel { get; }

        public GainWeightFoodPage()
        {
            InitializeComponent();
            ViewModel = new GainWeightFoodViewModel();
            BindingContext = ViewModel;
        }

        protected override async void OnAppearing()
        {
            base.OnAppearing();
            await ViewModel.LoadAsync();
        }
    }
}
