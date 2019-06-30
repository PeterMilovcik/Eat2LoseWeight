using Eat2LoseWeight.ViewModels;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace Eat2LoseWeight.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class MorePage : ContentPage
    {
        private MoreViewModel ViewModel { get; }

        public MorePage()
        {
            InitializeComponent();
            ViewModel = new MoreViewModel(Navigation);
            BindingContext = ViewModel;
        }
    }
}