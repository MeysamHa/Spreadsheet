namespace SpreadsheetGUI
{
    partial class ViewForm
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
            this.menuStrip1 = new System.Windows.Forms.MenuStrip();
            this.fileToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.newToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.openToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.saveToolStripMenuItem1 = new System.Windows.Forms.ToolStripMenuItem();
            this.closeToolStripMenuItem2 = new System.Windows.Forms.ToolStripMenuItem();
            this.helpToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.viewHelpToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.spreadsheetpanel = new SSGui.SpreadsheetPanel();
            this.updatebtn = new System.Windows.Forms.Button();
            this.formulatxt = new System.Windows.Forms.TextBox();
            this.openFileDialog1 = new System.Windows.Forms.OpenFileDialog();
            this.saveFileDialog1 = new System.Windows.Forms.SaveFileDialog();
            this.backgroundWorker1 = new System.ComponentModel.BackgroundWorker();
            this.nameTextBox = new System.Windows.Forms.TextBox();
            this.cellLabel = new System.Windows.Forms.Label();
            this.valueLabel = new System.Windows.Forms.Label();
            this.valueTextBox = new System.Windows.Forms.TextBox();
            this.formulaLabel = new System.Windows.Forms.Label();
            this.panel1 = new System.Windows.Forms.Panel();
            this.menuStrip1.SuspendLayout();
            this.panel1.SuspendLayout();
            this.SuspendLayout();
            // 
            // menuStrip1
            // 
            this.menuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.fileToolStripMenuItem,
            this.helpToolStripMenuItem});
            this.menuStrip1.Location = new System.Drawing.Point(0, 0);
            this.menuStrip1.Name = "menuStrip1";
            this.menuStrip1.Size = new System.Drawing.Size(802, 24);
            this.menuStrip1.TabIndex = 0;
            this.menuStrip1.Text = "menuStrip1";
            this.menuStrip1.ItemClicked += new System.Windows.Forms.ToolStripItemClickedEventHandler(this.menuStrip1_ItemClicked);
            // 
            // fileToolStripMenuItem
            // 
            this.fileToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.newToolStripMenuItem,
            this.openToolStripMenuItem,
            this.saveToolStripMenuItem1,
            this.closeToolStripMenuItem2});
            this.fileToolStripMenuItem.Name = "fileToolStripMenuItem";
            this.fileToolStripMenuItem.Size = new System.Drawing.Size(37, 20);
            this.fileToolStripMenuItem.Text = "File";
            this.fileToolStripMenuItem.Click += new System.EventHandler(this.fileToolStripMenuItem_Click);
            // 
            // newToolStripMenuItem
            // 
            this.newToolStripMenuItem.Name = "newToolStripMenuItem";
            this.newToolStripMenuItem.Size = new System.Drawing.Size(152, 22);
            this.newToolStripMenuItem.Text = "New";
            this.newToolStripMenuItem.Click += new System.EventHandler(this.newToolStripMenuItem_Click);
            // 
            // openToolStripMenuItem
            // 
            this.openToolStripMenuItem.Name = "openToolStripMenuItem";
            this.openToolStripMenuItem.Size = new System.Drawing.Size(152, 22);
            this.openToolStripMenuItem.Text = "Open ";
            this.openToolStripMenuItem.Click += new System.EventHandler(this.openToolStripMenuItem_Click);
            // 
            // saveToolStripMenuItem1
            // 
            this.saveToolStripMenuItem1.Name = "saveToolStripMenuItem1";
            this.saveToolStripMenuItem1.Size = new System.Drawing.Size(152, 22);
            this.saveToolStripMenuItem1.Text = "Save";
            this.saveToolStripMenuItem1.Click += new System.EventHandler(this.saveToolStripMenuItem1_Click);
            // 
            // closeToolStripMenuItem2
            // 
            this.closeToolStripMenuItem2.Name = "closeToolStripMenuItem2";
            this.closeToolStripMenuItem2.Size = new System.Drawing.Size(152, 22);
            this.closeToolStripMenuItem2.Text = "Close";
            this.closeToolStripMenuItem2.Click += new System.EventHandler(this.closeToolStripMenuItem2_Click);
            // 
            // helpToolStripMenuItem
            // 
            this.helpToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.viewHelpToolStripMenuItem});
            this.helpToolStripMenuItem.Name = "helpToolStripMenuItem";
            this.helpToolStripMenuItem.Size = new System.Drawing.Size(44, 20);
            this.helpToolStripMenuItem.Text = "Help";
            // 
            // viewHelpToolStripMenuItem
            // 
            this.viewHelpToolStripMenuItem.Name = "viewHelpToolStripMenuItem";
            this.viewHelpToolStripMenuItem.Size = new System.Drawing.Size(127, 22);
            this.viewHelpToolStripMenuItem.Text = "View Help";
            this.viewHelpToolStripMenuItem.Click += new System.EventHandler(this.viewHelpToolStripMenuItem_Click);
            // 
            // spreadsheetpanel
            // 
            this.spreadsheetpanel.BackColor = System.Drawing.SystemColors.AppWorkspace;
            this.spreadsheetpanel.Dock = System.Windows.Forms.DockStyle.Fill;
            this.spreadsheetpanel.ForeColor = System.Drawing.SystemColors.ControlText;
            this.spreadsheetpanel.Location = new System.Drawing.Point(0, 24);
            this.spreadsheetpanel.Name = "spreadsheetpanel";
            this.spreadsheetpanel.Size = new System.Drawing.Size(802, 411);
            this.spreadsheetpanel.TabIndex = 1;
            this.spreadsheetpanel.Load += new System.EventHandler(this.spreadsheetpanel_Load);
            // 
            // updatebtn
            // 
            this.updatebtn.Location = new System.Drawing.Point(628, 0);
            this.updatebtn.Name = "updatebtn";
            this.updatebtn.Size = new System.Drawing.Size(75, 23);
            this.updatebtn.TabIndex = 2;
            this.updatebtn.Text = "Update";
            this.updatebtn.UseVisualStyleBackColor = true;
            this.updatebtn.Click += new System.EventHandler(this.updatebtn_Click);
            // 
            // formulatxt
            // 
            this.formulatxt.Location = new System.Drawing.Point(505, 3);
            this.formulatxt.Name = "formulatxt";
            this.formulatxt.Size = new System.Drawing.Size(117, 20);
            this.formulatxt.TabIndex = 3;
            // 
            // openFileDialog1
            // 
            this.openFileDialog1.FileName = "openFileDialog1";
            this.openFileDialog1.FileOk += new System.ComponentModel.CancelEventHandler(this.openFileDialog1_FileOk);
            // 
            // saveFileDialog1
            // 
            this.saveFileDialog1.FileOk += new System.ComponentModel.CancelEventHandler(this.saveFileDialog1_FileOk);
            // 
            // backgroundWorker1
            // 
            this.backgroundWorker1.DoWork += new System.ComponentModel.DoWorkEventHandler(this.backgroundWorker1_DoWork);
            // 
            // nameTextBox
            // 
            this.nameTextBox.Location = new System.Drawing.Point(80, 3);
            this.nameTextBox.Name = "nameTextBox";
            this.nameTextBox.ReadOnly = true;
            this.nameTextBox.Size = new System.Drawing.Size(100, 20);
            this.nameTextBox.TabIndex = 4;
            this.nameTextBox.Text = "A1";
            this.nameTextBox.TextChanged += new System.EventHandler(this.nameTextBox_TextChanged);
            // 
            // cellLabel
            // 
            this.cellLabel.AutoSize = true;
            this.cellLabel.Location = new System.Drawing.Point(35, 5);
            this.cellLabel.Name = "cellLabel";
            this.cellLabel.Size = new System.Drawing.Size(27, 13);
            this.cellLabel.TabIndex = 5;
            this.cellLabel.Text = "Cell:";
            this.cellLabel.Click += new System.EventHandler(this.cellLabel_Click);
            // 
            // valueLabel
            // 
            this.valueLabel.AutoSize = true;
            this.valueLabel.Location = new System.Drawing.Point(241, 6);
            this.valueLabel.Name = "valueLabel";
            this.valueLabel.Size = new System.Drawing.Size(37, 13);
            this.valueLabel.TabIndex = 6;
            this.valueLabel.Text = "Value:";
            // 
            // valueTextBox
            // 
            this.valueTextBox.Location = new System.Drawing.Point(305, 3);
            this.valueTextBox.Name = "valueTextBox";
            this.valueTextBox.Size = new System.Drawing.Size(100, 20);
            this.valueTextBox.TabIndex = 7;
            // 
            // formulaLabel
            // 
            this.formulaLabel.AutoSize = true;
            this.formulaLabel.Location = new System.Drawing.Point(452, 6);
            this.formulaLabel.Name = "formulaLabel";
            this.formulaLabel.Size = new System.Drawing.Size(47, 13);
            this.formulaLabel.TabIndex = 8;
            this.formulaLabel.Text = "Formula:";
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.nameTextBox);
            this.panel1.Controls.Add(this.formulaLabel);
            this.panel1.Controls.Add(this.updatebtn);
            this.panel1.Controls.Add(this.valueTextBox);
            this.panel1.Controls.Add(this.formulatxt);
            this.panel1.Controls.Add(this.valueLabel);
            this.panel1.Controls.Add(this.cellLabel);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Top;
            this.panel1.Location = new System.Drawing.Point(0, 24);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(802, 31);
            this.panel1.TabIndex = 9;
            // 
            // ViewForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(802, 435);
            this.Controls.Add(this.panel1);
            this.Controls.Add(this.spreadsheetpanel);
            this.Controls.Add(this.menuStrip1);
            this.MainMenuStrip = this.menuStrip1;
            this.Name = "ViewForm";
            this.Text = "Form";
            this.Load += new System.EventHandler(this.ViewForm_Load);
            this.menuStrip1.ResumeLayout(false);
            this.menuStrip1.PerformLayout();
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.MenuStrip menuStrip1;
        private System.Windows.Forms.ToolStripMenuItem fileToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem newToolStripMenuItem;
        private SSGui.SpreadsheetPanel spreadsheetpanel;
        private System.Windows.Forms.Button updatebtn;
        private System.Windows.Forms.TextBox formulatxt;
        private System.Windows.Forms.ToolStripMenuItem openToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem helpToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem saveToolStripMenuItem1;
        private System.Windows.Forms.ToolStripMenuItem closeToolStripMenuItem2;
        private System.Windows.Forms.OpenFileDialog openFileDialog1;
        private System.Windows.Forms.SaveFileDialog saveFileDialog1;
        private System.ComponentModel.BackgroundWorker backgroundWorker1;
        private System.Windows.Forms.TextBox nameTextBox;
        private System.Windows.Forms.Label cellLabel;
        private System.Windows.Forms.Label valueLabel;
        private System.Windows.Forms.TextBox valueTextBox;
        private System.Windows.Forms.Label formulaLabel;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.ToolStripMenuItem viewHelpToolStripMenuItem;
    }
}

