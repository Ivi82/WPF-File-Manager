namespace MVVM_Com
{
    internal delegate void Delegate();

    internal delegate void DelegWithStringParam(string str);

    internal delegate void DelegWith2StringParam(string str1, string str2);

    internal delegate void ParamDelegate(List list, string str, Delegate prosto);

    internal delegate void ParamDelegate2(Item selectedItem, string panelPart, Delegate prosto);

    internal delegate void DelegateTwoParam(string str, Delegate deleg);

    internal static class ShowWindow
    {
        internal static event ParamDelegate EventShowDeleteWindow;

        internal static event ParamDelegate EventShowMoveWindow;

        internal static event DelegateTwoParam EventShowNewFolderWindow;

        internal static event ParamDelegate2 EventShowRenameItemsWindow, EventShowAttribItemWindow;

        internal static event ParamDelegate EventShowCopyWindow;

        internal static void Delete(List list, string panelPart, Delegate refreshWindow)
        {
            EventShowDeleteWindow(list, panelPart, refreshWindow);
        }

        internal static void Move(List list, string panelPart, Delegate refreshWindow)
        {
            EventShowMoveWindow(list, panelPart, refreshWindow);
        }

        internal static void NewFolder(string Path, Delegate refreshWindow)
        {
            EventShowNewFolderWindow(Path, refreshWindow);
        }

        internal static void RenameItem(Item selectedItem, string panelPart, Delegate refreshWindow)
        {
            EventShowRenameItemsWindow(selectedItem, panelPart, refreshWindow);
        }

        internal static void AttribItem(Item selectedItem, string panelPart, Delegate refreshWindow)
        {
            EventShowAttribItemWindow(selectedItem, panelPart, refreshWindow);
        }

        internal static void Copy(List list, string panelPart, Delegate refreshWindow)
        {
            EventShowCopyWindow(list, panelPart, refreshWindow);
        }
    }
}