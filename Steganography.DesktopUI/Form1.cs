using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using BLL;
using BLL.AudioEncoders;
using BLL.Interfaces;
using BLL.Models;

namespace Steganography.DesktopUI
{
    public partial class Form1 : Form
    {
        private Stopwatch timer;

        private string baseFilePath;
        private string dataFilePath;
        private byte[] decryptedResult;
        private byte[] dataFileBytes;

        private ISteganographer steganographer;

        private ICommand OpenBaseFileButton;
        private ICommand OpenDataFileButton;

        private ICommand SaveEncryptedFileButton;
        private ICommand SaveDecryptedFileButton;

        private ICommand EncryptButton;
        private ICommand DecryptButton;

        private ICommand CheckedChanged;
        private ICommand ClearAll;

        public Form1()
        {
            InitializeComponent();

            ConfigureSteganographer(false);

            this.OpenBaseFileButton = new Command(this.OpenOriginalFile);
            this.OpenDataFileButton = new Command(this.OpenDataFile);

            this.SaveEncryptedFileButton = new Command(this.SaveEncryptedResultIntoFile);
            this.SaveDecryptedFileButton = new Command(this.SaveDecryptedResultIntoFile);

            this.EncryptButton = new Command(this.EncryptText);
            this.DecryptButton = new Command(this.DecryptText);
            this.CheckedChanged = new Command(this.AlgorithmChecked);
            this.ClearAll = new Command(this.ClearUI);
        }

        #region EventHandlers
        private void openButton_Click(object sender, EventArgs e)
        {
            this.ClearAll.Execute();
            this.OpenBaseFileButton.Execute();
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
            this.SaveEncryptedFileButton.Execute();
        }

        private void Checker_CheckedChanged(object sender, EventArgs e)
        {
            this.CheckedChanged.Execute();
        }

        private void openDataFileButton_Click(object sender, EventArgs e)
        {
            this.OpenDataFileButton.Execute();
        }

        private void saveDecryptedFileButton_Click(object sender, EventArgs e)
        {
            this.SaveDecryptedFileButton.Execute();
        }
        #endregion

        #region Commands
        private void SaveEncryptedResultIntoFile()
        {
            SaveFileDialog fileDialog = new SaveFileDialog();
            fileDialog.Filter = "Bitmap файл(*.bmp)|*.bmp|" + "JPEG файл(*.jpg)|*.jpg|" + "GIF файл(*.gif)|*.gif|" + "PNG файл(*.png)|*.png|" + "TIF файл(*.tif)|*.tif";

            if (fileDialog.ShowDialog() == DialogResult.OK)
            {
                this.steganographer.SaveInFile(fileDialog.FileName);
                this.console.Text += "Saved encrypted result in " + fileDialog.FileName + System.Environment.NewLine;
            }
        }

        private void SaveDecryptedResultIntoFile()
        {
            SaveFileDialog fileDialog = new SaveFileDialog();
            fileDialog.Filter = "All files(*.*)|*.*";

            if (fileDialog.ShowDialog() == DialogResult.OK)
            {
                File.WriteAllBytes(fileDialog.FileName, decryptedResult);
                this.console.Text += "Saved decrypted result in " + fileDialog.FileName + System.Environment.NewLine;
            }
        }

