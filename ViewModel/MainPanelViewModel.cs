using System.ComponentModel;
using System.Windows.Media;

namespace MVVM_Com

{
    internal partial class MainPanelViewModel : INotifyPropertyChanged
    {
        private Drives drives;
        private List leftList;
        private List rightList;

        private string selectedPartWindow;

        private Delegate refreshWindow;

        internal void RefreshLeftPanel()
        {
            leftList.Load(Pathes.LeftPath);
            OnPropertyChanged("DisplayLeftPath");  // Обновляем строку пути
            OnPropertyChanged("LeftDownInfo");     // Обновляем инфу о количестве файлов-каталогов
        }

        internal void RefreshRightPanel()
        {
            rightList.Load(Pathes.RightPath);
            OnPropertyChanged("DisplayRightPath");  // Обновляем строку пути
            OnPropertyChanged("RightDownInfo");     // Обновляем инфу о количестве файлов-каталогов
        }

        private Brush textBoxFrontColor;

        public Brush TextBoxFrontColor
        {
            get { return textBoxFrontColor; }

            set
            {
                textBoxFrontColor = value;
                OnPropertyChanged("TextBoxFrontColor");
            }
        }

        private Brush gridBackColor;

        public Brush GridBackColor
        {
            get { return gridBackColor; }

            set
            {
                gridBackColor = value;
                OnPropertyChanged("GridBackColor");
            }
        }

        private Brush listBackColor;

        public Brush ListBackColor
        {
            get { return listBackColor; }

            set
            {
                listBackColor = value;
                OnPropertyChanged("ListBackColor");
            }
        }

        private Brush listFrontColor;

        public Brush ListFrontColor
        {
            get { return listFrontColor; }

            set
            {
                listFrontColor = value;
                OnPropertyChanged("ListFrontColor");
            }
        }

        private Brush listBoxBackColor;

        public Brush ListBoxBackColor
        {
            get { return listBoxBackColor; }

            set
            {
                listBoxBackColor = value;
                OnPropertyChanged("ListBoxBackColor");
            }
        }

        private Brush columnHeaderBackColor;

        public Brush ColumnHeaderBackColor
        {
            get { return columnHeaderBackColor; }

            set
            {
                columnHeaderBackColor = value;
                OnPropertyChanged("ColumnHeaderBackColor");
            }
        }

        private RelayCommand lightScreen;

        public RelayCommand LightScreen
        {
            get
            {
                return lightScreen ??
                (lightScreen = new RelayCommand(obj =>
                {
                    Light();
                }));
            }
        }

        private RelayCommand darkScreen;

        public RelayCommand DarkScreen
        {
            get
            {
                return darkScreen ??
                (darkScreen = new RelayCommand(obj =>
                {
                    Dark();
                }));
            }
        }

        private void Dark()
        {
            GridBackColor = new SolidColorBrush(Color.FromRgb(37, 37, 38));
            ColumnHeaderBackColor = new SolidColorBrush(Color.FromRgb(67, 67, 69));
            TextBoxFrontColor = new SolidColorBrush(Color.FromRgb(43, 145, 175));
            ListBackColor = new SolidColorBrush(Color.FromRgb(45, 45, 48));
            ListFrontColor = new SolidColorBrush(Color.FromRgb(103, 140, 177));
            ListBoxBackColor = new SolidColorBrush(Color.FromRgb(67, 67, 69));
            Theme.Color = "Dark";
        }

        private void Light()
        {
            GridBackColor = new SolidColorBrush(Color.FromRgb(245, 245, 245));
            ColumnHeaderBackColor = new SolidColorBrush(Color.FromRgb(255, 255, 255));
            TextBoxFrontColor = new SolidColorBrush(Color.FromRgb(0, 0, 0));
            ListBackColor = new SolidColorBrush(Color.FromRgb(233, 233, 233));
            ListFrontColor = new SolidColorBrush(Color.FromRgb(0, 0, 0));
            ColumnHeaderBackColor = new SolidColorBrush(Color.FromRgb(255, 255, 255));
            ListBoxBackColor = new SolidColorBrush(Color.FromRgb(233, 233, 233));
            Theme.Color = "Light";
        }

        public event PropertyChangedEventHandler PropertyChanged;

        public void OnPropertyChanged(string prop = "")
        {
            if (PropertyChanged != null)
                PropertyChanged(this, new PropertyChangedEventArgs(prop));
        }

        internal MainPanelViewModel() // Конструктор
        {
            Pathes.LeftPath = ConfFileRead.GetLeftPath();  // Читаем пути из файла
            Pathes.RightPath = ConfFileRead.GetRightPath();

            drives = new Drives();  // Формируем список дисков

            leftList = new List();  // Формируем список каталогов и файлов
            rightList = new List(); // Формируем список каталогов и файлов

            if (ConfFileRead.GetTheme() == "Light") Light();
            else Dark();

            for (int x = 0; x < drives.leftDrives.Count; x++)	  //  drives.leftDrives.Count - число дисков
            {
                if (Pathes.LeftPath.Substring(0, 3) == drives.leftDrives[x].Name)
                    SelectedLeftDrive = drives.leftDrives[x]; //set Ставим курсор

                if (Pathes.RightPath.Substring(0, 3) == drives.leftDrives[x].Name)
                    SelectedRightDrive = drives.rightDrives[x]; //set Ставим курсор
            }

            // Анонимные методы

            refreshWindow = () =>

             {
                 RefreshLeftPanel();
                 RefreshRightPanel();
             };

            Pathes.EventChangesRightPath += delegate { SelectedRightDrive.WorkPath = Pathes.RightPath; };
            Pathes.EventChangesLeftPath += delegate { SelectedLeftDrive.WorkPath = Pathes.LeftPath; };
        }
    }
}