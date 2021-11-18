
namespace Steganography.DesktopUI
{
    partial class Form1
    {
        /// <summary>
        ///  Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        ///  Clean up any resources being used.
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
        ///  Required method for Designer support - do not modify
        ///  the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.openOriginalFileButton = new System.Windows.Forms.Button();
            this.encryptButton = new System.Windows.Forms.Button();
            this.decryptButton = new System.Windows.Forms.Button();
            this.saveEncryptedFileButton = new System.Windows.Forms.Button();
            this.lsbChecker = new System.Windows.Forms.RadioButton();
            this.simpleChecker = new System.Windows.Forms.RadioButton();
            this.console = new System.Windows.Forms.TextBox();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.openDataFileButton = new System.Windows.Forms.Button();
            this.saveDecryptedFileButton = new System.Windows.Forms.Button();
            this.groupBox1.SuspendLayout();
            this.SuspendLayout();
            // 
            // openOriginalFileButton
            // 
            this.openOriginalFileButton.Location = new System.Drawing.Point(557, 13);
            this.openOriginalFileButton.Name = "openOriginalFileButton";
            this.openOriginalFileButton.Size = new System.Drawing.Size(155, 23);
            this.openOriginalFileButton.TabIndex = 1;
            this.openOriginalFileButton.Text = "Select Original File";
            this.openOriginalFileButton.UseVisualStyleBackColor = true;
            this.openOriginalFileButton.Click += new System.EventHandler(this.openButton_Click);
            // 
            // encryptButton
            // 
            this.encryptButton.Location = new System.Drawing.Point(557, 71);
            this.encryptButton.Name = "encryptButton";
            this.encryptButton.Size = new System.Drawing.Size(75, 23);
            this.encryptButton.TabIndex = 2;
            this.encryptButton.Text = "Encrypt";
            this.encryptButton.UseVisualStyleBackColor = true;
            this.encryptButton.Click += new System.EventHandler(this.encryptButton_Click);
            // 
            // decryptButton
            // 
            this.decryptButton.Location = new System.Drawing.Point(637, 71);
            this.decryptButton.Name = "decryptButton";
            this.decryptButton.Size = new System.Drawing.Size(75, 23);
            this.decryptButton.TabIndex = 6;
            this.decryptButton.Text = "Decrypt";
            this.decryptButton.UseVisualStyleBackColor = true;
            this.decryptButton.Click += new System.EventHandler(this.decryptButton_Click);
            // 
            // saveEncryptedFileButton
            // 
            this.saveEncryptedFileButton.Location = new System.Drawing.Point(557, 100);
            this.saveEncryptedFileButton.Name = "saveEncryptedFileButton";
            this.saveEncryptedFileButton.Size = new System.Drawing.Size(155, 23);
            this.saveEncryptedFileButton.TabIndex = 7;
            this.saveEncryptedFileButton.Text = "Save Encrypted File";
            this.saveEncryptedFileButton.UseVisualStyleBackColor = true;
            this.saveEncryptedFileButton.Click += new System.EventHandler(this.saveButton_Click);
            // 
            // lsbChecker
            // 
            this.lsbChecker.AutoSize = true;
            this.lsbChecker.Checked = true;
            this.lsbChecker.Location = new System.Drawing.Point(6, 22);
            this.lsbChecker.Name = "lsbChecker";
            this.lsbChecker.Size = new System.Drawing.Size(44, 19);
            this.lsbChecker.TabIndex = 8;
            this.lsbChecker.TabStop = true;
            this.lsbChecker.Text = "LSB";
            this.lsbChecker.UseVisualStyleBackColor = true;
            this.lsbChecker.CheckedChanged += new System.EventHandler(this.Checker_CheckedChanged);
            // 
            // simpleChecker
            // 
            this.simpleChecker.AutoSize = true;
            this.simpleChecker.Location = new System.Drawing.Point(6, 47);
            this.simpleChecker.Name = "simpleChecker";
            this.simpleChecker.Size = new System.Drawing.Size(61, 19);
            this.simpleChecker.TabIndex = 9;
            this.simpleChecker.Text = "Simple";
            this.simpleChecker.UseVisualStyleBackColor = true;
            this.simpleChecker.CheckedChanged += new System.EventHandler(this.Checker_CheckedChanged);
            // 
            // console
            // 
            this.console.Location = new System.Drawing.Point(13, 13);
            this.console.Multiline = true;
            this.console.Name = "console";
            this.console.ScrollBars = System.Windows.Forms.ScrollBars.Both;
            this.console.Size = new System.Drawing.Size(403, 253);
            this.console.TabIndex = 10;
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.lsbChecker);
            this.groupBox1.Controls.Add(this.simpleChecker);
            this.groupBox1.Location = new System.Drawing.Point(422, 13);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(106, 100);
            this.groupBox1.TabIndex = 11;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Algorithm";
            // 
            // openDataFileButton
            // 
            this.openDataFileButton.Location = new System.Drawing.Point(557, 42);
            this.openDataFileButton.Name = "openDataFileButton";
            this.openDataFileButton.Size = new System.Drawing.Size(155, 23);
            this.openDataFileButton.TabIndex = 12;
            this.openDataFileButton.Text = "Select Data File";
            this.openDataFileButton.UseVisualStyleBackColor = true;
            this.openDataFileButton.Click += new System.EventHandler(this.openDataFileButton_Click);
            // 
            // saveDecryptedFileButton
            // 
            this.saveDecryptedFileButton.Location = new System.Drawing.Point(557, 129);
            this.saveDecryptedFileButton.Name = "saveDecryptedFileButton";
            this.saveDecryptedFileButton.Size = new System.Drawing.Size(155, 23);
            this.saveDecryptedFileButton.TabIndex = 13;
            this.saveDecryptedFileButton.Text = "Save Decrypted File";
            this.saveDecryptedFileButton.UseVisualStyleBackColor = true;
            this.saveDecryptedFileButton.Click += new System.EventHandler(this.saveDecryptedFileButton_Click);
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(724, 278);
            this.Controls.Add(this.saveDecryptedFileButton);
            this.Controls.Add(this.openDataFileButton);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.console);
            this.Controls.Add(this.saveEncryptedFileButton);
            this.Controls.Add(this.decryptButton);
            this.Controls.Add(this.encryptButton);
            this.Controls.Add(this.openOriginalFileButton);
            this.Name = "Form1";
            this.Text = "Steganography";
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private System.Windows.Forms.Button openOriginalFileButton;
        private System.Windows.Forms.Button encryptButton;
        private System.Windows.Forms.Button decryptButton;
        private System.Windows.Forms.Button saveEncryptedFileButton;
        private System.Windows.Forms.RadioButton lsbChecker;
        private System.Windows.Forms.RadioButton simpleChecker;
        private System.Windows.Forms.Label freeSpaceLabel;
        private System.Windows.Forms.TextBox console;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.Button openDataFileButton;
        private System.Windows.Forms.Button saveDecryptedFileButton;
    }
}

