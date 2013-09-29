using ReactiveUI;
using System.Reactive.Concurrency;
using System;

namespace TurboTrainer.Core
{
    public class MainViewModel : ReactiveObject
    {
        private GpxPoint currentPoint;
        private readonly ReactiveCommand loadGpxDataCommand = new ReactiveCommand();

        public MainViewModel(IScheduler scheduler, IFileChooserUi fileChooser)
        {
            loadGpxDataCommand.RegisterAsyncAction(_ =>
	                              {
                                      using (var stream = fileChooser.ChooseFile())
									  {
										  var reader = new GpxReader(stream);
                                          reader.Points.Replay(scheduler).Subscribe(x => CurrentPoint = x);
									  }
	                              });
        }

        public GpxPoint CurrentPoint
        {
            get { return currentPoint; }
            private set { this.RaiseAndSetIfChanged(ref currentPoint, value); }
        }

        public ReactiveCommand LoadGpxDataCommand { get { return loadGpxDataCommand; } }
    }
}
