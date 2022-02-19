namespace MVVM_Com
{
    // Класс описывающий пути
    internal static class Pathes
    {
        internal static event Delegate EventChangesLeftPath;

        internal static event Delegate EventChangesRightPath;

        private static string leftPath;

        internal static string LeftPath
        {
            get { return leftPath; }
            set
            {
                leftPath = value;
                if (EventChangesLeftPath != null) EventChangesLeftPath();
            }
        }

        private static string rightPath;

        internal static string RightPath
        {
            get { return rightPath; }
            set
            {
                rightPath = value;
                if (EventChangesRightPath != null) EventChangesRightPath();
            }
        }
    }
}