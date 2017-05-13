namespace Pixel_Art_Tool
{
    partial class FormMain
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
            this.LabelOutDir = new System.Windows.Forms.Label();
            this.DialogFolderBrowsing = new System.Windows.Forms.FolderBrowserDialog();
            this.TBOutDir = new System.Windows.Forms.TextBox();
            this.BtnOutBrowse = new System.Windows.Forms.Button();
            this.LabelInImage = new System.Windows.Forms.Label();
            this.TBInImage = new System.Windows.Forms.TextBox();
            this.BtnInBrowse = new System.Windows.Forms.Button();
            this.DialogFileOpen = new System.Windows.Forms.OpenFileDialog();
            this.TBProjectName = new System.Windows.Forms.TextBox();
            this.LabelProjectName = new System.Windows.Forms.Label();
            this.LableProjectDirCreationNotice = new System.Windows.Forms.Label();
            this.BtnGenerate = new System.Windows.Forms.Button();
            this.LabelMaxHeight = new System.Windows.Forms.Label();
            this.TBMaxHeight = new System.Windows.Forms.TextBox();
            this.BtnSaveIndex = new System.Windows.Forms.Button();
            this.LabelAbout = new System.Windows.Forms.LinkLabel();
            this.GBOptions = new System.Windows.Forms.GroupBox();
            this.CBAllowUpscale = new System.Windows.Forms.CheckBox();
            this.BtnRestoreDefault = new System.Windows.Forms.Button();
            this.LabelOptionsNotice = new System.Windows.Forms.Label();
            this.CBOpenDirWhenDone = new System.Windows.Forms.CheckBox();
            this.CBSaveFilteredPixelated = new System.Windows.Forms.CheckBox();
            this.CBSaveFiltered = new System.Windows.Forms.CheckBox();
            this.CBSavePixelated = new System.Windows.Forms.CheckBox();
            this.CBSaveDownscaled = new System.Windows.Forms.CheckBox();
            this.DialogFileSave = new System.Windows.Forms.SaveFileDialog();
            this.TBMaxWidth = new System.Windows.Forms.TextBox();
            this.LabelMaxWidth = new System.Windows.Forms.Label();
            this.LabelDatabase = new System.Windows.Forms.Label();
            this.CBDatabase = new System.Windows.Forms.ComboBox();
            this.GBOptions.SuspendLayout();
            this.SuspendLayout();
            // 
            // LabelOutDir
            // 
            this.LabelOutDir.AutoSize = true;
            this.LabelOutDir.Location = new System.Drawing.Point(12, 11);
            this.LabelOutDir.Name = "LabelOutDir";
            this.LabelOutDir.Size = new System.Drawing.Size(74, 13);
            this.LabelOutDir.TabIndex = 0;
            this.LabelOutDir.Text = "Output folder: ";
            // 
            // TBOutDir
            // 
            this.TBOutDir.Location = new System.Drawing.Point(113, 7);
            this.TBOutDir.Name = "TBOutDir";
            this.TBOutDir.ReadOnly = true;
            this.TBOutDir.Size = new System.Drawing.Size(552, 20);
            this.TBOutDir.TabIndex = 1;
            // 
            // BtnOutBrowse
            // 
            this.BtnOutBrowse.Location = new System.Drawing.Point(674, 7);
            this.BtnOutBrowse.Name = "BtnOutBrowse";
            this.BtnOutBrowse.Size = new System.Drawing.Size(78, 23);
            this.BtnOutBrowse.TabIndex = 2;
            this.BtnOutBrowse.Text = "Browse...";
            this.BtnOutBrowse.UseVisualStyleBackColor = true;
            this.BtnOutBrowse.Click += new System.EventHandler(this.BtnOutBrowse_Click);
            // 
            // LabelInImage
            // 
            this.LabelInImage.AutoSize = true;
            this.LabelInImage.Location = new System.Drawing.Point(12, 39);
            this.LabelInImage.Name = "LabelInImage";
            this.LabelInImage.Size = new System.Drawing.Size(68, 13);
            this.LabelInImage.TabIndex = 0;
            this.LabelInImage.Text = "Input image: ";
            // 
            // TBInImage
            // 
            this.TBInImage.Location = new System.Drawing.Point(113, 36);
            this.TBInImage.Name = "TBInImage";
            this.TBInImage.ReadOnly = true;
            this.TBInImage.Size = new System.Drawing.Size(552, 20);
            this.TBInImage.TabIndex = 3;
            // 
            // BtnInBrowse
            // 
            this.BtnInBrowse.Location = new System.Drawing.Point(674, 36);
            this.BtnInBrowse.Name = "BtnInBrowse";
            this.BtnInBrowse.Size = new System.Drawing.Size(78, 23);
            this.BtnInBrowse.TabIndex = 4;
            this.BtnInBrowse.Text = "Browse...";
            this.BtnInBrowse.UseVisualStyleBackColor = true;
            this.BtnInBrowse.Click += new System.EventHandler(this.BtnInBrowse_Click);
            // 
            // TBProjectName
            // 
            this.TBProjectName.Location = new System.Drawing.Point(113, 65);
            this.TBProjectName.Name = "TBProjectName";
            this.TBProjectName.Size = new System.Drawing.Size(639, 20);
            this.TBProjectName.TabIndex = 5;
            // 
            // LabelProjectName
            // 
            this.LabelProjectName.AutoSize = true;
            this.LabelProjectName.Location = new System.Drawing.Point(12, 68);
            this.LabelProjectName.Name = "LabelProjectName";
            this.LabelProjectName.Size = new System.Drawing.Size(75, 13);
            this.LabelProjectName.TabIndex = 0;
            this.LabelProjectName.Text = "Project name: ";
            // 
            // LableProjectDirCreationNotice
            // 
            this.LableProjectDirCreationNotice.AutoSize = true;
            this.LableProjectDirCreationNotice.Location = new System.Drawing.Point(12, 91);
            this.LableProjectDirCreationNotice.Name = "LableProjectDirCreationNotice";
            this.LableProjectDirCreationNotice.Size = new System.Drawing.Size(510, 13);
            this.LableProjectDirCreationNotice.TabIndex = 0;
            this.LableProjectDirCreationNotice.Text = "Note: Project name cannot contain special characters, a folder for your project w" +
    "ill be automatically created";
            // 
            // BtnGenerate
            // 
            this.BtnGenerate.Font = new System.Drawing.Font("SimSun", 18F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.BtnGenerate.Location = new System.Drawing.Point(305, 134);
            this.BtnGenerate.Name = "BtnGenerate";
            this.BtnGenerate.Size = new System.Drawing.Size(447, 141);
            this.BtnGenerate.TabIndex = 16;
            this.BtnGenerate.Text = "Generate!";
            this.BtnGenerate.UseVisualStyleBackColor = true;
            this.BtnGenerate.Click += new System.EventHandler(this.BtnGenerate_Click);
            // 
            // LabelMaxHeight
            // 
            this.LabelMaxHeight.AutoSize = true;
            this.LabelMaxHeight.Location = new System.Drawing.Point(12, 111);
            this.LabelMaxHeight.Name = "LabelMaxHeight";
            this.LabelMaxHeight.Size = new System.Drawing.Size(65, 13);
            this.LabelMaxHeight.TabIndex = 0;
            this.LabelMaxHeight.Text = "Max height: ";
            // 
            // TBMaxHeight
            // 
            this.TBMaxHeight.Location = new System.Drawing.Point(83, 107);
            this.TBMaxHeight.Name = "TBMaxHeight";
            this.TBMaxHeight.Size = new System.Drawing.Size(216, 20);
            this.TBMaxHeight.TabIndex = 6;
            this.TBMaxHeight.Text = "150";
            // 
            // BtnSaveIndex
            // 
            this.BtnSaveIndex.Location = new System.Drawing.Point(305, 281);
            this.BtnSaveIndex.Name = "BtnSaveIndex";
            this.BtnSaveIndex.Size = new System.Drawing.Size(447, 70);
            this.BtnSaveIndex.TabIndex = 17;
            this.BtnSaveIndex.Text = "Save current database index as .txt file";
            this.BtnSaveIndex.UseVisualStyleBackColor = true;
            this.BtnSaveIndex.Click += new System.EventHandler(this.BtnSaveIndex_Click);
            // 
            // LabelAbout
            // 
            this.LabelAbout.AutoSize = true;
            this.LabelAbout.Location = new System.Drawing.Point(305, 357);
            this.LabelAbout.Name = "LabelAbout";
            this.LabelAbout.Size = new System.Drawing.Size(224, 13);
            this.LabelAbout.TabIndex = 18;
            this.LabelAbout.TabStop = true;
            this.LabelAbout.Text = "By jspenguin2017 - Click to enter project page";
            this.LabelAbout.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(this.LabelAbout_LinkClicked);
            // 
            // GBOptions
            // 
            this.GBOptions.Controls.Add(this.CBAllowUpscale);
            this.GBOptions.Controls.Add(this.BtnRestoreDefault);
            this.GBOptions.Controls.Add(this.LabelOptionsNotice);
            this.GBOptions.Controls.Add(this.CBOpenDirWhenDone);
            this.GBOptions.Controls.Add(this.CBSaveFilteredPixelated);
            this.GBOptions.Controls.Add(this.CBSaveFiltered);
            this.GBOptions.Controls.Add(this.CBSavePixelated);
            this.GBOptions.Controls.Add(this.CBSaveDownscaled);
            this.GBOptions.Location = new System.Drawing.Point(14, 160);
            this.GBOptions.Name = "GBOptions";
            this.GBOptions.Size = new System.Drawing.Size(285, 221);
            this.GBOptions.TabIndex = 0;
            this.GBOptions.TabStop = false;
            this.GBOptions.Text = "Options";
            // 
            // CBAllowUpscale
            // 
            this.CBAllowUpscale.AutoSize = true;
            this.CBAllowUpscale.Location = new System.Drawing.Point(6, 19);
            this.CBAllowUpscale.Name = "CBAllowUpscale";
            this.CBAllowUpscale.Size = new System.Drawing.Size(91, 17);
            this.CBAllowUpscale.TabIndex = 9;
            this.CBAllowUpscale.Text = "Allow upscale";
            this.CBAllowUpscale.UseVisualStyleBackColor = true;
            // 
            // BtnRestoreDefault
            // 
            this.BtnRestoreDefault.Location = new System.Drawing.Point(5, 176);
            this.BtnRestoreDefault.Name = "BtnRestoreDefault";
            this.BtnRestoreDefault.Size = new System.Drawing.Size(273, 34);
            this.BtnRestoreDefault.TabIndex = 15;
            this.BtnRestoreDefault.Text = "Restore default";
            this.BtnRestoreDefault.UseVisualStyleBackColor = true;
            // 
            // LabelOptionsNotice
            // 
            this.LabelOptionsNotice.AutoSize = true;
            this.LabelOptionsNotice.Location = new System.Drawing.Point(5, 160);
            this.LabelOptionsNotice.Name = "LabelOptionsNotice";
            this.LabelOptionsNotice.Size = new System.Drawing.Size(237, 13);
            this.LabelOptionsNotice.TabIndex = 0;
            this.LabelOptionsNotice.Text = "Construction plan and index will always be saved";
            // 
            // CBOpenDirWhenDone
            // 
            this.CBOpenDirWhenDone.AutoSize = true;
            this.CBOpenDirWhenDone.Checked = true;
            this.CBOpenDirWhenDone.CheckState = System.Windows.Forms.CheckState.Checked;
            this.CBOpenDirWhenDone.Location = new System.Drawing.Point(6, 140);
            this.CBOpenDirWhenDone.Name = "CBOpenDirWhenDone";
            this.CBOpenDirWhenDone.Size = new System.Drawing.Size(137, 17);
            this.CBOpenDirWhenDone.TabIndex = 14;
            this.CBOpenDirWhenDone.Text = "Open folder when done";
            this.CBOpenDirWhenDone.UseVisualStyleBackColor = true;
            // 
            // CBSaveFilteredPixelated
            // 
            this.CBSaveFilteredPixelated.AutoSize = true;
            this.CBSaveFilteredPixelated.Checked = true;
            this.CBSaveFilteredPixelated.CheckState = System.Windows.Forms.CheckState.Checked;
            this.CBSaveFilteredPixelated.Location = new System.Drawing.Point(6, 117);
            this.CBSaveFilteredPixelated.Name = "CBSaveFilteredPixelated";
            this.CBSaveFilteredPixelated.Size = new System.Drawing.Size(161, 17);
            this.CBSaveFilteredPixelated.TabIndex = 13;
            this.CBSaveFilteredPixelated.Text = "Save filtered pixelated image";
            this.CBSaveFilteredPixelated.UseVisualStyleBackColor = true;
            // 
            // CBSaveFiltered
            // 
            this.CBSaveFiltered.AutoSize = true;
            this.CBSaveFiltered.Location = new System.Drawing.Point(6, 92);
            this.CBSaveFiltered.Name = "CBSaveFiltered";
            this.CBSaveFiltered.Size = new System.Drawing.Size(116, 17);
            this.CBSaveFiltered.TabIndex = 12;
            this.CBSaveFiltered.Text = "Save filtered image";
            this.CBSaveFiltered.UseVisualStyleBackColor = true;
            // 
            // CBSavePixelated
            // 
            this.CBSavePixelated.AutoSize = true;
            this.CBSavePixelated.Location = new System.Drawing.Point(6, 67);
            this.CBSavePixelated.Name = "CBSavePixelated";
            this.CBSavePixelated.Size = new System.Drawing.Size(127, 17);
            this.CBSavePixelated.TabIndex = 11;
            this.CBSavePixelated.Text = "Save pixelated image";
            this.CBSavePixelated.UseVisualStyleBackColor = true;
            // 
            // CBSaveDownscaled
            // 
            this.CBSaveDownscaled.AutoSize = true;
            this.CBSaveDownscaled.Location = new System.Drawing.Point(6, 42);
            this.CBSaveDownscaled.Name = "CBSaveDownscaled";
            this.CBSaveDownscaled.Size = new System.Drawing.Size(142, 17);
            this.CBSaveDownscaled.TabIndex = 10;
            this.CBSaveDownscaled.Text = "Save downscaled image";
            this.CBSaveDownscaled.UseVisualStyleBackColor = true;
            // 
            // TBMaxWidth
            // 
            this.TBMaxWidth.Location = new System.Drawing.Point(83, 134);
            this.TBMaxWidth.Name = "TBMaxWidth";
            this.TBMaxWidth.Size = new System.Drawing.Size(216, 20);
            this.TBMaxWidth.TabIndex = 8;
            this.TBMaxWidth.Text = "999999";
            // 
            // LabelMaxWidth
            // 
            this.LabelMaxWidth.AutoSize = true;
            this.LabelMaxWidth.Location = new System.Drawing.Point(12, 134);
            this.LabelMaxWidth.Name = "LabelMaxWidth";
            this.LabelMaxWidth.Size = new System.Drawing.Size(61, 13);
            this.LabelMaxWidth.TabIndex = 0;
            this.LabelMaxWidth.Text = "Max width: ";
            // 
            // LabelDatabase
            // 
            this.LabelDatabase.AutoSize = true;
            this.LabelDatabase.Location = new System.Drawing.Point(305, 111);
            this.LabelDatabase.Name = "LabelDatabase";
            this.LabelDatabase.Size = new System.Drawing.Size(59, 13);
            this.LabelDatabase.TabIndex = 0;
            this.LabelDatabase.Text = "Database: ";
            // 
            // CBDatabase
            // 
            this.CBDatabase.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.CBDatabase.FormattingEnabled = true;
            this.CBDatabase.Items.AddRange(new object[] {
            "Trove Neutral by Summer Haas"});
            this.CBDatabase.Location = new System.Drawing.Point(371, 108);
            this.CBDatabase.Name = "CBDatabase";
            this.CBDatabase.Size = new System.Drawing.Size(381, 21);
            this.CBDatabase.TabIndex = 7;
            this.CBDatabase.SelectedIndexChanged += new System.EventHandler(this.CBDatabase_SelectedIndexChanged);
            // 
            // FormMain
            // 
            this.AllowDrop = true;
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(764, 390);
            this.Controls.Add(this.CBDatabase);
            this.Controls.Add(this.LabelDatabase);
            this.Controls.Add(this.LabelMaxWidth);
            this.Controls.Add(this.TBMaxWidth);
            this.Controls.Add(this.GBOptions);
            this.Controls.Add(this.LabelAbout);
            this.Controls.Add(this.BtnSaveIndex);
            this.Controls.Add(this.TBMaxHeight);
            this.Controls.Add(this.LabelMaxHeight);
            this.Controls.Add(this.BtnGenerate);
            this.Controls.Add(this.LableProjectDirCreationNotice);
            this.Controls.Add(this.LabelProjectName);
            this.Controls.Add(this.TBProjectName);
            this.Controls.Add(this.BtnInBrowse);
            this.Controls.Add(this.TBInImage);
            this.Controls.Add(this.LabelInImage);
            this.Controls.Add(this.BtnOutBrowse);
            this.Controls.Add(this.TBOutDir);
            this.Controls.Add(this.LabelOutDir);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.MaximizeBox = false;
            this.Name = "FormMain";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Pixel Art Tool";
            this.Load += new System.EventHandler(this.FormMain_Load);
            this.DragDrop += new System.Windows.Forms.DragEventHandler(this.FormMain_DragDrop);
            this.DragEnter += new System.Windows.Forms.DragEventHandler(this.FormMain_DragEnter);
            this.GBOptions.ResumeLayout(false);
            this.GBOptions.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label LabelOutDir;
        private System.Windows.Forms.FolderBrowserDialog DialogFolderBrowsing;
        private System.Windows.Forms.TextBox TBOutDir;
        private System.Windows.Forms.Button BtnOutBrowse;
        private System.Windows.Forms.Label LabelInImage;
        private System.Windows.Forms.TextBox TBInImage;
        private System.Windows.Forms.Button BtnInBrowse;
        private System.Windows.Forms.OpenFileDialog DialogFileOpen;
        private System.Windows.Forms.TextBox TBProjectName;
        private System.Windows.Forms.Label LabelProjectName;
        private System.Windows.Forms.Label LableProjectDirCreationNotice;
        private System.Windows.Forms.Button BtnGenerate;
        private System.Windows.Forms.Label LabelMaxHeight;
        private System.Windows.Forms.TextBox TBMaxHeight;
        private System.Windows.Forms.Button BtnSaveIndex;
        private System.Windows.Forms.LinkLabel LabelAbout;
        private System.Windows.Forms.GroupBox GBOptions;
        private System.Windows.Forms.CheckBox CBSaveFilteredPixelated;
        private System.Windows.Forms.CheckBox CBSavePixelated;
        private System.Windows.Forms.CheckBox CBSaveDownscaled;
        private System.Windows.Forms.CheckBox CBOpenDirWhenDone;
        private System.Windows.Forms.SaveFileDialog DialogFileSave;
        private System.Windows.Forms.CheckBox CBSaveFiltered;
        private System.Windows.Forms.Button BtnRestoreDefault;
        private System.Windows.Forms.Label LabelOptionsNotice;
        private System.Windows.Forms.CheckBox CBAllowUpscale;
        private System.Windows.Forms.TextBox TBMaxWidth;
        private System.Windows.Forms.Label LabelMaxWidth;
        private System.Windows.Forms.Label LabelDatabase;
        private System.Windows.Forms.ComboBox CBDatabase;
    }
}

