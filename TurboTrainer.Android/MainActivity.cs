using Android.App;
using Android.OS;
using TurboTrainer.Core;
using ReactiveUI;
using Android.Widget;
using ReactiveUI.Android;
using System.Threading.Tasks;
using System.IO;
using Android.Content;

namespace TurboTrainer
{
	[Activity (Label = "TurboTrainer", MainLauncher = true)]
	public class MainActivity : ReactiveActivity, IViewFor<MainViewModel>, IFileChooserUi
	{
		private TaskCompletionSource<Stream> chooseFileTaskCompletion;

		protected override void OnCreate (Bundle bundle)
		{
			base.OnCreate (bundle);
			SetContentView (Resource.Layout.Main);

			var resolver = (IMutableDependencyResolver)RxApp.DependencyResolver;
			resolver.Register(() => new CreatesAndroidButtonCommandBinding(), typeof(ICreatesCommandBinding));

			ViewModel = new MainViewModel(RxApp.TaskpoolScheduler, this);

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

		public Task<Stream> ChooseFile()
		{
			var intent = new Intent(Intent.ActionGetContent);
			intent.SetType("*/*");
			intent.AddCategory(Intent.CategoryOpenable);

			StartActivityForResult(Intent.CreateChooser(intent, "Select a GPX file"), 0);

			chooseFileTaskCompletion = new TaskCompletionSource<Stream>();
			return chooseFileTaskCompletion.Task;
		}

		protected override void OnActivityResult(int requestCode, Result resultCode, Intent data)
		{
			if (requestCode == 0)
			{
				var stream = (resultCode == Result.Ok) ?
					ContentResolver.OpenInputStream(data.Data) : Stream.Null;

				chooseFileTaskCompletion.SetResult(stream);
			}

			base.OnActivityResult(requestCode, resultCode, data);
		}
	}
}