        private void OpenOriginalFile()
        {
            OpenFileDialog fileDialog = new OpenFileDialog();
            fileDialog.Filter = "Bitmap файл(*.bmp)|*.bmp|" + "JPEG файл(*.jpg)|*.jpg|" + "GIF файл(*.gif)|*.gif|" + "PNG файл(*.png)|*.png|" + "TIF файл(*.tif)|*.tif|" + "WAV файл(*.wav)|*.wav";
            if (fileDialog.ShowDialog() == DialogResult.OK)
            {
                this.baseFilePath = fileDialog.FileName;
                this.console.Text += "File selected: "+ this.baseFilePath + System.Environment.NewLine;
                if (!fileDialog.FileName.EndsWith(".wav"))
                {
                    ConfigureSteganographer(false);
                    this.steganographer.SetNewSource(fileDialog.FileName);
                }
                else
                {
                    ConfigureSteganographer(true);
                    this.steganographer.SetNewSource(fileDialog.FileName);
                    //var EncryptResult = this.steganographer.Encrypt("HELLO WORLD! HELLO WORLD! HELLO WORLD! HELLO WORLD! HELLO WORLD! HELLO WORLD! HELLO WORLD! HELLO WORLD! HELLO WORLD! HELLO WORLD! HELLO WORLD! HELLO WORLD! HELLO WORLD! HELLO WORLD! HELLO WORLD! HELLO WORLD! HELLO WORLD! ");
                    //var DecryptResult = this.steganographer.Decrypt();
                    
                    //SaveFileDialog saveFileDialog = new SaveFileDialog();
                    //saveFileDialog.Filter = "WAV файл(*.wav)|*.wav";

                    //if (saveFileDialog.ShowDialog() == DialogResult.OK)
                    //{
                    //    this.steganographer.SaveInFile(saveFileDialog.FileName);
                    //}
                }
                this.CalculateFreeAndUsedSpace();
                this.CheckDataAndOriginalFilesFreeSpace();
            }
        }

        private void OpenDataFile()
        {
            OpenFileDialog fileDialog = new OpenFileDialog();
            fileDialog.Filter = "All files(*.*)|*.*";
            if (fileDialog.ShowDialog() == DialogResult.OK)
            {
                this.dataFilePath = fileDialog.FileName;
                this.dataFileBytes = File.ReadAllBytes(dataFilePath);
                this.console.Text += "Data file selected: " + this.dataFilePath + System.Environment.NewLine;
                this.CheckDataAndOriginalFilesFreeSpace();
            }
        }

        private void EncryptText()
        {
            this.StartTimer();
            var inputBytes = File.ReadAllBytes(dataFilePath);
            this.steganographer.Encrypt(inputBytes);
            this.console.Text += "Encrypt time: ";
            this.EndTimer();
        }

        private void DecryptText()
        {
            this.StartTimer();
            this.decryptedResult = this.steganographer.Decrypt(this.baseFilePath);
            this.console.Text += "Decrypt time: ";
            this.EndTimer();
        }

        private void AlgorithmChecked()
        {
            if (this.lsbChecker.Checked)
            {
                if (this.steganographer.SetLSBAlgorithm())
                {
                    this.console.Text += "Steganographer algorithm selected: LSB" + System.Environment.NewLine;
                    this.CalculateFreeAndUsedSpace();
                }
            }
            else if (this.simpleChecker.Checked)
            {
                if (this.steganographer.SetSimpleAlgorithm())
                {
                    this.console.Text += "Steganographer algorithm selected: Simple" + System.Environment.NewLine;
                    this.CalculateFreeAndUsedSpace();
                }
            }
        }

        private void ClearUI()
        {
            this.console.Text = "";
            this.simpleChecker.Checked = false;
            this.lsbChecker.Checked = true;
            this.baseFilePath = "";
            this.dataFilePath = "";
            this.decryptedResult = null;
    }

        private void CalculateFreeAndUsedSpace()
        {
            var free = this.steganographer.GetFreeSpace();
            this.console.Text += "Free space: " + free + System.Environment.NewLine;
        }
        #endregion

        #region TimerFunc
        private void StartTimer()
        {
            this.timer = new Stopwatch();
            this.timer.Start();
        }

        private void EndTimer()
        {
            this.timer.Stop();
            var timeSpan = this.timer.Elapsed;
            this.console.Text += timeSpan.ToString() + System.Environment.NewLine;
        }

        #endregion

        private void ConfigureSteganographer(bool isAudio)
        {
            if (isAudio)
            {
                this.steganographer = new AudioSteganographer();
            }
            else
            {
                this.steganographer = new ImageSteganographer();
            }

            this.steganographer.SetLSBAlgorithm();
        }

        private void CheckDataAndOriginalFilesFreeSpace()
        {
            if (this.dataFilePath != "" && this.baseFilePath != "")
            {
                var baseFreeSpace = this.steganographer.GetFreeSpace();
                if (baseFreeSpace < this.dataFileBytes.Length)
                {
                    this.console.Text += "YOU CAN\'T ENCRYPT, free space is lower" + System.Environment.NewLine;
                }

            }
        }

    }
}
