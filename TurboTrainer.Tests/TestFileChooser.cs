using System.IO;
using System.Text;
using System.Threading.Tasks;
using TurboTrainer.Core;

namespace TurboTrainer.Tests
{
    public class TestFileChooser : IFileChooserUi
    {
        public Task<Stream> ChooseFile()
        {
            return Task.FromResult<Stream>(new MemoryStream(Encoding.UTF8.GetBytes(Properties.Resources.SampleGpxDocument)));
        }
    }
}
