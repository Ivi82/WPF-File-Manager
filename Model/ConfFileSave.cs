using System.IO;

namespace MVVM_Com
{
    internal static class Confile
    {
        internal static void Save()

        {
            StreamWriter sw = new StreamWriter("pathes.cfg", false);
            sw.WriteLine(Pathes.LeftPath);
            sw.WriteLine(Pathes.RightPath);
            sw.WriteLine(Theme.Color);
            sw.Close();
        }
    }
}