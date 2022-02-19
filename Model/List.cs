using System;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.IO;
using System.Windows.Data;
using MessageBox = System.Windows.MessageBox;

namespace MVVM_Com
{
    internal class List
    {
        // Коллекция элементов списка (файлов и каталогов)
        internal ObservableCollection<Item> list;

        internal int filesCount, catalogsCount;

        private ListSortDirection lastDirection = ListSortDirection.Ascending;
        private ListSortDirection direction;

        public List()
        {
            list = new ObservableCollection<Item>();
        }

        internal void Sort(string columnName)
        {
            ICollectionView view = CollectionViewSource.GetDefaultView(list);

            view.SortDescriptions.Clear();

            direction = lastDirection == ListSortDirection.Ascending ? ListSortDirection.Descending : ListSortDirection.Ascending;

            view.SortDescriptions.Add(new SortDescription("NeedSorting", ListSortDirection.Ascending));
            view.SortDescriptions.Add(new SortDescription("Extension", direction));
            view.SortDescriptions.Add(new SortDescription(columnName, direction));

            lastDirection = direction;
        }

        internal void AllIsSelected()
        {
            for (int x = 0; x < list.Count; x++)
            {
                list[x].IsSelected = true;
                //MessageBox.Show("all selected");
            }
        }

        internal void AllIsUnSelected()
        {
            for (int x = 0; x < list.Count; x++)
            {
                list[x].IsSelected = false;
                //  MessageBox.Show("all unselected");
            }
        }

        internal void Load(string pathForLoad)
        {
            list.Clear(); // Очищаем список

            if (!Directory.Exists(pathForLoad)) MessageBox.Show("Директории " + pathForLoad + " не существует!");

            DirectoryInfo[] catalogs;

            // Получаем списки каталогов
            try
            {
                catalogs = new DirectoryInfo(pathForLoad).GetDirectories();
            }

            // если ошибка - меняем путь на ступеньку выше и по новому пути получаем списки каталогов
            catch (Exception e)
            {
                MessageBox.Show("Каталог. " + e.Message);

                pathForLoad = Directory.GetParent(pathForLoad).ToString();

                catalogs = new DirectoryInfo(pathForLoad).GetDirectories();
            }

            // Получаем списки файлов. здесь по идее не должно возникнуть исключений, если с каталогом все ок

            FileInfo[] files = new DirectoryInfo(pathForLoad).GetFiles();

            if (pathForLoad.Length != 3) //Если не в корне диска - добавляем строчку <↑↑↑>
            {
                list.Add(new Item
                {
                    CatOrFile = "catalog",
                    Name = "<↑↑↑>",
                    Form = "",
                    Parent = Directory.GetParent(pathForLoad).ToString(),
                    NeedSorting = "No",
                });
            }

            // Добавляем каталоги в коллекцию
            for (int x = 0; x < catalogs.Length; x++)
            {
                list.Add(new Item
                {
                    CatOrFile = "catalog",
                    Name = catalogs[x].Name,
                    Extension = "[ КАТАЛОГ ]",
                    Data = catalogs[x].LastWriteTime,
                    FullName = catalogs[x].FullName,
                    Parent = catalogs[x].Parent.Name,
                    NeedSorting = "Yes",
                });
            }

            // Добавляем файлы в коллекцию
            for (int x = 0; x < files.Length; x++)
            {
                // string[] shortName = files[x].Name.Split(new char[] { '.' }); // Делит название файла на 2 части по символу '.'

                string tempName = (files[x].Extension.Length != 0) ? files[x].Name.Remove(files[x].Name.Length - files[x].Extension.Length) : files[x].Name;
                list.Add(new Item
                {
                    CatOrFile = "file",
                    Name = tempName,                  // первая часть  массива [0] будет коротким именем файла без расширения
                    Extension = files[x].Extension,   // расширение
                    Size = files[x].Length.ToString(),
                    Data = files[x].LastWriteTime,
                    FullName = files[x].FullName,
                    NeedSorting = "Yes",
                });
            }

            filesCount = files.Length;
            catalogsCount = catalogs.Length;
        }
    }
}