using System.Windows;

namespace MVVM_Com
{

    partial class CopyItemsWindow : Window


    {
        private CopyItemsVM copyItemsVM;

        private void WindowClose()
        {
            
            this.Close();
           
        }

        

        internal CopyItemsWindow(List list, string panelPart)
        {
            InitializeComponent();

            copyItemsVM = new CopyItemsVM(list, panelPart);

            copyItemsVM.EventExit += WindowClose;

            DataContext = copyItemsVM;
        }
    }
}