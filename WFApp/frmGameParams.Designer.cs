namespace WFApp
{
    partial class frmGameParams
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
            this.gbGameType = new System.Windows.Forms.GroupBox();
            this.rbOrienteering = new System.Windows.Forms.RadioButton();
            this.rbMaze = new System.Windows.Forms.RadioButton();
            this.btnOK = new System.Windows.Forms.Button();
            this.edtWidth = new System.Windows.Forms.NumericUpDown();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.edtHeight = new System.Windows.Forms.NumericUpDown();
            this.gbGameType.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.edtWidth)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.edtHeight)).BeginInit();
            this.SuspendLayout();
            // 
            // gbGameType
            // 
            this.gbGameType.Controls.Add(this.rbOrienteering);
            this.gbGameType.Controls.Add(this.rbMaze);
            this.gbGameType.Location = new System.Drawing.Point(15, 10);
            this.gbGameType.Name = "gbGameType";
            this.gbGameType.Size = new System.Drawing.Size(96, 71);
            this.gbGameType.TabIndex = 0;
            this.gbGameType.TabStop = false;
            this.gbGameType.Text = "Game type";
            // 
            // rbOrienteering
            // 
            this.rbOrienteering.AutoSize = true;
            this.rbOrienteering.Checked = true;
            this.rbOrienteering.Location = new System.Drawing.Point(7, 43);
            this.rbOrienteering.Name = "rbOrienteering";
            this.rbOrienteering.Size = new System.Drawing.Size(82, 17);
            this.rbOrienteering.TabIndex = 1;
            this.rbOrienteering.TabStop = true;
            this.rbOrienteering.Text = "Orienteering";
            this.rbOrienteering.UseVisualStyleBackColor = true;
            // 
            // rbMaze
            // 
            this.rbMaze.AutoSize = true;
            this.rbMaze.Location = new System.Drawing.Point(6, 19);
            this.rbMaze.Name = "rbMaze";
            this.rbMaze.Size = new System.Drawing.Size(51, 17);
            this.rbMaze.TabIndex = 0;
            this.rbMaze.Text = "Maze";
            this.rbMaze.UseVisualStyleBackColor = true;
            // 
            // btnOK
            // 
            this.btnOK.DialogResult = System.Windows.Forms.DialogResult.OK;
            this.btnOK.Location = new System.Drawing.Point(142, 59);
            this.btnOK.Name = "btnOK";
            this.btnOK.Size = new System.Drawing.Size(75, 23);
            this.btnOK.TabIndex = 1;
            this.btnOK.Text = "OK";
            this.btnOK.UseVisualStyleBackColor = true;
            // 
            // edtWidth
            // 
            this.edtWidth.Location = new System.Drawing.Point(169, 10);
            this.edtWidth.Maximum = new decimal(new int[] {
            50,
            0,
            0,
            0});
            this.edtWidth.Minimum = new decimal(new int[] {
            15,
            0,
            0,
            0});
            this.edtWidth.Name = "edtWidth";
            this.edtWidth.Size = new System.Drawing.Size(90, 20);
            this.edtWidth.TabIndex = 2;
            this.edtWidth.Value = new decimal(new int[] {
            15,
            0,
            0,
            0});
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(128, 12);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(35, 13);
            this.label1.TabIndex = 3;
            this.label1.Text = "Width";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(128, 35);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(38, 13);
            this.label2.TabIndex = 5;
            this.label2.Text = "Height";
            // 
            // edtHeight
            // 
            this.edtHeight.Location = new System.Drawing.Point(169, 33);
            this.edtHeight.Maximum = new decimal(new int[] {
            50,
            0,
            0,
            0});
            this.edtHeight.Minimum = new decimal(new int[] {
            10,
            0,
            0,
            0});
            this.edtHeight.Name = "edtHeight";
            this.edtHeight.Size = new System.Drawing.Size(90, 20);
            this.edtHeight.TabIndex = 4;
            this.edtHeight.Value = new decimal(new int[] {
            10,
            0,
            0,
            0});
            // 
            // frmGameParams
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(296, 94);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.edtHeight);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.edtWidth);
            this.Controls.Add(this.btnOK);
            this.Controls.Add(this.gbGameType);
            this.Name = "frmGameParams";
            this.Text = "Game Parameters";
            this.gbGameType.ResumeLayout(false);
            this.gbGameType.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.edtWidth)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.edtHeight)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.GroupBox gbGameType;
        private System.Windows.Forms.RadioButton rbOrienteering;
        private System.Windows.Forms.RadioButton rbMaze;
        private System.Windows.Forms.Button btnOK;
        private System.Windows.Forms.NumericUpDown edtWidth;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.NumericUpDown edtHeight;
    }
}