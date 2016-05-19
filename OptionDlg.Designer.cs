namespace KeeSAPLogon
{
    partial class OptionDlg
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.grbDisplayColLogon = new System.Windows.Forms.GroupBox();
            this.cbTransaction = new System.Windows.Forms.CheckBox();
            this.cbLanguage = new System.Windows.Forms.CheckBox();
            this.cbClient = new System.Windows.Forms.CheckBox();
            this.cbSAPID = new System.Windows.Forms.CheckBox();
            this.grbDefaults = new System.Windows.Forms.GroupBox();
            this.tbDefaultLng = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.tbDefaultTx = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.grbSAPGUI = new System.Windows.Forms.GroupBox();
            this.tbPathValidInfo = new System.Windows.Forms.TextBox();
            this.btnFolderDlg = new System.Windows.Forms.Button();
            this.label3 = new System.Windows.Forms.Label();
            this.tbSAPGUIPath = new System.Windows.Forms.TextBox();
            this.fbDlgGUIPath = new System.Windows.Forms.FolderBrowserDialog();
            this.btnCancel = new System.Windows.Forms.Button();
            this.btnOK = new System.Windows.Forms.Button();
            this.grbDisplayColLogon.SuspendLayout();
            this.grbDefaults.SuspendLayout();
            this.grbSAPGUI.SuspendLayout();
            this.SuspendLayout();
            // 
            // grbDisplayColLogon
            // 
            this.grbDisplayColLogon.Controls.Add(this.cbTransaction);
            this.grbDisplayColLogon.Controls.Add(this.cbLanguage);
            this.grbDisplayColLogon.Controls.Add(this.cbClient);
            this.grbDisplayColLogon.Controls.Add(this.cbSAPID);
            this.grbDisplayColLogon.Location = new System.Drawing.Point(12, 12);
            this.grbDisplayColLogon.Name = "grbDisplayColLogon";
            this.grbDisplayColLogon.Size = new System.Drawing.Size(170, 118);
            this.grbDisplayColLogon.TabIndex = 1;
            this.grbDisplayColLogon.TabStop = false;
            this.grbDisplayColLogon.Text = "Display column SAP logon";
            // 
            // cbTransaction
            // 
            this.cbTransaction.AutoSize = true;
            this.cbTransaction.Location = new System.Drawing.Point(7, 92);
            this.cbTransaction.Name = "cbTransaction";
            this.cbTransaction.Size = new System.Drawing.Size(82, 17);
            this.cbTransaction.TabIndex = 3;
            this.cbTransaction.Text = "Transaction";
            this.cbTransaction.UseVisualStyleBackColor = true;
            // 
            // cbLanguage
            // 
            this.cbLanguage.AutoSize = true;
            this.cbLanguage.Location = new System.Drawing.Point(7, 68);
            this.cbLanguage.Name = "cbLanguage";
            this.cbLanguage.Size = new System.Drawing.Size(74, 17);
            this.cbLanguage.TabIndex = 2;
            this.cbLanguage.Text = "Language";
            this.cbLanguage.UseVisualStyleBackColor = true;
            // 
            // cbClient
            // 
            this.cbClient.AutoSize = true;
            this.cbClient.Location = new System.Drawing.Point(7, 44);
            this.cbClient.Name = "cbClient";
            this.cbClient.Size = new System.Drawing.Size(52, 17);
            this.cbClient.TabIndex = 1;
            this.cbClient.Text = "Client";
            this.cbClient.UseVisualStyleBackColor = true;
            // 
            // cbSAPID
            // 
            this.cbSAPID.AutoSize = true;
            this.cbSAPID.Location = new System.Drawing.Point(7, 20);
            this.cbSAPID.Name = "cbSAPID";
            this.cbSAPID.Size = new System.Drawing.Size(74, 17);
            this.cbSAPID.TabIndex = 0;
            this.cbSAPID.Text = "System ID";
            this.cbSAPID.UseVisualStyleBackColor = true;
            // 
            // grbDefaults
            // 
            this.grbDefaults.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.grbDefaults.Controls.Add(this.tbDefaultLng);
            this.grbDefaults.Controls.Add(this.label2);
            this.grbDefaults.Controls.Add(this.tbDefaultTx);
            this.grbDefaults.Controls.Add(this.label1);
            this.grbDefaults.Location = new System.Drawing.Point(188, 12);
            this.grbDefaults.Name = "grbDefaults";
            this.grbDefaults.Size = new System.Drawing.Size(245, 118);
            this.grbDefaults.TabIndex = 2;
            this.grbDefaults.TabStop = false;
            this.grbDefaults.Text = "Defaults";
            // 
            // tbDefaultLng
            // 
            this.tbDefaultLng.CharacterCasing = System.Windows.Forms.CharacterCasing.Lower;
            this.tbDefaultLng.Location = new System.Drawing.Point(79, 19);
            this.tbDefaultLng.MaxLength = 2;
            this.tbDefaultLng.Name = "tbDefaultLng";
            this.tbDefaultLng.Size = new System.Drawing.Size(23, 20);
            this.tbDefaultLng.TabIndex = 4;
            this.tbDefaultLng.Text = "zz";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(10, 48);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(63, 13);
            this.label2.TabIndex = 3;
            this.label2.Text = "Transaction";
            // 
            // tbDefaultTx
            // 
            this.tbDefaultTx.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.tbDefaultTx.Location = new System.Drawing.Point(79, 45);
            this.tbDefaultTx.MaxLength = 128;
            this.tbDefaultTx.Name = "tbDefaultTx";
            this.tbDefaultTx.Size = new System.Drawing.Size(160, 20);
            this.tbDefaultTx.TabIndex = 2;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(18, 22);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(55, 13);
            this.label1.TabIndex = 1;
            this.label1.Text = "Language";
            // 
            // grbSAPGUI
            // 
            this.grbSAPGUI.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.grbSAPGUI.Controls.Add(this.tbPathValidInfo);
            this.grbSAPGUI.Controls.Add(this.btnFolderDlg);
            this.grbSAPGUI.Controls.Add(this.label3);
            this.grbSAPGUI.Controls.Add(this.tbSAPGUIPath);
            this.grbSAPGUI.Location = new System.Drawing.Point(12, 136);
            this.grbSAPGUI.Name = "grbSAPGUI";
            this.grbSAPGUI.Size = new System.Drawing.Size(421, 74);
            this.grbSAPGUI.TabIndex = 3;
            this.grbSAPGUI.TabStop = false;
            this.grbSAPGUI.Text = "SAP GUI installation";
            // 
            // tbPathValidInfo
            // 
            this.tbPathValidInfo.BackColor = System.Drawing.SystemColors.Control;
            this.tbPathValidInfo.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.tbPathValidInfo.Location = new System.Drawing.Point(41, 45);
            this.tbPathValidInfo.Name = "tbPathValidInfo";
            this.tbPathValidInfo.ReadOnly = true;
            this.tbPathValidInfo.Size = new System.Drawing.Size(374, 13);
            this.tbPathValidInfo.TabIndex = 3;
            this.tbPathValidInfo.Text = "Validation info...";
            // 
            // btnFolderDlg
            // 
            this.btnFolderDlg.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnFolderDlg.Image = global::KeeSAPLogon.Properties.Resources.CleanIcon_png;
            this.btnFolderDlg.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnFolderDlg.Location = new System.Drawing.Point(339, 14);
            this.btnFolderDlg.Name = "btnFolderDlg";
            this.btnFolderDlg.Size = new System.Drawing.Size(76, 29);
            this.btnFolderDlg.TabIndex = 2;
            this.btnFolderDlg.Text = "Select";
            this.btnFolderDlg.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.btnFolderDlg.UseVisualStyleBackColor = true;
            this.btnFolderDlg.Click += new System.EventHandler(this.btFolderDlg_Click);
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(6, 22);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(29, 13);
            this.label3.TabIndex = 1;
            this.label3.Text = "Path";
            // 
            // tbSAPGUIPath
            // 
            this.tbSAPGUIPath.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.tbSAPGUIPath.Location = new System.Drawing.Point(41, 19);
            this.tbSAPGUIPath.Name = "tbSAPGUIPath";
            this.tbSAPGUIPath.Size = new System.Drawing.Size(292, 20);
            this.tbSAPGUIPath.TabIndex = 0;
            this.tbSAPGUIPath.TextChanged += new System.EventHandler(this.tbSAPGUIPath_TextChanged);
            // 
            // fbDlgGUIPath
            // 
            this.fbDlgGUIPath.RootFolder = System.Environment.SpecialFolder.MyComputer;
            this.fbDlgGUIPath.ShowNewFolderButton = false;
            // 
            // btnCancel
            // 
            this.btnCancel.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnCancel.Location = new System.Drawing.Point(351, 218);
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.Size = new System.Drawing.Size(82, 31);
            this.btnCancel.TabIndex = 4;
            this.btnCancel.Text = "&Cancel";
            this.btnCancel.UseVisualStyleBackColor = true;
            this.btnCancel.Click += new System.EventHandler(this.btnCancel_Click);
            // 
            // btnOK
            // 
            this.btnOK.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnOK.Location = new System.Drawing.Point(263, 218);
            this.btnOK.Name = "btnOK";
            this.btnOK.Size = new System.Drawing.Size(82, 31);
            this.btnOK.TabIndex = 5;
            this.btnOK.Text = "&Ok";
            this.btnOK.UseVisualStyleBackColor = true;
            this.btnOK.Click += new System.EventHandler(this.btnOK_Click);
            // 
            // OptionDlg
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(445, 261);
            this.Controls.Add(this.btnOK);
            this.Controls.Add(this.btnCancel);
            this.Controls.Add(this.grbSAPGUI);
            this.Controls.Add(this.grbDefaults);
            this.Controls.Add(this.grbDisplayColLogon);
            this.Name = "OptionDlg";
            this.Text = "OptionDlg";
            this.Shown += new System.EventHandler(this.OptionDlg_Shown);
            this.grbDisplayColLogon.ResumeLayout(false);
            this.grbDisplayColLogon.PerformLayout();
            this.grbDefaults.ResumeLayout(false);
            this.grbDefaults.PerformLayout();
            this.grbSAPGUI.ResumeLayout(false);
            this.grbSAPGUI.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.GroupBox grbDisplayColLogon;
        private System.Windows.Forms.CheckBox cbTransaction;
        private System.Windows.Forms.CheckBox cbLanguage;
        private System.Windows.Forms.CheckBox cbClient;
        private System.Windows.Forms.CheckBox cbSAPID;
        private System.Windows.Forms.GroupBox grbDefaults;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox tbDefaultTx;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.GroupBox grbSAPGUI;
        private System.Windows.Forms.Button btnFolderDlg;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.TextBox tbSAPGUIPath;
        private System.Windows.Forms.FolderBrowserDialog fbDlgGUIPath;
        private System.Windows.Forms.TextBox tbPathValidInfo;
        private System.Windows.Forms.Button btnCancel;
        private System.Windows.Forms.Button btnOK;
        private System.Windows.Forms.TextBox tbDefaultLng;
    }
}