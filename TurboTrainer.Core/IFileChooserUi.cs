using System.IO;

namespace TurboTrainer.Core
{
    public interface IFileChooserUi
    {
        Stream ChooseFile();
    }
}
