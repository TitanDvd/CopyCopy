namespace CopyCopy.Controls
{
    partial class DialogForm
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
            this.panel1 = new System.Windows.Forms.Panel();
            this.btn_retry = new System.Windows.Forms.Button();
            this.btn_abort = new System.Windows.Forms.Button();
            this.chk_itinerance = new System.Windows.Forms.CheckBox();
            this.btn_ignore = new System.Windows.Forms.Button();
            this.btn_override = new System.Windows.Forms.Button();
            this.btn_rename = new System.Windows.Forms.Button();
            this.panel2 = new System.Windows.Forms.Panel();
            this.pictureBox1 = new System.Windows.Forms.PictureBox();
            this.panel3 = new System.Windows.Forms.Panel();
            this.lbl_text = new Titan.Core.Controls.GrowLabel();
            this.panel1.SuspendLayout();
            this.panel2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
            this.panel3.SuspendLayout();
            this.SuspendLayout();
            // 
            // panel1
            // 
            this.panel1.BackColor = System.Drawing.Color.LightGray;
            this.panel1.Controls.Add(this.btn_retry);
            this.panel1.Controls.Add(this.btn_abort);
            this.panel1.Controls.Add(this.chk_itinerance);
            this.panel1.Controls.Add(this.btn_ignore);
            this.panel1.Controls.Add(this.btn_override);
            this.panel1.Controls.Add(this.btn_rename);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.panel1.Location = new System.Drawing.Point(0, 138);
            this.panel1.Name = "panel1";
            this.panel1.Padding = new System.Windows.Forms.Padding(7);
            this.panel1.Size = new System.Drawing.Size(520, 44);
            this.panel1.TabIndex = 0;
            // 
            // btn_retry
            // 
            this.btn_retry.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btn_retry.Dock = System.Windows.Forms.DockStyle.Right;
            this.btn_retry.Location = new System.Drawing.Point(138, 7);
            this.btn_retry.Name = "btn_retry";
            this.btn_retry.Size = new System.Drawing.Size(75, 30);
            this.btn_retry.TabIndex = 5;
            this.btn_retry.Text = "Reintentar";
            this.btn_retry.UseVisualStyleBackColor = true;
            this.btn_retry.Visible = false;
            this.btn_retry.Click += new System.EventHandler(this.btn_retry_Click);
            // 
            // btn_abort
            // 
            this.btn_abort.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btn_abort.Dock = System.Windows.Forms.DockStyle.Right;
            this.btn_abort.Location = new System.Drawing.Point(213, 7);
            this.btn_abort.Name = "btn_abort";
            this.btn_abort.Size = new System.Drawing.Size(75, 30);
            this.btn_abort.TabIndex = 4;
            this.btn_abort.Text = "Abortar";
            this.btn_abort.UseVisualStyleBackColor = true;
            this.btn_abort.Visible = false;
            this.btn_abort.Click += new System.EventHandler(this.btn_abort_Click);
            // 
            // chk_itinerance
            // 
            this.chk_itinerance.AutoSize = true;
            this.chk_itinerance.Location = new System.Drawing.Point(14, 15);
            this.chk_itinerance.Name = "chk_itinerance";
            this.chk_itinerance.Size = new System.Drawing.Size(123, 17);
            this.chk_itinerance.TabIndex = 3;
            this.chk_itinerance.Text = "Hacer esto con todo";
            this.chk_itinerance.UseVisualStyleBackColor = true;
            this.chk_itinerance.CheckedChanged += new System.EventHandler(this.chk_itinerance_CheckedChanged);
            // 
            // btn_ignore
            // 
            this.btn_ignore.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btn_ignore.Dock = System.Windows.Forms.DockStyle.Right;
            this.btn_ignore.Location = new System.Drawing.Point(288, 7);
            this.btn_ignore.Name = "btn_ignore";
            this.btn_ignore.Size = new System.Drawing.Size(75, 30);
            this.btn_ignore.TabIndex = 2;
            this.btn_ignore.Text = "Ignorar";
            this.btn_ignore.UseVisualStyleBackColor = true;
            this.btn_ignore.Visible = false;
            this.btn_ignore.Click += new System.EventHandler(this.btn_ignore_Click);
            // 
            // btn_override
            // 
            this.btn_override.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btn_override.Dock = System.Windows.Forms.DockStyle.Right;
            this.btn_override.Location = new System.Drawing.Point(363, 7);
            this.btn_override.Name = "btn_override";
            this.btn_override.Size = new System.Drawing.Size(75, 30);
            this.btn_override.TabIndex = 1;
            this.btn_override.Text = "Aplastar";
            this.btn_override.UseVisualStyleBackColor = true;
            this.btn_override.Visible = false;
            this.btn_override.Click += new System.EventHandler(this.btn_override_Click);
            // 
            // btn_rename
            // 
            this.btn_rename.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btn_rename.Dock = System.Windows.Forms.DockStyle.Right;
            this.btn_rename.Location = new System.Drawing.Point(438, 7);
            this.btn_rename.Name = "btn_rename";
            this.btn_rename.Size = new System.Drawing.Size(75, 30);
            this.btn_rename.TabIndex = 0;
            this.btn_rename.Text = "Renombrar";
            this.btn_rename.UseVisualStyleBackColor = true;
            this.btn_rename.Visible = false;
            this.btn_rename.Click += new System.EventHandler(this.btn_rename_Click);
            // 
            // panel2
            // 
            this.panel2.Controls.Add(this.pictureBox1);
            this.panel2.Dock = System.Windows.Forms.DockStyle.Left;
            this.panel2.Location = new System.Drawing.Point(0, 0);
            this.panel2.Name = "panel2";
            this.panel2.Padding = new System.Windows.Forms.Padding(14);
            this.panel2.Size = new System.Drawing.Size(75, 138);
            this.panel2.TabIndex = 1;
            // 
            // pictureBox1
            // 
            this.pictureBox1.Dock = System.Windows.Forms.DockStyle.Top;
            this.pictureBox1.Location = new System.Drawing.Point(14, 14);
            this.pictureBox1.Name = "pictureBox1";
            this.pictureBox1.Size = new System.Drawing.Size(47, 45);
            this.pictureBox1.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.pictureBox1.TabIndex = 0;
            this.pictureBox1.TabStop = false;
            // 
            // panel3
            // 
            this.panel3.AutoSize = true;
            this.panel3.Controls.Add(this.lbl_text);
            this.panel3.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel3.Location = new System.Drawing.Point(75, 0);
            this.panel3.Name = "panel3";
            this.panel3.Padding = new System.Windows.Forms.Padding(14);
            this.panel3.Size = new System.Drawing.Size(445, 138);
            this.panel3.TabIndex = 2;
            // 
            // lbl_text
            // 
            this.lbl_text.Dock = System.Windows.Forms.DockStyle.Top;
            this.lbl_text.Font = new System.Drawing.Font("Arial", 8.25F);
            this.lbl_text.Location = new System.Drawing.Point(14, 14);
            this.lbl_text.Name = "lbl_text";
            this.lbl_text.Size = new System.Drawing.Size(417, 14);
            this.lbl_text.TabIndex = 0;
            this.lbl_text.Text = "Texto del mensaje";
            // 
            // DialogForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.AutoSize = true;
            this.ClientSize = new System.Drawing.Size(520, 182);
            this.ControlBox = false;
            this.Controls.Add(this.panel3);
            this.Controls.Add(this.panel2);
            this.Controls.Add(this.panel1);
            this.MinimumSize = new System.Drawing.Size(466, 198);
            this.Name = "DialogForm";
            this.ShowIcon = false;
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "DialogForm";
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
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
        private System.Windows.Forms.Button btn_ignore;
        private System.Windows.Forms.Button btn_override;
        private System.Windows.Forms.Button btn_rename;
        private Titan.Core.Controls.GrowLabel lbl_text;
        private System.Windows.Forms.CheckBox chk_itinerance;
        private System.Windows.Forms.Button btn_abort;
        private System.Windows.Forms.Button btn_retry;
    }
}