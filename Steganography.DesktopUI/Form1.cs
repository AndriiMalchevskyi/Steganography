using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using BLL;

namespace Steganography.DesktopUI
{
    public partial class Form1 : Form
    {
        private Stopwatch timer;
        private string baseImagePath;
        private ImageSteganographer steganographer;
        private ICommand OpenButton;
        private ICommand SaveButton;
        private ICommand EncryptButton;
        private ICommand DecryptButton;
        private ICommand CheckedChanged;
        private ICommand Clear;
        private ICommand inputTextBoxTextChanged;

        public Form1()
        {
            InitializeComponent();

            this.steganographer = new ImageSteganographer();
            //for now
            this.steganographer.SetLSBAlgorithm();

            this.OpenButton = new Command(this.OpenImage);
            this.SaveButton = new Command(this.SaveImage);
            this.EncryptButton = new Command(this.EncryptText);
            this.DecryptButton = new Command(this.DecryptText);
            this.CheckedChanged = new Command(this.AlgorithmChecked);
            this.inputTextBoxTextChanged = new Command(this.CalculateFreeAndUsedSpace);
            this.Clear = new Command(this.ClearUI);
        }

        #region EventHandlers
        private void openButton_Click(object sender, EventArgs e)
        {
            this.Clear.Execute();
            this.OpenButton.Execute();
        }

        private void encryptButton_Click(object sender, EventArgs e)
        {
            this.EncryptButton.Execute();
        }

        private void decryptButton_Click(object sender, EventArgs e)
        {
            this.DecryptButton.Execute();
        }

        private void saveButton_Click(object sender, EventArgs e)
        {
            this.SaveButton.Execute();
        }

        private void Checker_CheckedChanged(object sender, EventArgs e)
        {
            this.CheckedChanged.Execute();
        }

        private void inputTextBox_TextChanged(object sender, EventArgs e)
        {
            this.inputTextBoxTextChanged.Execute();
        }
        #endregion

        #region Commands
        private void SaveImage()
        {
            SaveFileDialog fileDialog = new SaveFileDialog();
            fileDialog.Filter = "Bitmap файл(*.bmp)|*.bmp|" + "JPEG файл(*.jpg)|*.jpg|" + "GIF файл(*.gif)|*.gif|" + "PNG файл(*.png)|*.png|" + "TIF файл(*.tif)|*.tif";

            if (fileDialog.ShowDialog() == DialogResult.OK)
            {
                this.steganographer.SaveImage(fileDialog.FileName);
            }
        }

        private void OpenImage()
        {
            OpenFileDialog fileDialog = new OpenFileDialog();
            fileDialog.Filter = "Bitmap файл(*.bmp)|*.bmp|" + "JPEG файл(*.jpg)|*.jpg|" + "GIF файл(*.gif)|*.gif|" + "PNG файл(*.png)|*.png|" + "TIF файл(*.tif)|*.tif";
            if (fileDialog.ShowDialog() == DialogResult.OK)
            {
                this.baseImagePath = fileDialog.FileName;
                this.steganographer.SetNewImage(fileDialog.FileName);
                this.pictureBox.Image = this.steganographer.Bitmap;
                this.inputTextBoxTextChanged.Execute();
            }
        }

        private void EncryptText()
        {
            this.StartTimer();
            var textToEncrypt = this.inputTextBox.Text;
            if (textToEncrypt.Length > 0)
            {
                this.steganographer.SetNewImage(this.steganographer.EncryptText(textToEncrypt));
                this.pictureBox.Image = this.steganographer.Bitmap;
            }
            this.EndTimer();
        }

        private void DecryptText()
        {
            this.StartTimer();
            this.outputTextBox.Text = this.steganographer.DecryptText(this.baseImagePath);
            this.EndTimer();
        }

        private void AlgorithmChecked()
        {
            if (this.lsbChecker.Checked)
            {
                this.steganographer.SetLSBAlgorithm();
            }
            else if (this.simpleChecker.Checked)
            {
                this.steganographer.SetSimpleAlgorithm();
            }
            this.CalculateFreeAndUsedSpace();
        }

        private void ClearUI()
        {
            this.inputTextBox.Text = "";
            this.outputTextBox.Text = "";
            this.spaceLabel.Text = "0/0 ";
        }

        private void CalculateFreeAndUsedSpace()
        {
            var free = this.steganographer.GetFreeSpace();
            this.spaceLabel.Text = free + "/" + this.inputTextBox.Text.Length;
        }
        #endregion

        #region TimerFunc
        private void StartTimer()
        {
            this.timer = new Stopwatch();
            this.encoderTime.Text = "Encoder time: "; 
            this.timer.Start();
        }

        private void EndTimer()
        {
            this.timer.Stop();
            var timeSpan = this.timer.Elapsed;
            this.encoderTime.Text += timeSpan.ToString();
        }

        #endregion
    }
}
