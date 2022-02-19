using System;
using System.Collections.ObjectModel;
using System.IO;
using MessageBox = System.Windows.MessageBox;

namespace MVVM_Com
{
    internal class Drives
    {
        // Коллекция устройств
        internal ObservableCollection<DriveInfo> drivesCollection;

        // Коллекция характеристик диска
        internal ObservableCollection<DriveItem> leftDrives, rightDrives;

        // Новый список дисков
        private ObservableCollection<DriveInfo> newDrivesCollection;

        public Drives()
        {
            InsertRemove insertRemove = new InsertRemove();
            insertRemove.EventInsert += AddDisk;
            insertRemove.EventRemove += RemoveDisk;

            CreateDrivesList();
        }

        private void AddDisk()
        {
            newDrivesCollection = new ObservableCollection<DriveInfo>(DriveInfo.GetDrives());

            int numberDisk = 0;

            // Цикл ищет отличия нового списка дисков от предыдущего, чтобы определить какой именно диск добавился
            // новый список всегда Больше старого - т.к. диск уже добавлен

            for (int x = 0; x < drivesCollection.Count; x++)
            {
                //если имя старого диска не совпадает с именем нового - значит искомый диск есть данный диск нового списка
                if (drivesCollection[x].Name != newDrivesCollection[x].Name)
                {
                    //MessageBox.Show("added disk is " + newDrivesCollection[x].Name);
                    numberDisk = x;

                    break;
                }

                //если старый список заканчивается а различий не найдено -значит искомый диск это след. диск нового списка
                if (x == drivesCollection.Count - 1)
                {
                    // MessageBox.Show("added disk is " + newDrivesCollection[x].Name);
                    numberDisk = newDrivesCollection.Count - 1;
                    break;
                }
            }

            string selection = newDrivesCollection[numberDisk].DriveType.ToString();

            // Поскольку ObservableCollection создан в потоке пользовательского интерфейса, то и изменять его можно только из
            // потока пользовательского интерфейса, а не из других потоков. Чтобы изменить  ObservableCollection из другого потока
            // надо делегировать его в поток пользовательского интерфейса.

            App.Current.Dispatcher.Invoke((Action)delegate
           {
               drivesCollection.Insert(numberDisk, newDrivesCollection[numberDisk]);

               leftDrives.Insert(numberDisk, new DriveItem
               {
                   Icon = DiskIcon.Get(selection),
                   Name = newDrivesCollection[numberDisk].Name,
                   WorkPath = newDrivesCollection[numberDisk].Name
               });

               rightDrives.Insert(numberDisk, new DriveItem
               {
                   Icon = DiskIcon.Get(selection),
                   Name = newDrivesCollection[numberDisk].Name,
                   WorkPath = newDrivesCollection[numberDisk].Name
               });
           });
        }

        private void RemoveDisk()
        {
            newDrivesCollection = new ObservableCollection<DriveInfo>(DriveInfo.GetDrives());

            int numberDisk = 0;

            // Цикл ищет отличия нового списка дисков от предыдущего, чтобы определить какой именно диск удалили
            // новый список всегда МЕНЬШЕ старого - т.к. диск уже удален

            for (int x = 0; x < newDrivesCollection.Count; x++)
            {
                //если имя нового диска не совпадает с именем старого - значит искомый диск это диск старого списка
                if (drivesCollection[x].Name != newDrivesCollection[x].Name)
                {
                    MessageBox.Show("deleted disk is " + drivesCollection[x].Name);
                    numberDisk = x;

                    break;
                }

                // Если новый список заканчивается а различий не найдено - значит искомый диск это след. диск старого списка

                if (x == newDrivesCollection.Count - 1)
                {
                    numberDisk = drivesCollection.Count - 1;
                    MessageBox.Show("deleted disk is " + drivesCollection[numberDisk].Name);
                    break;
                }
            }

            App.Current.Dispatcher.Invoke((Action)delegate
              {
                  drivesCollection = newDrivesCollection;
                  leftDrives.Remove(leftDrives[numberDisk]);
                  rightDrives.Remove(rightDrives[numberDisk]);
              });
        }

        private void CreateDrivesList()  // Метод формирует список дисков
        {
            // Получаем список дисков.
            drivesCollection = new ObservableCollection<DriveInfo>(DriveInfo.GetDrives());

            leftDrives = new ObservableCollection<DriveItem>();  // Коллекция характеристик диска
            rightDrives = new ObservableCollection<DriveItem>();

            for (int x = 0; x < drivesCollection.Count; x++)  // drivesCollection.Count - число дисков
            {
                // Если устройство готово

                bool selection = true;

                try
                {
                    selection = drivesCollection[x].IsReady;
                }
                catch (Exception e)
                {
                    MessageBox.Show(e.ToString());
                }

                string driveType = drivesCollection[x].DriveType.ToString();

                switch (selection)
                {
                    case true:

                        leftDrives.Add(new DriveItem
                        {
                            Icon = DiskIcon.Get(driveType), // Присваиваем иконки согласно типу диска
                            Name = drivesCollection[x].Name,
                            WorkPath = (Pathes.LeftPath.Substring(0, 3) == drivesCollection[x].Name) ? Pathes.LeftPath : drivesCollection[x].Name
                        });

                        rightDrives.Add(new DriveItem
                        {
                            Icon = DiskIcon.Get(driveType),
                            Name = drivesCollection[x].Name,
                            WorkPath = (Pathes.RightPath.Substring(0, 3) == drivesCollection[x].Name) ? Pathes.RightPath : drivesCollection[x].Name
                        });

                        break;

                    case false:

                        leftDrives.Add(new DriveItem
                        {
                            Icon = DiskIcon.Get(driveType),
                            Name = drivesCollection[x].Name,
                        });

                        rightDrives.Add(new DriveItem
                        {
                            Icon = DiskIcon.Get(driveType),
                            Name = drivesCollection[x].Name,
                        });

                        break;
                }
            }
        }
    }
}