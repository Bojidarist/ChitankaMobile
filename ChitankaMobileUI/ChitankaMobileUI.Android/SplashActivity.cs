using Android.App;
using Android.Content;
using Android.OS;
using System.Threading;
using System.Threading.Tasks;

namespace ChitankaMobileUI.Droid
{
    [Activity(Theme = "@style/Theme.Splash", MainLauncher = true, NoHistory = true)]
    public class SplashActivity : Activity
    {
        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);
            SetContentView(Resource.Layout.splash_screen);
            Task.Run(() =>
            {
                Thread.Sleep(2000);
                StartActivity(new Intent(Application.Context, typeof(MainActivity)));
            });
        }
    }
}