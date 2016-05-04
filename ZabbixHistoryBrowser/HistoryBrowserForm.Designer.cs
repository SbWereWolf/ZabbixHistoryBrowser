namespace ZabbixHistoryBrowser
{
    partial class ZabbixHistoryBrowserForm
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
            this.DesktopTabControl = new System.Windows.Forms.TabControl();
            this.ConnectionTabPage = new System.Windows.Forms.TabPage();
            this.ConnectionRequsitesGroupBox = new System.Windows.Forms.GroupBox();
            this.TestConnectionGroupBox = new System.Windows.Forms.GroupBox();
            this.TestConnectionButton = new System.Windows.Forms.Button();
            this.UserPasswordGroupBox = new System.Windows.Forms.GroupBox();
            this.UserPasswordTextBox = new System.Windows.Forms.TextBox();
            this.UserIdGroupBox = new System.Windows.Forms.GroupBox();
            this.UserIdTextBox = new System.Windows.Forms.TextBox();
            this.DatabaseNameGroupBox = new System.Windows.Forms.GroupBox();
            this.DatabaseNameTextBox = new System.Windows.Forms.TextBox();
            this.ConnectionsPortGroupBox = new System.Windows.Forms.GroupBox();
            this.ConnectionsPortTextBox = new System.Windows.Forms.TextBox();
            this.ServerAddressGroupBox = new System.Windows.Forms.GroupBox();
            this.ServerAddressTextBox = new System.Windows.Forms.TextBox();
            this.HostsTabPage = new System.Windows.Forms.TabPage();
            this.HostsListGroupBox = new System.Windows.Forms.GroupBox();
            this.HostsDataGridView = new System.Windows.Forms.DataGridView();
            this.ChooseHostGroupBox = new System.Windows.Forms.GroupBox();
            this.ChooseHostButton = new System.Windows.Forms.Button();
            this.GetHostsListGroupBox = new System.Windows.Forms.GroupBox();
            this.GetHostsListButton = new System.Windows.Forms.Button();
            this.ItemsTabPage = new System.Windows.Forms.TabPage();
            this.HistoryTabPage = new System.Windows.Forms.TabPage();
            this.ReportTabPage = new System.Windows.Forms.TabPage();
            this.DesktopTabControl.SuspendLayout();
            this.ConnectionTabPage.SuspendLayout();
            this.ConnectionRequsitesGroupBox.SuspendLayout();
            this.TestConnectionGroupBox.SuspendLayout();
            this.UserPasswordGroupBox.SuspendLayout();
            this.UserIdGroupBox.SuspendLayout();
            this.DatabaseNameGroupBox.SuspendLayout();
            this.ConnectionsPortGroupBox.SuspendLayout();
            this.ServerAddressGroupBox.SuspendLayout();
            this.HostsTabPage.SuspendLayout();
            this.HostsListGroupBox.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.HostsDataGridView)).BeginInit();
            this.ChooseHostGroupBox.SuspendLayout();
            this.GetHostsListGroupBox.SuspendLayout();
            this.SuspendLayout();
            // 
            // DesktopTabControl
            // 
            this.DesktopTabControl.Controls.Add(this.ConnectionTabPage);
            this.DesktopTabControl.Controls.Add(this.HostsTabPage);
            this.DesktopTabControl.Controls.Add(this.ItemsTabPage);
            this.DesktopTabControl.Controls.Add(this.HistoryTabPage);
            this.DesktopTabControl.Controls.Add(this.ReportTabPage);
            this.DesktopTabControl.Dock = System.Windows.Forms.DockStyle.Fill;
            this.DesktopTabControl.Location = new System.Drawing.Point(0, 0);
            this.DesktopTabControl.Name = "DesktopTabControl";
            this.DesktopTabControl.SelectedIndex = 0;
            this.DesktopTabControl.Size = new System.Drawing.Size(777, 548);
            this.DesktopTabControl.TabIndex = 0;
            // 
            // ConnectionTabPage
            // 
            this.ConnectionTabPage.Controls.Add(this.ConnectionRequsitesGroupBox);
            this.ConnectionTabPage.Location = new System.Drawing.Point(4, 25);
            this.ConnectionTabPage.Name = "ConnectionTabPage";
            this.ConnectionTabPage.Padding = new System.Windows.Forms.Padding(3);
            this.ConnectionTabPage.Size = new System.Drawing.Size(769, 519);
            this.ConnectionTabPage.TabIndex = 0;
            this.ConnectionTabPage.Text = "Соединение с БД";
            this.ConnectionTabPage.UseVisualStyleBackColor = true;
            // 
            // ConnectionRequsitesGroupBox
            // 
            this.ConnectionRequsitesGroupBox.Controls.Add(this.TestConnectionGroupBox);
            this.ConnectionRequsitesGroupBox.Controls.Add(this.UserPasswordGroupBox);
            this.ConnectionRequsitesGroupBox.Controls.Add(this.UserIdGroupBox);
            this.ConnectionRequsitesGroupBox.Controls.Add(this.DatabaseNameGroupBox);
            this.ConnectionRequsitesGroupBox.Controls.Add(this.ConnectionsPortGroupBox);
            this.ConnectionRequsitesGroupBox.Controls.Add(this.ServerAddressGroupBox);
            this.ConnectionRequsitesGroupBox.Dock = System.Windows.Forms.DockStyle.Fill;
            this.ConnectionRequsitesGroupBox.Location = new System.Drawing.Point(3, 3);
            this.ConnectionRequsitesGroupBox.Name = "ConnectionRequsitesGroupBox";
            this.ConnectionRequsitesGroupBox.Size = new System.Drawing.Size(763, 513);
            this.ConnectionRequsitesGroupBox.TabIndex = 0;
            this.ConnectionRequsitesGroupBox.TabStop = false;
            this.ConnectionRequsitesGroupBox.Text = "Реквизиты соединения";
            // 
            // TestConnectionGroupBox
            // 
            this.TestConnectionGroupBox.Controls.Add(this.TestConnectionButton);
            this.TestConnectionGroupBox.Dock = System.Windows.Forms.DockStyle.Top;
            this.TestConnectionGroupBox.Location = new System.Drawing.Point(3, 248);
            this.TestConnectionGroupBox.Name = "TestConnectionGroupBox";
            this.TestConnectionGroupBox.Size = new System.Drawing.Size(757, 54);
            this.TestConnectionGroupBox.TabIndex = 6;
            this.TestConnectionGroupBox.TabStop = false;
            this.TestConnectionGroupBox.Text = "Проверить соединение";
            // 
            // TestConnectionButton
            // 
            this.TestConnectionButton.Dock = System.Windows.Forms.DockStyle.Fill;
            this.TestConnectionButton.Location = new System.Drawing.Point(3, 18);
            this.TestConnectionButton.Name = "TestConnectionButton";
            this.TestConnectionButton.Size = new System.Drawing.Size(751, 33);
            this.TestConnectionButton.TabIndex = 5;
            this.TestConnectionButton.Text = "Проверить";
            this.TestConnectionButton.UseVisualStyleBackColor = true;
            this.TestConnectionButton.Click += new System.EventHandler(this.TestConnectionButton_Click);
            // 
            // UserPasswordGroupBox
            // 
            this.UserPasswordGroupBox.Controls.Add(this.UserPasswordTextBox);
            this.UserPasswordGroupBox.Dock = System.Windows.Forms.DockStyle.Top;
            this.UserPasswordGroupBox.Location = new System.Drawing.Point(3, 202);
            this.UserPasswordGroupBox.Name = "UserPasswordGroupBox";
            this.UserPasswordGroupBox.Size = new System.Drawing.Size(757, 46);
            this.UserPasswordGroupBox.TabIndex = 4;
            this.UserPasswordGroupBox.TabStop = false;
            this.UserPasswordGroupBox.Text = "Пароль";
            // 
            // UserPasswordTextBox
            // 
            this.UserPasswordTextBox.Dock = System.Windows.Forms.DockStyle.Fill;
            this.UserPasswordTextBox.Location = new System.Drawing.Point(3, 18);
            this.UserPasswordTextBox.Name = "UserPasswordTextBox";
            this.UserPasswordTextBox.Size = new System.Drawing.Size(751, 22);
            this.UserPasswordTextBox.TabIndex = 0;
            this.UserPasswordTextBox.Text = "G1vfEDmTCZ7";
            // 
            // UserIdGroupBox
            // 
            this.UserIdGroupBox.Controls.Add(this.UserIdTextBox);
            this.UserIdGroupBox.Dock = System.Windows.Forms.DockStyle.Top;
            this.UserIdGroupBox.Location = new System.Drawing.Point(3, 156);
            this.UserIdGroupBox.Name = "UserIdGroupBox";
            this.UserIdGroupBox.Size = new System.Drawing.Size(757, 46);
            this.UserIdGroupBox.TabIndex = 3;
            this.UserIdGroupBox.TabStop = false;
            this.UserIdGroupBox.Text = "Имя пользователя";
            // 
            // UserIdTextBox
            // 
            this.UserIdTextBox.Dock = System.Windows.Forms.DockStyle.Fill;
            this.UserIdTextBox.Location = new System.Drawing.Point(3, 18);
            this.UserIdTextBox.Name = "UserIdTextBox";
            this.UserIdTextBox.Size = new System.Drawing.Size(751, 22);
            this.UserIdTextBox.TabIndex = 0;
            this.UserIdTextBox.Text = "zabbixr";
            // 
            // DatabaseNameGroupBox
            // 
            this.DatabaseNameGroupBox.Controls.Add(this.DatabaseNameTextBox);
            this.DatabaseNameGroupBox.Dock = System.Windows.Forms.DockStyle.Top;
            this.DatabaseNameGroupBox.Location = new System.Drawing.Point(3, 110);
            this.DatabaseNameGroupBox.Name = "DatabaseNameGroupBox";
            this.DatabaseNameGroupBox.Size = new System.Drawing.Size(757, 46);
            this.DatabaseNameGroupBox.TabIndex = 2;
            this.DatabaseNameGroupBox.TabStop = false;
            this.DatabaseNameGroupBox.Text = "База данных";
            // 
            // DatabaseNameTextBox
            // 
            this.DatabaseNameTextBox.Dock = System.Windows.Forms.DockStyle.Fill;
            this.DatabaseNameTextBox.Location = new System.Drawing.Point(3, 18);
            this.DatabaseNameTextBox.Name = "DatabaseNameTextBox";
            this.DatabaseNameTextBox.Size = new System.Drawing.Size(751, 22);
            this.DatabaseNameTextBox.TabIndex = 0;
            this.DatabaseNameTextBox.Text = "zabbix";
            // 
            // ConnectionsPortGroupBox
            // 
            this.ConnectionsPortGroupBox.Controls.Add(this.ConnectionsPortTextBox);
            this.ConnectionsPortGroupBox.Dock = System.Windows.Forms.DockStyle.Top;
            this.ConnectionsPortGroupBox.Location = new System.Drawing.Point(3, 64);
            this.ConnectionsPortGroupBox.Name = "ConnectionsPortGroupBox";
            this.ConnectionsPortGroupBox.Size = new System.Drawing.Size(757, 46);
            this.ConnectionsPortGroupBox.TabIndex = 1;
            this.ConnectionsPortGroupBox.TabStop = false;
            this.ConnectionsPortGroupBox.Text = "Порт для соединения";
            // 
            // ConnectionsPortTextBox
            // 
            this.ConnectionsPortTextBox.Dock = System.Windows.Forms.DockStyle.Fill;
            this.ConnectionsPortTextBox.Location = new System.Drawing.Point(3, 18);
            this.ConnectionsPortTextBox.Name = "ConnectionsPortTextBox";
            this.ConnectionsPortTextBox.Size = new System.Drawing.Size(751, 22);
            this.ConnectionsPortTextBox.TabIndex = 0;
            this.ConnectionsPortTextBox.Text = "3306";
            // 
            // ServerAddressGroupBox
            // 
            this.ServerAddressGroupBox.Controls.Add(this.ServerAddressTextBox);
            this.ServerAddressGroupBox.Dock = System.Windows.Forms.DockStyle.Top;
            this.ServerAddressGroupBox.Location = new System.Drawing.Point(3, 18);
            this.ServerAddressGroupBox.Name = "ServerAddressGroupBox";
            this.ServerAddressGroupBox.Size = new System.Drawing.Size(757, 46);
            this.ServerAddressGroupBox.TabIndex = 0;
            this.ServerAddressGroupBox.TabStop = false;
            this.ServerAddressGroupBox.Text = "Адрес сервера базы данных";
            // 
            // ServerAddressTextBox
            // 
            this.ServerAddressTextBox.Dock = System.Windows.Forms.DockStyle.Fill;
            this.ServerAddressTextBox.Location = new System.Drawing.Point(3, 18);
            this.ServerAddressTextBox.Name = "ServerAddressTextBox";
            this.ServerAddressTextBox.Size = new System.Drawing.Size(751, 22);
            this.ServerAddressTextBox.TabIndex = 0;
            this.ServerAddressTextBox.Text = "vpn.magna-tech.org";
            // 
            // HostsTabPage
            // 
            this.HostsTabPage.Controls.Add(this.HostsListGroupBox);
            this.HostsTabPage.Controls.Add(this.ChooseHostGroupBox);
            this.HostsTabPage.Controls.Add(this.GetHostsListGroupBox);
            this.HostsTabPage.Location = new System.Drawing.Point(4, 25);
            this.HostsTabPage.Name = "HostsTabPage";
            this.HostsTabPage.Padding = new System.Windows.Forms.Padding(3);
            this.HostsTabPage.Size = new System.Drawing.Size(769, 519);
            this.HostsTabPage.TabIndex = 1;
            this.HostsTabPage.Text = "Приборы";
            this.HostsTabPage.UseVisualStyleBackColor = true;
            // 
            // HostsListGroupBox
            // 
            this.HostsListGroupBox.Controls.Add(this.HostsDataGridView);
            this.HostsListGroupBox.Dock = System.Windows.Forms.DockStyle.Fill;
            this.HostsListGroupBox.Location = new System.Drawing.Point(3, 60);
            this.HostsListGroupBox.Name = "HostsListGroupBox";
            this.HostsListGroupBox.Size = new System.Drawing.Size(763, 400);
            this.HostsListGroupBox.TabIndex = 1;
            this.HostsListGroupBox.TabStop = false;
            this.HostsListGroupBox.Text = "Список приборов";
            // 
            // HostsDataGridView
            // 
            this.HostsDataGridView.AllowUserToAddRows = false;
            this.HostsDataGridView.AllowUserToDeleteRows = false;
            this.HostsDataGridView.AllowUserToOrderColumns = true;
            this.HostsDataGridView.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.HostsDataGridView.Dock = System.Windows.Forms.DockStyle.Fill;
            this.HostsDataGridView.Location = new System.Drawing.Point(3, 18);
            this.HostsDataGridView.Name = "HostsDataGridView";
            this.HostsDataGridView.ReadOnly = true;
            this.HostsDataGridView.RowTemplate.Height = 24;
            this.HostsDataGridView.Size = new System.Drawing.Size(757, 379);
            this.HostsDataGridView.TabIndex = 0;
            // 
            // ChooseHostGroupBox
            // 
            this.ChooseHostGroupBox.Controls.Add(this.ChooseHostButton);
            this.ChooseHostGroupBox.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.ChooseHostGroupBox.Location = new System.Drawing.Point(3, 460);
            this.ChooseHostGroupBox.Name = "ChooseHostGroupBox";
            this.ChooseHostGroupBox.Size = new System.Drawing.Size(763, 56);
            this.ChooseHostGroupBox.TabIndex = 2;
            this.ChooseHostGroupBox.TabStop = false;
            this.ChooseHostGroupBox.Text = "Выбор прибора";
            // 
            // ChooseHostButton
            // 
            this.ChooseHostButton.Dock = System.Windows.Forms.DockStyle.Fill;
            this.ChooseHostButton.Location = new System.Drawing.Point(3, 18);
            this.ChooseHostButton.Name = "ChooseHostButton";
            this.ChooseHostButton.Size = new System.Drawing.Size(757, 35);
            this.ChooseHostButton.TabIndex = 1;
            this.ChooseHostButton.Text = "Выбрать прибор";
            this.ChooseHostButton.UseVisualStyleBackColor = true;
            // 
            // GetHostsListGroupBox
            // 
            this.GetHostsListGroupBox.Controls.Add(this.GetHostsListButton);
            this.GetHostsListGroupBox.Dock = System.Windows.Forms.DockStyle.Top;
            this.GetHostsListGroupBox.Location = new System.Drawing.Point(3, 3);
            this.GetHostsListGroupBox.Name = "GetHostsListGroupBox";
            this.GetHostsListGroupBox.Size = new System.Drawing.Size(763, 57);
            this.GetHostsListGroupBox.TabIndex = 0;
            this.GetHostsListGroupBox.TabStop = false;
            this.GetHostsListGroupBox.Text = "Загрузка списка приборов";
            // 
            // GetHostsListButton
            // 
            this.GetHostsListButton.Dock = System.Windows.Forms.DockStyle.Fill;
            this.GetHostsListButton.Location = new System.Drawing.Point(3, 18);
            this.GetHostsListButton.Name = "GetHostsListButton";
            this.GetHostsListButton.Size = new System.Drawing.Size(757, 36);
            this.GetHostsListButton.TabIndex = 0;
            this.GetHostsListButton.Text = "Загрузить список приборов";
            this.GetHostsListButton.UseVisualStyleBackColor = true;
            this.GetHostsListButton.Click += new System.EventHandler(this.GetHostsListButton_Click);
            // 
            // ItemsTabPage
            // 
            this.ItemsTabPage.Location = new System.Drawing.Point(4, 25);
            this.ItemsTabPage.Name = "ItemsTabPage";
            this.ItemsTabPage.Padding = new System.Windows.Forms.Padding(3);
            this.ItemsTabPage.Size = new System.Drawing.Size(769, 519);
            this.ItemsTabPage.TabIndex = 2;
            this.ItemsTabPage.Text = "Показатели";
            this.ItemsTabPage.UseVisualStyleBackColor = true;
            // 
            // HistoryTabPage
            // 
            this.HistoryTabPage.Location = new System.Drawing.Point(4, 25);
            this.HistoryTabPage.Name = "HistoryTabPage";
            this.HistoryTabPage.Padding = new System.Windows.Forms.Padding(3);
            this.HistoryTabPage.Size = new System.Drawing.Size(769, 519);
            this.HistoryTabPage.TabIndex = 3;
            this.HistoryTabPage.Text = "Значения";
            this.HistoryTabPage.UseVisualStyleBackColor = true;
            // 
            // ReportTabPage
            // 
            this.ReportTabPage.Location = new System.Drawing.Point(4, 25);
            this.ReportTabPage.Name = "ReportTabPage";
            this.ReportTabPage.Padding = new System.Windows.Forms.Padding(3);
            this.ReportTabPage.Size = new System.Drawing.Size(769, 519);
            this.ReportTabPage.TabIndex = 4;
            this.ReportTabPage.Text = "Отчёт";
            this.ReportTabPage.UseVisualStyleBackColor = true;
            // 
            // ZabbixHistoryBrowserForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(777, 548);
            this.Controls.Add(this.DesktopTabControl);
            this.Name = "ZabbixHistoryBrowserForm";
            this.Text = "Просмотр истории измерений";
            this.DesktopTabControl.ResumeLayout(false);
            this.ConnectionTabPage.ResumeLayout(false);
            this.ConnectionRequsitesGroupBox.ResumeLayout(false);
            this.TestConnectionGroupBox.ResumeLayout(false);
            this.UserPasswordGroupBox.ResumeLayout(false);
            this.UserPasswordGroupBox.PerformLayout();
            this.UserIdGroupBox.ResumeLayout(false);
            this.UserIdGroupBox.PerformLayout();
            this.DatabaseNameGroupBox.ResumeLayout(false);
            this.DatabaseNameGroupBox.PerformLayout();
            this.ConnectionsPortGroupBox.ResumeLayout(false);
            this.ConnectionsPortGroupBox.PerformLayout();
            this.ServerAddressGroupBox.ResumeLayout(false);
            this.ServerAddressGroupBox.PerformLayout();
            this.HostsTabPage.ResumeLayout(false);
            this.HostsListGroupBox.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.HostsDataGridView)).EndInit();
            this.ChooseHostGroupBox.ResumeLayout(false);
            this.GetHostsListGroupBox.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.TabControl DesktopTabControl;
        private System.Windows.Forms.TabPage ConnectionTabPage;
        private System.Windows.Forms.TabPage HostsTabPage;
        private System.Windows.Forms.TabPage ItemsTabPage;
        private System.Windows.Forms.TabPage HistoryTabPage;
        private System.Windows.Forms.TabPage ReportTabPage;
        private System.Windows.Forms.GroupBox ConnectionRequsitesGroupBox;
        private System.Windows.Forms.GroupBox TestConnectionGroupBox;
        private System.Windows.Forms.Button TestConnectionButton;
        private System.Windows.Forms.GroupBox UserPasswordGroupBox;
        private System.Windows.Forms.TextBox UserPasswordTextBox;
        private System.Windows.Forms.GroupBox UserIdGroupBox;
        private System.Windows.Forms.TextBox UserIdTextBox;
        private System.Windows.Forms.GroupBox DatabaseNameGroupBox;
        private System.Windows.Forms.TextBox DatabaseNameTextBox;
        private System.Windows.Forms.GroupBox ConnectionsPortGroupBox;
        private System.Windows.Forms.TextBox ConnectionsPortTextBox;
        private System.Windows.Forms.GroupBox ServerAddressGroupBox;
        private System.Windows.Forms.TextBox ServerAddressTextBox;
        private System.Windows.Forms.GroupBox HostsListGroupBox;
        private System.Windows.Forms.DataGridView HostsDataGridView;
        private System.Windows.Forms.GroupBox ChooseHostGroupBox;
        private System.Windows.Forms.Button ChooseHostButton;
        private System.Windows.Forms.GroupBox GetHostsListGroupBox;
        private System.Windows.Forms.Button GetHostsListButton;
    }
}

