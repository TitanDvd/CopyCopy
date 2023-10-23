namespace CcCore.Views
{
    partial class CopyOptionsForm
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(CopyOptionsForm));
            this.panel1 = new System.Windows.Forms.Panel();
            this.chk_sameForAll = new System.Windows.Forms.CheckBox();
            this.panel8 = new System.Windows.Forms.Panel();
            this.btn_createNewCopy = new System.Windows.Forms.Button();
            this.panel7 = new System.Windows.Forms.Panel();
            this.btn_addExistent = new System.Windows.Forms.Button();
            this.panel6 = new System.Windows.Forms.Panel();
            this.btn_rename = new System.Windows.Forms.Button();
            this.panel5 = new System.Windows.Forms.Panel();
            this.btn_skip = new System.Windows.Forms.Button();
            this.panel4 = new System.Windows.Forms.Panel();
            this.btn_overWrite = new System.Windows.Forms.Button();
            this.panel2 = new System.Windows.Forms.Panel();
            this.pictureBox1 = new System.Windows.Forms.PictureBox();
            this.panel3 = new System.Windows.Forms.Panel();
            this.lbl_text = new CustomLabel();
            this.panel1.SuspendLayout();
            this.panel8.SuspendLayout();
            this.panel7.SuspendLayout();
            this.panel6.SuspendLayout();
            this.panel5.SuspendLayout();
            this.panel4.SuspendLayout();
            this.panel2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
            this.panel3.SuspendLayout();
            this.SuspendLayout();
            // 
            // panel1
            // 
            this.panel1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(224)))), ((int)(((byte)(224)))), ((int)(((byte)(224)))));
            this.panel1.Controls.Add(this.chk_sameForAll);
            this.panel1.Controls.Add(this.panel8);
            this.panel1.Controls.Add(this.panel7);
            this.panel1.Controls.Add(this.panel6);
            this.panel1.Controls.Add(this.panel5);
            this.panel1.Controls.Add(this.panel4);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.panel1.Location = new System.Drawing.Point(0, 86);
            this.panel1.Name = "panel1";
            this.panel1.Padding = new System.Windows.Forms.Padding(9);
            this.panel1.Size = new System.Drawing.Size(419, 48);
            this.panel1.TabIndex = 0;
            // 
            // chk_sameForAll
            // 
            this.chk_sameForAll.AutoSize = true;
            this.chk_sameForAll.Location = new System.Drawing.Point(12, 16);
            this.chk_sameForAll.Name = "chk_sameForAll";
            this.chk_sameForAll.Size = new System.Drawing.Size(95, 17);
            this.chk_sameForAll.TabIndex = 5;
            this.chk_sameForAll.Text = "Repetir accion";
            this.chk_sameForAll.UseVisualStyleBackColor = true;
            // 
            // panel8
            // 
            this.panel8.Controls.Add(this.btn_createNewCopy);
            this.panel8.Dock = System.Windows.Forms.DockStyle.Right;
            this.panel8.Location = new System.Drawing.Point(-162, 9);
            this.panel8.Name = "panel8";
            this.panel8.Padding = new System.Windows.Forms.Padding(7, 2, 2, 2);
            this.panel8.Size = new System.Drawing.Size(163, 30);
            this.panel8.TabIndex = 4;
            // 
            // btn_createNewCopy
            // 
            this.btn_createNewCopy.BackColor = System.Drawing.Color.Gray;
            this.btn_createNewCopy.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btn_createNewCopy.Dock = System.Windows.Forms.DockStyle.Fill;
            this.btn_createNewCopy.FlatAppearance.BorderSize = 0;
            this.btn_createNewCopy.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btn_createNewCopy.ForeColor = System.Drawing.Color.White;
            this.btn_createNewCopy.Location = new System.Drawing.Point(7, 2);
            this.btn_createNewCopy.Name = "btn_createNewCopy";
            this.btn_createNewCopy.Size = new System.Drawing.Size(154, 26);
            this.btn_createNewCopy.TabIndex = 0;
            this.btn_createNewCopy.Text = "Crear lista nueva de copias";
            this.btn_createNewCopy.UseVisualStyleBackColor = false;
            this.btn_createNewCopy.Click += new System.EventHandler(this.btn_createNewCopy_Click);
            // 
            // panel7
            // 
            this.panel7.Controls.Add(this.btn_addExistent);
            this.panel7.Dock = System.Windows.Forms.DockStyle.Right;
            this.panel7.Location = new System.Drawing.Point(1, 9);
            this.panel7.Name = "panel7";
            this.panel7.Padding = new System.Windows.Forms.Padding(7, 2, 2, 2);
            this.panel7.Size = new System.Drawing.Size(146, 30);
            this.panel7.TabIndex = 3;
            // 
            // btn_addExistent
            // 
            this.btn_addExistent.BackColor = System.Drawing.Color.Gray;
            this.btn_addExistent.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btn_addExistent.Dock = System.Windows.Forms.DockStyle.Fill;
            this.btn_addExistent.FlatAppearance.BorderSize = 0;
            this.btn_addExistent.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btn_addExistent.ForeColor = System.Drawing.Color.White;
            this.btn_addExistent.Location = new System.Drawing.Point(7, 2);
            this.btn_addExistent.Name = "btn_addExistent";
            this.btn_addExistent.Size = new System.Drawing.Size(137, 26);
            this.btn_addExistent.TabIndex = 0;
            this.btn_addExistent.Text = "Sumar a la lista de copias";
            this.btn_addExistent.UseVisualStyleBackColor = false;
            this.btn_addExistent.Click += new System.EventHandler(this.btn_addExistent_Click);
            // 
            // panel6
            // 
            this.panel6.Controls.Add(this.btn_rename);
            this.panel6.Dock = System.Windows.Forms.DockStyle.Right;
            this.panel6.Location = new System.Drawing.Point(147, 9);
            this.panel6.Name = "panel6";
            this.panel6.Padding = new System.Windows.Forms.Padding(7, 2, 2, 2);
            this.panel6.Size = new System.Drawing.Size(85, 30);
            this.panel6.TabIndex = 2;
            // 
            // btn_rename
            // 
            this.btn_rename.BackColor = System.Drawing.Color.Gray;
            this.btn_rename.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btn_rename.Dock = System.Windows.Forms.DockStyle.Fill;
            this.btn_rename.FlatAppearance.BorderSize = 0;
            this.btn_rename.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btn_rename.ForeColor = System.Drawing.Color.White;
            this.btn_rename.Location = new System.Drawing.Point(7, 2);
            this.btn_rename.Name = "btn_rename";
            this.btn_rename.Size = new System.Drawing.Size(76, 26);
            this.btn_rename.TabIndex = 0;
            this.btn_rename.Text = "Renombrar";
            this.btn_rename.UseVisualStyleBackColor = false;
            this.btn_rename.Click += new System.EventHandler(this.btn_rename_Click);
            // 
            // panel5
            // 
            this.panel5.Controls.Add(this.btn_skip);
            this.panel5.Dock = System.Windows.Forms.DockStyle.Right;
            this.panel5.Location = new System.Drawing.Point(232, 9);
            this.panel5.Name = "panel5";
            this.panel5.Padding = new System.Windows.Forms.Padding(7, 2, 2, 2);
            this.panel5.Size = new System.Drawing.Size(88, 30);
            this.panel5.TabIndex = 1;
            // 
            // btn_skip
            // 
            this.btn_skip.BackColor = System.Drawing.Color.Gray;
            this.btn_skip.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btn_skip.Dock = System.Windows.Forms.DockStyle.Fill;
            this.btn_skip.FlatAppearance.BorderSize = 0;
            this.btn_skip.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btn_skip.ForeColor = System.Drawing.Color.White;
            this.btn_skip.Location = new System.Drawing.Point(7, 2);
            this.btn_skip.Name = "btn_skip";
            this.btn_skip.Size = new System.Drawing.Size(79, 26);
            this.btn_skip.TabIndex = 0;
            this.btn_skip.Text = "Pasar";
            this.btn_skip.UseVisualStyleBackColor = false;
            this.btn_skip.Click += new System.EventHandler(this.btn_skip_Click);
            // 
            // panel4
            // 
            this.panel4.Controls.Add(this.btn_overWrite);
            this.panel4.Dock = System.Windows.Forms.DockStyle.Right;
            this.panel4.Location = new System.Drawing.Point(320, 9);
            this.panel4.Name = "panel4";
            this.panel4.Padding = new System.Windows.Forms.Padding(7, 2, 2, 2);
            this.panel4.Size = new System.Drawing.Size(90, 30);
            this.panel4.TabIndex = 0;
            // 
            // btn_overWrite
            // 
            this.btn_overWrite.BackColor = System.Drawing.Color.Gray;
            this.btn_overWrite.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btn_overWrite.Dock = System.Windows.Forms.DockStyle.Fill;
            this.btn_overWrite.FlatAppearance.BorderSize = 0;
            this.btn_overWrite.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btn_overWrite.ForeColor = System.Drawing.Color.White;
            this.btn_overWrite.Location = new System.Drawing.Point(7, 2);
            this.btn_overWrite.Name = "btn_overWrite";
            this.btn_overWrite.Size = new System.Drawing.Size(81, 26);
            this.btn_overWrite.TabIndex = 0;
            this.btn_overWrite.Text = "Sobreescribir";
            this.btn_overWrite.UseVisualStyleBackColor = false;
            this.btn_overWrite.Click += new System.EventHandler(this.btn_overWrite_Click);
            // 
            // panel2
            // 
            this.panel2.Controls.Add(this.pictureBox1);
            this.panel2.Dock = System.Windows.Forms.DockStyle.Left;
            this.panel2.Location = new System.Drawing.Point(0, 0);
            this.panel2.Name = "panel2";
            this.panel2.Padding = new System.Windows.Forms.Padding(25);
            this.panel2.Size = new System.Drawing.Size(88, 86);
            this.panel2.TabIndex = 1;
            // 
            // pictureBox1
            // 
            this.pictureBox1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pictureBox1.Location = new System.Drawing.Point(25, 25);
            this.pictureBox1.Name = "pictureBox1";
            this.pictureBox1.Size = new System.Drawing.Size(38, 36);
            this.pictureBox1.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.pictureBox1.TabIndex = 0;
            this.pictureBox1.TabStop = false;
            // 
            // panel3
            // 
            this.panel3.AutoSize = true;
            this.panel3.Controls.Add(this.lbl_text);
            this.panel3.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel3.Location = new System.Drawing.Point(88, 0);
            this.panel3.Name = "panel3";
            this.panel3.Padding = new System.Windows.Forms.Padding(10);
            this.panel3.Size = new System.Drawing.Size(331, 86);
            this.panel3.TabIndex = 2;
            // 
            // lbl_text
            // 
            this.lbl_text.Dock = System.Windows.Forms.DockStyle.Top;
            this.lbl_text.Location = new System.Drawing.Point(10, 10);
            this.lbl_text.Name = "lbl_text";
            this.lbl_text.Size = new System.Drawing.Size(311, 26);
            this.lbl_text.TabIndex = 0;
            this.lbl_text.Text = "Esto es un texto super largo que tiene como objetivo ver ocmo se comporta el labe" +
    "l  personalizado";
            // 
            // CopyOptionsForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.AutoSize = true;
            this.ClientSize = new System.Drawing.Size(419, 134);
            this.Controls.Add(this.panel3);
            this.Controls.Add(this.panel2);
            this.Controls.Add(this.panel1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.Name = "CopyOptionsForm";
            this.ShowIcon = false;
            this.SizeGripStyle = System.Windows.Forms.SizeGripStyle.Hide;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "Opciones";
            this.FormClosed += new System.Windows.Forms.FormClosedEventHandler(this.CopyOptionsForm_FormClosed);
            this.Load += new System.EventHandler(this.CopyOptionsForm_Load);
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            this.panel8.ResumeLayout(false);
            this.panel7.ResumeLayout(false);
            this.panel6.ResumeLayout(false);
            this.panel5.ResumeLayout(false);
            this.panel4.ResumeLayout(false);
            this.panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
            this.panel3.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Panel panel2;
        private System.Windows.Forms.PictureBox pictureBox1;
        private System.Windows.Forms.Panel panel3;
        private CustomLabel lbl_text;
        private System.Windows.Forms.CheckBox chk_sameForAll;
        private System.Windows.Forms.Panel panel8;
        private System.Windows.Forms.Button btn_createNewCopy;
        private System.Windows.Forms.Panel panel7;
        private System.Windows.Forms.Button btn_addExistent;
        private System.Windows.Forms.Panel panel6;
        private System.Windows.Forms.Button btn_rename;
        private System.Windows.Forms.Panel panel5;
        private System.Windows.Forms.Button btn_skip;
        private System.Windows.Forms.Panel panel4;
        private System.Windows.Forms.Button btn_overWrite;
    }
}