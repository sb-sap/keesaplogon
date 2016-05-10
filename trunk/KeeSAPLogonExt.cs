/*
  Copyright (C) 2016 Marko Graf
*/

using System;
using System.Drawing;
using System.Windows.Forms;
using System.Diagnostics;

using KeePass.Plugins;



namespace KeeSAPLogon
{
    public sealed class KeeSAPLogonExt : Plugin, IDisposable
    {
        public const string PlugInName = "KeeSAPLogon";

        private static IPluginHost m_host = null;

        private bool m_disposed = false;

        private ToolStripMenuItem m_tsmiSetDlg = null;
        private LogonColumnProvider m_prov = null;
        private SAPLogonOpt m_provOpt = null;


        //---------------------------------------------------------------------------------------------------
        // Interface Implementation: KeePass.Plugins.Plugin
        //---------------------------------------------------------------------------------------------------
        #region KeePass.Plugins.Plugin implementation

        public override Image SmallIcon { get { return KeeSAPLogon.Properties.Resources.KeeSAPLogonIcon_png; } }


        public override string UpdateUrl { get { return KeeSAPLogon.Properties.Resources.URLVersionCheck; } }


        public override bool Initialize(IPluginHost host)
        {
            Debug.Assert(host != null, PlugInName + ": " + "No host instance available.", "PlugIn host instance is null.");
            Terminate();

            m_host = host;
            if (host != null)
            {
                m_provOpt = new SAPLogonOpt(host.CustomConfig);
                m_prov = new LogonColumnProvider(m_provOpt);
                m_host.ColumnProviderPool.Add(m_prov);


                // Get a reference to the 'Tools' menu item container
                ToolStripItemCollection tsMenu = m_host.MainWindow.ToolsMenu.DropDownItems;

                // Add the popup menu item
                m_tsmiSetDlg = new ToolStripMenuItem();
                m_tsmiSetDlg.Text = Translatable.ButtonSAPLogonDlg;
                m_tsmiSetDlg.ToolTipText = Translatable.ButtonSAPLogonDlg;
                m_tsmiSetDlg.Image = KeeSAPLogon.Properties.Resources.KeeSAPLogonIcon_png;
                m_tsmiSetDlg.Click += this.OnSettingsDlg;
                tsMenu.Add(m_tsmiSetDlg);

                return true;
            }

            return false;
        }

        public override void Terminate()
        {
            if (m_host != null)
            {
                if (m_prov != null)
                {
                    m_host.ColumnProviderPool.Remove(m_prov);
                }

                // Remove all of our menu items
                ToolStripItemCollection tsMenu = m_host.MainWindow.ToolsMenu.DropDownItems;

                if (m_tsmiSetDlg != null)
                {
                    m_tsmiSetDlg.Click -= this.OnSettingsDlg;
                    tsMenu.Remove(m_tsmiSetDlg);
                }

                m_host = null;
            }

            base.Terminate();
        }

        #endregion


        //---------------------------------------------------------------------------------------------------
        // Interface Implementation: IDisposable
        //---------------------------------------------------------------------------------------------------
        #region IDisposable implementation

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        #endregion


        //---------------------------------------------------------------------------------------------------
        // Class Destructor
        //---------------------------------------------------------------------------------------------------
        // Use C# destructor syntax for finalization code.
        ~KeeSAPLogonExt()
        {
            // Simply call Dispose(false).
            Dispose(false);
        }


        //---------------------------------------------------------------------------------------------------
        // Private Methods
        //---------------------------------------------------------------------------------------------------
        private void Dispose(bool disposing)
        {
            if (!m_disposed)
            {
                if (disposing)
                {
                    // Free other state (managed objects).
                    m_tsmiSetDlg.Dispose();
                    m_prov.Dispose();
                    m_provOpt.Dispose();
                }

                // Free your own state (unmanaged objects).
                // Set large fields to null.

                m_disposed = true;
            }
        }


        private void OnSettingsDlg(object sender, EventArgs e)
        {
            OptionDlg dlg = new OptionDlg(m_host.MainWindow, new SAPLogonOpt(m_host.CustomConfig));

            dlg.ShowDialog();

            dlg.Dispose();
        }

    }
}
