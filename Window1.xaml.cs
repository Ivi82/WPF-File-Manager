using System;
using System.Windows;

namespace MVVM_Com
{
    // Класс управляющий всеми окнами
    public partial class App : Application
    {
        private MainPanelWindow mainPanelWindow;

        private Delegate refreshWindow;

        private void RefreshMainPanel(object sender, EventArgs e)
        {
            refreshWindow();
        }

        private void CreateNewFolderWindow(string Path, Delegate refreshWindow)
        {
            this.refreshWindow = refreshWindow;
            CreateFolderWindow cf = new CreateFolderWindow(Path);
            cf.Closed += RefreshMainPanel;
            cf.Show();
        }

        private void CreateDeleteWindow(List list, string panelPart, Delegate refreshWindow)
        {
            MessageBoxResult result = MessageBox.Show("Хотите удалить выбранные файлы/каталоги?", "Simple Commander", MessageBoxButton.YesNo);

            switch (result)

            {
                case MessageBoxResult.Yes:

                    this.refreshWindow = refreshWindow;
                    DeleteItemsWindow deleteItemsWindow = new DeleteItemsWindow(list, panelPart);
                    deleteItemsWindow.Topmost = true; // Окно поверх остальных
                    deleteItemsWindow.Closed += RefreshMainPanel;
                    deleteItemsWindow.Show();

                    break;

                case MessageBoxResult.No:

                    break;
            }
        }

        private void CreateCopyItemsWindow(List list, string panelPart, Delegate refreshWindow)
        {
            this.refreshWindow = refreshWindow;
            CopyItemsWindow copyItemsWindow = new CopyItemsWindow(list, panelPart);
            copyItemsWindow.Topmost = true; // Окно поверх остальных

            copyItemsWindow.Closed += RefreshMainPanel;

            copyItemsWindow.Show();
        }

        private void CreateMoveWindow(List list, string panelPart, Delegate refreshWindow)
        {
            this.refreshWindow = refreshWindow;
            MoveItemsWindow moveItemsWindow = new MoveItemsWindow(list, panelPart);
            moveItemsWindow.Topmost = true; // Окно поверх остальных

            moveItemsWindow.Closed += RefreshMainPanel;

            moveItemsWindow.Show();
        }

        private void CreateRenameItemsWindow(Item selectedItem, string panelPart, Delegate refreshWindow)
        {
            this.refreshWindow = refreshWindow;

            RenameItemsWindow renameItemsWindow = new RenameItemsWindow(selectedItem, panelPart);

            renameItemsWindow.Topmost = true; // Окно поверх остальных

            renameItemsWindow.Closed += RefreshMainPanel;

            renameItemsWindow.Show();
        }

        private void CreateAttribItemWindow(Item selectedItem, string panelPart, Delegate refreshWindow)
        {
            this.refreshWindow = refreshWindow;

            AttibWindow aw = new AttibWindow(selectedItem);

            aw.Topmost = true; // Окно поверх остальных

            //  aw.Closed += RefreshMainPanel;

            aw.Show();
        }

        private void StopProgramm(object sender, EventArgs e)
        {
            ShowWindow.EventShowDeleteWindow -= CreateDeleteWindow;
            ShowWindow.EventShowMoveWindow -= CreateMoveWindow;
            ShowWindow.EventShowNewFolderWindow -= CreateNewFolderWindow;
            ShowWindow.EventShowRenameItemsWindow -= CreateRenameItemsWindow;
            ShowWindow.EventShowCopyWindow -= CreateCopyItemsWindow;
            ShowWindow.EventShowAttribItemWindow -= CreateAttribItemWindow;

            Confile.Save(); // Запись в файл последних WorkPath

            Application.Current.Shutdown();
            //MessageBox.Show("id из StopProgramm posle otrubki=" + Thread.CurrentThread.ManagedThreadId.ToString());
        }

        private App() // Конструктор класса
        {
            mainPanelWindow = new MainPanelWindow();

            // Если главное окно закроется но останутся не закрытые диалоговые окна - тогда они тоже закроются
            mainPanelWindow.Closed += StopProgramm;

            ShowWindow.EventShowDeleteWindow += CreateDeleteWindow;
            ShowWindow.EventShowNewFolderWindow += CreateNewFolderWindow;
            ShowWindow.EventShowRenameItemsWindow += CreateRenameItemsWindow;
            ShowWindow.EventShowCopyWindow += CreateCopyItemsWindow;
            ShowWindow.EventShowMoveWindow += CreateMoveWindow;
            ShowWindow.EventShowAttribItemWindow += CreateAttribItemWindow;
            mainPanelWindow.Show();
        }
    }
}