using System.ComponentModel;

namespace MVVM_Com
{
    internal class MoveItemsVM : INotifyPropertyChanged
    {
        private Move move;

        internal event Delegate EventExit;

        public string ButtonState
        {
            get { return move.buttonState; }
            set { move.buttonState = value; }
        }

        private string mode;

        public string Mode
        {
            get { return mode; }

            set
            {
                mode = value;

                if (mode == "construct")
                {
                    FirstString = "Хотите перенести выбранные файл(ы)/каталог(и)";

                    VisibilityText = "Collapsed";
                    VisibilityButtonYes = "Visible";
                    Visibility = "Collapsed";
                    VisibilitySecondString = "Collapsed";
                    IsReadOnly = "False";
                }

                if (mode == "copy" || mode == "copyAll")
                {
                    FirstString = "Переносим";

                    VisibilityText = "Visible";
                    Visibility = "Collapsed";
                    VisibilityButtonYes = "Collapsed";
                    VisibilitySecondString = "Hidden";
                    IsReadOnly = "True";
                }

                if (mode == "rewrite")
                {
                    FirstString = "Переносимый каталог или файл ";

                    SecondString = "Перезаписать?";
                    Visibility = "Visible";
                    VisibilityButtonYes = "Visible";
                    VisibilitySecondString = "Visible";
                    IsReadOnly = "True";
                }

                OnPropertyChanged("Mode");
            }
        }

        private string visibility;

        public string Visibility
        {
            get { return visibility; }
            set
            {
                visibility = value;
                OnPropertyChanged("Visibility");
            }
        }

        private string visibilityButtonYes;

        public string VisibilityButtonYes
        {
            get { return visibilityButtonYes; }
            set
            {
                visibilityButtonYes = value;
                OnPropertyChanged("VisibilityButtonYes");
            }
        }

        private string visibilitySecondString;

        public string VisibilitySecondString
        {
            get { return visibilitySecondString; }
            set
            {
                visibilitySecondString = value;
                OnPropertyChanged("VisibilitySecondString");
            }
        }

        private string visibilityText;

        public string VisibilityText
        {
            get { return visibilityText; }
            set
            {
                visibilityText = value;
                OnPropertyChanged("VisibilityText");
            }
        }

        private string isReadOnly;

        public string IsReadOnly
        {
            get { return isReadOnly; }
            set
            {
                isReadOnly = value;
                OnPropertyChanged("IsReadOnly");
            }
        }

        private string fileName;

        public string FileName
        {
            get { return fileName; }
            set
            {
                fileName = value;
                OnPropertyChanged("FileName");
            }
        }

        private string firstString;

        public string FirstString
        {
            get { return firstString; }
            set
            {
                firstString = value;
                OnPropertyChanged("FirstString");
            }
        }

        private string secondString;

        public string SecondString
        {
            get { return secondString; }
            set
            {
                secondString = value;
                OnPropertyChanged("SecondString");
            }
        }

        private string targetPath;

        public string PathName
        {
            get { return targetPath; }
            set
            {
                targetPath = value;
                OnPropertyChanged("PathName");
            }
        }

        private void Message(string x, string x2)
        {
            FileName = x;
            PathName = x2;
        }

        private void ReWrite(string x, string x2)
        {
            PathName = x2;
            FileName = x + " уже существует!";
            Mode = "rewrite";
        }

        private void Close()
        {
            // Отписываемся от всех событий
            move.EventExit -= Close;
            move.EventFileIsCopied -= Message;
            move.EventReWrite -= ReWrite;

            EventExit();
        }

        private RelayCommand yesAllButton;

        public RelayCommand YesALLButton
        {
            get
            {
                return yesAllButton ??
                (yesAllButton = new RelayCommand(obj =>
                {
                    Mode = "copy";
                    ButtonState = "all";
                }));
            }
        }

        private RelayCommand yesButton;

        public virtual RelayCommand YesButton
        {
            get
            {
                return yesButton ??
                (yesButton = new RelayCommand(obj =>
                {
                    switch (Mode)
                    {
                        case "construct":

                            Mode = "copy";                           

                            move.Run();

                            break;

                        default:

                            Mode = "copy";
                            ButtonState = "yes";

                            break;
                    }
                }));
            }
        }

        private RelayCommand noButton;

        public RelayCommand NoButton
        {
            get
            {
                return noButton ??
                (noButton = new RelayCommand(obj =>
                {
                    ButtonState = "no";
                }));
            }
        }

        private RelayCommand canselButton;

        public RelayCommand CanselButton
        {
            get
            {
                return canselButton ??
                (canselButton = new RelayCommand(obj =>
                {
                    if (Mode == "construct") Close();
                    else

                        ButtonState = "cansel";
                }));
            }
        }

        internal MoveItemsVM(List list, string panelPart)

        {
            PathName = panelPart == "Left" ? (Pathes.RightPath) : (Pathes.LeftPath);

            move = new Move(list, PathName);

            Mode = "construct";
            move.EventExit += Close;
            move.EventFileIsCopied += Message;
            move.EventReWrite += ReWrite;
        }

       

        private void OnPropertyChanged(string propName)
        {
            if (PropertyChanged != null)
                PropertyChanged(this, new PropertyChangedEventArgs(propName));
        }

        public event PropertyChangedEventHandler PropertyChanged;
    }
}