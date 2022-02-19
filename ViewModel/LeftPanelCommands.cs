using System;
using MessageBox = System.Windows.MessageBox;

namespace MVVM_Com

{
    internal partial class MainPanelViewModel
    {
        // Команда обрабатывает двойное нажатие клавиши мыши на выделенном элементе списка
        private RelayCommand leftListMouseDoubleClick;

        public RelayCommand LeftListMouseDoubleClick
        {
            get
            {
                return leftListMouseDoubleClick ??
                (leftListMouseDoubleClick = new RelayCommand(obj =>
                {
                    if (SelectedLeftItem == null) // Реакция на клик в пустоту

                        return;

                    LeftPath = Clicks.Click(leftList, Pathes.LeftPath); //set изменится путь - изменится и содержимое
                }));
            }
        }

        // Команда обрабатывает нажатие клавиши Enter в поле пути
        private RelayCommand leftPathEnterClick;

        public RelayCommand LeftPathEnterClick
        {
            get
            {
                return leftPathEnterClick ??
                (leftPathEnterClick = new RelayCommand(obj =>
                {
                    string enteredPath = obj.ToString(); // Путь введенный в строке

                    if (enteredPath.Length == 2)
                        enteredPath += "\\";   // Дописываем полное имя диска если наберут просто С

                    if (enteredPath.Length == 1)
                        enteredPath += ":\\"; // Если наберут С:

                    if (enteredPath.Length == 0)
                        enteredPath += SelectedLeftDrive.WorkPath; // Если на пустой строке нажмут Enter

                    // Перевод имени диска (первых трех символов) в верхний регистр, на тот случай если наберут в нижнем
                    enteredPath = enteredPath.Replace(enteredPath.Substring(0, 3), enteredPath.Substring(0, 3).ToUpper());

                    string enteredDiskName = enteredPath.Substring(0, 3); // Имя диска из этого пути

                    //Если выделенный слева диск = первым 3м буквам записанного path то диск менять не надо

                    if (SelectedLeftDrive.Name == enteredDiskName)
                    {
                        MessageBox.Show(enteredPath);
                        MessageBox.Show(SelectedLeftDrive.WorkPath);

                        LeftPath = Clicks.ClickOnPath(enteredPath, SelectedLeftDrive.WorkPath); // Изменится путь - изменится и содержимое
                    }
                    // Иначе, ищем с каким из дисков совпадает первые 3 буквы записанного path
                    else

                    {
                        for (int x = 0; x < drives.leftDrives.Count; x++)
                        {
                            if (drives.leftDrives[x].Name == enteredDiskName)
                            {
                                drives.leftDrives[x].WorkPath = enteredPath; // Присваиваем рабочему пути искомого диска указаный пользователем путь

                                SelectedLeftDrive = drives.leftDrives[x]; // Переход на найденный диск

                                OnPropertyChanged("SelectedLeftDrive"); // Обновляем т.к. свойство меняется программно

                                break;
                            }
                        }
                    }
                }));
            }
        }

        private RelayCommand sortLeftName;

        public RelayCommand SortLeftName
        {
            get
            {
                return sortLeftName ??
                (sortLeftName = new RelayCommand(obj =>
                {
                    leftList.Sort("Name");
                }));
            }
        }

        private RelayCommand sortLeftSize;

        public RelayCommand SortLeftSize
        {
            get
            {
                return sortLeftSize ??
                (sortLeftSize = new RelayCommand(obj =>
                {
                    leftList.Sort("Size");
                }));
            }
        }

        private RelayCommand sortLeftExtension;

        public RelayCommand SortLeftExtension
        {
            get
            {
                return sortLeftExtension ??
                (sortLeftExtension = new RelayCommand(obj =>
                {
                    leftList.Sort("Extension");
                }));
            }
        }

        private RelayCommand sortLeftData;

        public RelayCommand SortLeftData
        {
            get
            {
                return sortLeftData ??
                (sortLeftData = new RelayCommand(obj =>
                {
                    leftList.Sort("Data");
                }));
            }
        }

        private RelayCommand leftAllIsSelected;

        public RelayCommand LeftAllIsSelected
        {
            get
            {
                return leftAllIsSelected ??
                (leftAllIsSelected = new RelayCommand(obj =>
                {
                    leftList.AllIsSelected();
                }));
            }
        }

        private RelayCommand leftCreateFolder;

        public RelayCommand LeftCreateFolder
        {
            get
            {
                return leftCreateFolder ??
                (leftCreateFolder = new RelayCommand(obj =>
                {
                    ShowWindow.NewFolder(Pathes.LeftPath, refreshWindow);
                }));
            }
        }

