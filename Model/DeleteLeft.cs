using System.IO;

namespace MVVM_Com
{
    internal class DeleteLeft : Delete
    {
        internal DeleteLeft(List list) : base(list)
        {
        }

        internal override void CheckPath(string str)
        {
            if (Pathes.RightPath.Contains(str))

                Pathes.RightPath = Directory.GetParent(str).ToString() + "\\";
        }
    }
}