namespace knoledge_spv
{
    partial class FormMain
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;


        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FormMain));
            this.menuStrip = new System.Windows.Forms.MenuStrip();
            this.fileToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.exitToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.walletToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.addWalletToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator2 = new System.Windows.Forms.ToolStripSeparator();
            this.createNewAddressToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.copyAddressToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.statusStrip = new System.Windows.Forms.StatusStrip();
            this.labelStatus = new System.Windows.Forms.ToolStripStatusLabel();
            this.buttonStatus = new System.Windows.Forms.ToolStripDropDownButton();
            this.buttonInfo = new System.Windows.Forms.ToolStripDropDownButton();
            this.tableLayoutPanel = new System.Windows.Forms.TableLayoutPanel();
            this.panelTop = new System.Windows.Forms.Panel();
            this.labelBalance = new System.Windows.Forms.Label();
            this.comboBoxAddress = new System.Windows.Forms.ComboBox();
            this.label10 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.comboBoxWallet = new System.Windows.Forms.ComboBox();
            this.label1 = new System.Windows.Forms.Label();
            this.tabControl = new System.Windows.Forms.TabControl();
            this.tabPageTx = new System.Windows.Forms.TabPage();
            this.listView = new System.Windows.Forms.ListView();
            this.columnHeaderAmount = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.columnHeaderTxId = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.columnHeaderBlockId = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.columnHeaderConfirms = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.contextMenuStripTx = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.showDetailsToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.tabPageSend = new System.Windows.Forms.TabPage();
            this.groupBoxFee = new System.Windows.Forms.GroupBox();
            this.buttonVerify = new System.Windows.Forms.Button();
            this.buttonSend = new System.Windows.Forms.Button();
            this.labelFee = new System.Windows.Forms.Label();
            this.label7 = new System.Windows.Forms.Label();
            this.buttonClear = new System.Windows.Forms.Button();
            this.buttonPastePayTo = new System.Windows.Forms.Button();
            this.label6 = new System.Windows.Forms.Label();
            this.numericUpDownAmount = new System.Windows.Forms.NumericUpDown();
            this.textBoxLabel = new System.Windows.Forms.TextBox();
            this.textBoxPayTo = new System.Windows.Forms.TextBox();
            this.label5 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.toolTip = new System.Windows.Forms.ToolTip(this.components);
            this.menuStrip.SuspendLayout();
            this.statusStrip.SuspendLayout();
            this.tableLayoutPanel.SuspendLayout();
            this.panelTop.SuspendLayout();
            this.tabControl.SuspendLayout();
            this.tabPageTx.SuspendLayout();
            this.contextMenuStripTx.SuspendLayout();
            this.tabPageSend.SuspendLayout();
            this.groupBoxFee.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDownAmount)).BeginInit();
            this.SuspendLayout();
            // 
            // menuStrip
            // 
            this.menuStrip.ImageScalingSize = new System.Drawing.Size(24, 24);
            this.menuStrip.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.fileToolStripMenuItem,
            this.walletToolStripMenuItem});
            this.menuStrip.Location = new System.Drawing.Point(0, 0);
            this.menuStrip.Name = "menuStrip";
            this.menuStrip.Size = new System.Drawing.Size(1258, 33);
            this.menuStrip.TabIndex = 0;
            this.menuStrip.Text = "menuStrip1";
            // 
            // fileToolStripMenuItem
            // 
            this.fileToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.exitToolStripMenuItem});
            this.fileToolStripMenuItem.Name = "fileToolStripMenuItem";
            this.fileToolStripMenuItem.Size = new System.Drawing.Size(50, 29);
            this.fileToolStripMenuItem.Text = "&File";
            // 
            // exitToolStripMenuItem
            // 
            this.exitToolStripMenuItem.Name = "exitToolStripMenuItem";
            this.exitToolStripMenuItem.Size = new System.Drawing.Size(124, 30);
            this.exitToolStripMenuItem.Text = "E&xit";
            this.exitToolStripMenuItem.Click += new System.EventHandler(this.exitToolStripMenuItem_Click);
            // 
            // walletToolStripMenuItem
            // 
            this.walletToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.addWalletToolStripMenuItem,
            this.toolStripSeparator2,
            this.createNewAddressToolStripMenuItem,
            this.copyAddressToolStripMenuItem});
            this.walletToolStripMenuItem.Name = "walletToolStripMenuItem";
            this.walletToolStripMenuItem.Size = new System.Drawing.Size(72, 29);
            this.walletToolStripMenuItem.Text = "Wallet";
            // 
            // addWalletToolStripMenuItem
            // 
            this.addWalletToolStripMenuItem.Name = "addWalletToolStripMenuItem";
            this.addWalletToolStripMenuItem.Size = new System.Drawing.Size(251, 30);
            this.addWalletToolStripMenuItem.Text = "Add a wallet";
            this.addWalletToolStripMenuItem.Click += new System.EventHandler(this.addWalletToolStripMenuItem_Click);
            // 
            // toolStripSeparator2
            // 
            this.toolStripSeparator2.Name = "toolStripSeparator2";
            this.toolStripSeparator2.Size = new System.Drawing.Size(248, 6);
            // 
            // createNewAddressToolStripMenuItem
            // 
            this.createNewAddressToolStripMenuItem.Name = "createNewAddressToolStripMenuItem";
            this.createNewAddressToolStripMenuItem.Size = new System.Drawing.Size(251, 30);
            this.createNewAddressToolStripMenuItem.Text = "Create new address";
            this.createNewAddressToolStripMenuItem.Click += new System.EventHandler(this.createNewAddressToolStripMenuItem_Click);
            // 
            // copyAddressToolStripMenuItem
            // 
            this.copyAddressToolStripMenuItem.Name = "copyAddressToolStripMenuItem";
            this.copyAddressToolStripMenuItem.Size = new System.Drawing.Size(251, 30);
            this.copyAddressToolStripMenuItem.Text = "Copy Address";
            this.copyAddressToolStripMenuItem.Click += new System.EventHandler(this.copyAddressToolStripMenuItem_Click);
            // 
            // statusStrip
            // 
            this.statusStrip.ImageScalingSize = new System.Drawing.Size(24, 24);
            this.statusStrip.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.labelStatus,
            this.buttonStatus,
            this.buttonInfo});
            this.statusStrip.Location = new System.Drawing.Point(0, 804);
            this.statusStrip.Name = "statusStrip";
            this.statusStrip.ShowItemToolTips = true;
            this.statusStrip.Size = new System.Drawing.Size(1258, 22);
            this.statusStrip.SizingGrip = false;
            this.statusStrip.TabIndex = 1;
            this.statusStrip.Text = "statusStrip1";
            // 
            // labelStatus
            // 
            this.labelStatus.Name = "labelStatus";
            this.labelStatus.Size = new System.Drawing.Size(1203, 17);
            this.labelStatus.Spring = true;
            this.labelStatus.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // buttonStatus
            // 
            this.buttonStatus.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.buttonStatus.Image = global::knoledge_spv.Properties.Resources.network_wireless_none;
            this.buttonStatus.ImageScaling = System.Windows.Forms.ToolStripItemImageScaling.None;
            this.buttonStatus.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.buttonStatus.Name = "buttonStatus";
            this.buttonStatus.ShowDropDownArrow = false;
            this.buttonStatus.Size = new System.Drawing.Size(20, 20);
            this.buttonStatus.Text = "toolStripDropDownButton1";
            this.buttonStatus.ToolTipText = "Connected to 0 Nodes";
            this.buttonStatus.Click += new System.EventHandler(this.buttonStatus_Click);
            // 
            // buttonInfo
            // 
            this.buttonInfo.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.buttonInfo.Image = global::knoledge_spv.Properties.Resources.dialog_question;
            this.buttonInfo.ImageScaling = System.Windows.Forms.ToolStripItemImageScaling.None;
            this.buttonInfo.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.buttonInfo.Name = "buttonInfo";
            this.buttonInfo.ShowDropDownArrow = false;
            this.buttonInfo.Size = new System.Drawing.Size(20, 20);
            this.buttonInfo.Text = "toolStripDropDownButton2";
            this.buttonInfo.ToolTipText = "No Blockchain Info.";
            // 
            // tableLayoutPanel
            // 
            this.tableLayoutPanel.ColumnCount = 1;
            this.tableLayoutPanel.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle());
            this.tableLayoutPanel.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 20F));
            this.tableLayoutPanel.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 20F));
            this.tableLayoutPanel.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 20F));
            this.tableLayoutPanel.Controls.Add(this.panelTop, 0, 0);
            this.tableLayoutPanel.Controls.Add(this.tabControl, 0, 1);
            this.tableLayoutPanel.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanel.Location = new System.Drawing.Point(0, 33);
            this.tableLayoutPanel.Name = "tableLayoutPanel";
            this.tableLayoutPanel.RowCount = 2;
            this.tableLayoutPanel.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 100F));
            this.tableLayoutPanel.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.tableLayoutPanel.Size = new System.Drawing.Size(1258, 771);
            this.tableLayoutPanel.TabIndex = 2;
            // 
            // panelTop
            // 
            this.panelTop.Controls.Add(this.labelBalance);
            this.panelTop.Controls.Add(this.comboBoxAddress);
            this.panelTop.Controls.Add(this.label10);
            this.panelTop.Controls.Add(this.label2);
            this.panelTop.Controls.Add(this.comboBoxWallet);
            this.panelTop.Controls.Add(this.label1);
            this.panelTop.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panelTop.Location = new System.Drawing.Point(3, 3);
            this.panelTop.Name = "panelTop";
            this.panelTop.Size = new System.Drawing.Size(1252, 94);
            this.panelTop.TabIndex = 1;
            // 
            // labelBalance
            // 
            this.labelBalance.AutoSize = true;
            this.labelBalance.Location = new System.Drawing.Point(1010, 16);
            this.labelBalance.Name = "labelBalance";
            this.labelBalance.Size = new System.Drawing.Size(40, 20);
            this.labelBalance.TabIndex = 3;
            this.labelBalance.Text = "BTC";
            // 
            // comboBoxAddress
            // 
            this.comboBoxAddress.FormattingEnabled = true;
            this.comboBoxAddress.Location = new System.Drawing.Point(87, 57);
            this.comboBoxAddress.Name = "comboBoxAddress";
            this.comboBoxAddress.Size = new System.Drawing.Size(402, 28);
            this.comboBoxAddress.TabIndex = 3;
            this.comboBoxAddress.SelectedIndexChanged += new System.EventHandler(this.comboBoxAddress_SelectedIndexChanged);
            // 
            // label10
            // 
            this.label10.AutoSize = true;
            this.label10.Font = new System.Drawing.Font("Microsoft Sans Serif", 8F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label10.Location = new System.Drawing.Point(925, 16);
            this.label10.Name = "label10";
            this.label10.Size = new System.Drawing.Size(79, 20);
            this.label10.TabIndex = 2;
            this.label10.Text = "Balance:";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(9, 60);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(72, 20);
            this.label2.TabIndex = 2;
            this.label2.Text = "Address:";
            // 
            // comboBoxWallet
            // 
            this.comboBoxWallet.FormattingEnabled = true;
            this.comboBoxWallet.Location = new System.Drawing.Point(87, 13);
            this.comboBoxWallet.Name = "comboBoxWallet";
            this.comboBoxWallet.Size = new System.Drawing.Size(402, 28);
            this.comboBoxWallet.TabIndex = 1;
            this.comboBoxWallet.SelectedIndexChanged += new System.EventHandler(this.comboBoxWallet_SelectedIndexChanged);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(9, 16);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(57, 20);
            this.label1.TabIndex = 0;
            this.label1.Text = "Wallet:";
            // 
            // tabControl
            // 
            this.tabControl.Controls.Add(this.tabPageTx);
            this.tabControl.Controls.Add(this.tabPageSend);
            this.tabControl.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tabControl.Location = new System.Drawing.Point(3, 103);
            this.tabControl.Name = "tabControl";
            this.tabControl.SelectedIndex = 0;
            this.tabControl.Size = new System.Drawing.Size(1252, 665);
            this.tabControl.TabIndex = 2;
            // 
            // tabPageTx
            // 
            this.tabPageTx.Controls.Add(this.listView);
            this.tabPageTx.Location = new System.Drawing.Point(4, 29);
            this.tabPageTx.Name = "tabPageTx";
            this.tabPageTx.Padding = new System.Windows.Forms.Padding(3);
            this.tabPageTx.Size = new System.Drawing.Size(1244, 632);
            this.tabPageTx.TabIndex = 0;
            this.tabPageTx.Text = "Transactions";
            this.tabPageTx.UseVisualStyleBackColor = true;
            // 
            // listView
            // 
            this.listView.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.columnHeaderAmount,
            this.columnHeaderTxId,
            this.columnHeaderBlockId,
            this.columnHeaderConfirms});
            this.listView.ContextMenuStrip = this.contextMenuStripTx;
            this.listView.Dock = System.Windows.Forms.DockStyle.Fill;
            this.listView.FullRowSelect = true;
            this.listView.Location = new System.Drawing.Point(3, 3);
            this.listView.MultiSelect = false;
            this.listView.Name = "listView";
            this.listView.Size = new System.Drawing.Size(1238, 626);
            this.listView.TabIndex = 2;
            this.listView.UseCompatibleStateImageBehavior = false;
            this.listView.View = System.Windows.Forms.View.Details;
            // 
            // columnHeaderAmount
            // 
            this.columnHeaderAmount.Text = "Amount";
            this.columnHeaderAmount.Width = 264;
            // 
            // columnHeaderTxId
            // 
            this.columnHeaderTxId.Text = "Transaction Id";
            this.columnHeaderTxId.Width = 243;
            // 
            // columnHeaderBlockId
            // 
            this.columnHeaderBlockId.Text = "Block Id";
            this.columnHeaderBlockId.Width = 314;
            // 
            // columnHeaderConfirms
            // 
            this.columnHeaderConfirms.Text = "Confirmations";
            this.columnHeaderConfirms.Width = 223;
            // 
            // contextMenuStripTx
            // 
            this.contextMenuStripTx.ImageScalingSize = new System.Drawing.Size(24, 24);
            this.contextMenuStripTx.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.showDetailsToolStripMenuItem});
            this.contextMenuStripTx.Name = "contextMenuStripTx";
            this.contextMenuStripTx.Size = new System.Drawing.Size(200, 34);
            // 
            // showDetailsToolStripMenuItem
            // 
            this.showDetailsToolStripMenuItem.Name = "showDetailsToolStripMenuItem";
            this.showDetailsToolStripMenuItem.Size = new System.Drawing.Size(211, 30);
            this.showDetailsToolStripMenuItem.Text = "Show Details";
            this.showDetailsToolStripMenuItem.Click += new System.EventHandler(this.showDetailsToolStripMenuItem_Click);
            // 
            // tabPageSend
            // 
            this.tabPageSend.Controls.Add(this.groupBoxFee);
            this.tabPageSend.Controls.Add(this.buttonClear);
            this.tabPageSend.Controls.Add(this.buttonPastePayTo);
            this.tabPageSend.Controls.Add(this.label6);
            this.tabPageSend.Controls.Add(this.numericUpDownAmount);
            this.tabPageSend.Controls.Add(this.textBoxLabel);
            this.tabPageSend.Controls.Add(this.textBoxPayTo);
            this.tabPageSend.Controls.Add(this.label5);
            this.tabPageSend.Controls.Add(this.label4);
            this.tabPageSend.Controls.Add(this.label3);
            this.tabPageSend.Location = new System.Drawing.Point(4, 29);
            this.tabPageSend.Name = "tabPageSend";
            this.tabPageSend.Padding = new System.Windows.Forms.Padding(3);
            this.tabPageSend.Size = new System.Drawing.Size(1244, 632);
            this.tabPageSend.TabIndex = 1;
            this.tabPageSend.Text = "Send";
            this.tabPageSend.UseVisualStyleBackColor = true;
            // 
            // groupBoxFee
            // 
            this.groupBoxFee.Controls.Add(this.buttonVerify);
            this.groupBoxFee.Controls.Add(this.buttonSend);
            this.groupBoxFee.Controls.Add(this.labelFee);
            this.groupBoxFee.Controls.Add(this.label7);
            this.groupBoxFee.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.groupBoxFee.Location = new System.Drawing.Point(3, 567);
            this.groupBoxFee.Name = "groupBoxFee";
            this.groupBoxFee.Size = new System.Drawing.Size(1238, 62);
            this.groupBoxFee.TabIndex = 11;
            this.groupBoxFee.TabStop = false;
            // 
            // buttonVerify
            // 
            this.buttonVerify.Location = new System.Drawing.Point(1076, 16);
            this.buttonVerify.Name = "buttonVerify";
            this.buttonVerify.Size = new System.Drawing.Size(75, 33);
            this.buttonVerify.TabIndex = 3;
            this.buttonVerify.Text = "Verify";
            this.buttonVerify.UseVisualStyleBackColor = true;
            this.buttonVerify.Click += new System.EventHandler(this.buttonVerify_Click);
            // 
            // buttonSend
            // 
            this.buttonSend.Location = new System.Drawing.Point(1157, 16);
            this.buttonSend.Name = "buttonSend";
            this.buttonSend.Size = new System.Drawing.Size(75, 33);
            this.buttonSend.TabIndex = 2;
            this.buttonSend.Text = "Send";
            this.buttonSend.UseVisualStyleBackColor = true;
            this.buttonSend.Click += new System.EventHandler(this.buttonSend_Click);
            // 
            // labelFee
            // 
            this.labelFee.AutoSize = true;
            this.labelFee.Location = new System.Drawing.Point(165, 22);
            this.labelFee.Name = "labelFee";
            this.labelFee.Size = new System.Drawing.Size(150, 20);
            this.labelFee.TabIndex = 1;
            this.labelFee.Text = "0.00001000 BTC/kb";
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Font = new System.Drawing.Font("Microsoft Sans Serif", 8F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label7.Location = new System.Drawing.Point(8, 22);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(144, 20);
            this.label7.TabIndex = 0;
            this.label7.Text = "Transaction Fee:";
            // 
            // buttonClear
            // 
            this.buttonClear.Image = ((System.Drawing.Image)(resources.GetObject("buttonClear.Image")));
            this.buttonClear.Location = new System.Drawing.Point(908, 7);
            this.buttonClear.Name = "buttonClear";
            this.buttonClear.Size = new System.Drawing.Size(40, 40);
            this.buttonClear.TabIndex = 10;
            this.toolTip.SetToolTip(this.buttonClear, "Clear all");
            this.buttonClear.UseVisualStyleBackColor = true;
            this.buttonClear.Click += new System.EventHandler(this.buttonClear_Click);
            // 
            // buttonPastePayTo
            // 
            this.buttonPastePayTo.Image = ((System.Drawing.Image)(resources.GetObject("buttonPastePayTo.Image")));
            this.buttonPastePayTo.Location = new System.Drawing.Point(860, 7);
            this.buttonPastePayTo.Name = "buttonPastePayTo";
            this.buttonPastePayTo.Size = new System.Drawing.Size(40, 40);
            this.buttonPastePayTo.TabIndex = 9;
            this.toolTip.SetToolTip(this.buttonPastePayTo, "Paste from clipboard");
            this.buttonPastePayTo.UseVisualStyleBackColor = true;
            this.buttonPastePayTo.Click += new System.EventHandler(this.buttonPastePayTo_Click);
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(224, 104);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(40, 20);
            this.label6.TabIndex = 7;
            this.label6.Text = "BTC";
            // 
            // numericUpDownAmount
            // 
            this.numericUpDownAmount.DecimalPlaces = 8;
            this.numericUpDownAmount.Increment = new decimal(new int[] {
            1,
            0,
            0,
            196608});
            this.numericUpDownAmount.Location = new System.Drawing.Point(83, 102);
            this.numericUpDownAmount.Name = "numericUpDownAmount";
            this.numericUpDownAmount.Size = new System.Drawing.Size(136, 26);
            this.numericUpDownAmount.TabIndex = 6;
            // 
            // textBoxLabel
            // 
            this.textBoxLabel.Location = new System.Drawing.Point(83, 60);
            this.textBoxLabel.Name = "textBoxLabel";
            this.textBoxLabel.Size = new System.Drawing.Size(865, 26);
            this.textBoxLabel.TabIndex = 5;
            // 
            // textBoxPayTo
            // 
            this.textBoxPayTo.Location = new System.Drawing.Point(83, 14);
            this.textBoxPayTo.Name = "textBoxPayTo";
            this.textBoxPayTo.Size = new System.Drawing.Size(771, 26);
            this.textBoxPayTo.TabIndex = 4;
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(8, 104);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(69, 20);
            this.label5.TabIndex = 3;
            this.label5.Text = "Amount:";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(11, 63);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(52, 20);
            this.label4.TabIndex = 2;
            this.label4.Text = "Label:";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(11, 17);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(61, 20);
            this.label3.TabIndex = 1;
            this.label3.Text = "Pay To:";
            // 
            // FormMain
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(9F, 20F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1258, 826);
            this.Controls.Add(this.tableLayoutPanel);
            this.Controls.Add(this.statusStrip);
            this.Controls.Add(this.menuStrip);
            this.MainMenuStrip = this.menuStrip;
            this.Name = "FormMain";
            this.Text = "SPV Client";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.FormMain_FormClosing);
            this.Load += new System.EventHandler(this.FormMain_Load);
            this.Shown += new System.EventHandler(this.FormMain_Shown);
            this.menuStrip.ResumeLayout(false);
            this.menuStrip.PerformLayout();
            this.statusStrip.ResumeLayout(false);
            this.statusStrip.PerformLayout();
            this.tableLayoutPanel.ResumeLayout(false);
            this.panelTop.ResumeLayout(false);
            this.panelTop.PerformLayout();
            this.tabControl.ResumeLayout(false);
            this.tabPageTx.ResumeLayout(false);
            this.contextMenuStripTx.ResumeLayout(false);
            this.tabPageSend.ResumeLayout(false);
            this.tabPageSend.PerformLayout();
            this.groupBoxFee.ResumeLayout(false);
            this.groupBoxFee.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDownAmount)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.MenuStrip menuStrip;
        private System.Windows.Forms.StatusStrip statusStrip;
        private System.Windows.Forms.ToolStripMenuItem fileToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem exitToolStripMenuItem;
        private System.Windows.Forms.ToolStripStatusLabel labelStatus;
        private System.Windows.Forms.ToolStripDropDownButton buttonStatus;
        private System.Windows.Forms.ToolStripDropDownButton buttonInfo;
        private System.Windows.Forms.ToolStripMenuItem walletToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem addWalletToolStripMenuItem;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel;
        private System.Windows.Forms.Panel panelTop;
        private System.Windows.Forms.ComboBox comboBoxWallet;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.ToolStripMenuItem createNewAddressToolStripMenuItem;
        private System.Windows.Forms.ComboBox comboBoxAddress;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator2;
        private System.Windows.Forms.ToolStripMenuItem copyAddressToolStripMenuItem;
        private System.Windows.Forms.ListView listView;
        private System.Windows.Forms.ColumnHeader columnHeaderAmount;
        private System.Windows.Forms.ColumnHeader columnHeaderTxId;
        private System.Windows.Forms.ColumnHeader columnHeaderBlockId;
        private System.Windows.Forms.ColumnHeader columnHeaderConfirms;
        private System.Windows.Forms.TabControl tabControl;
        private System.Windows.Forms.TabPage tabPageSend;
        private System.Windows.Forms.TabPage tabPageTx;
        private System.Windows.Forms.ContextMenuStrip contextMenuStripTx;
        private System.Windows.Forms.ToolStripMenuItem showDetailsToolStripMenuItem;
        private System.Windows.Forms.Button buttonPastePayTo;
        private System.Windows.Forms.ToolTip toolTip;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.NumericUpDown numericUpDownAmount;
        private System.Windows.Forms.TextBox textBoxLabel;
        private System.Windows.Forms.TextBox textBoxPayTo;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Button buttonClear;
        private System.Windows.Forms.Label labelBalance;
        private System.Windows.Forms.Label label10;
        private System.Windows.Forms.GroupBox groupBoxFee;
        private System.Windows.Forms.Button buttonSend;
        private System.Windows.Forms.Label labelFee;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.Button buttonVerify;
    }
}

