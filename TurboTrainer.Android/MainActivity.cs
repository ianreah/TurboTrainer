using Android.App;
using Android.OS;
using TurboTrainer.Core;
using ReactiveUI;
using Android.Widget;
using ReactiveUI.Android;

namespace TurboTrainer
{
	[Activity (Label = "TurboTrainer", MainLauncher = true)]
	public class MainActivity : ReactiveActivity, IViewFor<MainViewModel>
	{
		protected override void OnCreate (Bundle bundle)
		{
			base.OnCreate (bundle);
			SetContentView (Resource.Layout.Main);

			var resolver = (IMutableDependencyResolver)RxApp.DependencyResolver;
			resolver.Register(() => new CreatesAndroidButtonCommandBinding(), typeof(ICreatesCommandBinding));

			ViewModel = new MainViewModel(RxApp.TaskpoolScheduler, new FileChooser(Assets));

			GradientText = FindViewById<TextView>(Resource.Id.gradientText);
			this.OneWayBind(ViewModel, vm => vm.CurrentPoint.Elevation, v => v.GradientText.Text);

			LoadGpxButton = FindViewById<Button>(Resource.Id.loadGpxButton);
			this.BindCommand(ViewModel, vm => vm.LoadGpxDataCommand, v => v.LoadGpxButton);
		}

		public MainViewModel ViewModel { get; set; }

		object IViewFor.ViewModel
		{
			get { return ViewModel; }
			set { ViewModel = (MainViewModel)value; }
		}
		
		public TextView GradientText { get; private set; }
		public Button LoadGpxButton { get; private set; }
	}
}


