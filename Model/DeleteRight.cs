using System.IO;

namespace MVVM_Com
{
    internal class DeleteRight : Delete
    {
        internal DeleteRight(List list) : base(list)
        {
        }

        internal override void CheckPath(string str)
        {
            if (Pathes.LeftPath.Contains(str))

                Pathes.LeftPath = Directory.GetParent(str).ToString() + "\\";
        }
    }
}