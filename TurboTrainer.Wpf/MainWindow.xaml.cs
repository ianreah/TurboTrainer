using ReactiveUI;
using System.Reactive.Concurrency;
using System.Windows;
using TurboTrainer.Core;

namespace TurboTrainer.Wpf
{
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();

            DataContext = new MainViewModel(RxApp.TaskpoolScheduler, RxApp.MainThreadScheduler, new FileChooserUi());
        }
    }
}
