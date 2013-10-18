using ReactiveUI;
using System.Reactive.Concurrency;
using System.Reactive.Linq;

namespace TurboTrainer.Core
{
    public class MainViewModel : ReactiveObject
    {
		private readonly ObservableAsPropertyHelper<GpxPoint> currentPoint;
        private readonly ReactiveCommand loadGpxDataCommand = new ReactiveCommand();

        public MainViewModel(IScheduler backgroundScheduler, IFileChooserUi fileChooser)
        {
            var gpxReplays = loadGpxDataCommand.RegisterAsyncFunction(_ =>
	                               {
									   using (var stream = fileChooser.ChooseFile())
									   {
										   var reader = new GpxReader(stream);
										   return reader.Points.Replay(backgroundScheduler);
									   }
								   });

            currentPoint = gpxReplays.Switch().ToProperty(this, x => x.CurrentPoint);
        }

        public GpxPoint CurrentPoint { get { return currentPoint.Value; } }
        public ReactiveCommand LoadGpxDataCommand { get { return loadGpxDataCommand; } }
    }
}
