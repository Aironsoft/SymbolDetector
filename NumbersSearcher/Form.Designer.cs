namespace NumbersSearcher
{
    partial class Form
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
            this.openFileDialog = new System.Windows.Forms.OpenFileDialog();
            this.Result = new System.Windows.Forms.TextBox();
            this.Preview = new System.Windows.Forms.PictureBox();
            this.button1 = new System.Windows.Forms.Button();
            this.button2 = new System.Windows.Forms.Button();
            this.button3 = new System.Windows.Forms.Button();
            this.btTrain = new System.Windows.Forms.Button();
            this.folderBrowserDialog = new System.Windows.Forms.FolderBrowserDialog();
            this.rtbReaction = new System.Windows.Forms.RichTextBox();
            this.bgwTraining = new System.ComponentModel.BackgroundWorker();
            this.btBreakTrain = new System.Windows.Forms.Button();
            this.tbCorrectionCounts = new System.Windows.Forms.TextBox();
            this.lbCorrections = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)(this.Preview)).BeginInit();
            this.SuspendLayout();
            // 
            // openFileDialog
            // 
            this.openFileDialog.Title = "Выбрать тестируемую картинку";
            // 
            // Result
            // 
            this.Result.Location = new System.Drawing.Point(4, 4);
            this.Result.Name = "Result";
            this.Result.Size = new System.Drawing.Size(156, 20);
            this.Result.TabIndex = 2;
            // 
            // Preview
            // 
            this.Preview.InitialImage = null;
            this.Preview.Location = new System.Drawing.Point(0, 56);
            this.Preview.Name = "Preview";
            this.Preview.Size = new System.Drawing.Size(320, 240);
            this.Preview.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.Preview.TabIndex = 0;
            this.Preview.TabStop = false;
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(4, 26);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(75, 23);
            this.button1.TabIndex = 3;
            this.button1.Text = "Открыть";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.открытьToolStripMenuItem_Click);
            // 
            // button2
            // 
            this.button2.Location = new System.Drawing.Point(85, 26);
            this.button2.Name = "button2";
            this.button2.Size = new System.Drawing.Size(75, 23);
            this.button2.TabIndex = 4;
            this.button2.Text = "Распознать";
            this.button2.UseVisualStyleBackColor = true;
            this.button2.Click += new System.EventHandler(this.распознатьToolStripMenuItem_Click);
            // 
            // button3
            // 
            this.button3.Location = new System.Drawing.Point(162, 26);
            this.button3.Name = "button3";
            this.button3.Size = new System.Drawing.Size(75, 23);
            this.button3.TabIndex = 5;
            this.button3.Text = "Исправить";
            this.button3.UseVisualStyleBackColor = true;
            this.button3.Click += new System.EventHandler(this.исправитьToolStripMenuItem_Click);
            // 
            // btTrain
            // 
            this.btTrain.Location = new System.Drawing.Point(162, 2);
            this.btTrain.Name = "btTrain";
            this.btTrain.Size = new System.Drawing.Size(75, 23);
            this.btTrain.TabIndex = 6;
            this.btTrain.Text = "Обучить";
            this.btTrain.UseVisualStyleBackColor = true;
            this.btTrain.Click += new System.EventHandler(this.btTrain_Click);
            // 
            // folderBrowserDialog
            // 
            this.folderBrowserDialog.Description = "Поиск папки с обучающими картинками";
            // 
            // rtbReaction
            // 
            this.rtbReaction.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.rtbReaction.Location = new System.Drawing.Point(326, 2);
            this.rtbReaction.Name = "rtbReaction";
            this.rtbReaction.ReadOnly = true;
            this.rtbReaction.Size = new System.Drawing.Size(246, 359);
            this.rtbReaction.TabIndex = 7;
            this.rtbReaction.Text = "";
            // 
            // bgwTraining
            // 
            this.bgwTraining.DoWork += new System.ComponentModel.DoWorkEventHandler(this.bgwTraining_DoWork);
            // 
            // btBreakTrain
            // 
            this.btBreakTrain.Location = new System.Drawing.Point(243, 2);
            this.btBreakTrain.Name = "btBreakTrain";
            this.btBreakTrain.Size = new System.Drawing.Size(75, 34);
            this.btBreakTrain.TabIndex = 8;
            this.btBreakTrain.Text = "Прервать обучение";
            this.btBreakTrain.UseVisualStyleBackColor = true;
            this.btBreakTrain.Click += new System.EventHandler(this.btBreakTrain_Click);
            // 
            // tbCorrectionCounts
            // 
            this.tbCorrectionCounts.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left)));
            this.tbCorrectionCounts.Location = new System.Drawing.Point(4, 314);
            this.tbCorrectionCounts.Multiline = true;
            this.tbCorrectionCounts.Name = "tbCorrectionCounts";
            this.tbCorrectionCounts.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.tbCorrectionCounts.Size = new System.Drawing.Size(316, 47);
            this.tbCorrectionCounts.TabIndex = 9;
            // 
            // lbCorrections
            // 
            this.lbCorrections.AutoSize = true;
            this.lbCorrections.ForeColor = System.Drawing.SystemColors.ButtonHighlight;
            this.lbCorrections.Location = new System.Drawing.Point(12, 298);
            this.lbCorrections.Name = "lbCorrections";
            this.lbCorrections.Size = new System.Drawing.Size(174, 13);
            this.lbCorrections.TabIndex = 10;
            this.lbCorrections.Text = "Исправлений за заход обучения:";
            // 
            // Form
            // 
            this.AllowDrop = true;
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            this.ClientSize = new System.Drawing.Size(574, 363);
            this.Controls.Add(this.lbCorrections);
            this.Controls.Add(this.tbCorrectionCounts);
            this.Controls.Add(this.btBreakTrain);
            this.Controls.Add(this.rtbReaction);
            this.Controls.Add(this.btTrain);
            this.Controls.Add(this.button3);
            this.Controls.Add(this.button2);
            this.Controls.Add(this.button1);
            this.Controls.Add(this.Result);
            this.Controls.Add(this.Preview);
            this.MinimumSize = new System.Drawing.Size(580, 402);
            this.Name = "Form";
            this.Text = "SymbolDetector";
            this.DragDrop += new System.Windows.Forms.DragEventHandler(this.Form1_DragDrop);
            this.DragEnter += new System.Windows.Forms.DragEventHandler(this.Form1_DragEnter);
            ((System.ComponentModel.ISupportInitialize)(this.Preview)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.PictureBox Preview;
        private System.Windows.Forms.OpenFileDialog openFileDialog;
        private System.Windows.Forms.TextBox Result;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.Button button2;
        private System.Windows.Forms.Button button3;
        private System.Windows.Forms.Button btTrain;
        private System.Windows.Forms.FolderBrowserDialog folderBrowserDialog;
        private System.Windows.Forms.RichTextBox rtbReaction;
        private System.ComponentModel.BackgroundWorker bgwTraining;
        private System.Windows.Forms.Button btBreakTrain;
        private System.Windows.Forms.TextBox tbCorrectionCounts;
        private System.Windows.Forms.Label lbCorrections;
    }
}

