namespace knoledge_spv
{
    partial class FormWallet
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
            this.label1 = new System.Windows.Forms.Label();
            this.textBoxName = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.textBoxSigs = new System.Windows.Forms.TextBox();
            this.checkBoxP2SH = new System.Windows.Forms.CheckBox();
            this.listBoxPublicKeys = new System.Windows.Forms.ListBox();
            this.buttonGenerate = new System.Windows.Forms.Button();
            this.buttonOk = new System.Windows.Forms.Button();
            this.buttonCancel = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(12, 9);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(55, 20);
            this.label1.TabIndex = 0;
            this.label1.Text = "Name:";
            // 
            // textBoxName
            // 
            this.textBoxName.Location = new System.Drawing.Point(73, 6);
            this.textBoxName.Name = "textBoxName";
            this.textBoxName.Size = new System.Drawing.Size(425, 26);
            this.textBoxName.TabIndex = 1;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(12, 50);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(187, 20);
            this.label2.TabIndex = 2;
            this.label2.Text = "No. Signatures Required:";
            // 
            // textBoxSigs
            // 
            this.textBoxSigs.Location = new System.Drawing.Point(205, 47);
            this.textBoxSigs.Name = "textBoxSigs";
            this.textBoxSigs.ReadOnly = true;
            this.textBoxSigs.Size = new System.Drawing.Size(63, 26);
            this.textBoxSigs.TabIndex = 3;
            this.textBoxSigs.Text = "1";
            // 
            // checkBoxP2SH
            // 
            this.checkBoxP2SH.AutoSize = true;
            this.checkBoxP2SH.Location = new System.Drawing.Point(12, 88);
            this.checkBoxP2SH.Name = "checkBoxP2SH";
            this.checkBoxP2SH.RightToLeft = System.Windows.Forms.RightToLeft.Yes;
            this.checkBoxP2SH.Size = new System.Drawing.Size(94, 24);
            this.checkBoxP2SH.TabIndex = 4;
            this.checkBoxP2SH.Text = "Is P2SH";
            this.checkBoxP2SH.UseVisualStyleBackColor = true;
            // 
            // listBoxPublicKeys
            // 
            this.listBoxPublicKeys.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.listBoxPublicKeys.FormattingEnabled = true;
            this.listBoxPublicKeys.ItemHeight = 20;
            this.listBoxPublicKeys.Location = new System.Drawing.Point(12, 188);
            this.listBoxPublicKeys.Name = "listBoxPublicKeys";
            this.listBoxPublicKeys.Size = new System.Drawing.Size(1329, 424);
            this.listBoxPublicKeys.TabIndex = 7;
            // 
            // buttonGenerate
            // 
            this.buttonGenerate.Location = new System.Drawing.Point(16, 136);
            this.buttonGenerate.Name = "buttonGenerate";
            this.buttonGenerate.Size = new System.Drawing.Size(96, 37);
            this.buttonGenerate.TabIndex = 6;
            this.buttonGenerate.Text = "Generate";
            this.buttonGenerate.UseVisualStyleBackColor = true;
            this.buttonGenerate.Click += new System.EventHandler(this.buttonGenerate_Click);
            // 
            // buttonOk
            // 
            this.buttonOk.DialogResult = System.Windows.Forms.DialogResult.OK;
            this.buttonOk.Enabled = false;
            this.buttonOk.Location = new System.Drawing.Point(1185, 623);
            this.buttonOk.Name = "buttonOk";
            this.buttonOk.Size = new System.Drawing.Size(75, 35);
            this.buttonOk.TabIndex = 8;
            this.buttonOk.Text = "Ok";
            this.buttonOk.UseVisualStyleBackColor = true;
            this.buttonOk.Click += new System.EventHandler(this.buttonOk_Click);
            // 
            // buttonCancel
            // 
            this.buttonCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.buttonCancel.Location = new System.Drawing.Point(1266, 623);
            this.buttonCancel.Name = "buttonCancel";
            this.buttonCancel.Size = new System.Drawing.Size(75, 35);
            this.buttonCancel.TabIndex = 9;
            this.buttonCancel.Text = "Cancel";
            this.buttonCancel.UseVisualStyleBackColor = true;
            // 
            // FormWallet
            // 
            this.AcceptButton = this.buttonOk;
            this.AutoScaleDimensions = new System.Drawing.SizeF(9F, 20F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.CancelButton = this.buttonCancel;
            this.ClientSize = new System.Drawing.Size(1353, 670);
            this.Controls.Add(this.buttonCancel);
            this.Controls.Add(this.buttonOk);
            this.Controls.Add(this.buttonGenerate);
            this.Controls.Add(this.listBoxPublicKeys);
            this.Controls.Add(this.checkBoxP2SH);
            this.Controls.Add(this.textBoxSigs);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.textBoxName);
            this.Controls.Add(this.label1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.Fixed3D;
            this.MinimizeBox = false;
            this.Name = "FormWallet";
            this.Text = "Wallet";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.FormWallet_FormClosing);
            this.Load += new System.EventHandler(this.FormWallet_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox textBoxName;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox textBoxSigs;
        private System.Windows.Forms.CheckBox checkBoxP2SH;
        private System.Windows.Forms.ListBox listBoxPublicKeys;
        private System.Windows.Forms.Button buttonGenerate;
        private System.Windows.Forms.Button buttonOk;
        private System.Windows.Forms.Button buttonCancel;
    }
}