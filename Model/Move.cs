using System.IO;

namespace MVVM_Com

{
    internal class Move : Copy
    {
        // Т.к. в базовом классе Copy есть конструктор с параметрами
        //, то в производном классе надо обязательно вызвать один из этих конструкторов через base
        public Move(List list, string targetPath)
            : base(list, targetPath)
        {
        }

        // Переопределяем метод
        internal override void FileAction(string source, string destination)
        {
            
            FILE.Delete(destination); // !!!  Удаление дубликата. Если по адресу нет файла, File.Delete НЕ ВЫДАЕТ ОШИБКУ!!!
            FILE.Move(source, destination); // Переносим файл
        }

        public override void FolderAction(string source, string destination)
        {
            if (!Directory.Exists(destination)) // Если нет дубликата переносимого каталога

            {
                
                Directory.Move(source, destination); // ?????
            }
            else // Если нет дубликата переносимого каталога
            {
                // Если есть файлы в каталоге - перенос файлов
                if (Directory.GetFiles(source).Length != 0)

                {
                    

                    FileInfo[] fi = new DirectoryInfo(source).GetFiles(); //Получаем массив FileInfo

                    for (int x = 0; x < fi.Length; x++)
                    {
                        FileAction(source + "\\" + fi[x].Name, destination + "\\" + fi[x].Name);
                    }
                }

                // Если есть каталоги в каталоге - перенос
                if (Directory.GetDirectories(source).Length != 0)
                {
                    

                    DirectoryInfo[] sl;
                    sl = new DirectoryInfo(source).GetDirectories();

                    for (int x = 0; x < sl.Length; x++)
                    {
                        FolderAction(sl[x].FullName, destination + "\\" + sl[x].Name); //рекурсия

                        continue;
                    }
                }
               
                Directory.Delete(source);
            }
        }
    }
}