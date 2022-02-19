using System.Windows;

namespace MVVM_Com
{
    partial class DeleteItemsWindow : Window

    {
        private DeleteItemsVM deleteItemsVM;

        private void WindowClose()
        {
            this.Close();
        }

        internal DeleteItemsWindow(List list, string panelPart)
        {
            InitializeComponent();

            deleteItemsVM = new DeleteItemsVM(list, panelPart);

            deleteItemsVM.EventExit += WindowClose;
            DataContext = deleteItemsVM;
        }
    }
}