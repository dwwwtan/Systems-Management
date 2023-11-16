using System;

namespace APICs
{
    partial class Form1
    {
        private System.ComponentModel.IContainer components = null;
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

        private void InitializeComponent()
        {
            btnOpenFile = new Button();
            btnCopyFile = new Button();
            btnDeleteFile = new Button();
            btnGetFileAttributes = new Button();
            btnCreateFile = new Button();
            btnInfo = new Button();
            btnTaskManager = new Button();
            SuspendLayout();
            // 
            // btnOpenFile
            // 
            btnOpenFile.FlatStyle = FlatStyle.Popup;
            btnOpenFile.Location = new Point(297, 14);
            btnOpenFile.Margin = new Padding(4, 5, 4, 5);
            btnOpenFile.Name = "btnOpenFile";
            btnOpenFile.Size = new Size(200, 46);
            btnOpenFile.TabIndex = 0;
            btnOpenFile.Text = "Open File";
            btnOpenFile.UseVisualStyleBackColor = true;
            btnOpenFile.Click += btnOpenFile_Click;
            // 
            // btnCopyFile
            // 
            btnCopyFile.FlatStyle = FlatStyle.Popup;
            btnCopyFile.Location = new Point(297, 70);
            btnCopyFile.Margin = new Padding(4, 5, 4, 5);
            btnCopyFile.Name = "btnCopyFile";
            btnCopyFile.Size = new Size(200, 46);
            btnCopyFile.TabIndex = 1;
            btnCopyFile.Text = "Copy File";
            btnCopyFile.UseVisualStyleBackColor = true;
            btnCopyFile.Click += btnCopyFile_Click;
            // 
            // btnDeleteFile
            // 
            btnDeleteFile.FlatStyle = FlatStyle.Popup;
            btnDeleteFile.Location = new Point(297, 126);
            btnDeleteFile.Margin = new Padding(4, 5, 4, 5);
            btnDeleteFile.Name = "btnDeleteFile";
            btnDeleteFile.Size = new Size(200, 46);
            btnDeleteFile.TabIndex = 2;
            btnDeleteFile.Text = "Delete File";
            btnDeleteFile.UseVisualStyleBackColor = true;
            btnDeleteFile.Click += btnDeleteFile_Click;
            // 
            // btnGetFileAttributes
            // 
            btnGetFileAttributes.FlatStyle = FlatStyle.Popup;
            btnGetFileAttributes.Location = new Point(297, 182);
            btnGetFileAttributes.Margin = new Padding(4, 5, 4, 5);
            btnGetFileAttributes.Name = "btnGetFileAttributes";
            btnGetFileAttributes.Size = new Size(200, 46);
            btnGetFileAttributes.TabIndex = 3;
            btnGetFileAttributes.Text = "Get File Attributes";
            btnGetFileAttributes.UseVisualStyleBackColor = true;
            btnGetFileAttributes.Click += btnGetFileAttributes_Click;
            // 
            // btnCreateFile
            // 
            btnCreateFile.FlatStyle = FlatStyle.Popup;
            btnCreateFile.Location = new Point(297, 238);
            btnCreateFile.Margin = new Padding(4, 5, 4, 5);
            btnCreateFile.Name = "btnCreateFile";
            btnCreateFile.Size = new Size(200, 46);
            btnCreateFile.TabIndex = 4;
            btnCreateFile.Text = "Create File";
            btnCreateFile.UseVisualStyleBackColor = true;
            btnCreateFile.Click += btnCreateFile_Click;
            // 
            // btnInfo
            // 
            btnInfo.FlatStyle = FlatStyle.Popup;
            btnInfo.Location = new Point(297, 294);
            btnInfo.Margin = new Padding(4, 5, 4, 5);
            btnInfo.Name = "btnInfo";
            btnInfo.Size = new Size(200, 46);
            btnInfo.TabIndex = 5;
            btnInfo.Text = "System Information";
            btnInfo.UseVisualStyleBackColor = true;
            btnInfo.Click += btnInfo_Click;
            // 
            // btnTaskManager
            // 
            btnTaskManager.FlatStyle = FlatStyle.Popup;
            btnTaskManager.Location = new Point(297, 350);
            btnTaskManager.Margin = new Padding(4, 5, 4, 5);
            btnTaskManager.Name = "btnTaskManager";
            btnTaskManager.Size = new Size(200, 46);
            btnTaskManager.TabIndex = 6;
            btnTaskManager.Text = "Task Manager";
            btnTaskManager.UseVisualStyleBackColor = true;
            btnTaskManager.Click += btnTaskManager_Click;
            // 
            // Form1
            // 
            AutoScaleDimensions = new SizeF(8F, 20F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(795, 547);
            Controls.Add(btnTaskManager);
            Controls.Add(btnInfo);
            Controls.Add(btnCreateFile);
            Controls.Add(btnGetFileAttributes);
            Controls.Add(btnDeleteFile);
            Controls.Add(btnCopyFile);
            Controls.Add(btnOpenFile);
            Margin = new Padding(4, 5, 4, 5);
            Name = "Form1";
            Text = "Systems Managerment";
            Load += Form1_Load;
            ResumeLayout(false);
        }

        #endregion

        private Button btnOpenFile;
        private Button btnCopyFile;
        private Button btnDeleteFile;
        private Button btnGetFileAttributes;
        private Button btnCreateFile;
        private Button btnInfo;
        private Button btnTaskManager;
    }
}