using Microsoft.Win32;
using System.IO;
using System.Threading.Tasks;
using TurboTrainer.Core;

namespace TurboTrainer.Wpf
{
    public class FileChooserUi : IFileChooserUi
    {
        public Task<Stream> ChooseFile()
        {
            var dialog = new OpenFileDialog { Filter = "GPX Files (*.gpx)|*.gpx|All files (*.*)|*.*" };
            var stream = (dialog.ShowDialog() == true) ? File.OpenRead(dialog.FileName) : Stream.Null;
            return Task.FromResult(stream);
        }
    }
}
