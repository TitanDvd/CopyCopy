namespace CccInstaller
{
    partial class MUI
    {
        /// <summary>
        /// Variable del diseñador necesaria.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Limpiar los recursos que se estén usando.
        /// </summary>
        /// <param name="disposing">true si los recursos administrados se deben desechar; false en caso contrario.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Código generado por el Diseñador de Windows Forms

        /// <summary>
        /// Método necesario para admitir el Diseñador. No se puede modificar
        /// el contenido de este método con el editor de código.
        /// </summary>
        private void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(MUI));
            this.panel1 = new System.Windows.Forms.Panel();
            this.label2 = new System.Windows.Forms.Label();
            this.pictureBox1 = new System.Windows.Forms.PictureBox();
            this.panel2 = new System.Windows.Forms.Panel();
            this.label3 = new System.Windows.Forms.Label();
            this.btn_install = new System.Windows.Forms.Button();
            this.cfg_installPath = new System.Windows.Forms.TextBox();
            this.btn_searchPath = new System.Windows.Forms.Button();
            this.lbl_status = new System.Windows.Forms.Label();
            this.progressBarInstallation = new System.Windows.Forms.ProgressBar();
            this.checkBoxRunOnInstallComplete = new System.Windows.Forms.CheckBox();
            this.checkBoxRunAtBoot = new System.Windows.Forms.CheckBox();
            this.label1 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.panel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
            this.SuspendLayout();
            // 
            // panel1
            // 
            this.panel1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(224)))), ((int)(((byte)(224)))), ((int)(((byte)(224)))));
            this.panel1.Controls.Add(this.label2);
            this.panel1.Controls.Add(this.pictureBox1);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Top;
            this.panel1.Location = new System.Drawing.Point(0, 0);
            this.panel1.Name = "panel1";
            this.panel1.Padding = new System.Windows.Forms.Padding(7);
            this.panel1.Size = new System.Drawing.Size(412, 73);
            this.panel1.TabIndex = 1;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Arial", 15.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.ForeColor = System.Drawing.Color.DimGray;
            this.label2.Location = new System.Drawing.Point(85, 25);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(305, 24);
            this.label2.TabIndex = 1;
            this.label2.Text = "Copy Copy Installer Assistant";
            // 
            // pictureBox1
            // 
            this.pictureBox1.Dock = System.Windows.Forms.DockStyle.Left;
            this.pictureBox1.Image = global::CccInstaller.Properties.Resources.copycopy_brand;
            this.pictureBox1.Location = new System.Drawing.Point(7, 7);
            this.pictureBox1.Name = "pictureBox1";
            this.pictureBox1.Size = new System.Drawing.Size(78, 59);
            this.pictureBox1.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.pictureBox1.TabIndex = 0;
            this.pictureBox1.TabStop = false;
            // 
            // panel2
            // 
            this.panel2.BackColor = System.Drawing.Color.Gainsboro;
            this.panel2.Location = new System.Drawing.Point(15, 265);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(386, 1);
            this.panel2.TabIndex = 2;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.ForeColor = System.Drawing.Color.Gray;
            this.label3.Location = new System.Drawing.Point(235, 258);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(165, 13);
            this.label3.TabIndex = 3;
            this.label3.Text = "Version 10052022 Alpha Release";
            // 
            // btn_install
            // 
            this.btn_install.Location = new System.Drawing.Point(301, 191);
            this.btn_install.Name = "btn_install";
            this.btn_install.Size = new System.Drawing.Size(99, 23);
            this.btn_install.TabIndex = 4;
            this.btn_install.Text = "Install";
            this.btn_install.UseVisualStyleBackColor = true;
            this.btn_install.Click += new System.EventHandler(this.button1_Click);
            // 
            // cfg_installPath
            // 
            this.cfg_installPath.Location = new System.Drawing.Point(15, 192);
            this.cfg_installPath.Name = "cfg_installPath";
            this.cfg_installPath.Size = new System.Drawing.Size(249, 20);
            this.cfg_installPath.TabIndex = 5;
            // 
            // btn_searchPath
            // 
            this.btn_searchPath.Font = new System.Drawing.Font("Arial", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btn_searchPath.Location = new System.Drawing.Point(270, 191);
            this.btn_searchPath.Name = "btn_searchPath";
            this.btn_searchPath.Size = new System.Drawing.Size(25, 23);
            this.btn_searchPath.TabIndex = 6;
            this.btn_searchPath.Text = "...";
            this.btn_searchPath.UseVisualStyleBackColor = true;
            this.btn_searchPath.Click += new System.EventHandler(this.btn_searchPath_Click);
            // 
            // lbl_status
            // 
            this.lbl_status.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            this.lbl_status.Location = new System.Drawing.Point(12, 176);
            this.lbl_status.Name = "lbl_status";
            this.lbl_status.Size = new System.Drawing.Size(388, 13);
            this.lbl_status.TabIndex = 7;
            this.lbl_status.Text = "Installation Path";
            // 
            // progressBarInstallation
            // 
            this.progressBarInstallation.Location = new System.Drawing.Point(15, 191);
            this.progressBarInstallation.Name = "progressBarInstallation";
            this.progressBarInstallation.Size = new System.Drawing.Size(385, 20);
            this.progressBarInstallation.TabIndex = 8;
            this.progressBarInstallation.Visible = false;
            // 
            // checkBoxRunOnInstallComplete
            // 
            this.checkBoxRunOnInstallComplete.AutoSize = true;
            this.checkBoxRunOnInstallComplete.Checked = true;
            this.checkBoxRunOnInstallComplete.CheckState = System.Windows.Forms.CheckState.Checked;
            this.checkBoxRunOnInstallComplete.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.checkBoxRunOnInstallComplete.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            this.checkBoxRunOnInstallComplete.Location = new System.Drawing.Point(15, 233);
            this.checkBoxRunOnInstallComplete.Name = "checkBoxRunOnInstallComplete";
            this.checkBoxRunOnInstallComplete.Size = new System.Drawing.Size(170, 17);
            this.checkBoxRunOnInstallComplete.TabIndex = 9;
            this.checkBoxRunOnInstallComplete.Text = "Run when installation complete";
            this.checkBoxRunOnInstallComplete.UseVisualStyleBackColor = true;
            // 
            // checkBoxRunAtBoot
            // 
            this.checkBoxRunAtBoot.AutoSize = true;
            this.checkBoxRunAtBoot.Checked = true;
            this.checkBoxRunAtBoot.CheckState = System.Windows.Forms.CheckState.Checked;
            this.checkBoxRunAtBoot.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.checkBoxRunAtBoot.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            this.checkBoxRunAtBoot.Location = new System.Drawing.Point(15, 214);
            this.checkBoxRunAtBoot.Name = "checkBoxRunAtBoot";
            this.checkBoxRunAtBoot.Size = new System.Drawing.Size(96, 17);
            this.checkBoxRunAtBoot.TabIndex = 10;
            this.checkBoxRunAtBoot.Text = "Run when boot";
            this.checkBoxRunAtBoot.UseVisualStyleBackColor = true;
            // 
            // label1
            // 
            this.label1.Font = new System.Drawing.Font("Arial", 8.25F);
            this.label1.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            this.label1.Location = new System.Drawing.Point(15, 95);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(385, 57);
            this.label1.TabIndex = 11;
            this.label1.Text = "Note:\r\nYou are about to install Copy Copy Core and Copy Copy UI, both in alpha re" +
    "lease. These are tools for file copy and replace the default windows copy shell";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.ForeColor = System.Drawing.Color.Red;
            this.label4.Location = new System.Drawing.Point(15, 154);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(243, 13);
            this.label4.TabIndex = 12;
            this.label4.Text = "Installation requires all windows explorer be closed";
            // 
            // MUI
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(412, 303);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.checkBoxRunAtBoot);
            this.Controls.Add(this.checkBoxRunOnInstallComplete);
            this.Controls.Add(this.lbl_status);
            this.Controls.Add(this.btn_searchPath);
            this.Controls.Add(this.cfg_installPath);
            this.Controls.Add(this.btn_install);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.panel2);
            this.Controls.Add(this.panel1);
            this.Controls.Add(this.progressBarInstallation);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "MUI";
            this.SizeGripStyle = System.Windows.Forms.SizeGripStyle.Hide;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Ccc Installer Assistant";
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.PictureBox pictureBox1;
        private System.Windows.Forms.Panel panel2;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Button btn_install;
        private System.Windows.Forms.TextBox cfg_installPath;
        private System.Windows.Forms.Button btn_searchPath;
        private System.Windows.Forms.Label lbl_status;
        private System.Windows.Forms.ProgressBar progressBarInstallation;
        private System.Windows.Forms.CheckBox checkBoxRunOnInstallComplete;
        private System.Windows.Forms.CheckBox checkBoxRunAtBoot;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label4;
    }
}

