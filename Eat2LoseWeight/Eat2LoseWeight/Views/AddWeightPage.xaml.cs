using Eat2LoseWeight.ViewModels;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace Eat2LoseWeight.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class AddWeightPage : ContentPage
    {
        private AddWeightViewModel ViewModel { get; }

        public AddWeightPage()
        {
            InitializeComponent();
            ViewModel = new AddWeightViewModel();
            BindingContext = ViewModel;
        }

        protected override void OnAppearing()
        {
            base.OnAppearing();
            ViewModel.Initialize();
        }
    }
}