using System.Windows;

namespace MVVM_Com
{
     partial class CreateFolderWindow : Window
    {
        private string Path;

        internal CreateFolderWindow(string Path) //Конструктор
        {
            InitializeComponent();

            this.Path = Path;
            xamlPath.Text = this.Path;
        }

        private void YesClick(object sender, RoutedEventArgs e)
        {
            if (newFolder.Text.Trim() == "") return;

            if (FOLDER.Create(Path, newFolder.Text))
            {
                this.Close();
            }
            else

                MessageBox.Show("Каталог " + newFolder.Text + " уже существует! Измените имя каталога");
        }

        private void NoClick(object sender, RoutedEventArgs e)
        {
            this.Close();
        }
    }
}