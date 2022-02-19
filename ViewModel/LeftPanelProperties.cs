using System.Collections.ObjectModel;

namespace MVVM_Com
{
    internal partial class MainPanelViewModel
    {
        // ЛЕВАЯ ПАНЕЛЬ =========================================================================================

        // Свойство выводящее на экран коллекцию дисков.
        // Tолько для чтения - при изменении коллекции в модели,
        // обновляется на экране автоматически, т.к. ObservableCollection
        //  =======================================================================================
        public ObservableCollection<DriveItem> LeftListDrives
        {
            get { return drives.leftDrives; }
            set { drives.leftDrives = value; }
        }

        // Свойства отображающие выбранный диск ==================================================

        private DriveItem selectedLeftDrive;

        public DriveItem SelectedLeftDrive
        {
            get
            {
                // Задается значение выбранного диска если пользователь удалит диск на котором стоит курсор выбора.
                // это предотвратит ошибку
                if (selectedLeftDrive == null)
                    selectedLeftDrive = LeftListDrives[0];

                return selectedLeftDrive;
            }

            set
            {
                selectedLeftDrive = value;

                // Если курсор выбора диска будет стоять на том диске который уберут из компа
                if (selectedLeftDrive == null)
                    selectedLeftDrive = LeftListDrives[0];

                LeftPath = SelectedLeftDrive.WorkPath;
                // MessageBox.Show(drives.leftDrives.Count.ToString());
            }
        }

        // Свойство связанное с статическим Pathes.LeftPath модели
        // Необходимо только для автоматического обновления списка
        public string LeftPath
        {
            get { return Pathes.LeftPath; }

            set
            {
                Pathes.LeftPath = value;
                RefreshLeftPanel();
            }
        }

        // Свойство относится к строке пути - она вторична, для наглядности и поиска в текущем каталоге
        public string DisplayLeftPath
        {
            get { return Pathes.LeftPath; }
            set
            {
                // Здесь приоритетнее адрес пути SelectedLeftDrive.WorkPath, т.к. Pathes.LeftPath
                // изменена вводом с клавиатуры и не может быть корректной до проверки

                //  int index = value.IndexOf(SelectedLeftDrive.WorkPath);

                if (value.Length > SelectedLeftDrive.WorkPath.Length)
                {
                    string text = value.Substring(SelectedLeftDrive.WorkPath.Length);

                    for (int x = 0; x < LeftList.Count; x++)
                    {
                        if ((LeftList[x].Name + LeftList[x].Extension).StartsWith(text))
                        {
                            SelectedLeftItem = LeftList[x];
                            break;
                        }
                    }
                }

                Pathes.LeftPath = value; // присваиваем значение для модели
                OnPropertyChanged("DisplayLeftPath");
            }
        }

        // Коллекция файлов и каталогов только для чтения - при изменении списка в модели,
        // обновляется на экране автоматически, т.к. ObservableCollection
        public ObservableCollection<Item> LeftList
        {
            get { return leftList.list; }
        }

        // Свойство отображающее курсор на выделенном элементе списка
        private Item selectedLeftItem;

        public Item SelectedLeftItem
        {
            get
            {
                return selectedLeftItem;
            }

            set
            {
                if (selectedPartWindow == "right") { rightList.AllIsUnSelected(); }  //Сброс выделенных элементов в соседнем окне
                selectedPartWindow = "left";

                selectedLeftItem = value;

                // Если set устанавливается пользователем вручную в интерфейсе то OnPropertyChanged("SelectedLeftItem"); не нужно.
                // но поскольку selected_left_item из DisplayLeftPath меняется не вручную необходим  OnPropertyChanged("SelectedLeftItem")
                OnPropertyChanged("SelectedLeftItem");
            }
        }

        public string LeftDownInfo
        {
            get { return "Файлов: " + leftList.filesCount + "   Каталогов: " + leftList.catalogsCount; }
        }
    }
}