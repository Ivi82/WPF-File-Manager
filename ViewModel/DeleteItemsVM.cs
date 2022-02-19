using System.ComponentModel;

namespace MVVM_Com
{
    internal class DeleteItemsVM : INotifyPropertyChanged
    {
        private Delete delete;

        internal event Delegate EventExit;

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

        private void Message(string item)
        {
            FileName = item;
        }

        private void Close()
        {
            delete.EventItemIsSelected -= Message;
            delete.EventExit -= Close;
            if (EventExit != null) EventExit();
            
        }

        public DeleteItemsVM(List list, string panelPart)
        {
            delete = panelPart == "Left" ? (new DeleteLeft(list)) : (Delete)(new DeleteRight(list));

            delete.EventExit += Close;
            delete.EventItemIsSelected += Message;
            delete.Run();
        }

        private void OnPropertyChanged(string propName)
        {
            if (PropertyChanged != null)
                PropertyChanged(this, new PropertyChangedEventArgs(propName));
        }

        public event PropertyChangedEventHandler PropertyChanged;
    }
}