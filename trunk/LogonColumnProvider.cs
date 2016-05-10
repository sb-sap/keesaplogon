/*
  Copyright (C) 2016 Marko Graf
*/

using System;
using System.Diagnostics;
using System.Windows.Forms;

using KeePass.UI;
using KeePassLib;
using KeePassLib.Security;



namespace KeeSAPLogon
{
    public sealed class LogonColumnProvider : ColumnProvider, IDisposable
    {
        private static string[] m_vColNames = new string[] { Translatable.ColumnName };

        private readonly SAPLogonOpt m_config = null;



        //Constructor
        public LogonColumnProvider(SAPLogonOpt config)
        {
            if (config == null)
            {
                Debug.Assert((config == null), "No option handler defined.");
                throw new ApplicationException("No option handler defined.");
            }

            m_config = config;
        }


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
                return string.Empty;
            }

            if (strColumnName != m_vColNames[0]) return string.Empty;

            if (pe == null)
            {
                Debug.Assert(false);
                return string.Empty;
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

                ProtectedString pUser = pe.Strings.GetSafe(PwDefs.UserNameField);
                ProtectedString pPw = pe.Strings.GetSafe(PwDefs.PasswordField);

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

    }
}
