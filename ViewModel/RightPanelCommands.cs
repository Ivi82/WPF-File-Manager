using System;
using MessageBox = System.Windows.MessageBox;

namespace MVVM_Com
{
    internal partial class MainPanelViewModel
    {
        // Команда обрабатывает нажатие клавиши мыши на выделенном элементе списка
        private RelayCommand rightListMouseDoubleClick;

        public RelayCommand RightListMouseDoubleClick
        {
            get
            {
                return rightListMouseDoubleClick ??
                (rightListMouseDoubleClick = new RelayCommand(obj =>
                {
                    if (SelectedRightItem == null) // Реакция на клик в пустоту

                        return;

                    RightPath = Clicks.Click(rightList, Pathes.RightPath); //set изменится путь - изменится и содержимое
                }));
            }
        }

        // Команда обрабатывает нажатие клавиши Enter в поле пути
        private RelayCommand rightPathEnterClick;

        public RelayCommand RightPathEnterClick
        {
            get
            {
                return rightPathEnterClick ??
                (rightPathEnterClick = new RelayCommand(obj =>
                {
                    string enteredPath = obj.ToString(); // Путь введенный в строке

                    if (enteredPath.Length == 2)
                        enteredPath += "\\";   // Дописываем полное имя диска если наберут просто С

                    if (enteredPath.Length == 1)
                        enteredPath += ":\\"; // Если наберут С:

                    if (enteredPath.Length == 0)
                        enteredPath += SelectedRightDrive.WorkPath; // Если на пустой строке нажмут Enter

                    // Перевод имени диска (первых трех символов) в верхний регистр, на тот случай если наберут в нижнем
                    enteredPath = enteredPath.Replace(enteredPath.Substring(0, 3), enteredPath.Substring(0, 3).ToUpper());

                    string enteredDiskName = enteredPath.Substring(0, 3); // Имя диска из этого пути

                    //Если выделенный слева диск = первым 3м буквам записанного path то диск менять не надо

                    if (SelectedRightDrive.Name == enteredDiskName)

                        RightPath = Clicks.ClickOnPath(enteredPath, SelectedRightDrive.WorkPath); //Изменится путь - изменится и содержимое

                    // Иначе, ищем с каким из дисков совпадает первые 3 буквы записанного path
                    else

                    {
                        for (int x = 0; x < drives.rightDrives.Count; x++)
                        {
                            if (drives.rightDrives[x].Name == enteredDiskName)
                            {
                                drives.rightDrives[x].WorkPath = enteredPath; // Присваиваем рабочему пути искомого диска указаный пользователем путь

                                SelectedRightDrive = drives.rightDrives[x]; // Переход на найденный диск

                                OnPropertyChanged("SelectedRightDrive"); // Обновляем т.к. свойство меняется программно

                                break;
                            }
                        }
                    }
                }));
            }
        }

        private RelayCommand sortRightName;

        public RelayCommand SortRightName
        {
            get
            {
                return sortRightName ??
                (sortRightName = new RelayCommand(obj =>
                {
                    rightList.Sort("Name");
                }));
            }
        }

        private RelayCommand sortRightSize;

        public RelayCommand SortRightSize
        {
            get
            {
                return sortRightSize ??
                (sortRightSize = new RelayCommand(obj =>
                {
                    rightList.Sort("Size");
                }));
            }
        }

        private RelayCommand sortRightExtension;

        public RelayCommand SortRightExtension
        {
            get
            {
                return sortRightExtension ??
                (sortRightExtension = new RelayCommand(obj =>
                {
                    rightList.Sort("Extension");
                }));
            }
        }

        private RelayCommand sortRightData;

        public RelayCommand SortRightData
        {
            get
            {
                return sortRightData ??
                (sortRightData = new RelayCommand(obj =>
                {
                    rightList.Sort("Data");
                }));
            }
        }

        private RelayCommand rightAllIsSelected;

        public RelayCommand RightAllIsSelected
        {
            get
            {
                return rightAllIsSelected ??
                (rightAllIsSelected = new RelayCommand(obj =>
                {
                    rightList.AllIsSelected();
                }));
            }
        }

        private RelayCommand rightCreateFolder;

        public RelayCommand RightCreateFolder
        {
            get
            {
                return rightCreateFolder ??
                (rightCreateFolder = new RelayCommand(obj =>
                {
                    ShowWindow.NewFolder(Pathes.RightPath, refreshWindow);
                }));
            }
        }



