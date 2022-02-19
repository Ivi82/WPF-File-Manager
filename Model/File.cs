using System;
using System.IO;
using MessageBox = System.Windows.MessageBox;

namespace MVVM_Com
{
    internal static class FILE
    {
        internal static bool Rename(Item selectedItem, string newName)

        {
            if (File.Exists(selectedItem.FullName))

            {
                Move(selectedItem.FullName, newName);

                return true;
            }

            return false;
        }

        internal static void Move(string oldName, string newName)
        {
            try
            {
                File.Move(oldName, newName);
            }
            catch (Exception e)
            {
                MessageBox.Show(e.Message);
            }
        }

        internal static void Delete(string fileName)
        {
            try
            {
                File.Delete(fileName);
            }
            catch (Exception e)
            {
                MessageBox.Show(e.Message);
            }
        }

        public static void Copy(string source, string destination)
        {
            try
            {
                File.Copy(source, destination, true); // Копируем файл
            }
            catch (Exception e)
            {
                MessageBox.Show(e.Message);
            }
        }
    }
}