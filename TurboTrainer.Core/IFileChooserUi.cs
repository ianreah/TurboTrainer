using System.IO;
using System.Threading.Tasks;

namespace TurboTrainer.Core
{
    public interface IFileChooserUi
    {
        Task<Stream> ChooseFile();
    }
}
