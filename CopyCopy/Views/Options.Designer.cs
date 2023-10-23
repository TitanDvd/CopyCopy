namespace CopyCopy.Views
{
    partial class Options
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
            this.checkBoxDoSound = new System.Windows.Forms.CheckBox();
            this.cfg_soundPath = new System.Windows.Forms.TextBox();
            this.button1 = new System.Windows.Forms.Button();
            this.checkBoxCloseWhenFinish = new System.Windows.Forms.CheckBox();
            this.checkBoxShowProgressInTitlebar = new System.Windows.Forms.CheckBox();
            this.checkBoxUnitsInTitlebar = new System.Windows.Forms.CheckBox();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.radioButtonAllCopysOneIcon = new System.Windows.Forms.RadioButton();
            this.radioButtonShowIconPerCopy = new System.Windows.Forms.RadioButton();
            this.label1 = new System.Windows.Forms.Label();
            this.checkBoxAskWhenFoceClose = new System.Windows.Forms.CheckBox();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.btn_save = new System.Windows.Forms.Button();
            this.groupBox1.SuspendLayout();
            this.groupBox2.SuspendLayout();
            this.SuspendLayout();
            // 
            // checkBoxDoSound
            // 
            this.checkBoxDoSound.AutoSize = true;
            this.checkBoxDoSound.Location = new System.Drawing.Point(12, 59);
            this.checkBoxDoSound.Name = "checkBoxDoSound";
            this.checkBoxDoSound.Size = new System.Drawing.Size(150, 17);
            this.checkBoxDoSound.TabIndex = 0;
            this.checkBoxDoSound.Text = "Sonido al terminar la copia";
            this.checkBoxDoSound.UseVisualStyleBackColor = true;
            this.checkBoxDoSound.CheckedChanged += new System.EventHandler(this.checkBoxDoSound_CheckedChanged);
            // 
            // cfg_soundPath
            // 
            this.cfg_soundPath.Location = new System.Drawing.Point(12, 33);
            this.cfg_soundPath.Name = "cfg_soundPath";
            this.cfg_soundPath.Size = new System.Drawing.Size(236, 20);
            this.cfg_soundPath.TabIndex = 1;
            // 
            // button1
            // 
            this.button1.Font = new System.Drawing.Font("Arial", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.button1.Location = new System.Drawing.Point(254, 32);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(42, 23);
            this.button1.TabIndex = 2;
            this.button1.Text = "...";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // checkBoxCloseWhenFinish
            // 
            this.checkBoxCloseWhenFinish.AutoSize = true;
            this.checkBoxCloseWhenFinish.ForeColor = System.Drawing.Color.Black;
            this.checkBoxCloseWhenFinish.Location = new System.Drawing.Point(16, 49);
            this.checkBoxCloseWhenFinish.Name = "checkBoxCloseWhenFinish";
            this.checkBoxCloseWhenFinish.Size = new System.Drawing.Size(176, 17);
            this.checkBoxCloseWhenFinish.TabIndex = 3;
            this.checkBoxCloseWhenFinish.Text = "Cerrar ventana al terminar copia";
            this.checkBoxCloseWhenFinish.UseVisualStyleBackColor = true;
            // 
            // checkBoxShowProgressInTitlebar
            // 
            this.checkBoxShowProgressInTitlebar.AutoSize = true;
            this.checkBoxShowProgressInTitlebar.ForeColor = System.Drawing.Color.Black;
            this.checkBoxShowProgressInTitlebar.Location = new System.Drawing.Point(16, 72);
            this.checkBoxShowProgressInTitlebar.Name = "checkBoxShowProgressInTitlebar";
            this.checkBoxShowProgressInTitlebar.Size = new System.Drawing.Size(198, 17);
            this.checkBoxShowProgressInTitlebar.TabIndex = 4;
            this.checkBoxShowProgressInTitlebar.Text = "Mostrar progreso en la barra de titulo";
            this.checkBoxShowProgressInTitlebar.UseVisualStyleBackColor = true;
            // 
            // checkBoxUnitsInTitlebar
            // 
            this.checkBoxUnitsInTitlebar.AutoSize = true;
            this.checkBoxUnitsInTitlebar.ForeColor = System.Drawing.Color.Black;
            this.checkBoxUnitsInTitlebar.Location = new System.Drawing.Point(16, 95);
            this.checkBoxUnitsInTitlebar.Name = "checkBoxUnitsInTitlebar";
            this.checkBoxUnitsInTitlebar.Size = new System.Drawing.Size(244, 17);
            this.checkBoxUnitsInTitlebar.TabIndex = 5;
            this.checkBoxUnitsInTitlebar.Text = "Mostrar unidades en copia en la barra de titulo";
            this.checkBoxUnitsInTitlebar.UseVisualStyleBackColor = true;
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.radioButtonAllCopysOneIcon);
            this.groupBox1.Controls.Add(this.radioButtonShowIconPerCopy);
            this.groupBox1.ForeColor = System.Drawing.Color.Gray;
            this.groupBox1.Location = new System.Drawing.Point(10, 231);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(286, 81);
            this.groupBox1.TabIndex = 6;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Comportamiento en la barra de tareas";
            // 
            // radioButtonAllCopysOneIcon
            // 
            this.radioButtonAllCopysOneIcon.AutoSize = true;
            this.radioButtonAllCopysOneIcon.ForeColor = System.Drawing.Color.Black;
            this.radioButtonAllCopysOneIcon.Location = new System.Drawing.Point(16, 48);
            this.radioButtonAllCopysOneIcon.Name = "radioButtonAllCopysOneIcon";
            this.radioButtonAllCopysOneIcon.Size = new System.Drawing.Size(264, 17);
            this.radioButtonAllCopysOneIcon.TabIndex = 1;
            this.radioButtonAllCopysOneIcon.TabStop = true;
            this.radioButtonAllCopysOneIcon.Text = "Todas las copias en un icono de la barra de tareas";
            this.radioButtonAllCopysOneIcon.UseVisualStyleBackColor = true;
            // 
            // radioButtonShowIconPerCopy
            // 
            this.radioButtonShowIconPerCopy.AutoSize = true;
            this.radioButtonShowIconPerCopy.ForeColor = System.Drawing.Color.Black;
            this.radioButtonShowIconPerCopy.Location = new System.Drawing.Point(16, 25);
            this.radioButtonShowIconPerCopy.Name = "radioButtonShowIconPerCopy";
            this.radioButtonShowIconPerCopy.Size = new System.Drawing.Size(215, 17);
            this.radioButtonShowIconPerCopy.TabIndex = 0;
            this.radioButtonShowIconPerCopy.TabStop = true;
            this.radioButtonShowIconPerCopy.Text = "Un icono en la barra de tareas por copia";
            this.radioButtonShowIconPerCopy.UseVisualStyleBackColor = true;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(9, 17);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(213, 13);
            this.label1.TabIndex = 7;
            this.label1.Text = "Ruta del archivo de sonido en formato .wav";
            // 
            // checkBoxAskWhenFoceClose
            // 
            this.checkBoxAskWhenFoceClose.AutoSize = true;
            this.checkBoxAskWhenFoceClose.ForeColor = System.Drawing.Color.Black;
            this.checkBoxAskWhenFoceClose.Location = new System.Drawing.Point(16, 26);
            this.checkBoxAskWhenFoceClose.Name = "checkBoxAskWhenFoceClose";
            this.checkBoxAskWhenFoceClose.Size = new System.Drawing.Size(242, 17);
            this.checkBoxAskWhenFoceClose.TabIndex = 8;
            this.checkBoxAskWhenFoceClose.Text = "Preguntar al forzar cierre de ventana de copia";
            this.checkBoxAskWhenFoceClose.UseVisualStyleBackColor = true;
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.checkBoxCloseWhenFinish);
            this.groupBox2.Controls.Add(this.checkBoxAskWhenFoceClose);
            this.groupBox2.Controls.Add(this.checkBoxShowProgressInTitlebar);
            this.groupBox2.Controls.Add(this.checkBoxUnitsInTitlebar);
            this.groupBox2.ForeColor = System.Drawing.Color.Gray;
            this.groupBox2.Location = new System.Drawing.Point(10, 90);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(286, 129);
            this.groupBox2.TabIndex = 9;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "Comportamiento general";
            // 
            // btn_save
            // 
            this.btn_save.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            this.btn_save.FlatAppearance.BorderSize = 0;
            this.btn_save.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btn_save.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(224)))), ((int)(((byte)(224)))), ((int)(((byte)(224)))));
            this.btn_save.Location = new System.Drawing.Point(190, 324);
            this.btn_save.Name = "btn_save";
            this.btn_save.Size = new System.Drawing.Size(106, 33);
            this.btn_save.TabIndex = 10;
            this.btn_save.Text = "Guardar";
            this.btn_save.UseVisualStyleBackColor = false;
            this.btn_save.Click += new System.EventHandler(this.button2_Click);
            // 
            // Options
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(308, 369);
            this.Controls.Add(this.btn_save);
            this.Controls.Add(this.groupBox2);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.button1);
            this.Controls.Add(this.cfg_soundPath);
            this.Controls.Add(this.checkBoxDoSound);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow;
            this.Name = "Options";
            this.ShowIcon = false;
            this.SizeGripStyle = System.Windows.Forms.SizeGripStyle.Hide;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Options";
            this.Load += new System.EventHandler(this.Options_Load);
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.groupBox2.ResumeLayout(false);
            this.groupBox2.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.CheckBox checkBoxDoSound;
        private System.Windows.Forms.TextBox cfg_soundPath;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.CheckBox checkBoxCloseWhenFinish;
        private System.Windows.Forms.CheckBox checkBoxShowProgressInTitlebar;
        private System.Windows.Forms.CheckBox checkBoxUnitsInTitlebar;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.RadioButton radioButtonAllCopysOneIcon;
        private System.Windows.Forms.RadioButton radioButtonShowIconPerCopy;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.CheckBox checkBoxAskWhenFoceClose;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.Button btn_save;
    }
}