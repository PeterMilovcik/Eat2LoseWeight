using Eat2LoseWeight.ViewModels;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace Eat2LoseWeight.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class AddWeightPage : ContentPage
    {
        public AddWeightPage()
        {
            InitializeComponent();
            BindingContext = new AddWeightViewModel();
        }
    }
}