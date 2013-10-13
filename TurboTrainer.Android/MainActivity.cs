using Android.App;
using Android.OS;

namespace TurboTrainer
{
	[Activity (Label = "TurboTrainer", MainLauncher = true)]
	public class MainActivity : Activity
	{
		protected override void OnCreate (Bundle bundle)
		{
			base.OnCreate (bundle);
			SetContentView (Resource.Layout.Main);
		}
	}
}


