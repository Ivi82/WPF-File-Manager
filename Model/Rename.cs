namespace MVVM_Com
{
    internal class Rename
    {
        internal Rename(Item selectedItem, string newName)
        {
            switch (selectedItem.CatOrFile)
            {
                case "catalog":

                    FOLDER.Rename(selectedItem, newName);

                    break;

                case "file":

                    FILE.Rename(selectedItem, newName);

                    break;
            }
        }
    }
}