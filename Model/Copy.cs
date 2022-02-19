using System.IO;
using System.Threading.Tasks;
using MessageBox = System.Windows.MessageBox;

namespace MVVM_Com
{
    internal class Copy
    {
        internal event Delegate EventExit;

        internal event DelegWith2StringParam EventReWrite;

        internal event DelegWith2StringParam EventFileIsCopied;

        public string buttonState = null;

        private List list;
        private string targetPath;

        public Copy(List list, string targetPath)
        {
            this.list = list;
            this.targetPath = targetPath;
        }

        internal async void Run()
        {
            await Task.Run(() =>

           {
               // Сначала работаем со списком выделенных элементов
               for (int x = 0; x < list.list.Count; x++)
               {
                   if (list.list[x].Name == "<↑↑↑>") continue; //исключаем элемент <↑↑↑>

                   if (list.list[x].FullName == targetPath + list.list[x].Name + list.list[x].Extension)
                       continue;  //если элемент копируется сам на себя т.е. (адрес1 == адрес2)

                   // 1.Если копируемый элемент выделен и это существующий файл
                   if (list.list[x].IsSelected && File.Exists(list.list[x].FullName))
                   {
                       // Если по адресу назначения существует файл с таким же именем (адрес1 !=адрес2)

                       if (File.Exists(targetPath + list.list[x].Name + list.list[x].Extension))
                       {
                           // Если  кнопка "Перезаписать Все" не была нажата
                           if (buttonState != "all")

                           {
                               // Cоздаем событие перезаписи - окно спрашивает надо ли перезаписывать?
                               EventReWrite(list.list[x].FullName, targetPath);

                               // Ожидание нажатия клавиши в окне
                               while (buttonState == null) { }

                               switch (buttonState)
                               {
                                   case "all": // "Перезаписать Все файлы"

                                       buttonState = "all";

                                       break;

                                   case "yes": // "Перезаписать один элемент"

                                       buttonState = null;

                                       break;

                                   case "no": // "Не перезаписывать"

                                       buttonState = null;

                                       continue; // Без копирования переходит к след элементу

                                   case "cansel": // "Отмена"

                                       break;
                               }

                               if (buttonState == "cansel") break; // Если отмена - прерываем цикл
                           }
                       }
                       FileAction(list.list[x].FullName, targetPath + list.list[x].Name + list.list[x].Extension); // Копируем файл

                       EventFileIsCopied(list.list[x].FullName, targetPath); // Создаем событие
                       //System.Threading.Thread.Sleep(2000);
                       continue; // Продолжаем, т.к. элемент уже обработан
                   }

                   // 2.Если каталог выделен и существует

                   if (list.list[x].IsSelected && Directory.Exists(list.list[x].FullName))
                   {
                       // Если каталог копируется сам В себя  т.е. адрес назначения содержит в себе адрес копируемого каталога

                       if (targetPath.StartsWith(list.list[x].FullName))
                       {
                           MessageBox.Show("Каталог не может быть скопирован\\перенесен сам в себя");

                           continue;
                       }

                       // Если по адресу назначения существует каталог с таким же именем (адрес1 !=адрес2)

                       if (Directory.Exists(targetPath + list.list[x].Name))
                       {
                           // Если  кнопка "Перезаписать Все" не была нажата
                           if (buttonState != "all")

                           {
                               // Cоздаем событие перезаписи - окно спрашивает надо ли перезаписывать?
                               EventReWrite(list.list[x].FullName, targetPath);

                               // Ожидание нажатия клавиши в окне
                               while (buttonState == null) { }

                               switch (buttonState)
                               {
                                   case "all": // "Перезаписать Все файлы"

                                       buttonState = "all";

                                       break;

                                   case "yes": // "Перезаписать один элемент"

                                       buttonState = null;

                                       break;

                                   case "no": // "Не перезаписывать"

                                       buttonState = null;

                                       continue; // Без копирования переходит к след элементу

                                   case "cansel": // "Отмена"

                                       break;
                               }

                               if (buttonState == "cansel") break; // Если отмена - прерываем цикл
                           }
                       }

                       FolderAction(list.list[x].FullName, targetPath + list.list[x].Name);
                   }
               }
           });

            EventExit(); // Cобытие завершения
        }

        internal virtual void FileAction(string source, string destination)
        {
            FILE.Copy(source, destination); //Копируем файл
        }

        public virtual void FolderAction(string source, string destination)
        {
            // Cоздаем этот каталог в новом пути, если он уже есть ошибки не будет
            FOLDER.Create(destination);

            // Если есть файлы в каталоге - копирование
            if (Directory.GetFiles(source).Length != 0)

            {
                FileInfo[] fi = new DirectoryInfo(source).GetFiles(); //Получаем массив FileInfo

                for (int x = 0; x < fi.Length; x++)
                {
                    FILE.Copy(source + "\\" + fi[x].Name, destination + "\\" + fi[x].Name); //Копируем файл

                    EventFileIsCopied(fi[x].FullName, destination + "\\"); // Создаем событие
                }
            }

            // Если есть каталоги в каталоге - копирование
            if (Directory.GetDirectories(source).Length != 0)
            {
                DirectoryInfo[] sl;
                sl = new DirectoryInfo(source).GetDirectories();

                for (int x = 0; x < sl.Length; x++)
                {
                    FOLDER.Create(destination + "\\" + sl[x].Name);

                    FolderAction(sl[x].FullName, destination + "\\" + sl[x].Name); // Рекурсия

                    continue;
                }
            }
        }
    }
}