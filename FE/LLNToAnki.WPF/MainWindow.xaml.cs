using LLNToAnki.WPF.Client;
using LLNToAnki.WPF.ViewModels;
using System.Windows;

namespace LLNToAnki.WPF
{
    public partial class MainWindow : Window
    {
        public FlowPageVM ViewModel { get => (FlowPageVM)DataContext; }

        public MainWindow()
        {
            InitializeComponent();

            DataContext = new FlowPageVM(new FacadeClient(null));
        }

    }
}
