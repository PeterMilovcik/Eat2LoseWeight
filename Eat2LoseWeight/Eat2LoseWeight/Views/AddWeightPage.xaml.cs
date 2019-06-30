using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace Eat2LoseWeight
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class AddWeightPage : ContentPage
    {
        public AddWeightPage()
        {
            InitializeComponent();
            BindingContext = new AddWeightViewModel(Navigation);
        }
    }
}