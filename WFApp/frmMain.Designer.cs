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
            this.gridMap = new System.Windows.Forms.DataGridView();
            this.btnStartNew = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.gridMap)).BeginInit();
            this.SuspendLayout();
            // 
            // gridMap
            // 
            this.gridMap.AllowUserToAddRows = false;
            this.gridMap.AllowUserToDeleteRows = false;
            this.gridMap.CellBorderStyle = System.Windows.Forms.DataGridViewCellBorderStyle.None;
            this.gridMap.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.gridMap.ColumnHeadersVisible = false;
            this.gridMap.Location = new System.Drawing.Point(1, 2);
            this.gridMap.Name = "gridMap";
            this.gridMap.ReadOnly = true;
            this.gridMap.RowHeadersVisible = false;
            this.gridMap.Size = new System.Drawing.Size(281, 189);
            this.gridMap.TabIndex = 0;
            this.gridMap.KeyDown += new System.Windows.Forms.KeyEventHandler(this.frmMain_KeyDown);
            // 
            // btnStartNew
            // 
            this.btnStartNew.Location = new System.Drawing.Point(106, 227);
            this.btnStartNew.Name = "btnStartNew";
            this.btnStartNew.Size = new System.Drawing.Size(75, 23);
            this.btnStartNew.TabIndex = 1;
            this.btnStartNew.Text = "Start!";
            this.btnStartNew.UseVisualStyleBackColor = true;
            this.btnStartNew.Click += new System.EventHandler(this.btnStartNew_Click);
            // 
            // frmMain
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(284, 262);
            this.Controls.Add(this.btnStartNew);
            this.Controls.Add(this.gridMap);
            this.Name = "frmMain";
            this.Text = "Main";
            this.KeyDown += new System.Windows.Forms.KeyEventHandler(this.frmMain_KeyDown);
            ((System.ComponentModel.ISupportInitialize)(this.gridMap)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.DataGridView gridMap;
        private System.Windows.Forms.Button btnStartNew;

    }
}

