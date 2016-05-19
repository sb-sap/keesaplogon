/*
  Copyright (C) 2016 Marko Graf
*/

using System;
using System.Drawing;
using System.IO;
using System.Runtime.InteropServices;



namespace KeeSAPLogon
{
    class IconExtractor
    {
        public enum IconSource
        {
            CommonPropSheet,
            Devices,
            IE,
            Network,
            Shell,
            WinImg
        }


        private const string IconSrcCommonPropSheet = "%windir%\\system32\\compstui.dll";
        private const string IconSrcDevice = "%windir%\\system32\\ddores.dll";
        private const string IconSrcIE = "%windir%\\system32\\ieframe.dll";
        private const string IconSrcNetwork = "%windir%\\system32\\netshell.dll";
        private const string IconSrcShell = "%windir%\\system32\\shell32.dll";
        private const string IconSrcWinImg = "%windir%\\system32\\imageres.dll";


        //---------------------------------------------------------------------------------------------------
        // Public Static Methods
        //---------------------------------------------------------------------------------------------------
        public static Icon GetOpenFolderIcon()
        {
            Icon i = IconExtractor.Extract(GetSourceLocation(IconSource.Shell), 4, true);

            if (i == null)
            {
                return SystemIcons.Application;

            }

            return i;
        }

        public static Icon GetSettingsIcon()
        {
            Icon i = IconExtractor.Extract(GetSourceLocation(IconSource.Shell), 21, true);

            if (i == null)
            {
                return SystemIcons.Application;

            }

            return i;
        }


        private static Icon Extract(string file, int number, bool largeIcon)
        {
            IntPtr large;
            IntPtr small;
            ExtractIconEx(file, number, out large, out small, 1);

            try
            {
                return Icon.FromHandle(largeIcon ? large : small);
            }
            catch
            {
                return null;
            }

        }

        [DllImport("Shell32.dll", EntryPoint = "ExtractIconExW", CharSet = CharSet.Unicode, ExactSpelling = true, CallingConvention = CallingConvention.StdCall)]
        private static extern int ExtractIconEx(string sFile, int iIndex, out IntPtr piLargeVersion, out IntPtr piSmallVersion, int amountIcons);


        private static string GetSourceLocation(IconSource source)
        {
            string fileLoc = GetIconSource(source);
            if (fileLoc == null)
            {
                return null;
            }

            fileLoc = Environment.ExpandEnvironmentVariables(fileLoc);
            FileInfo fileInfo = new FileInfo(fileLoc);
            if (!fileInfo.Exists)
            {
                return null;
            }

            return fileLoc;
        }

        private static string GetIconSource(IconSource source)
        {
            switch (source)
            {
                case IconSource.CommonPropSheet:
                    return IconSrcCommonPropSheet;

                case IconSource.Devices:
                    return IconSrcDevice;

                case IconSource.IE:
                    return IconSrcIE;

                case IconSource.Network:
                    return IconSrcNetwork;

                case IconSource.Shell:
                    return IconSrcShell;

                case IconSource.WinImg:
                    return IconSrcWinImg;

                default:
                    return null;
            }
        }

    }
}
