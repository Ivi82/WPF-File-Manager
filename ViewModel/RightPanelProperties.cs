using System.Collections.ObjectModel;

namespace MVVM_Com
{
    internal partial class MainPanelViewModel
    {
        public ObservableCollection<DriveItem> RightListDrives
        {
            get { return drives.rightDrives; }
        }

        private DriveItem selectedRightDrive;

        public DriveItem SelectedRightDrive
        {
            get
            {
                // Задается значение выбранного диска если пользователь удалит диск на котором стоит курсор выбора.
                // это предотвратит ошибку
                if (selectedRightDrive == null)
                    selectedRightDrive = RightListDrives[0];

                return selectedRightDrive;
            }

            set
            {
                selectedRightDrive = value;

                // Если курсор выбора диска будет стоять на том диске который уберут из компа
                if (selectedRightDrive == null)
                    selectedRightDrive = RightListDrives[0];

                RightPath = SelectedRightDrive.WorkPath;
            }
        }

        // Свойство связанное с статическим Pathes.RightPath модели
        public string RightPath
        {
            get { return Pathes.RightPath; }

            set
            {
                Pathes.RightPath = value;

                RefreshRightPanel();
            }
        }

        // Свойство относится к строке пути - она вторична, для наглядности и поиска в текущем каталоге
        public string DisplayRightPath
        {
            get
            {
                return Pathes.RightPath;
            }
            set
            {
                // Pathes.LeftPath = value; // присваиваем значение для модели

                // Здесь приоритетнее адрес пути SelectedLeftDrive.WorkPath, т.к. Pathes.LeftPath
                // изменена вводом с клавиатуры и не может быть корректной до проверки

                //  int index = value.IndexOf(SelectedRightDrive.WorkPath);

                if (value.Length > SelectedRightDrive.WorkPath.Length)
                {
                    string text = value.Substring(SelectedRightDrive.WorkPath.Length);

                    for (int x = 0; x < RightList.Count; x++)
                    {
                        if ((RightList[x].Name + RightList[x].Extension).StartsWith(text))
                        {
                            SelectedRightItem = RightList[x];
                            break;
                        }
                    }
                }

                Pathes.RightPath = value; // присваиваем значение для модели
                OnPropertyChanged("DisplayRightPath");
            }
        }

        // Коллекция файлов и каталогов только для чтения - при изменении списка в модели,
        // обновляется на экране автоматически, т.к. ObservableCollection
        public ObservableCollection<Item> RightList
        {
            get { return rightList.list; }
        }

        // Свойство отображающее курсор на выделенном элементе списка
        private Item selectedRightItem;

        public Item SelectedRightItem
        {
            get
            {
                return selectedRightItem;
            }

            set
            {
                if (selectedPartWindow == "left") { leftList.AllIsUnSelected(); }  //Сброс выделенных элементов в соседнем окне
                selectedPartWindow = "right";

                selectedRightItem = value;

                // Если set устанавливается пользователем вручную в интерфейсе то OnPropertyChanged("SelectedLeftItem"); не нужно.
                // но поскольку selected_left_item из DisplayLeftPath меняется не вручную необходим  OnPropertyChanged("SelectedLeftItem")
                OnPropertyChanged("SelectedRightItem");
            }
        }

        public string RightDownInfo
        {
            get { return "Файлов: " + rightList.filesCount + "   Каталогов: " + rightList.catalogsCount; }
        }
    }
}