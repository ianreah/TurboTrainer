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

            DataContext = new MainViewModel(Scheduler.Default, new FileChooserUi());
        }
    }
}
