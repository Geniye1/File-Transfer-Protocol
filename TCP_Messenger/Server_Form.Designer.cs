namespace TCP_Messenger
{
    partial class Server_Form
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
            this.components = new System.ComponentModel.Container();
            this.BeginButton = new System.Windows.Forms.Button();
            this.OutputDialog = new System.Windows.Forms.RichTextBox();
            this.pathText = new System.Windows.Forms.RichTextBox();
            this.contextMenuStrip1 = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.pathBtn = new System.Windows.Forms.Button();
            this.folderBrowserDialog1 = new System.Windows.Forms.FolderBrowserDialog();
            this.SuspendLayout();
            // 
            // BeginButton
            // 
            this.BeginButton.Location = new System.Drawing.Point(269, 80);
            this.BeginButton.Name = "BeginButton";
            this.BeginButton.Size = new System.Drawing.Size(197, 29);
            this.BeginButton.TabIndex = 0;
            this.BeginButton.Text = "Begin TCP Server";
            this.BeginButton.UseVisualStyleBackColor = true;
            this.BeginButton.Click += new System.EventHandler(this.BeginButton_Click);
            // 
            // OutputDialog
            // 
            this.OutputDialog.Font = new System.Drawing.Font("Arial", 15.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.OutputDialog.Location = new System.Drawing.Point(269, 130);
            this.OutputDialog.Name = "OutputDialog";
            this.OutputDialog.Size = new System.Drawing.Size(668, 456);
            this.OutputDialog.TabIndex = 1;
            this.OutputDialog.Text = "";
            // 
            // pathText
            // 
            this.pathText.Location = new System.Drawing.Point(740, 80);
            this.pathText.Name = "pathText";
            this.pathText.ReadOnly = true;
            this.pathText.Size = new System.Drawing.Size(197, 26);
            this.pathText.TabIndex = 2;
            this.pathText.Text = "";
            // 
            // contextMenuStrip1
            // 
            this.contextMenuStrip1.Name = "contextMenuStrip1";
            this.contextMenuStrip1.Size = new System.Drawing.Size(61, 4);
            // 
            // pathBtn
            // 
            this.pathBtn.Location = new System.Drawing.Point(642, 80);
            this.pathBtn.Name = "pathBtn";
            this.pathBtn.Size = new System.Drawing.Size(92, 29);
            this.pathBtn.TabIndex = 4;
            this.pathBtn.Text = "Path";
            this.pathBtn.UseVisualStyleBackColor = true;
            this.pathBtn.Click += new System.EventHandler(this.pathbtn_Click);
            // 
            // Server_Form
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.SystemColors.WindowFrame;
            this.ClientSize = new System.Drawing.Size(1148, 700);
            this.Controls.Add(this.pathBtn);
            this.Controls.Add(this.pathText);
            this.Controls.Add(this.OutputDialog);
            this.Controls.Add(this.BeginButton);
            this.Name = "Server_Form";
            this.Text = "Form1";
            this.Load += new System.EventHandler(this.Server_Form_Load);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Button BeginButton;
        private System.Windows.Forms.RichTextBox OutputDialog;
        private System.Windows.Forms.RichTextBox pathText;
        private System.Windows.Forms.ContextMenuStrip contextMenuStrip1;
        private System.Windows.Forms.Button pathBtn;
        private System.Windows.Forms.FolderBrowserDialog folderBrowserDialog1;
    }
}

