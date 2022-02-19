using System;
using System.Collections.Generic;
using System.IO;

namespace MVVM_Com
{
    internal static class DriveInformation
    {
        public static List<String> GetInfo(DriveItem driveItem)
        {
            List<String> infoList = new List<String>();
            DriveInfo[] drives = DriveInfo.GetDrives();
            int numberDrive = 0;

            for (int i = 0; i < drives.Length; i++)
            {
                if (drives[i].Name == driveItem.Name)
                    numberDrive = i;
            }

            infoList.Add("Имя диска: " + drives[numberDrive].Name);
            infoList.Add("Размер диска: " + drives[numberDrive].TotalSize);
            infoList.Add("Свободно: " + drives[numberDrive].TotalFreeSpace);
            infoList.Add("Файловая система: " + drives[numberDrive].DriveFormat);
            infoList.Add("Тип: " + drives[numberDrive].DriveType);
            infoList.Add("Метка диска: " + drives[numberDrive].VolumeLabel);

            return infoList;
        }
    }
}