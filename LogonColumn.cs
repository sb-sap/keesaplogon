/*
  Copyright (C) 2016 Marko Graf
*/

using System;



namespace KeeSAPLogon
{
    public sealed class LogonColumn : IComparable
    {
        private string m_SAPID;
        private string m_SAPClient;
        private string m_SAPLanguage;
        private string m_SAPTransaction;


        //---------------------------------------------------------------------------------------------------
        // Class Constructors
        //---------------------------------------------------------------------------------------------------
        public LogonColumn(string ID, string Client, string Language, string Transaction)
        {
            m_SAPID = ID;
            m_SAPClient = Client;
            m_SAPLanguage = Language;
            m_SAPTransaction = Transaction;
        }
        

        //---------------------------------------------------------------------------------------------------
        // Public Methods
        //---------------------------------------------------------------------------------------------------
        public string SAPID
        {
            get { return ReturnValidValue(m_SAPID); }
        }

        public string SAPClient
        {
            get { return ReturnValidValue(m_SAPClient); }
        }

        public string SAPLanguage
        {
            get { return ReturnValidValue(m_SAPLanguage); }
        }

        public string SAPTransaction
        {
            get { return ReturnValidValue(m_SAPTransaction); }
        }

        public bool HasSAPID()
        {
            return (!String.IsNullOrEmpty(this.SAPID));
        }

        public bool HasSAPClient()
        {
            return (!String.IsNullOrEmpty(this.SAPClient));
        }

        public bool HasSAPLanguage()
        {
            return (!String.IsNullOrEmpty(this.SAPLanguage));
        }

        public bool HasSAPTransaction()
        {
            return (!String.IsNullOrEmpty(this.SAPTransaction));
        }

        public bool IsValid()
        {
            return (this.HasSAPID() && this.HasSAPClient());
        }

        public void ExtendWithDefaults(string Language, string Transaction)
        {
            if (!HasSAPLanguage()) m_SAPLanguage = Language;
            if (!HasSAPTransaction()) m_SAPTransaction = Transaction;
        }


        //---------------------------------------------------------------------------------------------------
        // Interface Implementation: IComparable
        //---------------------------------------------------------------------------------------------------
        #region IComparable implementation

        public int CompareTo(object obj)
        {
            if (obj == null) return 1;

            LogonColumn otherLC = obj as LogonColumn;
            if (otherLC != null)
            {
                if (this.SAPID.CompareTo(otherLC.SAPID) < 0) return -1;
                if (this.SAPID.CompareTo(otherLC.SAPID) > 0) return 1;

                if (this.SAPID.CompareTo(otherLC.SAPID) == 0)
                {
                    if (this.SAPClient.CompareTo(otherLC.SAPClient) < 0) return -1;
                    if (this.SAPClient.CompareTo(otherLC.SAPClient) > 0) return 1;

                    if (this.SAPClient.CompareTo(otherLC.SAPClient) == 0)
                    {
                        if (this.SAPLanguage.CompareTo(otherLC.SAPLanguage) < 0) return -1;
                        if (this.SAPLanguage.CompareTo(otherLC.SAPLanguage) > 0) return 1;

                        if (this.SAPLanguage.CompareTo(otherLC.SAPLanguage) == 0)
                        {
                            return (this.SAPTransaction.CompareTo(otherLC.SAPTransaction));
                        }
                    }
                }
            }
            else
            {
                throw new ArgumentException("Object is not an object of type " + this.ToString());
            }

            return 1;
        }

        #endregion


        //---------------------------------------------------------------------------------------------------
        // Private Static Methods
        //---------------------------------------------------------------------------------------------------
        private static string ReturnValidValue(string value)
        {
            if (!String.IsNullOrEmpty(value))
            {
                return value;
            }

            return String.Empty;
        }
    }
}
