using System.Threading;
using System.Windows;

namespace MVVM_Com
{
    public partial class MoveItemsWindow : Window

    {
        private MoveItemsVM ada;

        private void WindowClose()
        {
            this.Dispatcher.BeginInvoke(new ThreadStart(delegate
            {
                this.Close();
            }));
        }

        internal MoveItemsWindow(List list, string panelPart)
        {
            InitializeComponent();

            ada = new MoveItemsVM(list, panelPart);

            ada.EventExit += WindowClose;

            DataContext = ada;
        }
    }
}