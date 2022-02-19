using System;
using System.Collections.Generic;
using System.IO;

namespace MVVM_Com
{
    internal static class ItemInformation
    {
        public static List<String> GetInfo(Item item)
        {
            List<String> y = new List<String>();

            if (item.CatOrFile == "file")

            {
                FileInfo f = new FileInfo(item.FullName);

                y.Add("Файл: " + f.Name);
                y.Add("Размер: " + f.Length);
                y.Add("Аттрибуты: " + f.Attributes);
                y.Add("Время создания: " + f.CreationTime);
                y.Add("Время изменения: " + f.LastWriteTime);
                y.Add("Время последнего открытия: " + f.LastAccessTime);
                y.Add("Расширение: " + f.Extension);
                y.Add("Директория: " + f.Directory);

                if (f.IsReadOnly)
                    y.Add("Только для чтения: ДА");
                else
                    y.Add("Только для чтения: НЕТ");
            }

            if (item.CatOrFile == "catalog")

            {
                DirectoryInfo f = new DirectoryInfo(item.FullName);

                y.Add("Каталог: " + f.Name);
                y.Add("Аттрибуты: " + f.Attributes);
                y.Add("Время создания: " + f.CreationTime);
                y.Add("Время изменения: " + f.LastWriteTime);
                y.Add("Время последнего открытия: " + f.LastAccessTime);
                y.Add("Расширение: " + f.Extension);
            }

            return y;
        }
    }
}