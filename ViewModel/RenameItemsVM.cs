using System.ComponentModel;

namespace MVVM_Com
{
    public class RenameItemsVM : INotifyPropertyChanged
    {
        public delegate void VoidString();

        public event VoidString EventExit;

        private Item selectedItem;

        private string oldName;
        private string panelPart;

        public string OldName
        {
            get { return oldName; }
            set
            {
                oldName = value;
                OnPropertyChanged("OldName");
            }
        }

        private string newName;

        public string NewName
        {
            get { return newName; }
            set
            {
                newName = value;
                OnPropertyChanged("NewName");
            }
        }

        private RelayCommand yesButton;

        public RelayCommand YesButton
        {
            get
            {
                return yesButton ??
                (yesButton = new RelayCommand(obj =>
                {
                    Rename rename = new Rename(selectedItem, NewName);

                    string Path = panelPart == "Left" ? (Pathes.RightPath) : (Pathes.LeftPath);

                    if (Path.Contains(selectedItem.FullName))
                    {
                        string text = Path.Substring(selectedItem.FullName.Length);

                        if (panelPart == "Left")

                            Pathes.RightPath = NewName + text;
                        else Pathes.LeftPath = NewName + text;
                    }

                    Close();
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
                    Close();
                }));
            }
        }

        private void Close()
        {
            EventExit(); // Создаем событие окончания работы
        }

        internal RenameItemsVM(Item selectedItem, string panelPart)
        {
            OldName = selectedItem.FullName;
            NewName = selectedItem.FullName;
            this.panelPart = panelPart;
            this.selectedItem = selectedItem;
        }

        

        private void OnPropertyChanged(string propName)
        {
            if (PropertyChanged != null)
                PropertyChanged(this, new PropertyChangedEventArgs(propName));
        }

        public event PropertyChangedEventHandler PropertyChanged;
    }
}