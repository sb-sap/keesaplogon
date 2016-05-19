/*
  Copyright (C) 2016 Marko Graf
*/

using System;
using System.IO;
using System.Windows.Forms;

using KeePass.Forms;



namespace KeeSAPLogon
{
    public partial class OptionDlg : Form
    {
        private readonly MainForm m_form = null;
        private readonly SAPLogonOpt m_config = null;


        //---------------------------------------------------------------------------------------------------
        // Class Constructors
        //---------------------------------------------------------------------------------------------------
        public OptionDlg(MainForm form, SAPLogonOpt config)
        {
            InitializeComponent();

            this.tbDefaultLng.KeyPress += new KeyPressEventHandler(this.tbDefaultLng_KeyPress);

            this.Text = Translatable.TitelOptionDlg;
            this.Icon = IconExtractor.GetSettingsIcon();
            this.btnFolderDlg.Image = IconExtractor.GetOpenFolderIcon().ToBitmap();
            this.fbDlgGUIPath.RootFolder = Environment.SpecialFolder.ProgramFilesX86;

            LoadOptions(config);

            m_form = form;
            m_config = config;
        }


        //---------------------------------------------------------------------------------------------------
        // Class Destructor
        //---------------------------------------------------------------------------------------------------
        ~OptionDlg()
        {
            this.tbDefaultLng.KeyPress -= new KeyPressEventHandler(this.tbDefaultLng_KeyPress);
        }


        //---------------------------------------------------------------------------------------------------
        // Eventhandler
        //---------------------------------------------------------------------------------------------------
        private void OptionDlg_Shown(object sender, EventArgs e)
        {
            UpdatePathMsg();
        }

        private void tbDefaultLng_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (char.IsLetter(e.KeyChar))
            {
                //Further processing allowed by setting FALSE, that's what we want
                e.Handled = false;
            }
            else
            {
                //No further processing allowed by setting TRUE, that's what we want
                e.Handled = true;
            }
        }

        private void tbSAPGUIPath_TextChanged(object sender, EventArgs e)
        {
            UpdatePathMsg();
        }

        private void btFolderDlg_Click(object sender, EventArgs e)
        {
            if (fbDlgGUIPath.ShowDialog() == System.Windows.Forms.DialogResult.OK)
            {
                this.tbSAPGUIPath.Text = fbDlgGUIPath.SelectedPath;
                UpdatePathMsg();
            }
        }

        private void btnOK_Click(object sender, EventArgs e)
        {
            if (!(this.cbSAPID.Checked || this.cbClient.Checked || this.cbLanguage.Checked || this.cbTransaction.Checked))
            {
                MessageBox.Show(Translatable.ErrorMsgDisplayOption, KeeSAPLogonExt.PlugInName + " settings error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            if (!SAPLogonHandler.ValidateSAPGUIPath(this.tbSAPGUIPath.Text))
            {
                MessageBox.Show(Translatable.ErrorMsgSAPGUIPath, KeeSAPLogonExt.PlugInName + " settings error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

            this.DialogResult = DialogResult.OK;

            SaveOptions();
            this.Close();
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        //---------------------------------------------------------------------------------------------------
        // Private Methods
        //---------------------------------------------------------------------------------------------------
        private void UpdatePathMsg()
        {
            string msg = "";

            if (!SAPLogonHandler.ValidateSAPGUIPath(this.tbSAPGUIPath.Text))
            {
                msg = Translatable.ValidationInfoNeg;
                msg = msg.Replace("%EXE_NAME%", SAPLogonHandler.SAPGUIShortCutEXE);
                this.tbPathValidInfo.Text = msg;
                return;
            }

            if (m_config != null)
            {
                if (m_config.AutoDetected)
                {
                    msg = Translatable.ValidationInfoAuto;
                    msg = msg.Replace("%EXE_NAME%", SAPLogonHandler.SAPGUIShortCutEXE);
                    this.tbPathValidInfo.Text = msg;
                    return;
                }
            }

            if (SAPLogonHandler.ValidateSAPGUIPath(this.tbSAPGUIPath.Text))
            {
                msg = Translatable.ValidationInfoPos;
                msg = msg.Replace("%EXE_NAME%", SAPLogonHandler.SAPGUIShortCutEXE);
                this.tbPathValidInfo.Text = msg;
                return;
            }

        }

        private void LoadOptions(SAPLogonOpt config)
        {
            if (config != null)
            {
                this.cbSAPID.Checked = Convert.ToBoolean(config.DisSystemID);
                this.cbClient.Checked = Convert.ToBoolean(config.DisClient);
                this.cbLanguage.Checked = Convert.ToBoolean(config.DisLanguage);
                this.cbTransaction.Checked = Convert.ToBoolean(config.DisTx);

                this.tbDefaultLng.Text = config.DefaultLng;
                this.tbDefaultTx.Text = config.DefaultTx;

                this.tbSAPGUIPath.Text = config.SAPGUIPath;
            }
        }

        private void SaveOptions()
        {
            if (m_config != null)
            {
                m_config.DisSystemID = this.cbSAPID.Checked;
                m_config.DisClient = this.cbClient.Checked;
                m_config.DisLanguage = this.cbLanguage.Checked;
                m_config.DisTx = this.cbTransaction.Checked;

                m_config.DefaultLng = this.tbDefaultLng.Text;
                m_config.DefaultTx = this.tbDefaultTx.Text;

                m_config.SAPGUIPath = this.tbSAPGUIPath.Text;
            }

            if (m_form != null)
            {
                m_form.RefreshEntriesList();
            }
        }


    }
}