        private RelayCommand leftDeleteItems;

        public RelayCommand LeftDeleteItems
        {
            get
            {
                return leftDeleteItems ??
                (leftDeleteItems = new RelayCommand(obj =>
                {
                    // Реакция на клик в пустоту
                    if (SelectedLeftItem == null || SelectedLeftItem.Name == "<↑↑↑>") return;

                    ShowWindow.Delete(leftList, "Left", refreshWindow);
                }));
            }
        }

        private RelayCommand leftMoveItems;

        public RelayCommand LeftMoveItems
        {
            get
            {
                return leftMoveItems ??
                (leftMoveItems = new RelayCommand(obj =>
                {
                    // Реакция на клик в пустоту
                    if (SelectedLeftItem == null || SelectedLeftItem.Name == "<↑↑↑>") return;

                    ShowWindow.Move(leftList, "Left", refreshWindow);
                }));
            }
        }

        private RelayCommand leftListCopy;

        public RelayCommand LeftListCopy
        {
            get
            {
                return leftListCopy ??
                (leftListCopy = new RelayCommand(obj =>
                {
                    // Реакция на клик в пустоту
                    if (SelectedLeftItem == null || SelectedLeftItem.Name == "<↑↑↑>") return;

                    ShowWindow.Copy(leftList, "Left", refreshWindow);
                }));
            }
        }

        private RelayCommand leftItemRename;

        public RelayCommand LeftItemRename
        {
            get
            {
                return leftItemRename ??
                (leftItemRename = new RelayCommand(obj =>
                {
                    // Реакция на клик в пустоту
                    if (SelectedLeftItem == null || SelectedLeftItem.Name == "<↑↑↑>") return;

                    ShowWindow.RenameItem(SelectedLeftItem, "Left", refreshWindow);
                }));
            }
        }

        private RelayCommand openInRightWindow;

        public RelayCommand OpenInRightWindow
        {
            get
            {
                return openInRightWindow ??
                (openInRightWindow = new RelayCommand(obj =>
                {
                    // Реакция на клик в пустоту
                    if (SelectedLeftItem == null) return;

                    string enteredPath = Clicks.ClickOnDirectory(SelectedLeftItem, Pathes.RightPath);

                    if (SelectedLeftDrive.Name == RightPath.Substring(0, 3)) RightPath = enteredPath; // Изменится путь - изменится и содержимое

                    // Если выделенный слева диск != выделенному диску справа, то надо изменить подсветку диска справа
                    else
                    {
                        for (int x = 0; x < drives.drivesCollection.Count; x++)
                        {
                            if (drives.rightDrives[x].Name == enteredPath.Substring(0, 3))
                            {
                                drives.rightDrives[x].WorkPath = enteredPath; // Присваиваем новый путь рабочему каталогу найденного диска
                                SelectedRightDrive = drives.rightDrives[x];   // Переход на найденный диск
                                OnPropertyChanged("SelectedRightDrive");      // Обновляем т.к. свойство меняется программно

                                break;
                            }
                        }
                        return;
                    }
                }));
            }
        }

        private RelayCommand leftPropertiesItem;

        public RelayCommand LeftPropertiesItem
        {
            get
            {
                return leftPropertiesItem ??
                (leftPropertiesItem = new RelayCommand(obj =>
                {
                    // Реакция на клик в пустоту

                    if (SelectedLeftItem == null || SelectedLeftItem.Name == "<↑↑↑>")
                        return;

                    MessageBox.Show(String.Join("\n", ItemInformation.GetInfo(SelectedLeftItem)));
                }));
            }
        }

        private RelayCommand infoLeftButton;

        public RelayCommand InfoLeftButton
        {
            get
            {
                return infoLeftButton ??
                (infoLeftButton = new RelayCommand(obj =>
                {
                    MessageBox.Show(String.Join("\n", DriveInformation.GetInfo(SelectedLeftDrive)));
                }));
            }
        }

        private RelayCommand leftAttribItem;

        public RelayCommand LeftAttribItem
        {
            get
            {
                return leftAttribItem ??
                (leftAttribItem = new RelayCommand(obj =>
                {
                    //реакция на клик в пустоту
                    if (SelectedLeftItem == null || SelectedLeftItem.Name == "<↑↑↑>")
                        return;

                    ShowWindow.AttribItem(SelectedLeftItem, "Left", refreshWindow);
                }));
            }
        }
    }
}