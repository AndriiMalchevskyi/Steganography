
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
            this.pictureBox = new System.Windows.Forms.PictureBox();
            this.openButton = new System.Windows.Forms.Button();
            this.encryptButton = new System.Windows.Forms.Button();
            this.inputTextBox = new System.Windows.Forms.TextBox();
            this.outputTextBox = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.decryptButton = new System.Windows.Forms.Button();
            this.saveButton = new System.Windows.Forms.Button();
            this.lsbChecker = new System.Windows.Forms.RadioButton();
            this.simpleChecker = new System.Windows.Forms.RadioButton();
            this.spaceLabel = new System.Windows.Forms.Label();
            this.encoderTime = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox)).BeginInit();
            this.SuspendLayout();
            // 
            // pictureBox
            // 
            this.pictureBox.BackColor = System.Drawing.SystemColors.ControlDark;
            this.pictureBox.Location = new System.Drawing.Point(12, 12);
            this.pictureBox.Name = "pictureBox";
            this.pictureBox.Size = new System.Drawing.Size(303, 178);
            this.pictureBox.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.pictureBox.TabIndex = 0;
            this.pictureBox.TabStop = false;
            // 
            // openButton
            // 
            this.openButton.Location = new System.Drawing.Point(637, 26);
            this.openButton.Name = "openButton";
            this.openButton.Size = new System.Drawing.Size(75, 23);
            this.openButton.TabIndex = 1;
            this.openButton.Text = "Open";
            this.openButton.UseVisualStyleBackColor = true;
            this.openButton.Click += new System.EventHandler(this.openButton_Click);
            // 
            // encryptButton
            // 
            this.encryptButton.Location = new System.Drawing.Point(637, 55);
            this.encryptButton.Name = "encryptButton";
            this.encryptButton.Size = new System.Drawing.Size(75, 23);
            this.encryptButton.TabIndex = 2;
            this.encryptButton.Text = "Encrypt";
            this.encryptButton.UseVisualStyleBackColor = true;
            this.encryptButton.Click += new System.EventHandler(this.encryptButton_Click);
            // 
            // inputTextBox
            // 
            this.inputTextBox.Location = new System.Drawing.Point(321, 27);
            this.inputTextBox.MaxLength = 2147483647;
            this.inputTextBox.Multiline = true;
            this.inputTextBox.Name = "inputTextBox";
            this.inputTextBox.Size = new System.Drawing.Size(310, 66);
            this.inputTextBox.TabIndex = 3;
            this.inputTextBox.TextChanged += new System.EventHandler(this.inputTextBox_TextChanged);
            // 
            // outputTextBox
            // 
            this.outputTextBox.Location = new System.Drawing.Point(321, 124);
            this.outputTextBox.Multiline = true;
            this.outputTextBox.Name = "outputTextBox";
            this.outputTextBox.ReadOnly = true;
            this.outputTextBox.Size = new System.Drawing.Size(310, 66);
            this.outputTextBox.TabIndex = 4;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(321, 12);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(28, 15);
            this.label1.TabIndex = 5;
            this.label1.Text = "Text";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(321, 107);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(68, 15);
            this.label2.TabIndex = 5;
            this.label2.Text = "Output text";
            // 
            // decryptButton
            // 
            this.decryptButton.Location = new System.Drawing.Point(637, 84);
            this.decryptButton.Name = "decryptButton";
            this.decryptButton.Size = new System.Drawing.Size(75, 23);
            this.decryptButton.TabIndex = 6;
            this.decryptButton.Text = "Decrypt";
            this.decryptButton.UseVisualStyleBackColor = true;
            this.decryptButton.Click += new System.EventHandler(this.decryptButton_Click);
            // 
            // saveButton
            // 
            this.saveButton.Location = new System.Drawing.Point(637, 113);
            this.saveButton.Name = "saveButton";
            this.saveButton.Size = new System.Drawing.Size(75, 23);
            this.saveButton.TabIndex = 7;
            this.saveButton.Text = "Save";
            this.saveButton.UseVisualStyleBackColor = true;
            this.saveButton.Click += new System.EventHandler(this.saveButton_Click);
            // 
            // lsbChecker
            // 
            this.lsbChecker.AutoSize = true;
            this.lsbChecker.Checked = true;
            this.lsbChecker.Location = new System.Drawing.Point(637, 146);
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
            this.simpleChecker.Location = new System.Drawing.Point(637, 171);
            this.simpleChecker.Name = "simpleChecker";
            this.simpleChecker.Size = new System.Drawing.Size(61, 19);
            this.simpleChecker.TabIndex = 9;
            this.simpleChecker.Text = "Simple";
            this.simpleChecker.UseVisualStyleBackColor = true;
            this.simpleChecker.CheckedChanged += new System.EventHandler(this.Checker_CheckedChanged);
            // 
            // spaceLabel
            // 
            this.spaceLabel.Location = new System.Drawing.Point(531, 93);
            this.spaceLabel.Name = "spaceLabel";
            this.spaceLabel.Size = new System.Drawing.Size(100, 15);
            this.spaceLabel.TabIndex = 10;
            this.spaceLabel.Text = "0/0";
            this.spaceLabel.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // encoderTime
            // 
            this.encoderTime.AutoSize = true;
            this.encoderTime.Location = new System.Drawing.Point(12, 193);
            this.encoderTime.Name = "encoderTime";
            this.encoderTime.Size = new System.Drawing.Size(83, 15);
            this.encoderTime.TabIndex = 11;
            this.encoderTime.Text = "Encoder time: ";
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(724, 278);
            this.Controls.Add(this.encoderTime);
            this.Controls.Add(this.spaceLabel);
            this.Controls.Add(this.simpleChecker);
            this.Controls.Add(this.lsbChecker);
            this.Controls.Add(this.saveButton);
            this.Controls.Add(this.decryptButton);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.outputTextBox);
            this.Controls.Add(this.inputTextBox);
            this.Controls.Add(this.encryptButton);
            this.Controls.Add(this.openButton);
            this.Controls.Add(this.pictureBox);
            this.Name = "Form1";
            this.Text = "Steganography";
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.PictureBox pictureBox;
        private System.Windows.Forms.Button openButton;
        private System.Windows.Forms.Button encryptButton;
        private System.Windows.Forms.TextBox inputTextBox;
        private System.Windows.Forms.TextBox outputTextBox;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Button decryptButton;
        private System.Windows.Forms.Button saveButton;
        private System.Windows.Forms.RadioButton lsbChecker;
        private System.Windows.Forms.RadioButton simpleChecker;
        private System.Windows.Forms.Label freeSpaceLabel;
        private System.Windows.Forms.Label spaceLabel;
        private System.Windows.Forms.Label encoderTime;
    }
}

