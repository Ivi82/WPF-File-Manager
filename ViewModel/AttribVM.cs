using System.IO;

namespace MVVM_Com
{
    internal class AttribVm
    {
        private Item item;

        public string ItemName
        {
            get { return item.FullName; }
        }

        private bool arch;

        public bool Arch
        {
            get
            {
                if ((File.GetAttributes(item.FullName) & FileAttributes.Archive) == FileAttributes.Archive) arch = true;
                else arch = false;

                return arch;
            }

            set
            {
                if (arch)

                {
                    arch = value; File.SetAttributes(item.FullName, File.GetAttributes(item.FullName) & ~FileAttributes.Archive);
                }
                else
                {
                    arch = value;

                    File.SetAttributes(item.FullName, File.GetAttributes(item.FullName) | FileAttributes.Archive);
                }
            }
        }

        private bool comp;

        public bool Comp
        {
            get
            {
                if ((File.GetAttributes(item.FullName) & FileAttributes.Compressed) == FileAttributes.Compressed) comp = true;
                else comp = false;

                return comp;
            }
            set
            {
                if (comp)

                {
                    comp = value; File.SetAttributes(item.FullName, File.GetAttributes(item.FullName) & ~FileAttributes.Compressed);
                }
                else
                {
                    comp = value;

                    File.SetAttributes(item.FullName, File.GetAttributes(item.FullName) | FileAttributes.Compressed);
                }
            }
        }

        private bool dev;

        public bool Dev
        {
            get
            {
                if ((File.GetAttributes(item.FullName) & FileAttributes.Device) == FileAttributes.Device) { dev = true; }
                else { dev = false; }

                return dev;
            }
            set
            {
                if (dev)

                {
                    dev = value; File.SetAttributes(item.FullName, File.GetAttributes(item.FullName) & ~FileAttributes.Device);
                }
                else
                {
                    dev = value;

                    File.SetAttributes(item.FullName, File.GetAttributes(item.FullName) | FileAttributes.Device);
                }
            }
        }

        private bool dir;

        public bool Dir
        {
            get
            {
                if ((File.GetAttributes(item.FullName) & FileAttributes.Directory) == FileAttributes.Directory) { dir = true; }
                else { dir = false; }

                return dir;
            }
            set
            {
                if (dir)

                {
                    dir = value; File.SetAttributes(item.FullName, File.GetAttributes(item.FullName) & ~FileAttributes.Directory);
                }
                else
                {
                    dir = value; File.SetAttributes(item.FullName, File.GetAttributes(item.FullName) | FileAttributes.Directory);
                }
            }
        }

        private bool enc;

        public bool Enc
        {
            get
            {
                if ((File.GetAttributes(item.FullName) & FileAttributes.Directory) == FileAttributes.Encrypted) { enc = true; }
                else { enc = false; }

                return enc;
            }
            set
            {
                if (enc)

                {
                    enc = value; File.SetAttributes(item.FullName, File.GetAttributes(item.FullName) & ~FileAttributes.Encrypted);
                }
                else
                {
                    enc = value; File.SetAttributes(item.FullName, File.GetAttributes(item.FullName) | FileAttributes.Encrypted);
                }
            }
        }

        private bool hid;

        public bool Hid

        {
            get
            {
                if ((File.GetAttributes(item.FullName) & FileAttributes.Hidden) == FileAttributes.Hidden) { hid = true; }
                else { hid = false; }

                return hid;
            }
            set
            {
                if (hid)

                {
                    hid = value; File.SetAttributes(item.FullName, File.GetAttributes(item.FullName) & ~FileAttributes.Hidden);
                }
                else
                {
                    hid = value; File.SetAttributes(item.FullName, File.GetAttributes(item.FullName) | FileAttributes.Hidden);
                }
            }
        }

        private bool integ;

        public bool Integ
        {
            get
            {
                if ((File.GetAttributes(item.FullName) & FileAttributes.IntegrityStream) == FileAttributes.IntegrityStream) { integ = true; }
                else { integ = false; }

                return integ;
            }
            set
            {
                if (integ)

                {
                    integ = value; File.SetAttributes(item.FullName, File.GetAttributes(item.FullName) & ~FileAttributes.IntegrityStream);
                }
                else
                {
                    integ = value; File.SetAttributes(item.FullName, File.GetAttributes(item.FullName) | FileAttributes.IntegrityStream);
                }
            }
        }

        private bool norm;

        public bool Norm
        {
            get
            {
                if ((File.GetAttributes(item.FullName) & FileAttributes.Normal) == FileAttributes.Normal) { norm = true; }
                else { norm = false; }

                return norm;
            }
            set
            {
                if (norm)

                {
                    norm = value; File.SetAttributes(item.FullName, File.GetAttributes(item.FullName) & ~FileAttributes.Normal);
                }
                else
                {
                    integ = value; File.SetAttributes(item.FullName, File.GetAttributes(item.FullName) | FileAttributes.Normal);
                }
            }
        }

        private bool noscrub;

