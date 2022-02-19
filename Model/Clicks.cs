using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using MessageBox = System.Windows.MessageBox;

namespace MVVM_Com
{
    internal class Clicks
    {
        private static DelegWithStringParam startSelectedFile = (fileName) =>

                 {
                     try
                     {
                         Process.Start(fileName); // Windows пытается открыть файл
                   }
                     catch (Exception e)

                     {
                         MessageBox.Show(e.Message);
                     }
                 };

        internal static string Click(List list, string oldPath)
        {
            List<Item> selectedList = new List<Item>();

            for (int x = 0; x < list.list.Count; x++)
            {
                if (list.list[x].IsSelected) selectedList.Add(list.list[x]);
            }

            switch (selectedList.Count)
            {
                case 1:

                    if (selectedList[0].CatOrFile == "file")

                    {
                        startSelectedFile(selectedList[0].FullName);

                        return oldPath; // Если будет клик на файл - вернет старый путь
                    }
                    else

                        return ClickOnDirectory(selectedList[0], oldPath);

                default:

                    for (int x = 0; x < selectedList.Count; x++)

                    {
                        if (selectedList[x].CatOrFile == "file") startSelectedFile(selectedList[x].FullName);
                    }

                    return oldPath; // Если будет клик на файл - вернет старый путь
            }
        }

        internal static string ClickOnDirectory(Item selectedItem, string oldPath)
        {
            string returnPath = oldPath;

            if (selectedItem.Name == "<↑↑↑>")
            {
                oldPath = oldPath.TrimEnd(new char[] { '\\' });

                returnPath = Directory.GetParent(oldPath).ToString().Length != 3 ? (Directory.GetParent(oldPath) + "\\") : (Directory.GetParent(oldPath).ToString());
            }

            if (selectedItem.CatOrFile == "catalog" && Directory.Exists(selectedItem.FullName))
                returnPath = selectedItem.FullName + "\\";

            return returnPath;
        }

        public static string ClickOnPath(string newPath, string oldPath)
        {
            if (File.Exists(newPath)) startSelectedFile(newPath);
            else if (Directory.Exists(newPath))

            {
                //если имя каталога без \ - добавляем \ для красоты
                if (newPath.Substring(newPath.Length - 1) != "\\")
                    newPath += "\\";

                return newPath;
            }
            else return oldPath;

            return Directory.GetParent(newPath).ToString() + "\\";
        }
    }
}