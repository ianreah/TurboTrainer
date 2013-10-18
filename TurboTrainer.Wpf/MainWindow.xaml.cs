using ReactiveUI;
using TurboTrainer.Core;

namespace TurboTrainer.Wpf
{
    public partial class MainWindow
    {
        public MainWindow()
        {
            InitializeComponent();

            DataContext = new MainViewModel(RxApp.TaskpoolScheduler, new FileChooserUi());
        }
    }
}
