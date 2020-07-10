namespace CodeGenerator
{
    partial class frmCodeGenerator
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
            System.Windows.Forms.ListViewItem listViewItem1 = new System.Windows.Forms.ListViewItem("");
            this.gbDatabaseConnection = new System.Windows.Forms.GroupBox();
            this.lblDatabase = new System.Windows.Forms.Label();
            this.txtConnectionString = new System.Windows.Forms.TextBox();
            this.lblConnectionString = new System.Windows.Forms.Label();
            this.cmbDatabaseList = new System.Windows.Forms.ComboBox();
            this.btnSelectOutputFolder = new System.Windows.Forms.Button();
            this.txtOutputFolder = new System.Windows.Forms.TextBox();
            this.lblOutputFolder = new System.Windows.Forms.Label();
            this.btnConnect = new System.Windows.Forms.Button();
            this.txtPassword = new System.Windows.Forms.TextBox();
            this.lblPassword = new System.Windows.Forms.Label();
            this.txtUsername = new System.Windows.Forms.TextBox();
            this.lblUsername = new System.Windows.Forms.Label();
            this.chkWindowAuthentication = new System.Windows.Forms.CheckBox();
            this.cmbServerList = new System.Windows.Forms.ComboBox();
            this.lblServerName = new System.Windows.Forms.Label();
            this.btnGenerate = new System.Windows.Forms.Button();
            this.gbLanguageOptions = new System.Windows.Forms.GroupBox();
            this.rdbVB = new System.Windows.Forms.RadioButton();
            this.rdbCS = new System.Windows.Forms.RadioButton();
            this.gbCodeGenerationLog = new System.Windows.Forms.GroupBox();
            this.txtGeneratorLog = new System.Windows.Forms.TextBox();
            this.lstTableList = new System.Windows.Forms.ListView();
            this.txtColumnsList = new System.Windows.Forms.TextBox();
            this.chkSelectAllTable = new System.Windows.Forms.CheckBox();
            this.lstStoredProcedures = new System.Windows.Forms.ListBox();
            this.btnRemoveProc = new System.Windows.Forms.Button();
            this.gbOptions = new System.Windows.Forms.GroupBox();
            this.chkClasses = new System.Windows.Forms.CheckBox();
            this.chkStoredProcedures = new System.Windows.Forms.CheckBox();
            this.gbDatabaseConnection.SuspendLayout();
            this.gbLanguageOptions.SuspendLayout();
            this.gbCodeGenerationLog.SuspendLayout();
            this.gbOptions.SuspendLayout();
            this.SuspendLayout();
            // 
            // gbDatabaseConnection
            // 
            this.gbDatabaseConnection.Controls.Add(this.lblDatabase);
            this.gbDatabaseConnection.Controls.Add(this.txtConnectionString);
            this.gbDatabaseConnection.Controls.Add(this.lblConnectionString);
            this.gbDatabaseConnection.Controls.Add(this.cmbDatabaseList);
            this.gbDatabaseConnection.Controls.Add(this.btnSelectOutputFolder);
            this.gbDatabaseConnection.Controls.Add(this.txtOutputFolder);
            this.gbDatabaseConnection.Controls.Add(this.lblOutputFolder);
            this.gbDatabaseConnection.Controls.Add(this.btnConnect);
            this.gbDatabaseConnection.Controls.Add(this.txtPassword);
            this.gbDatabaseConnection.Controls.Add(this.lblPassword);
            this.gbDatabaseConnection.Controls.Add(this.txtUsername);
            this.gbDatabaseConnection.Controls.Add(this.lblUsername);
            this.gbDatabaseConnection.Controls.Add(this.chkWindowAuthentication);
            this.gbDatabaseConnection.Controls.Add(this.cmbServerList);
            this.gbDatabaseConnection.Controls.Add(this.lblServerName);
            this.gbDatabaseConnection.Location = new System.Drawing.Point(29, 12);
            this.gbDatabaseConnection.Name = "gbDatabaseConnection";
            this.gbDatabaseConnection.Size = new System.Drawing.Size(588, 165);
            this.gbDatabaseConnection.TabIndex = 0;
            this.gbDatabaseConnection.TabStop = false;
            this.gbDatabaseConnection.Text = "Database Connection Setting";
            // 
            // lblDatabase
            // 
            this.lblDatabase.AutoSize = true;
            this.lblDatabase.Location = new System.Drawing.Point(8, 53);
            this.lblDatabase.Name = "lblDatabase";
            this.lblDatabase.Size = new System.Drawing.Size(53, 13);
            this.lblDatabase.TabIndex = 22;
            this.lblDatabase.Text = "Database";
            // 
            // txtConnectionString
            // 
            this.txtConnectionString.Location = new System.Drawing.Point(104, 104);
            this.txtConnectionString.Name = "txtConnectionString";
            this.txtConnectionString.Size = new System.Drawing.Size(457, 20);
            this.txtConnectionString.TabIndex = 21;
            // 
            // lblConnectionString
            // 
            this.lblConnectionString.AutoSize = true;
            this.lblConnectionString.Location = new System.Drawing.Point(8, 107);
            this.lblConnectionString.Name = "lblConnectionString";
            this.lblConnectionString.Size = new System.Drawing.Size(91, 13);
            this.lblConnectionString.TabIndex = 20;
            this.lblConnectionString.Text = "Connection String";
            // 
            // cmbDatabaseList
            // 
            this.cmbDatabaseList.FormattingEnabled = true;
            this.cmbDatabaseList.Location = new System.Drawing.Point(67, 50);
            this.cmbDatabaseList.Name = "cmbDatabaseList";
            this.cmbDatabaseList.Size = new System.Drawing.Size(214, 21);
            this.cmbDatabaseList.TabIndex = 19;
            this.cmbDatabaseList.Text = "DbCollegeInformationSystem";
            // 
            // btnSelectOutputFolder
            // 
            this.btnSelectOutputFolder.Location = new System.Drawing.Point(527, 132);
            this.btnSelectOutputFolder.Name = "btnSelectOutputFolder";
            this.btnSelectOutputFolder.Size = new System.Drawing.Size(34, 23);
            this.btnSelectOutputFolder.TabIndex = 18;
            this.btnSelectOutputFolder.Text = "...";
            this.btnSelectOutputFolder.UseVisualStyleBackColor = true;
            this.btnSelectOutputFolder.Click += new System.EventHandler(this.btnSelectOutputFolder_Click);
            // 
            // txtOutputFolder
            // 
            this.txtOutputFolder.Location = new System.Drawing.Point(104, 134);
            this.txtOutputFolder.Name = "txtOutputFolder";
            this.txtOutputFolder.Size = new System.Drawing.Size(415, 20);
            this.txtOutputFolder.TabIndex = 15;
            // 
            // lblOutputFolder
            // 
            this.lblOutputFolder.AutoSize = true;
            this.lblOutputFolder.Location = new System.Drawing.Point(8, 137);
            this.lblOutputFolder.Name = "lblOutputFolder";
            this.lblOutputFolder.Size = new System.Drawing.Size(71, 13);
            this.lblOutputFolder.TabIndex = 14;
            this.lblOutputFolder.Text = "Output Folder";
            // 
            // btnConnect
            // 
            this.btnConnect.Location = new System.Drawing.Point(486, 73);
            this.btnConnect.Name = "btnConnect";
            this.btnConnect.Size = new System.Drawing.Size(75, 23);
            this.btnConnect.TabIndex = 9;
            this.btnConnect.Text = "Connect";
            this.btnConnect.UseVisualStyleBackColor = true;
            this.btnConnect.Click += new System.EventHandler(this.btnConnect_Click);
            // 
            // txtPassword
            // 
            this.txtPassword.Location = new System.Drawing.Point(348, 50);
            this.txtPassword.Name = "txtPassword";
            this.txtPassword.PasswordChar = '#';
            this.txtPassword.Size = new System.Drawing.Size(213, 20);
            this.txtPassword.TabIndex = 8;
            // 
            // lblPassword
            // 
            this.lblPassword.AutoSize = true;
            this.lblPassword.Location = new System.Drawing.Point(286, 53);
            this.lblPassword.Name = "lblPassword";
            this.lblPassword.Size = new System.Drawing.Size(56, 13);
            this.lblPassword.TabIndex = 7;
            this.lblPassword.Text = "Password:";
            // 
            // txtUsername
            // 
            this.txtUsername.Location = new System.Drawing.Point(348, 24);
            this.txtUsername.Name = "txtUsername";
            this.txtUsername.Size = new System.Drawing.Size(213, 20);
            this.txtUsername.TabIndex = 6;
            // 
            // lblUsername
            // 
            this.lblUsername.AutoSize = true;
            this.lblUsername.Location = new System.Drawing.Point(284, 27);
            this.lblUsername.Name = "lblUsername";
            this.lblUsername.Size = new System.Drawing.Size(58, 13);
            this.lblUsername.TabIndex = 5;
            this.lblUsername.Text = "Username:";
            // 
            // chkWindowAuthentication
            // 
            this.chkWindowAuthentication.AutoSize = true;
            this.chkWindowAuthentication.Location = new System.Drawing.Point(55, 77);
            this.chkWindowAuthentication.Name = "chkWindowAuthentication";
            this.chkWindowAuthentication.Size = new System.Drawing.Size(136, 17);
            this.chkWindowAuthentication.TabIndex = 4;
            this.chkWindowAuthentication.Text = "Window Authentication";
            this.chkWindowAuthentication.UseVisualStyleBackColor = true;
            this.chkWindowAuthentication.CheckedChanged += new System.EventHandler(this.chkWindowAuthentication_CheckedChanged);
            // 
            // cmbServerList
            // 
            this.cmbServerList.FormattingEnabled = true;
            this.cmbServerList.Location = new System.Drawing.Point(67, 24);
            this.cmbServerList.Name = "cmbServerList";
            this.cmbServerList.Size = new System.Drawing.Size(214, 21);
            this.cmbServerList.TabIndex = 1;
            this.cmbServerList.Text = "HAIER-PC";
            this.cmbServerList.SelectedIndexChanged += new System.EventHandler(this.cmbServerList_SelectedIndexChanged);
            // 
            // lblServerName
            // 
            this.lblServerName.AutoSize = true;
            this.lblServerName.Location = new System.Drawing.Point(8, 27);
            this.lblServerName.Name = "lblServerName";
            this.lblServerName.Size = new System.Drawing.Size(41, 13);
            this.lblServerName.TabIndex = 0;
            this.lblServerName.Text = "Server:";
            // 
            // btnGenerate
            // 
            this.btnGenerate.Location = new System.Drawing.Point(417, 445);
            this.btnGenerate.Name = "btnGenerate";
            this.btnGenerate.Size = new System.Drawing.Size(200, 27);
            this.btnGenerate.TabIndex = 18;
            this.btnGenerate.Text = "Generate";
            this.btnGenerate.UseVisualStyleBackColor = true;
            this.btnGenerate.Click += new System.EventHandler(this.btnGenerate_Click);
            // 
            // gbLanguageOptions
            // 
            this.gbLanguageOptions.Controls.Add(this.rdbVB);
            this.gbLanguageOptions.Controls.Add(this.rdbCS);
            this.gbLanguageOptions.Location = new System.Drawing.Point(623, 89);
            this.gbLanguageOptions.Name = "gbLanguageOptions";
            this.gbLanguageOptions.Size = new System.Drawing.Size(269, 44);
            this.gbLanguageOptions.TabIndex = 1;
            this.gbLanguageOptions.TabStop = false;
            this.gbLanguageOptions.Text = "Language Options";
            // 
            // rdbVB
            // 
            this.rdbVB.AutoSize = true;
            this.rdbVB.Location = new System.Drawing.Point(53, 19);
            this.rdbVB.Name = "rdbVB";
            this.rdbVB.Size = new System.Drawing.Size(39, 17);
            this.rdbVB.TabIndex = 1;
            this.rdbVB.Text = "VB";
            this.rdbVB.UseVisualStyleBackColor = true;
            // 
            // rdbCS
            // 
            this.rdbCS.AutoSize = true;
            this.rdbCS.Checked = true;
            this.rdbCS.Location = new System.Drawing.Point(6, 19);
            this.rdbCS.Name = "rdbCS";
            this.rdbCS.Size = new System.Drawing.Size(39, 17);
            this.rdbCS.TabIndex = 0;
            this.rdbCS.TabStop = true;
            this.rdbCS.Text = "C#";
            this.rdbCS.UseVisualStyleBackColor = true;
            // 
            // gbCodeGenerationLog
            // 
            this.gbCodeGenerationLog.Controls.Add(this.txtGeneratorLog);
            this.gbCodeGenerationLog.Location = new System.Drawing.Point(898, 13);
            this.gbCodeGenerationLog.Name = "gbCodeGenerationLog";
            this.gbCodeGenerationLog.Size = new System.Drawing.Size(324, 455);
            this.gbCodeGenerationLog.TabIndex = 19;
            this.gbCodeGenerationLog.TabStop = false;
            this.gbCodeGenerationLog.Text = "Code Generation Log";
            // 
            // txtGeneratorLog
            // 
            this.txtGeneratorLog.Location = new System.Drawing.Point(6, 18);
            this.txtGeneratorLog.Multiline = true;
            this.txtGeneratorLog.Name = "txtGeneratorLog";
            this.txtGeneratorLog.Size = new System.Drawing.Size(312, 428);
            this.txtGeneratorLog.TabIndex = 0;
            this.txtGeneratorLog.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.txtGeneratorLog_KeyPress);
            // 
            // lstTableList
            // 
            this.lstTableList.Alignment = System.Windows.Forms.ListViewAlignment.Left;
            this.lstTableList.CheckBoxes = true;
            this.lstTableList.HideSelection = false;
            listViewItem1.Checked = true;
            listViewItem1.StateImageIndex = 1;
            this.lstTableList.Items.AddRange(new System.Windows.Forms.ListViewItem[] {
            listViewItem1});
            this.lstTableList.Location = new System.Drawing.Point(29, 183);
            this.lstTableList.Name = "lstTableList";
            this.lstTableList.Size = new System.Drawing.Size(309, 256);
            this.lstTableList.TabIndex = 20;
            this.lstTableList.UseCompatibleStateImageBehavior = false;
            this.lstTableList.View = System.Windows.Forms.View.List;
            this.lstTableList.MouseClick += new System.Windows.Forms.MouseEventHandler(this.lstTableList_MouseClick);
            // 
            // txtColumnsList
            // 
            this.txtColumnsList.Location = new System.Drawing.Point(344, 183);
            this.txtColumnsList.Multiline = true;
            this.txtColumnsList.Name = "txtColumnsList";
            this.txtColumnsList.Size = new System.Drawing.Size(273, 256);
            this.txtColumnsList.TabIndex = 21;
            // 
            // chkSelectAllTable
            // 
            this.chkSelectAllTable.AutoSize = true;
            this.chkSelectAllTable.Location = new System.Drawing.Point(29, 442);
            this.chkSelectAllTable.Name = "chkSelectAllTable";
            this.chkSelectAllTable.Size = new System.Drawing.Size(105, 17);
            this.chkSelectAllTable.TabIndex = 22;
            this.chkSelectAllTable.Text = "Select All Tables";
            this.chkSelectAllTable.UseVisualStyleBackColor = true;
            this.chkSelectAllTable.CheckedChanged += new System.EventHandler(this.chkSelectAllTable_CheckedChanged);
            // 
            // lstStoredProcedures
            // 
            this.lstStoredProcedures.FormattingEnabled = true;
            this.lstStoredProcedures.Location = new System.Drawing.Point(623, 142);
            this.lstStoredProcedures.Name = "lstStoredProcedures";
            this.lstStoredProcedures.Size = new System.Drawing.Size(269, 212);
            this.lstStoredProcedures.TabIndex = 23;
            // 
            // btnRemoreProc
            // 
            this.btnRemoveProc.Location = new System.Drawing.Point(623, 360);
            this.btnRemoveProc.Name = "btnRemoreProc";
            this.btnRemoveProc.Size = new System.Drawing.Size(269, 23);
            this.btnRemoveProc.TabIndex = 24;
            this.btnRemoveProc.Text = "Remove Procedures";
            this.btnRemoveProc.UseVisualStyleBackColor = true;
            this.btnRemoveProc.Click += new System.EventHandler(this.btnRemoveProc_Click);
            // 
            // gbOptions
            // 
            this.gbOptions.Controls.Add(this.chkStoredProcedures);
            this.gbOptions.Controls.Add(this.chkClasses);
            this.gbOptions.Location = new System.Drawing.Point(623, 13);
            this.gbOptions.Name = "gbOptions";
            this.gbOptions.Size = new System.Drawing.Size(269, 70);
            this.gbOptions.TabIndex = 25;
            this.gbOptions.TabStop = false;
            this.gbOptions.Text = "Options";
            // 
            // chkClasses
            // 
            this.chkClasses.AutoSize = true;
            this.chkClasses.Checked = true;
            this.chkClasses.CheckState = System.Windows.Forms.CheckState.Checked;
            this.chkClasses.Location = new System.Drawing.Point(6, 20);
            this.chkClasses.Name = "chkClasses";
            this.chkClasses.Size = new System.Drawing.Size(62, 17);
            this.chkClasses.TabIndex = 0;
            this.chkClasses.Text = "Classes";
            this.chkClasses.UseVisualStyleBackColor = true;
            // 
            // chkStoredProcedures
            // 
            this.chkStoredProcedures.AutoSize = true;
            this.chkStoredProcedures.Checked = true;
            this.chkStoredProcedures.CheckState = System.Windows.Forms.CheckState.Checked;
            this.chkStoredProcedures.Location = new System.Drawing.Point(6, 43);
            this.chkStoredProcedures.Name = "chkStoredProcedures";
            this.chkStoredProcedures.Size = new System.Drawing.Size(114, 17);
            this.chkStoredProcedures.TabIndex = 1;
            this.chkStoredProcedures.Text = "Stored Procedures";
            this.chkStoredProcedures.UseVisualStyleBackColor = true;
            // 
            // frmCodeGenerator
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1234, 484);
            this.Controls.Add(this.gbOptions);
            this.Controls.Add(this.btnRemoveProc);
            this.Controls.Add(this.lstStoredProcedures);
            this.Controls.Add(this.chkSelectAllTable);
            this.Controls.Add(this.txtColumnsList);
            this.Controls.Add(this.lstTableList);
            this.Controls.Add(this.gbCodeGenerationLog);
            this.Controls.Add(this.btnGenerate);
            this.Controls.Add(this.gbLanguageOptions);
            this.Controls.Add(this.gbDatabaseConnection);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.MaximizeBox = false;
            this.Name = "frmCodeGenerator";
            this.Text = "Code Generator 2.0";
            this.Load += new System.EventHandler(this.frmCodeGenerator_Load);
            this.gbDatabaseConnection.ResumeLayout(false);
            this.gbDatabaseConnection.PerformLayout();
            this.gbLanguageOptions.ResumeLayout(false);
            this.gbLanguageOptions.PerformLayout();
            this.gbCodeGenerationLog.ResumeLayout(false);
            this.gbCodeGenerationLog.PerformLayout();
            this.gbOptions.ResumeLayout(false);
            this.gbOptions.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.GroupBox gbDatabaseConnection;
        private System.Windows.Forms.ComboBox cmbServerList;
        private System.Windows.Forms.Label lblServerName;
        private System.Windows.Forms.CheckBox chkWindowAuthentication;
        private System.Windows.Forms.TextBox txtPassword;
        private System.Windows.Forms.Label lblPassword;
        private System.Windows.Forms.TextBox txtUsername;
        private System.Windows.Forms.Label lblUsername;
        private System.Windows.Forms.Button btnConnect;
        private System.Windows.Forms.TextBox txtOutputFolder;
        private System.Windows.Forms.Label lblOutputFolder;
        private System.Windows.Forms.Button btnGenerate;
        private System.Windows.Forms.GroupBox gbLanguageOptions;
        private System.Windows.Forms.RadioButton rdbVB;
        private System.Windows.Forms.RadioButton rdbCS;
        private System.Windows.Forms.GroupBox gbCodeGenerationLog;
        private System.Windows.Forms.TextBox txtGeneratorLog;
        private System.Windows.Forms.Button btnSelectOutputFolder;
        private System.Windows.Forms.ComboBox cmbDatabaseList;
        private System.Windows.Forms.TextBox txtConnectionString;
        private System.Windows.Forms.Label lblConnectionString;
        private System.Windows.Forms.Label lblDatabase;
        private System.Windows.Forms.ListView lstTableList;
        private System.Windows.Forms.TextBox txtColumnsList;
        private System.Windows.Forms.CheckBox chkSelectAllTable;
        private System.Windows.Forms.ListBox lstStoredProcedures;
        private System.Windows.Forms.Button btnRemoveProc;
        private System.Windows.Forms.GroupBox gbOptions;
        private System.Windows.Forms.CheckBox chkStoredProcedures;
        private System.Windows.Forms.CheckBox chkClasses;
    }
}

