/*
  Copyright (C) 2016 Marko Graf
*/

using System;
using System.Diagnostics;
using System.Windows.Forms;

using KeePass.Plugins;
using KeePass.UI;
using KeePass.Util.Spr;
using KeePassLib;
using KeePassLib.Security;



namespace KeeSAPLogon
{
    public sealed class LogonColumnProvider : ColumnProvider, IDisposable
    {
        private static string[] m_vColNames = new string[] { Translatable.ColumnName };

        private readonly IPluginHost m_host = null;
        private readonly SAPLogonOpt m_config = null;


        //---------------------------------------------------------------------------------------------------
        // Class Constructors
        //---------------------------------------------------------------------------------------------------
        public LogonColumnProvider(IPluginHost host, SAPLogonOpt config)
        {
            if (host == null)
            {
                string msg = "No plugin host defined.";
                Debug.Assert((host == null), msg);
                throw new ApplicationException(msg);
            }

            if (config == null)
            {
                string msg = "No option handler defined.";
                Debug.Assert((config == null), msg);
                throw new ApplicationException(msg);
            }

            m_host = host;
            m_config = config;
        }


        //---------------------------------------------------------------------------------------------------
        // Interface Implementation: KeePass.UI.ColumnProvider
        //---------------------------------------------------------------------------------------------------
        #region KeePass.UI.ColumnProvider implementation

        public override string[] ColumnNames
        {
            get { return m_vColNames; }
        }


        public override HorizontalAlignment TextAlign
        {
            get { return HorizontalAlignment.Left; }
        }


        public override string GetCellData(string strColumnName, PwEntry pe)
        {
            if (strColumnName == null)
            {
                Debug.Assert(false);
                return String.Empty;
            }

            if (strColumnName != m_vColNames[0]) return String.Empty;

            if (pe == null)
            {
                Debug.Assert(false);
                return String.Empty;
            }


            if (!SAPLogonHandler.ValidateSAPGUIPath(m_config.SAPGUIPath))
            {
                return Translatable.ColumnConfigFailure;
            }


            //detect SAP ID and SAP client
            string strCellData = "";
            LogonColumn lc = GetSAPLogonData(strColumnName, pe);
            if (lc.IsValid())
            {
                strCellData = FormattedCellData(lc);
            }
            else
            {
                if (lc.HasSAPID() || lc.HasSAPClient() || lc.HasSAPLanguage() || lc.HasSAPTransaction())
                {
                    strCellData = Translatable.ColumnInvalidData;
                }
                else
                {
                    strCellData = Translatable.ColumnNoneSAPData;
                }
            }

            return strCellData;
        }


        public override void PerformCellAction(string strColumnName, PwEntry pe)
        {
            LogonColumn lc = GetSAPLogonData(strColumnName, pe);
            if (lc.IsValid())
            {
                lc.ExtendWithDefaults(m_config.DefaultLng, m_config.DefaultTx);

                ProtectedString pUser = DerefValue(PwDefs.UserNameField, pe);
                ProtectedString pPw = DerefValue(PwDefs.PasswordField, pe);

                SAPLogonHandler.SAPGUIPath = m_config.SAPGUIPath;
                SAPLogonHandler.DoLogon(lc, pUser, pPw);
            }
        }


        public override bool SupportsCellAction(string strColumnName)
        {
            if (strColumnName == null)
            {
                Debug.Assert(false);
                return false;
            }

            if (strColumnName != m_vColNames[0]) return false;

            return SAPLogonHandler.ValidateSAPGUIPath(m_config.SAPGUIPath);
        }

        #endregion


        //---------------------------------------------------------------------------------------------------
        // Interface Implementation: IDisposable
        //---------------------------------------------------------------------------------------------------
        #region IDisposable implementation

        public void Dispose()
        {
            m_vColNames = null;
        }

        #endregion


        //---------------------------------------------------------------------------------------------------
        // Private Methods
        //---------------------------------------------------------------------------------------------------
        private string FormattedCellData(LogonColumn entry)
        {
            string result = "";

            if (entry.HasSAPID() && m_config.DisSystemID)
            {
                result = result + entry.SAPID;
                if (entry.HasSAPClient() && m_config.DisClient) result = result + ":";
            }

            if (entry.HasSAPClient() && m_config.DisClient)
            {
                result = result + entry.SAPClient;
            }

            if (entry.HasSAPLanguage() && m_config.DisLanguage)
            {
                result = result + " [" + entry.SAPLanguage + "]";
            }

            if (entry.HasSAPTransaction() && m_config.DisTx)
            {
                if ((entry.HasSAPID() && m_config.DisSystemID) ||
                    (entry.HasSAPClient() && m_config.DisClient) ||
                    (entry.HasSAPLanguage() && m_config.DisLanguage)) result = result + " - ";

                result = result + entry.SAPTransaction;
            }

            return result;
        }

        private LogonColumn GetSAPLogonData(string strColumnName, PwEntry pe)
        {
            if (strColumnName == null)
            {
                Debug.Assert(false);
                return null;
            }

            if (pe == null)
            {
                Debug.Assert(false);
                return null;
            }

            if (strColumnName == m_vColNames[0])
            {
                //detect SAP ID and SAP client
                string strSAPID = pe.Strings.ReadSafe(KeeSAPLogon.Properties.Resources.FieldKeySAPID);
                string strSAPClient = pe.Strings.ReadSafe(KeeSAPLogon.Properties.Resources.FieldKeySAPClient);
                string strSAPLanguage = pe.Strings.ReadSafe(KeeSAPLogon.Properties.Resources.FieldKeyLanguage);
                string strSAPTransaction = pe.Strings.ReadSafe(KeeSAPLogon.Properties.Resources.FieldKeyTransaction);

                LogonColumn lc = new LogonColumn(strSAPID, strSAPClient, strSAPLanguage, strSAPTransaction);
                return lc;
            }

            return null;
        }

        private ProtectedString DerefValue(string fieldName, PwEntry pe)
        {
            ProtectedString ps = new ProtectedString();

            string decrypted = pe.Strings.ReadSafe(fieldName);
            if (decrypted.IndexOf('{') >= 0)
            {
                if (m_host == null)
                {
                    Debug.Assert(false); return ps;
                }

                PwDatabase pd = null;
                try
                {
                    pd = m_host.MainWindow.DocumentManager.SafeFindContainerOf(pe);
                }
                catch (Exception)
                {
                    Debug.Assert(false);
                }

                SprContext ctx = new SprContext(pe, pd, (SprCompileFlags.Deref | SprCompileFlags.TextTransforms), false, false);
                ps = new ProtectedString(true, (SprEngine.Compile(decrypted, ctx)));
                decrypted = "";
                return ps;
            }
            else
            {
                ps = pe.Strings.GetSafe(fieldName);
            }

            return ps;
        }

    }
}
