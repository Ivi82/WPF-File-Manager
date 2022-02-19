using System;
using System.Windows.Media.Imaging;

namespace MVVM_Com
{
    internal static class DiskIcon
    {
        internal static BitmapFrame Get(string driveType)
        {
            BitmapFrame tempIcon = null;
            switch (driveType)
            {
                case "Fixed":

                    tempIcon = BitmapFrame.Create(new Uri("white.png", UriKind.RelativeOrAbsolute));

                    break;

                case "Network":

                    tempIcon = BitmapFrame.Create(new Uri("white-net.png", UriKind.RelativeOrAbsolute));

                    break;

                case "Removable":

                    tempIcon = BitmapFrame.Create(new Uri("usb.png", UriKind.RelativeOrAbsolute));

                    break;

                case "CDRom":

                    tempIcon = BitmapFrame.Create(new Uri("cd.png", UriKind.RelativeOrAbsolute));

                    break;
            }

            return tempIcon;
        }
    }
}