using System.IO;

namespace MVVM_Com
{
    internal static class ConfFileRead
    {
        private static string[] dataFromFile = new string[3];

        static ConfFileRead()
        {
            // Читаем файл pathes.cfg - 3 строки (путь для левой и правой панели, цветовое решение)
            StreamReader reader = new StreamReader("pathes.cfg");
            dataFromFile = new string[3];
            for (int x = 0; x < 3; x++)
            {
                dataFromFile[x] = reader.ReadLine();
            }

            reader.Close();

            // Если директории из файла  pathes.cfg не существуют - ставим корень первого диска
            if (!Directory.Exists(dataFromFile[0])) dataFromFile[0] = DriveInfo.GetDrives()[0].Name;
            if (!Directory.Exists(dataFromFile[1])) dataFromFile[1] = DriveInfo.GetDrives()[0].Name;

            //            // Если параметр цвета в файле pathes.cfg изменен или не существует - ставим Light
            //            if (dataFromFile[2] == "Light" || dataFromFile[2] == "Dark") Pathes.Theme = dataFromFile[2];
            //            else Pathes.Theme = "Light";
        }

        internal static string GetLeftPath()
        {
            return dataFromFile[0];
        }

        internal static string GetRightPath()
        {
            return dataFromFile[1];
        }

        internal static string GetTheme()
        {
            return dataFromFile[2];
        }
    }
}