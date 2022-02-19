using System;
using System.ComponentModel;

namespace MVVM_Com
{
    // Класс включающий сведения о файлах и каталогах
    internal class Item : INotifyPropertyChanged
    {
        public string Name { get; set; }
        public string Extension { get; set; }
        public string Form { get; set; }
        public string Size { get; set; }
        public DateTime Data { get; set; }
        public string FullName { get; set; }
        public string Parent { get; set; }
        public string Root { get; set; }
        public string CatOrFile { get; set; }
        public string NeedSorting { get; set; }

        private bool isSelected;

        public bool IsSelected
        {
            get
            {
                return isSelected;
            }
            set
            {
                isSelected = value;
                OnPropertyChanged("IsSelected");
            }
        }

        public event PropertyChangedEventHandler PropertyChanged;

        public void OnPropertyChanged(string prop = "")
        {
            if (PropertyChanged != null)
                PropertyChanged(this, new PropertyChangedEventArgs(prop));
        }
    }
}