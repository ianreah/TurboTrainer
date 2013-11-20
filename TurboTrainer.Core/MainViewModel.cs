using System.IO;
using ReactiveUI;
using System.Reactive.Concurrency;
using System.Reactive.Linq;

namespace TurboTrainer.Core
{
    public class MainViewModel : ReactiveObject
    {
        private readonly ObservableAsPropertyHelper<GpxSection> currentSection;
        private readonly ReactiveCommand loadGpxDataCommand = new ReactiveCommand();

        public MainViewModel(IScheduler backgroundScheduler, IFileChooserUi fileChooser)
        {
            var gpxReplays = loadGpxDataCommand.RegisterAsyncFunction(_ =>
                                   {
                                       using (var stream = fileChooser.ChooseFile().Result)
                                       {
                                           // Stream.Null means the user cancelled the file choosing
                                           // TODO: A better way for ChooseFile to indicate user cancelling
                                           if (stream == Stream.Null)
                                           {
                                               return Observable.Empty<GpxSection>();
                                           }

                                           var reader = new GpxReader(stream);
                                           return reader.Points.Replay(backgroundScheduler);
                                       }
                                   });

            currentSection = gpxReplays.Switch().ToProperty(this, x => x.CurrentSection);
        }

        public GpxSection CurrentSection { get { return currentSection.Value; } }
        public ReactiveCommand LoadGpxDataCommand { get { return loadGpxDataCommand; } }
    }
}
