using System.Windows;

namespace MVVM_Com
{
     partial class RenameItemsWindow : Window

    {
        private RenameItemsVM rivm;

        private void WindowClose()
        {
            this.Close();
        }

        internal RenameItemsWindow(Item selectedItem, string panelPart)
        {
            InitializeComponent();
            rivm = new RenameItemsVM(selectedItem, panelPart);
            rivm.EventExit += WindowClose;
            DataContext = rivm;
        }
    }
}