        private RelayCommand rightMoveItems;

        public RelayCommand RightMoveItems
        {
            get
            {
                return rightMoveItems ??
                (rightMoveItems = new RelayCommand(obj =>
                {
                    // Реакция на клик в пустоту
                    if (SelectedRightItem == null || SelectedRightItem.Name == "<↑↑↑>") return;

                    ShowWindow.Move(rightList, "Rigth", refreshWindow);
                }));
            }
        }






        private RelayCommand rightListCopy;

        public RelayCommand RightListCopy
        {
            get
            {
                return rightListCopy ??
                (rightListCopy = new RelayCommand(obj =>
                {
                    // Реакция на клик в пустоту
                    if (SelectedRightItem == null || SelectedRightItem.Name == "<↑↑↑>") return;

                    ShowWindow.Copy(rightList, "Right", refreshWindow);
                }));
            }
        }

        private RelayCommand rightDeleteItems;

        public RelayCommand RightDeleteItems
        {
            get
            {
                return rightDeleteItems ??
                (rightDeleteItems = new RelayCommand(obj =>
                {
                    // Реакция на клик в пустоту
                    if (SelectedRightItem == null || SelectedRightItem.Name == "<↑↑↑>") return;

                    ShowWindow.Delete(rightList, "Right", refreshWindow);
                }));
            }
        }

        private RelayCommand rightItemRename;

        public RelayCommand RightItemRename
        {
            get
            {
                return rightItemRename ??
                (rightItemRename = new RelayCommand(obj =>
                {
                    // Реакция на клик в пустоту
                    if (SelectedRightItem == null || SelectedRightItem.Name == "<↑↑↑>") return;

                    ShowWindow.RenameItem(SelectedRightItem, "Right", refreshWindow);
                }));
            }
        }

        private RelayCommand openInLeftWindow;

        public RelayCommand OpenInLeftWindow
        {
            get
            {
                return openInLeftWindow ??
                (openInLeftWindow = new RelayCommand(obj =>
                {
                    // Реакция на клик в пустоту
                    if (SelectedRightItem == null) return;

                    string enteredPath = Clicks.ClickOnDirectory(SelectedRightItem, Pathes.LeftPath);

                    if (SelectedRightDrive.Name == LeftPath.Substring(0, 3)) LeftPath = enteredPath; // Изменится путь - изменится и содержимое

                    // Если выделенный слева диск != выделенному диску справа, то надо изменить подсветку диска справа
                    else
                    {
                        for (int x = 0; x < drives.drivesCollection.Count; x++)
                        {
                            if (drives.leftDrives[x].Name == enteredPath.Substring(0, 3))
                            {
                                drives.leftDrives[x].WorkPath = enteredPath; // Присваиваем новый путь рабочему каталогу найденного диска
                                SelectedLeftDrive = drives.leftDrives[x];    // Переход на найденный диск
                                OnPropertyChanged("SelectedLeftDrive");      // Обновляем т.к. свойство меняется программно

                                break;
                            }
                        }
                        return;
                    }
                }));
            }
        }

        private RelayCommand rightPropertiesItem;

        public RelayCommand RightPropertiesItem
        {
            get
            {
                return rightPropertiesItem ??
                (rightPropertiesItem = new RelayCommand(obj =>
                {
                    // Реакция на клик в пустоту

                    if (SelectedRightItem == null || SelectedRightItem.Name == "<↑↑↑>")
                        return;

                    MessageBox.Show(String.Join("\n", ItemInformation.GetInfo(SelectedRightItem)));
                }));
            }
        }

        private RelayCommand infoRightButton;

        public RelayCommand InfoRightButton
        {
            get
            {
                return infoRightButton ??
                (infoRightButton = new RelayCommand(obj =>
                {
                    MessageBox.Show(String.Join("\n", DriveInformation.GetInfo(SelectedRightDrive)));
                }));
            }
        }

        private RelayCommand rightAttribItem;

        public RelayCommand RightAttribItem
        {
            get
            {
                return rightAttribItem ??
                (rightAttribItem = new RelayCommand(obj =>
                {
                    //реакция на клик в пустоту
                    if (SelectedRightItem == null || SelectedRightItem.Name == "<↑↑↑>")
                        return;

                    ShowWindow.AttribItem(SelectedRightItem, "Left", refreshWindow);
                }));
            }
        }
    }
}