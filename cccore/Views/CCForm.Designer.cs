namespace CcCore.Views
{
    partial class CopyCopyMF
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
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(CopyCopyMF));
            this.progressBar_currentState = new System.Windows.Forms.ProgressBar();
            this.lbl_copyingFrom = new System.Windows.Forms.Label();
            this.lbl_copyingTo = new System.Windows.Forms.Label();
            this.progressBar_globalState = new System.Windows.Forms.ProgressBar();
            this.faButton1 = new CcCore.Controls.FAButton();
            this.btn_startResume = new CcCore.Controls.FAButton();
            this.olv_queueList = new BrightIdeasSoftware.ObjectListView();
            this.olvColumn1 = ((BrightIdeasSoftware.OLVColumn)(new BrightIdeasSoftware.OLVColumn()));
            this.olvColumn2 = ((BrightIdeasSoftware.OLVColumn)(new BrightIdeasSoftware.OLVColumn()));
            this.olvColumn3 = ((BrightIdeasSoftware.OLVColumn)(new BrightIdeasSoftware.OLVColumn()));
            this.olvColumn4 = ((BrightIdeasSoftware.OLVColumn)(new BrightIdeasSoftware.OLVColumn()));
            this.panel1 = new System.Windows.Forms.Panel();
            this.btn_stepFoward = new CcCore.Controls.FAButton();
            this.lbl_bps = new System.Windows.Forms.Label();
            this.panel_queueContainer = new System.Windows.Forms.Panel();
            this.panel3 = new System.Windows.Forms.Panel();
            this.panel6 = new System.Windows.Forms.Panel();
            this.panel5 = new System.Windows.Forms.Panel();
            this.lbl_selection = new System.Windows.Forms.Label();
            this.panel4 = new System.Windows.Forms.Panel();
            this.lbl_volumeData = new System.Windows.Forms.Label();
            this.panel2 = new System.Windows.Forms.Panel();
            this.lbl_totalFilesCount = new System.Windows.Forms.Label();
            this.noticon = new System.Windows.Forms.NotifyIcon(this.components);
            ((System.ComponentModel.ISupportInitialize)(this.olv_queueList)).BeginInit();
            this.panel1.SuspendLayout();
            this.panel_queueContainer.SuspendLayout();
            this.panel3.SuspendLayout();
            this.panel6.SuspendLayout();
            this.panel5.SuspendLayout();
            this.panel4.SuspendLayout();
            this.panel2.SuspendLayout();
            this.SuspendLayout();
            // 
            // progressBar_currentState
            // 
            this.progressBar_currentState.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.progressBar_currentState.ForeColor = System.Drawing.Color.DodgerBlue;
            this.progressBar_currentState.Location = new System.Drawing.Point(9, 29);
            this.progressBar_currentState.Name = "progressBar_currentState";
            this.progressBar_currentState.Size = new System.Drawing.Size(503, 21);
            this.progressBar_currentState.Step = 1;
            this.progressBar_currentState.Style = System.Windows.Forms.ProgressBarStyle.Continuous;
            this.progressBar_currentState.TabIndex = 0;
            // 
            // lbl_copyingFrom
            // 
            this.lbl_copyingFrom.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.lbl_copyingFrom.Location = new System.Drawing.Point(6, 13);
            this.lbl_copyingFrom.Name = "lbl_copyingFrom";
            this.lbl_copyingFrom.Size = new System.Drawing.Size(505, 13);
            this.lbl_copyingFrom.TabIndex = 1;
            this.lbl_copyingFrom.Text = "Copiando desde";
            // 
            // lbl_copyingTo
            // 
            this.lbl_copyingTo.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.lbl_copyingTo.Location = new System.Drawing.Point(6, 59);
            this.lbl_copyingTo.Name = "lbl_copyingTo";
            this.lbl_copyingTo.Size = new System.Drawing.Size(505, 13);
            this.lbl_copyingTo.TabIndex = 2;
            this.lbl_copyingTo.Text = "Hacia:";
            // 
            // progressBar_globalState
            // 
            this.progressBar_globalState.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.progressBar_globalState.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(192)))), ((int)(((byte)(0)))));
            this.progressBar_globalState.Location = new System.Drawing.Point(9, 75);
            this.progressBar_globalState.Name = "progressBar_globalState";
            this.progressBar_globalState.Size = new System.Drawing.Size(503, 21);
            this.progressBar_globalState.Step = 1;
            this.progressBar_globalState.TabIndex = 6;
            // 
            // faButton1
            // 
            this.faButton1.ActiveOnClick = true;
            this.faButton1.BackColor = System.Drawing.Color.DarkGray;
            this.faButton1.BackgroundColor = System.Drawing.Color.DarkGray;
            this.faButton1.BackgroundHoverColor = System.Drawing.Color.Gray;
            this.faButton1.Cursor = System.Windows.Forms.Cursors.Hand;
            this.faButton1.IconColor = System.Drawing.Color.LightGray;
            this.faButton1.IconGraph = FontAwesome.Sharp.IconChar.CaretDown;
            this.faButton1.IconSize = 64;
            this.faButton1.Location = new System.Drawing.Point(10, 102);
            this.faButton1.Name = "faButton1";
            this.faButton1.Padding = new System.Windows.Forms.Padding(0, 4, 0, 0);
            this.faButton1.Size = new System.Drawing.Size(33, 25);
            this.faButton1.TabIndex = 7;
            this.faButton1.Click += new System.EventHandler(this.faButton1_Click);
            // 
            // btn_startResume
            // 
            this.btn_startResume.ActiveOnClick = false;
            this.btn_startResume.BackColor = System.Drawing.Color.DarkGray;
            this.btn_startResume.BackgroundColor = System.Drawing.Color.DarkGray;
            this.btn_startResume.BackgroundHoverColor = System.Drawing.Color.DimGray;
            this.btn_startResume.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btn_startResume.IconColor = System.Drawing.Color.LightGray;
            this.btn_startResume.IconGraph = FontAwesome.Sharp.IconChar.Pause;
            this.btn_startResume.IconSize = 64;
            this.btn_startResume.Location = new System.Drawing.Point(49, 102);
            this.btn_startResume.Name = "btn_startResume";
            this.btn_startResume.Padding = new System.Windows.Forms.Padding(0, 4, 0, 0);
            this.btn_startResume.Size = new System.Drawing.Size(33, 25);
            this.btn_startResume.TabIndex = 8;
            this.btn_startResume.Click += new System.EventHandler(this.faButton2_Click);
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
            this.olv_queueList.Location = new System.Drawing.Point(0, 0);
            this.olv_queueList.Name = "olv_queueList";
            this.olv_queueList.SelectedBackColor = System.Drawing.Color.Red;
            this.olv_queueList.ShowGroups = false;
            this.olv_queueList.Size = new System.Drawing.Size(524, 128);
            this.olv_queueList.TabIndex = 10;
            this.olv_queueList.UseCompatibleStateImageBehavior = false;
            this.olv_queueList.View = System.Windows.Forms.View.Details;
            this.olv_queueList.ModelCanDrop += new System.EventHandler<BrightIdeasSoftware.ModelDropEventArgs>(this.olv_queueList_ModelCanDrop);
            this.olv_queueList.SelectedIndexChanged += new System.EventHandler(this.olv_queueList_SelectedIndexChanged);
            this.olv_queueList.KeyDown += new System.Windows.Forms.KeyEventHandler(this.olv_queueList_KeyDown);
            // 
            // olvColumn1
            // 
            this.olvColumn1.AspectName = "SourceFile";
            this.olvColumn1.Text = "Fuente";
            this.olvColumn1.Width = 186;
            // 
            // olvColumn2
            // 
            this.olvColumn2.AspectName = "DestFile";
            this.olvColumn2.Text = "Destino";
            this.olvColumn2.Width = 208;
            // 
            // olvColumn3
            // 
            this.olvColumn3.AspectName = "FileInfo.Length";
            this.olvColumn3.AspectToStringFormat = "{0}";
            this.olvColumn3.Text = "Tamaño";
            this.olvColumn3.Width = 106;
            // 
            // olvColumn4
            // 
            this.olvColumn4.AspectName = "FileInfo.Extension";
            this.olvColumn4.Text = "Tipo de elemento";
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.btn_stepFoward);
            this.panel1.Controls.Add(this.lbl_copyingFrom);
            this.panel1.Controls.Add(this.progressBar_currentState);
            this.panel1.Controls.Add(this.lbl_copyingTo);
            this.panel1.Controls.Add(this.btn_startResume);
            this.panel1.Controls.Add(this.progressBar_globalState);
            this.panel1.Controls.Add(this.faButton1);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Top;
            this.panel1.Location = new System.Drawing.Point(0, 0);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(524, 133);
            this.panel1.TabIndex = 11;
            // 
            // btn_stepFoward
            // 
            this.btn_stepFoward.ActiveOnClick = false;
            this.btn_stepFoward.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btn_stepFoward.BackColor = System.Drawing.Color.DarkGray;
            this.btn_stepFoward.BackgroundColor = System.Drawing.Color.DarkGray;
            this.btn_stepFoward.BackgroundHoverColor = System.Drawing.Color.DimGray;
            this.btn_stepFoward.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btn_stepFoward.IconColor = System.Drawing.Color.LightGray;
            this.btn_stepFoward.IconGraph = FontAwesome.Sharp.IconChar.StepForward;
            this.btn_stepFoward.IconSize = 64;
            this.btn_stepFoward.Location = new System.Drawing.Point(479, 102);
            this.btn_stepFoward.Name = "btn_stepFoward";
            this.btn_stepFoward.Padding = new System.Windows.Forms.Padding(0, 4, 0, 0);
            this.btn_stepFoward.Size = new System.Drawing.Size(33, 25);
            this.btn_stepFoward.TabIndex = 9;
            this.btn_stepFoward.Click += new System.EventHandler(this.btn_stepFoward_Click);
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
            // panel_queueContainer
            // 
            this.panel_queueContainer.Controls.Add(this.olv_queueList);
            this.panel_queueContainer.Controls.Add(this.panel3);
            this.panel_queueContainer.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel_queueContainer.Location = new System.Drawing.Point(0, 133);
            this.panel_queueContainer.Name = "panel_queueContainer";
            this.panel_queueContainer.Size = new System.Drawing.Size(524, 154);
            this.panel_queueContainer.TabIndex = 12;
            // 
            // panel3
            // 
            this.panel3.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(224)))), ((int)(((byte)(224)))), ((int)(((byte)(224)))));
            this.panel3.Controls.Add(this.panel6);
            this.panel3.Controls.Add(this.panel5);
            this.panel3.Controls.Add(this.panel4);
            this.panel3.Controls.Add(this.panel2);
            this.panel3.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.panel3.Location = new System.Drawing.Point(0, 128);
            this.panel3.Name = "panel3";
            this.panel3.Padding = new System.Windows.Forms.Padding(3);
            this.panel3.Size = new System.Drawing.Size(524, 26);
            this.panel3.TabIndex = 11;
            // 
            // panel6
            // 
            this.panel6.AutoSize = true;
            this.panel6.Controls.Add(this.lbl_bps);
            this.panel6.Dock = System.Windows.Forms.DockStyle.Right;
            this.panel6.Location = new System.Drawing.Point(470, 3);
            this.panel6.Name = "panel6";
            this.panel6.Padding = new System.Windows.Forms.Padding(3);
            this.panel6.Size = new System.Drawing.Size(51, 20);
            this.panel6.TabIndex = 14;
            // 
            // panel5
            // 
            this.panel5.AutoSize = true;
            this.panel5.Controls.Add(this.lbl_selection);
            this.panel5.Dock = System.Windows.Forms.DockStyle.Left;
            this.panel5.Location = new System.Drawing.Point(251, 3);
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
            this.panel4.Controls.Add(this.lbl_volumeData);
            this.panel4.Dock = System.Windows.Forms.DockStyle.Left;
            this.panel4.Location = new System.Drawing.Point(126, 3);
            this.panel4.Name = "panel4";
            this.panel4.Padding = new System.Windows.Forms.Padding(3);
            this.panel4.Size = new System.Drawing.Size(125, 20);
            this.panel4.TabIndex = 12;
            // 
            // lbl_volumeData
            // 
            this.lbl_volumeData.AutoSize = true;
            this.lbl_volumeData.Dock = System.Windows.Forms.DockStyle.Left;
            this.lbl_volumeData.Location = new System.Drawing.Point(3, 3);
            this.lbl_volumeData.Name = "lbl_volumeData";
            this.lbl_volumeData.Size = new System.Drawing.Size(119, 13);
            this.lbl_volumeData.TabIndex = 0;
            this.lbl_volumeData.Text = "Volumen: 0 gb / 100 gb";
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
            // noticon
            // 
            this.noticon.Text = "Copy Copy";
            this.noticon.DoubleClick += new System.EventHandler(this.noticon_DoubleClick);
            // 
            // CopyCopyMF
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(524, 287);
            this.Controls.Add(this.panel_queueContainer);
            this.Controls.Add(this.panel1);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MinimumSize = new System.Drawing.Size(540, 197);
            this.Name = "CopyCopyMF";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Copy-Copy";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.CopyCopyMF_FormClosing_1);
            this.SizeChanged += new System.EventHandler(this.CopyCopyMF_SizeChanged);
            ((System.ComponentModel.ISupportInitialize)(this.olv_queueList)).EndInit();
            this.panel1.ResumeLayout(false);
            this.panel_queueContainer.ResumeLayout(false);
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
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.ProgressBar progressBar_currentState;
        private System.Windows.Forms.Label lbl_copyingFrom;
        private System.Windows.Forms.Label lbl_copyingTo;
        private System.Windows.Forms.ProgressBar progressBar_globalState;
        private Controls.FAButton faButton1;
        private Controls.FAButton btn_startResume;
        private BrightIdeasSoftware.ObjectListView olv_queueList;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Panel panel_queueContainer;
        private System.Windows.Forms.Panel panel3;
        private BrightIdeasSoftware.OLVColumn olvColumn1;
        private BrightIdeasSoftware.OLVColumn olvColumn2;
        private BrightIdeasSoftware.OLVColumn olvColumn3;
        private System.Windows.Forms.Label lbl_bps;
        private System.Windows.Forms.Panel panel2;
        private System.Windows.Forms.Label lbl_totalFilesCount;
        private Controls.FAButton btn_stepFoward;
        private System.Windows.Forms.NotifyIcon noticon;
        private System.Windows.Forms.Panel panel4;
        private System.Windows.Forms.Label lbl_volumeData;
        private System.Windows.Forms.Panel panel5;
        private System.Windows.Forms.Label lbl_selection;
        private System.Windows.Forms.Panel panel6;
        private BrightIdeasSoftware.OLVColumn olvColumn4;
    }
}

