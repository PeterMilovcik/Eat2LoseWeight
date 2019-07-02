using Eat2LoseWeight.ViewModels;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace Eat2LoseWeight.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class EditWeightRecordPage : ContentPage
    {
        public EditWeightRecordPage(WeightRecordViewModel viewModel)
        {
            InitializeComponent();
            BindingContext = viewModel;
        }
    }
}