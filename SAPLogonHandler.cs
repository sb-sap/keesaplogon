/*
  Copyright (C) 2016 Marko Graf
*/

using System.Collections.Generic;
using System.Diagnostics;
using System.IO;

using KeePassLib.Security;



namespace KeeSAPLogon
{
    public class SAPLogonHandler
    {
        public static string SAPGUIShortCutEXE = "sapshcut.exe";

        private static string m_sapguiPath = "";


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
                // "C:\Program Files (x86)\SAP\FrontEnd\SAPgui\sapshcut" -system=ABC -client=010 -user=logonname -pw=pass -language=DE -maxgui -command=SE10
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

    }
}
