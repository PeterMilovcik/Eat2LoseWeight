using Eat2LoseWeight.ViewModels;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace Eat2LoseWeight.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class LoseWeightFoodPage : ContentPage
    {
        private WeightFoodViewModel ViewModel { get; }

        public LoseWeightFoodPage()
        {
            InitializeComponent();
            ViewModel = new LoseWeightFoodViewModel();
            BindingContext = ViewModel;
        }

        protected override async void OnAppearing()
        {
            base.OnAppearing();
            await ViewModel.LoadAsync();
        }
    }
}