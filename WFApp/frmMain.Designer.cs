namespace WFApp
{
    partial class frmMain
    {
        /// <summary>
        /// Требуется переменная конструктора.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Освободить все используемые ресурсы.
        /// </summary>
        /// <param name="disposing">истинно, если управляемый ресурс должен быть удален; иначе ложно.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Код, автоматически созданный конструктором форм Windows

        /// <summary>
        /// Обязательный метод для поддержки конструктора - не изменяйте
        /// содержимое данного метода при помощи редактора кода.
        /// </summary>
        private void InitializeComponent()
        {
            this.menuMain = new System.Windows.Forms.MenuStrip();
            this.gameToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.startNewToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.parametersToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.exitToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.helpToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.helpTopicsToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.aboutToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.lblMessage = new System.Windows.Forms.Label();
            this.gridMap = new WFApp.BufferedDataGridView();
            this.menuMain.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.gridMap)).BeginInit();
            this.SuspendLayout();
            // 
            // menuMain
            // 
            this.menuMain.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.gameToolStripMenuItem,
            this.helpToolStripMenuItem});
            this.menuMain.Location = new System.Drawing.Point(0, 0);
            this.menuMain.Name = "menuMain";
            this.menuMain.Size = new System.Drawing.Size(284, 24);
            this.menuMain.TabIndex = 2;
            this.menuMain.Text = "menuStrip1";
            // 
            // gameToolStripMenuItem
            // 
            this.gameToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.startNewToolStripMenuItem,
            this.parametersToolStripMenuItem,
            this.exitToolStripMenuItem});
            this.gameToolStripMenuItem.Name = "gameToolStripMenuItem";
            this.gameToolStripMenuItem.Size = new System.Drawing.Size(50, 20);
            this.gameToolStripMenuItem.Text = "Game";
            // 
            // startNewToolStripMenuItem
            // 
            this.startNewToolStripMenuItem.Name = "startNewToolStripMenuItem";
            this.startNewToolStripMenuItem.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.N)));
            this.startNewToolStripMenuItem.Size = new System.Drawing.Size(166, 22);
            this.startNewToolStripMenuItem.Text = "Start new";
            this.startNewToolStripMenuItem.Click += new System.EventHandler(this.startNewToolStripMenuItem_Click);
            // 
            // parametersToolStripMenuItem
            // 
            this.parametersToolStripMenuItem.Name = "parametersToolStripMenuItem";
            this.parametersToolStripMenuItem.Size = new System.Drawing.Size(166, 22);
            this.parametersToolStripMenuItem.Text = "Parameters...";
            this.parametersToolStripMenuItem.Click += new System.EventHandler(this.parametersToolStripMenuItem_Click);
            // 
            // exitToolStripMenuItem
            // 
            this.exitToolStripMenuItem.Name = "exitToolStripMenuItem";
            this.exitToolStripMenuItem.Size = new System.Drawing.Size(166, 22);
            this.exitToolStripMenuItem.Text = "Exit";
            // 
            // helpToolStripMenuItem
            // 
            this.helpToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.helpTopicsToolStripMenuItem,
            this.aboutToolStripMenuItem});
            this.helpToolStripMenuItem.Name = "helpToolStripMenuItem";
            this.helpToolStripMenuItem.Size = new System.Drawing.Size(44, 20);
            this.helpToolStripMenuItem.Text = "Help";
            // 
            // helpTopicsToolStripMenuItem
            // 
            this.helpTopicsToolStripMenuItem.Name = "helpTopicsToolStripMenuItem";
            this.helpTopicsToolStripMenuItem.Size = new System.Drawing.Size(134, 22);
            this.helpTopicsToolStripMenuItem.Text = "HelpTopics";
            // 
            // aboutToolStripMenuItem
            // 
            this.aboutToolStripMenuItem.Name = "aboutToolStripMenuItem";
            this.aboutToolStripMenuItem.Size = new System.Drawing.Size(134, 22);
            this.aboutToolStripMenuItem.Text = "About";
            // 
            // lblMessage
            // 
            this.lblMessage.AutoSize = true;
            this.lblMessage.Location = new System.Drawing.Point(119, 34);
            this.lblMessage.Name = "lblMessage";
            this.lblMessage.Size = new System.Drawing.Size(0, 13);
            this.lblMessage.TabIndex = 3;
            // 
            // gridMap
            // 
            this.gridMap.AllowUserToAddRows = false;
            this.gridMap.AllowUserToDeleteRows = false;
            this.gridMap.AllowUserToResizeColumns = false;
            this.gridMap.AllowUserToResizeRows = false;
            this.gridMap.CellBorderStyle = System.Windows.Forms.DataGridViewCellBorderStyle.None;
            this.gridMap.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.gridMap.Location = new System.Drawing.Point(0, 50);
            this.gridMap.Name = "gridMap";
            this.gridMap.ReadOnly = true;
            this.gridMap.RowHeadersWidth = 35;
            this.gridMap.RowHeadersWidthSizeMode = System.Windows.Forms.DataGridViewRowHeadersWidthSizeMode.DisableResizing;
            this.gridMap.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.CellSelect;
            this.gridMap.Size = new System.Drawing.Size(284, 189);
            this.gridMap.TabIndex = 0;
            this.gridMap.KeyDown += new System.Windows.Forms.KeyEventHandler(this.frmMain_KeyDown);
            // 
            // frmMain
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.AutoSize = true;
            this.ClientSize = new System.Drawing.Size(284, 262);
            this.Controls.Add(this.lblMessage);
            this.Controls.Add(this.gridMap);
            this.Controls.Add(this.menuMain);
            this.MainMenuStrip = this.menuMain;
            this.Name = "frmMain";
            this.Text = "Main";
            this.KeyDown += new System.Windows.Forms.KeyEventHandler(this.frmMain_KeyDown);
            this.menuMain.ResumeLayout(false);
            this.menuMain.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.gridMap)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private BufferedDataGridView gridMap;
        private System.Windows.Forms.MenuStrip menuMain;
        private System.Windows.Forms.ToolStripMenuItem gameToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem startNewToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem parametersToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem exitToolStripMenuItem;
        private System.Windows.Forms.Label lblMessage;
        private System.Windows.Forms.ToolStripMenuItem helpToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem helpTopicsToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem aboutToolStripMenuItem;

    }
}