        public bool NoScrub
        {
            get
            {
                if ((File.GetAttributes(item.FullName) & FileAttributes.NoScrubData) == FileAttributes.NoScrubData) { noscrub = true; }
                else { noscrub = false; }

                return noscrub;
            }
            set
            {
                if (noscrub)

                {
                    noscrub = value; File.SetAttributes(item.FullName, File.GetAttributes(item.FullName) & ~FileAttributes.NoScrubData);
                }
                else
                {
                    noscrub = value; File.SetAttributes(item.FullName, File.GetAttributes(item.FullName) | FileAttributes.NoScrubData);
                }
            }
        }

        private bool nocont;

        public bool NoCont
        {
            get
            {
                if ((File.GetAttributes(item.FullName) & FileAttributes.NotContentIndexed) == FileAttributes.NotContentIndexed) { nocont = true; }
                else { nocont = false; }

                return nocont;
            }
            set
            {
                if (nocont)

                {
                    nocont = value; File.SetAttributes(item.FullName, File.GetAttributes(item.FullName) & ~FileAttributes.NotContentIndexed);
                }
                else
                {
                    nocont = value; File.SetAttributes(item.FullName, File.GetAttributes(item.FullName) | FileAttributes.NotContentIndexed);
                }
            }
        }

        private bool off;

        public bool Off
        {
            get
            {
                if ((File.GetAttributes(item.FullName) & FileAttributes.Offline) == FileAttributes.Offline) { off = true; }
                else { off = false; }

                return off;
            }
            set
            {
                if (off)

                {
                    off = value; File.SetAttributes(item.FullName, File.GetAttributes(item.FullName) & ~FileAttributes.Offline);
                }
                else
                {
                    off = value; File.SetAttributes(item.FullName, File.GetAttributes(item.FullName) | FileAttributes.Offline);
                }
            }
        }

        private bool rep;

        public bool Rep
        {
            get
            {
                if ((File.GetAttributes(item.FullName) & FileAttributes.ReparsePoint) == FileAttributes.ReparsePoint) { rep = true; }
                else { rep = false; }

                return rep;
            }
            set
            {
                if (rep)

                {
                    rep = value; File.SetAttributes(item.FullName, File.GetAttributes(item.FullName) & ~FileAttributes.ReparsePoint);
                }
                else
                {
                    off = value; File.SetAttributes(item.FullName, File.GetAttributes(item.FullName) | FileAttributes.ReparsePoint);
                }
            }
        }

        private bool spa;

        public bool Spa
        {
            get
            {
                if ((File.GetAttributes(item.FullName) & FileAttributes.SparseFile) == FileAttributes.SparseFile) { spa = true; }
                else { spa = false; }

                return spa;
            }
            set
            {
                if (spa)

                {
                    spa = value; File.SetAttributes(item.FullName, File.GetAttributes(item.FullName) & ~FileAttributes.SparseFile);
                }
                else
                {
                    spa = value; File.SetAttributes(item.FullName, File.GetAttributes(item.FullName) | FileAttributes.SparseFile);
                }
            }
        }

        private bool ron;

        public bool Ron
        {
            get
            {
                if ((File.GetAttributes(item.FullName) & FileAttributes.ReadOnly) == FileAttributes.ReadOnly) { ron = true; }
                else { ron = false; }

                return ron;
            }
            set
            {
                if (ron)

                {
                    ron = value; File.SetAttributes(item.FullName, File.GetAttributes(item.FullName) & ~FileAttributes.ReadOnly);
                }
                else
                {
                    ron = value; File.SetAttributes(item.FullName, File.GetAttributes(item.FullName) | FileAttributes.ReadOnly);
                }
            }
        }

        private bool sys;

        public bool Sys

        {
            get
            {
                if ((File.GetAttributes(item.FullName) & FileAttributes.System) == FileAttributes.System) { sys = true; }
                else { sys = false; }

                return sys;
            }
            set
            {
                if (sys)

                {
                    sys = value; File.SetAttributes(item.FullName, File.GetAttributes(item.FullName) & ~FileAttributes.System);
                }
                else
                {
                    sys = value; File.SetAttributes(item.FullName, File.GetAttributes(item.FullName) | FileAttributes.System);
                }
            }
        }

        private bool temp;

        public bool Temp

        {
            get
            {
                if ((File.GetAttributes(item.FullName) & FileAttributes.Temporary) == FileAttributes.Temporary) { temp = true; }
                else { temp = false; }

                return temp;
            }
            set
            {
                if (temp)

                {
                    temp = value; File.SetAttributes(item.FullName, File.GetAttributes(item.FullName) & ~FileAttributes.Temporary);
                }
                else
                {
                    temp = value; File.SetAttributes(item.FullName, File.GetAttributes(item.FullName) | FileAttributes.Temporary);
                }
            }
        }

        private RelayCommand okButton;

        public RelayCommand OkButton
        {
            get
            {
                return okButton ??
                (okButton = new RelayCommand(obj =>
                {
                    EventExit(); // Cоздаем событие
                }));
            }
        }

        public event Delegate EventExit; // событие

        internal AttribVm(Item item)
        {
            this.item = item;

            FileInfo f = new FileInfo(item.FullName);

            FileAttributes fa = File.GetAttributes(item.FullName);
        }
    }
}