using Android.App;
using Android.Content.PM;
using Android.OS;
using Xamarin.Forms;

namespace Eat2LoseWeight.Droid
{
    [Activity(Label = "Eat 2 Lose Weight", Icon = "@mipmap/icon", Theme = "@style/MainTheme", MainLauncher = true, ConfigurationChanges = ConfigChanges.ScreenSize | ConfigChanges.Orientation)]
    public class MainActivity : global::Xamarin.Forms.Platform.Android.FormsAppCompatActivity
    {
        protected override void OnCreate(Bundle savedInstanceState)
        {
            TabLayoutResource = Resource.Layout.Tabbar;
            ToolbarResource = Resource.Layout.Toolbar;

            RequestedOrientation = Device.Idiom == TargetIdiom.Phone
                ? ScreenOrientation.Portrait
                : ScreenOrientation.SensorLandscape;

            base.OnCreate(savedInstanceState);
            global::Xamarin.Forms.Forms.SetFlags("CollectionView_Experimental");
            global::Xamarin.Forms.Forms.Init(this, savedInstanceState);
            LoadApplication(new App());
        }
    }
}