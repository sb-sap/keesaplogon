/*
  Copyright (C) 2016 Marko Graf
*/

using System;

using KeePass.App.Configuration;



namespace KeeSAPLogon
{
    public class SAPLogonOpt : IDisposable
    {
        private const string flagDisSystemID = KeeSAPLogonExt.PlugInName + "_" + "DisSystemID";
        private const string flagDisClient = KeeSAPLogonExt.PlugInName + "_" + "DisClient";
        private const string flagDisLanguage = KeeSAPLogonExt.PlugInName + "_" + "DisLanguage";
        private const string flagDisTx = KeeSAPLogonExt.PlugInName + "_" + "DisTx";
        private const string flagDefaultLng = KeeSAPLogonExt.PlugInName + "_" + "DefaultLng";
        private const string flagDefaultTx = KeeSAPLogonExt.PlugInName + "_" + "DefaultTx";
        private const string flagSAPGUIPath = KeeSAPLogonExt.PlugInName + "_" + "SAPGUIPath";

        readonly AceCustomConfig m_config = null;



        public SAPLogonOpt(AceCustomConfig config)
        {
            m_config = config;
        }


        public bool DisSystemID
        {
            get { return m_config.GetBool(flagDisSystemID, true); }
            set { m_config.SetBool(flagDisSystemID, value); }
        }

        public bool DisClient
        {
            get { return m_config.GetBool(flagDisClient, true); }
            set { m_config.SetBool(flagDisClient, value); }
        }

        public bool DisLanguage
        {
            get { return m_config.GetBool(flagDisLanguage, false); }
            set { m_config.SetBool(flagDisLanguage, value); }
        }

        public bool DisTx
        {
            get { return m_config.GetBool(flagDisTx, false); }
            set { m_config.SetBool(flagDisTx, value); }
        }

        public string DefaultLng
        {
            get { return m_config.GetString(flagDefaultLng, ""); }
            set { m_config.SetString(flagDefaultLng, value); }
        }

        public string DefaultTx
        {
            get { return m_config.GetString(flagDefaultTx, ""); }
            set { m_config.SetString(flagDefaultTx, value); }
        }

        public string SAPGUIPath
        {
            get { return m_config.GetString(flagSAPGUIPath, ""); }
            set { m_config.SetString(flagSAPGUIPath, value); }
        }


        //---------------------------------------------------------------------------------------------------
        // Interface Implementation: IDisposable
        //---------------------------------------------------------------------------------------------------
        #region IDisposable implementation

        public void Dispose()
        {

        }

        #endregion

    }
}
