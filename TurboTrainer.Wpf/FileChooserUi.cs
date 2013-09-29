using Microsoft.Win32;
using System.IO;
using TurboTrainer.Core;

namespace TurboTrainer.Wpf
{
    public class FileChooserUi : IFileChooserUi
    {
        public Stream ChooseFile()
        {
            var dialog = new OpenFileDialog { Filter = "GPX Files (*.gpx)|*.gpx|All files (*.*)|*.*" };
            if (dialog.ShowDialog() == true)
            {
                return File.OpenRead(dialog.FileName);
            }

            return Stream.Null;
        }
    }
}
