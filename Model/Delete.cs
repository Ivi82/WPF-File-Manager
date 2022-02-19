using System;
using System.Collections.Generic;
using System.IO;
using System.Threading.Tasks;
using MessageBox = System.Windows.MessageBox;

namespace MVVM_Com
{
    internal abstract class Delete
    {
        internal abstract void CheckPath(string str);

        internal Delete(List list)
        {
            temp = new List<string>();

            for (int x = 0; x < list.list.Count; x++)
            {
                // Если случайно выделен <↑↑↑>
                if (list.list[x].Name == "<↑↑↑>") continue;

                // Если элемент выбран
                if (list.list[x].IsSelected) temp.Add(list.list[x].FullName);
            }
        }

        internal event DelegWithStringParam EventItemIsSelected;

        internal event Delegate EventExit;

        internal List<string> temp;

        internal static void BreakAttributes(string nameItem)
        {
            if ((File.GetAttributes(nameItem) & FileAttributes.ReadOnly) == FileAttributes.ReadOnly)

                File.SetAttributes(nameItem, File.GetAttributes(nameItem) & ~FileAttributes.ReadOnly);
        }

        internal async void Run()

        {
            await Task.Run(() =>

           {
               for (int x = 0; x < temp.Count; x++)

               {
                   // Если это каталог и он существует
                   if (Directory.Exists(temp[x]))
                   {
                       EventItemIsSelected(temp[x]); // Создаем событие и посылаем имя удаляемого файла\каталога

                       // Если katalog readonly - меняем аттрибут
                       BreakAttributes(temp[x]);

                       try
                       {
                           Directory.Delete(temp[x], true); // Удаление с содержимым

                           CheckPath(temp[x]);
                       }
                       catch (Exception e) { MessageBox.Show(e.Message); }

                       //Thread.Sleep(5000);
                   }

                   // Если это файл и он существует
                   if (File.Exists(temp[x]))
                   {
                       EventItemIsSelected(temp[x]); //Создаем событие и посылаем имя удаляемого файла\каталога

                       // Eсли файл readonly - меняем аттрибут
                       BreakAttributes(temp[x]);

                       try
                       {
                           File.Delete(temp[x]);
                       }
                       catch (Exception e) { MessageBox.Show(e.Message); }

                      // System.Threading.Thread.Sleep(2000);
                   }
               }
           });

            EventExit();
        }
    }
}