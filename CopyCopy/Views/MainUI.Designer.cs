namespace CopyCopy
{
    partial class MainUI
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(MainUI));
            this.panel1 = new System.Windows.Forms.Panel();
            this.customProgressBar_gState = new CopyCopy.Controls.CustomProgressBar();
            this.customProgressBar_cState = new CopyCopy.Controls.CustomProgressBar();
            this.btn_skipItem = new System.Windows.Forms.Button();
            this.btn_pauseResumeSw = new System.Windows.Forms.Button();
            this.btn_seeCopyListSw = new System.Windows.Forms.Button();
            this.lbl_copyingFrom = new System.Windows.Forms.Label();
            this.progressBar_currentState = new System.Windows.Forms.ProgressBar();
            this.lbl_copyingTo = new System.Windows.Forms.Label();
            this.progressBar_globalState = new System.Windows.Forms.ProgressBar();
            this.panel3 = new System.Windows.Forms.Panel();
            this.panel6 = new System.Windows.Forms.Panel();
            this.lbl_bps = new System.Windows.Forms.Label();
            this.panel5 = new System.Windows.Forms.Panel();
            this.lbl_selection = new System.Windows.Forms.Label();
            this.panel4 = new System.Windows.Forms.Panel();
            this.lbl_totalVolume = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.lbl_currentVolume = new System.Windows.Forms.Label();
            this.lbl_volumeData = new System.Windows.Forms.Label();
            this.panel2 = new System.Windows.Forms.Panel();
            this.lbl_totalFilesCount = new System.Windows.Forms.Label();
            this.olv_queueList = new BrightIdeasSoftware.ObjectListView();
            this.olvColumn1 = ((BrightIdeasSoftware.OLVColumn)(new BrightIdeasSoftware.OLVColumn()));
            this.olvColumn2 = ((BrightIdeasSoftware.OLVColumn)(new BrightIdeasSoftware.OLVColumn()));
            this.olvColumn3 = ((BrightIdeasSoftware.OLVColumn)(new BrightIdeasSoftware.OLVColumn()));
            this.olvColumn4 = ((BrightIdeasSoftware.OLVColumn)(new BrightIdeasSoftware.OLVColumn()));
            this.panel1.SuspendLayout();
            this.panel3.SuspendLayout();
            this.panel6.SuspendLayout();
            this.panel5.SuspendLayout();
            this.panel4.SuspendLayout();
            this.panel2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.olv_queueList)).BeginInit();
            this.SuspendLayout();
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.customProgressBar_gState);
            this.panel1.Controls.Add(this.customProgressBar_cState);
            this.panel1.Controls.Add(this.btn_skipItem);
            this.panel1.Controls.Add(this.btn_pauseResumeSw);
            this.panel1.Controls.Add(this.btn_seeCopyListSw);
            this.panel1.Controls.Add(this.lbl_copyingFrom);
            this.panel1.Controls.Add(this.progressBar_currentState);
            this.panel1.Controls.Add(this.lbl_copyingTo);
            this.panel1.Controls.Add(this.progressBar_globalState);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Top;
            this.panel1.Location = new System.Drawing.Point(0, 0);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(473, 139);
            this.panel1.TabIndex = 12;
            // 
            // customProgressBar_gState
            // 
            this.customProgressBar_gState.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.customProgressBar_gState.CustomText = "0%";
            this.customProgressBar_gState.Location = new System.Drawing.Point(10, 69);
            this.customProgressBar_gState.Name = "customProgressBar_gState";
            this.customProgressBar_gState.Size = new System.Drawing.Size(452, 21);
            this.customProgressBar_gState.TabIndex = 11;
            this.customProgressBar_gState.TextColor = System.Drawing.Color.Black;
            // 
            // customProgressBar_cState
            // 
            this.customProgressBar_cState.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.customProgressBar_cState.CustomText = "0%";
            this.customProgressBar_cState.Location = new System.Drawing.Point(10, 42);
            this.customProgressBar_cState.Name = "customProgressBar_cState";
            this.customProgressBar_cState.Size = new System.Drawing.Size(452, 21);
            this.customProgressBar_cState.TabIndex = 10;
            this.customProgressBar_cState.TextColor = System.Drawing.Color.Black;
            // 
            // btn_skipItem
            // 
            this.btn_skipItem.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btn_skipItem.Location = new System.Drawing.Point(368, 101);
            this.btn_skipItem.Name = "btn_skipItem";
            this.btn_skipItem.Size = new System.Drawing.Size(93, 29);
            this.btn_skipItem.TabIndex = 9;
            this.btn_skipItem.Text = "Saltar Elemento";
            this.btn_skipItem.UseVisualStyleBackColor = true;
            this.btn_skipItem.Click += new System.EventHandler(this.btn_skipItem_Click);
            // 
            // btn_pauseResumeSw
            // 
            this.btn_pauseResumeSw.Location = new System.Drawing.Point(119, 101);
            this.btn_pauseResumeSw.Name = "btn_pauseResumeSw";
            this.btn_pauseResumeSw.Size = new System.Drawing.Size(103, 29);
            this.btn_pauseResumeSw.TabIndex = 8;
            this.btn_pauseResumeSw.Text = "Iniciar Copia";
            this.btn_pauseResumeSw.UseVisualStyleBackColor = true;
            this.btn_pauseResumeSw.Click += new System.EventHandler(this.btn_pauseResumeSw_Click);
            // 
            // btn_seeCopyListSw
            // 
            this.btn_seeCopyListSw.Location = new System.Drawing.Point(10, 101);
            this.btn_seeCopyListSw.Name = "btn_seeCopyListSw";
            this.btn_seeCopyListSw.Size = new System.Drawing.Size(103, 29);
            this.btn_seeCopyListSw.TabIndex = 7;
            this.btn_seeCopyListSw.Text = "Ver Lista";
            this.btn_seeCopyListSw.UseVisualStyleBackColor = true;
            this.btn_seeCopyListSw.Click += new System.EventHandler(this.btn_seeCopyListSw_Click);
            // 
            // lbl_copyingFrom
            // 
            this.lbl_copyingFrom.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.lbl_copyingFrom.Location = new System.Drawing.Point(7, 9);
            this.lbl_copyingFrom.Name = "lbl_copyingFrom";
            this.lbl_copyingFrom.Size = new System.Drawing.Size(454, 13);
            this.lbl_copyingFrom.TabIndex = 1;
            this.lbl_copyingFrom.Text = "Copiando desde";
            // 
            // progressBar_currentState
            // 
            this.progressBar_currentState.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.progressBar_currentState.ForeColor = System.Drawing.Color.DodgerBlue;
            this.progressBar_currentState.Location = new System.Drawing.Point(444, 42);
            this.progressBar_currentState.Name = "progressBar_currentState";
            this.progressBar_currentState.Size = new System.Drawing.Size(18, 21);
            this.progressBar_currentState.Step = 1;
            this.progressBar_currentState.Style = System.Windows.Forms.ProgressBarStyle.Continuous;
            this.progressBar_currentState.TabIndex = 0;
            // 
            // lbl_copyingTo
            // 
            this.lbl_copyingTo.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.lbl_copyingTo.Location = new System.Drawing.Point(7, 26);
            this.lbl_copyingTo.Name = "lbl_copyingTo";
            this.lbl_copyingTo.Size = new System.Drawing.Size(454, 13);
            this.lbl_copyingTo.TabIndex = 2;
            this.lbl_copyingTo.Text = "Hacia:";
            // 
            // progressBar_globalState
            // 
            this.progressBar_globalState.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.progressBar_globalState.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(192)))), ((int)(((byte)(0)))));
            this.progressBar_globalState.Location = new System.Drawing.Point(444, 69);
            this.progressBar_globalState.Name = "progressBar_globalState";
            this.progressBar_globalState.Size = new System.Drawing.Size(18, 21);
            this.progressBar_globalState.Step = 1;
            this.progressBar_globalState.TabIndex = 6;
            // 
            // panel3
            // 
            this.panel3.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(224)))), ((int)(((byte)(224)))), ((int)(((byte)(224)))));
            this.panel3.Controls.Add(this.panel6);
            this.panel3.Controls.Add(this.panel5);
            this.panel3.Controls.Add(this.panel4);
            this.panel3.Controls.Add(this.panel2);
            this.panel3.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.panel3.Location = new System.Drawing.Point(0, 139);
            this.panel3.Name = "panel3";
            this.panel3.Padding = new System.Windows.Forms.Padding(3);
            this.panel3.Size = new System.Drawing.Size(473, 26);
            this.panel3.TabIndex = 13;
            // 
            // panel6
            // 
            this.panel6.AutoSize = true;
            this.panel6.Controls.Add(this.lbl_bps);
            this.panel6.Dock = System.Windows.Forms.DockStyle.Right;
            this.panel6.Location = new System.Drawing.Point(419, 3);
            this.panel6.Name = "panel6";
            this.panel6.Padding = new System.Windows.Forms.Padding(3);
            this.panel6.Size = new System.Drawing.Size(51, 20);
            this.panel6.TabIndex = 14;
            // 
            // lbl_bps
            // 
            this.lbl_bps.AutoSize = true;
            this.lbl_bps.Dock = System.Windows.Forms.DockStyle.Left;
            this.lbl_bps.Location = new System.Drawing.Point(3, 3);
            this.lbl_bps.Name = "lbl_bps";
            this.lbl_bps.RightToLeft = System.Windows.Forms.RightToLeft.No;
            this.lbl_bps.Size = new System.Drawing.Size(45, 13);
            this.lbl_bps.TabIndex = 10;
            this.lbl_bps.Text = "450 bps";
            this.lbl_bps.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // panel5
            // 
            this.panel5.AutoSize = true;
            this.panel5.Controls.Add(this.lbl_selection);
            this.panel5.Dock = System.Windows.Forms.DockStyle.Left;
            this.panel5.Location = new System.Drawing.Point(255, 3);
            this.panel5.Name = "panel5";
            this.panel5.Padding = new System.Windows.Forms.Padding(3);
            this.panel5.Size = new System.Drawing.Size(98, 20);
            this.panel5.TabIndex = 13;
            // 
            // lbl_selection
            // 
            this.lbl_selection.AutoSize = true;
            this.lbl_selection.Dock = System.Windows.Forms.DockStyle.Left;
            this.lbl_selection.Location = new System.Drawing.Point(3, 3);
            this.lbl_selection.Name = "lbl_selection";
            this.lbl_selection.Size = new System.Drawing.Size(92, 13);
            this.lbl_selection.TabIndex = 0;
            this.lbl_selection.Text = "Seleccion: 0 / GB";
            // 
            // panel4
            // 
            this.panel4.AutoSize = true;
            this.panel4.Controls.Add(this.lbl_totalVolume);
            this.panel4.Controls.Add(this.label1);
            this.panel4.Controls.Add(this.lbl_currentVolume);
            this.panel4.Controls.Add(this.lbl_volumeData);
            this.panel4.Dock = System.Windows.Forms.DockStyle.Left;
            this.panel4.Location = new System.Drawing.Point(126, 3);
            this.panel4.Name = "panel4";
            this.panel4.Padding = new System.Windows.Forms.Padding(3);
            this.panel4.Size = new System.Drawing.Size(129, 20);
            this.panel4.TabIndex = 12;
            // 
            // lbl_totalVolume
            // 
            this.lbl_totalVolume.AutoSize = true;
            this.lbl_totalVolume.Dock = System.Windows.Forms.DockStyle.Left;
            this.lbl_totalVolume.Location = new System.Drawing.Point(96, 3);
            this.lbl_totalVolume.Name = "lbl_totalVolume";
            this.lbl_totalVolume.Size = new System.Drawing.Size(30, 13);
            this.lbl_totalVolume.TabIndex = 3;
            this.lbl_totalVolume.Text = "0 Gb";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Dock = System.Windows.Forms.DockStyle.Left;
            this.label1.Location = new System.Drawing.Point(84, 3);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(12, 13);
            this.label1.TabIndex = 2;
            this.label1.Text = "/";
            // 
            // lbl_currentVolume
            // 
            this.lbl_currentVolume.AutoSize = true;
            this.lbl_currentVolume.Dock = System.Windows.Forms.DockStyle.Left;
            this.lbl_currentVolume.Location = new System.Drawing.Point(54, 3);
            this.lbl_currentVolume.Name = "lbl_currentVolume";
            this.lbl_currentVolume.Size = new System.Drawing.Size(30, 13);
            this.lbl_currentVolume.TabIndex = 1;
            this.lbl_currentVolume.Text = "0 Gb";
            // 
            // lbl_volumeData
            // 
            this.lbl_volumeData.AutoSize = true;
            this.lbl_volumeData.Dock = System.Windows.Forms.DockStyle.Left;
            this.lbl_volumeData.Location = new System.Drawing.Point(3, 3);
            this.lbl_volumeData.Name = "lbl_volumeData";
            this.lbl_volumeData.Size = new System.Drawing.Size(51, 13);
            this.lbl_volumeData.TabIndex = 0;
            this.lbl_volumeData.Text = "Volumen:";
            // 
            // panel2
            // 
            this.panel2.AutoSize = true;
            this.panel2.Controls.Add(this.lbl_totalFilesCount);
            this.panel2.Dock = System.Windows.Forms.DockStyle.Left;
            this.panel2.Location = new System.Drawing.Point(3, 3);
            this.panel2.Name = "panel2";
            this.panel2.Padding = new System.Windows.Forms.Padding(3);
            this.panel2.Size = new System.Drawing.Size(123, 20);
            this.panel2.TabIndex = 11;
            // 
            // lbl_totalFilesCount
            // 
            this.lbl_totalFilesCount.AutoSize = true;
            this.lbl_totalFilesCount.Dock = System.Windows.Forms.DockStyle.Left;
            this.lbl_totalFilesCount.Location = new System.Drawing.Point(3, 3);
            this.lbl_totalFilesCount.Name = "lbl_totalFilesCount";
            this.lbl_totalFilesCount.Size = new System.Drawing.Size(117, 13);
            this.lbl_totalFilesCount.TabIndex = 0;
            this.lbl_totalFilesCount.Text = "Archivos copiados: 0/0";
            // 
            // olv_queueList
            // 
            this.olv_queueList.AllColumns.Add(this.olvColumn1);
            this.olv_queueList.AllColumns.Add(this.olvColumn2);
            this.olv_queueList.AllColumns.Add(this.olvColumn3);
            this.olv_queueList.AllColumns.Add(this.olvColumn4);
            this.olv_queueList.AlternateRowBackColor = System.Drawing.Color.Silver;
            this.olv_queueList.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.olv_queueList.CellEditUseWholeCell = false;
            this.olv_queueList.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.olvColumn1,
            this.olvColumn2,
            this.olvColumn3,
            this.olvColumn4});
            this.olv_queueList.Cursor = System.Windows.Forms.Cursors.Default;
            this.olv_queueList.Dock = System.Windows.Forms.DockStyle.Fill;
            this.olv_queueList.FullRowSelect = true;
            this.olv_queueList.GridLines = true;
            this.olv_queueList.HasCollapsibleGroups = false;
            this.olv_queueList.IsSimpleDragSource = true;
            this.olv_queueList.IsSimpleDropSink = true;
            this.olv_queueList.Location = new System.Drawing.Point(0, 139);
            this.olv_queueList.Name = "olv_queueList";
            this.olv_queueList.SelectedBackColor = System.Drawing.Color.Red;
            this.olv_queueList.ShowGroups = false;
            this.olv_queueList.Size = new System.Drawing.Size(473, 0);
            this.olv_queueList.TabIndex = 14;
            this.olv_queueList.UseCompatibleStateImageBehavior = false;
            this.olv_queueList.View = System.Windows.Forms.View.Details;
            this.olv_queueList.SelectedIndexChanged += new System.EventHandler(this.olv_queueList_SelectedIndexChanged);
            this.olv_queueList.KeyUp += new System.Windows.Forms.KeyEventHandler(this.olv_queueList_KeyUp);
            // 
            // olvColumn1
            // 
            this.olvColumn1.AspectName = "Source";
            this.olvColumn1.Text = "Fuente";
            this.olvColumn1.Width = 186;
            // 
            // olvColumn2
            // 
            this.olvColumn2.AspectName = "Destination";
            this.olvColumn2.Text = "Destino";
            this.olvColumn2.Width = 208;
            // 
            // olvColumn3
            // 
            this.olvColumn3.AspectName = "Size";
            this.olvColumn3.AspectToStringFormat = "{0}";
            this.olvColumn3.Text = "Tamaño";
            this.olvColumn3.Width = 106;
            // 
            // olvColumn4
            // 
            this.olvColumn4.AspectName = "Extension";
            this.olvColumn4.Text = "Tipo de elemento";
            // 
            // MainUI
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(473, 165);
            this.Controls.Add(this.olv_queueList);
            this.Controls.Add(this.panel3);
            this.Controls.Add(this.panel1);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MinimumSize = new System.Drawing.Size(489, 204);
            this.Name = "MainUI";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "CopyCopy";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.MainUI_FormClosing);
            this.Shown += new System.EventHandler(this.MainUI_Shown);
            this.Resize += new System.EventHandler(this.MainUI_Resize);
            this.panel1.ResumeLayout(false);
            this.panel3.ResumeLayout(false);
            this.panel3.PerformLayout();
            this.panel6.ResumeLayout(false);
            this.panel6.PerformLayout();
            this.panel5.ResumeLayout(false);
            this.panel5.PerformLayout();
            this.panel4.ResumeLayout(false);
            this.panel4.PerformLayout();
            this.panel2.ResumeLayout(false);
            this.panel2.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.olv_queueList)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Label lbl_copyingFrom;
        private System.Windows.Forms.ProgressBar progressBar_currentState;
        private System.Windows.Forms.Label lbl_copyingTo;
        private System.Windows.Forms.ProgressBar progressBar_globalState;
        private System.Windows.Forms.Panel panel3;
        private System.Windows.Forms.Panel panel6;
        private System.Windows.Forms.Label lbl_bps;
        private System.Windows.Forms.Panel panel5;
        private System.Windows.Forms.Label lbl_selection;
        private System.Windows.Forms.Panel panel4;
        private System.Windows.Forms.Panel panel2;
        private System.Windows.Forms.Label lbl_totalFilesCount;
        private BrightIdeasSoftware.ObjectListView olv_queueList;
        private BrightIdeasSoftware.OLVColumn olvColumn1;
        private BrightIdeasSoftware.OLVColumn olvColumn2;
        private BrightIdeasSoftware.OLVColumn olvColumn3;
        private BrightIdeasSoftware.OLVColumn olvColumn4;
        private System.Windows.Forms.Button btn_seeCopyListSw;
        private System.Windows.Forms.Button btn_skipItem;
        private System.Windows.Forms.Button btn_pauseResumeSw;
        private System.Windows.Forms.Label lbl_totalVolume;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label lbl_currentVolume;
        private System.Windows.Forms.Label lbl_volumeData;
        private Controls.CustomProgressBar customProgressBar_cState;
        private Controls.CustomProgressBar customProgressBar_gState;
    }
}

