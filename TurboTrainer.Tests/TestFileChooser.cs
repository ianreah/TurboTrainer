using System.IO;
using System.Text;
using TurboTrainer.Core;

namespace TurboTrainer.Tests
{
    public class TestFileChooser : IFileChooserUi
    {
        public Stream ChooseFile()
        {
            return new MemoryStream(Encoding.UTF8.GetBytes(Properties.Resources.SampleGpxDocument));
        }
    }
}
