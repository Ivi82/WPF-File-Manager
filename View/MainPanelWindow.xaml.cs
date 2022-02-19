using System.Windows;
using System.Windows.Controls;

namespace MVVM_Com
{
    public partial class MainPanelWindow : Window
    {
        private MainPanelViewModel ap;

        public MainPanelWindow()
        {
            InitializeComponent();

            ap = new MainPanelViewModel();
            DataContext = ap;
        }

        public void LeftSelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            var listview = sender as ListView;

            //скроллим до выбранного элемента
            // listview.ScrollIntoView(ap.SelectedLeftItem);
        }
    }
}