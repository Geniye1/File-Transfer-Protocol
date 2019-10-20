namespace Client
{
    partial class Client_Form
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
            this.ConnectButton = new System.Windows.Forms.Button();
            this.OutputDialog = new System.Windows.Forms.RichTextBox();
            this.MessageDialog = new System.Windows.Forms.RichTextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.TextRadio = new System.Windows.Forms.RadioButton();
            this.ImageRadio = new System.Windows.Forms.RadioButton();
            this.ChooseFile = new System.Windows.Forms.Button();
            this.openFileDialog1 = new System.Windows.Forms.OpenFileDialog();
            this.singleFileRadio = new System.Windows.Forms.RadioButton();
            this.singleFileChooser = new System.Windows.Forms.Button();
            this.folderBrowserDialog1 = new System.Windows.Forms.FolderBrowserDialog();
            this.folderRadio = new System.Windows.Forms.RadioButton();
            this.folderChooserButton = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // ConnectButton
            // 
            this.ConnectButton.Location = new System.Drawing.Point(467, 37);
            this.ConnectButton.Name = "ConnectButton";
            this.ConnectButton.Size = new System.Drawing.Size(220, 51);
            this.ConnectButton.TabIndex = 0;
            this.ConnectButton.Text = "Send Message";
            this.ConnectButton.UseVisualStyleBackColor = true;
            this.ConnectButton.Click += new System.EventHandler(this.ConnectButton_Click);
            // 
            // OutputDialog
            // 
            this.OutputDialog.Font = new System.Drawing.Font("Arial", 15.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.OutputDialog.Location = new System.Drawing.Point(170, 215);
            this.OutputDialog.Name = "OutputDialog";
            this.OutputDialog.Size = new System.Drawing.Size(832, 500);
            this.OutputDialog.TabIndex = 1;
            this.OutputDialog.Text = "";
            // 
            // MessageDialog
            // 
            this.MessageDialog.Font = new System.Drawing.Font("Arial", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.MessageDialog.Location = new System.Drawing.Point(368, 125);
            this.MessageDialog.Name = "MessageDialog";
            this.MessageDialog.Size = new System.Drawing.Size(430, 84);
            this.MessageDialog.TabIndex = 2;
            this.MessageDialog.Text = "";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 20F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.ForeColor = System.Drawing.SystemColors.InfoText;
            this.label1.Location = new System.Drawing.Point(521, 91);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(124, 31);
            this.label1.TabIndex = 3;
            this.label1.Text = "Message";
            this.label1.Click += new System.EventHandler(this.label1_Click);
            // 
            // TextRadio
            // 
            this.TextRadio.AutoSize = true;
            this.TextRadio.Location = new System.Drawing.Point(828, 125);
            this.TextRadio.Name = "TextRadio";
            this.TextRadio.Size = new System.Drawing.Size(91, 17);
            this.TextRadio.TabIndex = 4;
            this.TextRadio.TabStop = true;
            this.TextRadio.Text = "Text message";
            this.TextRadio.UseVisualStyleBackColor = true;
            // 
            // ImageRadio
            // 
            this.ImageRadio.AutoSize = true;
            this.ImageRadio.Location = new System.Drawing.Point(828, 171);
            this.ImageRadio.Name = "ImageRadio";
            this.ImageRadio.Size = new System.Drawing.Size(99, 17);
            this.ImageRadio.TabIndex = 5;
            this.ImageRadio.TabStop = true;
            this.ImageRadio.Text = "Image message";
            this.ImageRadio.UseVisualStyleBackColor = true;
            // 
            // ChooseFile
            // 
            this.ChooseFile.Location = new System.Drawing.Point(927, 165);
            this.ChooseFile.Name = "ChooseFile";
            this.ChooseFile.Size = new System.Drawing.Size(75, 23);
            this.ChooseFile.TabIndex = 6;
            this.ChooseFile.Text = "Choose File";
            this.ChooseFile.UseVisualStyleBackColor = true;
            this.ChooseFile.Click += new System.EventHandler(this.ChooseFile_Click);
            // 
            // openFileDialog1
            // 
            this.openFileDialog1.FileName = "openFileDialog1";
            // 
            // singleFileRadio
            // 
            this.singleFileRadio.AutoSize = true;
            this.singleFileRadio.Location = new System.Drawing.Point(828, 148);
            this.singleFileRadio.Name = "singleFileRadio";
            this.singleFileRadio.Size = new System.Drawing.Size(70, 17);
            this.singleFileRadio.TabIndex = 7;
            this.singleFileRadio.TabStop = true;
            this.singleFileRadio.Text = "Single file";
            this.singleFileRadio.UseVisualStyleBackColor = true;
            this.singleFileRadio.CheckedChanged += new System.EventHandler(this.singleFileRadio_CheckedChanged);
            // 
            // singleFileChooser
            // 
            this.singleFileChooser.Location = new System.Drawing.Point(927, 145);
            this.singleFileChooser.Name = "singleFileChooser";
            this.singleFileChooser.Size = new System.Drawing.Size(75, 23);
            this.singleFileChooser.TabIndex = 8;
            this.singleFileChooser.Text = "Choose File";
            this.singleFileChooser.UseVisualStyleBackColor = true;
            this.singleFileChooser.Click += new System.EventHandler(this.singleFileChooser_Click);
            // 
            // folderRadio
            // 
            this.folderRadio.AutoSize = true;
            this.folderRadio.Location = new System.Drawing.Point(828, 191);
            this.folderRadio.Name = "folderRadio";
            this.folderRadio.Size = new System.Drawing.Size(57, 17);
            this.folderRadio.TabIndex = 9;
            this.folderRadio.TabStop = true;
            this.folderRadio.Text = "Folder ";
            this.folderRadio.UseVisualStyleBackColor = true;
            // 
            // folderChooserButton
            // 
            this.folderChooserButton.Location = new System.Drawing.Point(927, 185);
            this.folderChooserButton.Name = "folderChooserButton";
            this.folderChooserButton.Size = new System.Drawing.Size(75, 23);
            this.folderChooserButton.TabIndex = 10;
            this.folderChooserButton.Text = "Choose File";
            this.folderChooserButton.UseVisualStyleBackColor = true;
            this.folderChooserButton.Click += new System.EventHandler(this.folderChooserButton_Click);
            // 
            // Client_Form
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.SystemColors.WindowFrame;
            this.ClientSize = new System.Drawing.Size(1149, 752);
            this.Controls.Add(this.folderChooserButton);
            this.Controls.Add(this.folderRadio);
            this.Controls.Add(this.singleFileChooser);
            this.Controls.Add(this.singleFileRadio);
            this.Controls.Add(this.ChooseFile);
            this.Controls.Add(this.ImageRadio);
            this.Controls.Add(this.TextRadio);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.MessageDialog);
            this.Controls.Add(this.OutputDialog);
            this.Controls.Add(this.ConnectButton);
            this.Name = "Client_Form";
            this.Text = "Form1";
            this.Load += new System.EventHandler(this.Client_Form_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button ConnectButton;
        private System.Windows.Forms.RichTextBox OutputDialog;
        private System.Windows.Forms.RichTextBox MessageDialog;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.RadioButton TextRadio;
        private System.Windows.Forms.RadioButton ImageRadio;
        private System.Windows.Forms.Button ChooseFile;
        private System.Windows.Forms.OpenFileDialog openFileDialog1;
        private System.Windows.Forms.RadioButton singleFileRadio;
        private System.Windows.Forms.Button singleFileChooser;
        private System.Windows.Forms.FolderBrowserDialog folderBrowserDialog1;
        private System.Windows.Forms.RadioButton folderRadio;
        private System.Windows.Forms.Button folderChooserButton;
    }
}

