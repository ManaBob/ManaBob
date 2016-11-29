using System;

using Android.App;
using Android.Content.PM;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using Android.OS;

using ManaBob;
using ManaBob.Services;

namespace ManaBob.Droid
{
    [Activity(Label = "ManaBob", Icon = "@drawable/icon", MainLauncher = true, ConfigurationChanges = ConfigChanges.ScreenSize | ConfigChanges.Orientation)]
    public class MainActivity : global::Xamarin.Forms.Platform.Android.FormsApplicationActivity
    {
        protected override void OnCreate(Bundle bundle)
        {
            base.OnCreate(bundle);

            global::Xamarin.Forms.Forms.Init(this, bundle);

            var net = new ManaBob.Services.FakeNet();
            var Core = new ManaBob.AppCore(net);
            LoadApplication(Core);
        }
    }
}

