using System.Windows.Media.Imaging;

namespace MVVM_Com
{
    // Kласс включающий сведения о диске

    internal class DriveItem
    {
        public BitmapFrame Icon { get; set; }
        public string Name { get; set; }
        public string WorkPath { get; set; } // Каждый диск "запоминает" последний рабочий каталог для удобства при переходе между дисками
    }
}