using System;
using System.IO;
using MessageBox = System.Windows.MessageBox;

namespace MVVM_Com

{
    internal static class FOLDER
    {
        internal static bool Create(string pathName, string folderName)
        {
            if (!Directory.Exists(pathName + folderName))
            {
                try
                {
                    Directory.CreateDirectory(pathName + folderName);
                }
                catch (Exception e)
                {
                    MessageBox.Show(e.Message);
                }
                return true;
            }

            return false;
        }

        internal static void Create(string destination)
        {
            try
            {
                Directory.CreateDirectory(destination);
            }
            catch (Exception e)
            {
                MessageBox.Show(e.Message);
            }
        }

        internal static void Move(string oldName, string newName)
        {
            try
            {
                Directory.Move(oldName, newName);
            }
            catch (Exception e)
            {
                MessageBox.Show(e.Message);
            }
        }

        internal static bool Rename(Item selectedItem, string newName)

        {
            if (Directory.Exists(selectedItem.FullName))

            {
                Move(selectedItem.FullName, newName);

                return true;
            }

            return false;
        }
    }
}