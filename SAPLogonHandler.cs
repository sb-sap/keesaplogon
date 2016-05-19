/*
  Copyright (C) 2016 Marko Graf
*/

using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Windows.Forms;

using Microsoft.Win32;

using KeePassLib.Security;



namespace KeeSAPLogon
{
    public class SAPLogonHandler
    {
        public static string SAPGUIShortCutEXE = "sapshcut.exe";
        private static string m_sapguiPath = "";


        //---------------------------------------------------------------------------------------------------
        // Public Static Methods
        //---------------------------------------------------------------------------------------------------
        public static string SAPGUIPath
        {
            get { return m_sapguiPath; }
            set { m_sapguiPath = value; }
        }


        public static bool DoLogon(LogonColumn logonPoint, ProtectedString user, ProtectedString password)
        {
            if (ValidateSAPGUIPath(m_sapguiPath) && logonPoint.IsValid())
            {
                //Example:
                //
                // "C:\Program Files (x86)\SAP\FrontEnd\SAPgui\sapshcut" -system=ABC -client=010 -user=username -pw=pass -language=EN -maxgui -command=SE10
                //

                string strArgs = "-maxgui";

                Dictionary<string, string> args = new Dictionary<string, string>();
                args.Add("-system", logonPoint.SAPID);
                args.Add("-client", logonPoint.SAPClient);
                args.Add("-user", user.ReadString());
                args.Add("-pw", password.ReadString());
                args.Add("-language", logonPoint.SAPLanguage);
                args.Add("-command", logonPoint.SAPTransaction);

                foreach (string key in args.Keys)
                {
                    string value;
                    args.TryGetValue(key, out value);
                    strArgs = strArgs + " ";
                    strArgs = strArgs + key + "=" + value;
                }

                string fileLoc = Path.Combine(m_sapguiPath, SAPLogonHandler.SAPGUIShortCutEXE);
                FileInfo fileInfo = new FileInfo(fileLoc);
                ProcessStartInfo info = new ProcessStartInfo(fileInfo.FullName);
                info.Arguments = strArgs;
                info.CreateNoWindow = false;
                info.UseShellExecute = true;
                info.ErrorDialog = true;
                info.RedirectStandardInput = false;
                info.RedirectStandardOutput = false;

                Process process = Process.Start(info);

                return !(process.HasExited);
            }

            return false;
        }


        public static bool ValidateSAPGUIPath(string path)
        {
            //Validating if 'sapshcut.exe' located by given path
            string fileLoc = Path.Combine(path, SAPLogonHandler.SAPGUIShortCutEXE);
            FileInfo fileInfo = new FileInfo(fileLoc);
            return (fileInfo.Exists);
        }


        //---------------------------------------------------------------------------------------------------
        // Implementation: Detect SAP GUI installation automatically via Windows registry
        //---------------------------------------------------------------------------------------------------
        //***************************************************************************************************
        //*                                                                                                 *
        //*   Special thanks to ANatrix for the idea how to retrieve SAPGUI installation path.              *
        //*                                                                                                 *
        //***************************************************************************************************
        #region SAPGUIDetection

        public static string DetectSAPGUIPath()
        {
            RegistryKey rootKey = RegistryKey.OpenRemoteBaseKey(Microsoft.Win32.RegistryHive.LocalMachine, "");
            RegistryKey subKey = null;

            bool foundPath = true;
            object objPath;
            string sPath = "";
            string resPath = "";

            try
            {
                // Check path from registry for x86
                subKey = rootKey.OpenSubKey("SOFTWARE\\Microsoft\\Windows\\CurrentVersion\\App Paths\\" + SAPGUIShortCutEXE);
                objPath = subKey.GetValue("Path");
                sPath = Convert.ToString(objPath);
            }
            catch
            {
                foundPath = false;
            };

            if (!foundPath)
            {
                try
                {
                    // Check path from registry for 64bit
                    subKey = rootKey.OpenSubKey("SOFTWARE\\Wow6432Node\\Microsoft\\Windows\\CurrentVersion\\App Paths\\" + SAPGUIShortCutEXE);
                    objPath = subKey.GetValue("Path");
                    sPath = Convert.ToString(objPath);
                }
                catch
                {
                    foundPath = false;
                }
            }

            if (foundPath)
            {
                for (int i = 0; ((i < sPath.Length) && (sPath[i] != ';')); i++)
                {
                    resPath += sPath[i].ToString();
                }

                if (resPath.Length < 3)  // "C:\"
                {
                    foundPath = false;
                }
                else
                {
                    if (SAPLogonHandler.ValidateSAPGUIPath(resPath)) return resPath;
                }
            }
            else
            {
                MessageBox.Show("SAPGUI not installed!", KeeSAPLogonExt.PlugInName + " settings error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

            return resPath;

        } //DetectSAPGUIPath

        #endregion

    }
}
