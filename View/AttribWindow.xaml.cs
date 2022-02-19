using System.Windows;

namespace MVVM_Com
{

    partial class AttibWindow : Window


    {
        private AttribVm attribVM;

        private void WindowClose()
        {
            attribVM.EventExit -= WindowClose;
            this.Close();
        }

       
        internal AttibWindow(Item item)
        {
            InitializeComponent();

            attribVM = new AttribVm(item);

            attribVM.EventExit += WindowClose;

            DataContext = attribVM;
        }
    }
}