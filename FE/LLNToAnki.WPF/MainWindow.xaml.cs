using LLNToAnki.Business;
using LLNToAnki.Facade.Controllers;
using LLNToAnki.WPF.Client;
using LLNToAnki.WPF.ViewModels;
using System.Windows;
using System.Windows.Controls;

namespace LLNToAnki.WPF
{
    public partial class MainWindow : Window
    {
        public FlowPageVM ViewModel { get => (FlowPageVM)DataContext; }

        public MainWindow()
        {
            InitializeComponent();

            var facadeClient = new FacadeClient();
            
            DataContext = new FlowPageVM(facadeClient);
        }

        private void LanguageCombobox_SelectionChanged(object sender, System.Windows.Controls.SelectionChangedEventArgs e)
        {
            var cb = sender as ComboBox;

            ViewModel.ChangeLanguageCommand.Execute(cb.SelectedItem);
        }
    }
}
