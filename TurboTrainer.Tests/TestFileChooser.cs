using System.IO;
using System.Threading.Tasks;
using TurboTrainer.Core;

namespace TurboTrainer.Tests
{
    public class TestFileChooser : IFileChooserUi
    {
        private readonly Stream stream;

        public TestFileChooser(Stream stream)
        {
            this.stream = stream;
        }

        public Task<Stream> ChooseFile()
        {
            return Task.FromResult(stream);
        }
    }
}
