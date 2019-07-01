using Eat2LoseWeight.Views;
using Xamarin.Forms;

namespace Eat2LoseWeight
{
    public partial class AppShell : Shell
    {
        public AppShell()
        {
            InitializeComponent();
            RegisterRoutes();
        }

        private void RegisterRoutes()
        {
            Routing.RegisterRoute(nameof(AddFoodPage), typeof(AddFoodPage));
            Routing.RegisterRoute(nameof(AddWeightPage), typeof(AddWeightPage));
            Routing.RegisterRoute(nameof(MainPage), typeof(MainPage));
            Routing.RegisterRoute(nameof(MorePage), typeof(MorePage));
            Routing.RegisterRoute(nameof(SummaryPage), typeof(SummaryPage));
            Routing.RegisterRoute(nameof(TodayPage), typeof(TodayPage));
            Routing.RegisterRoute(nameof(FoodHistoryPage), typeof(FoodHistoryPage));
            Routing.RegisterRoute(nameof(WeightHistoryPage), typeof(WeightHistoryPage));
        }
    }
}