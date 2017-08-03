namespace RoomChattingClient
{
    partial class ChatForm
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
            this.showTextBox = new System.Windows.Forms.RichTextBox();
            this.chatSendButton = new System.Windows.Forms.Button();
            this.inputTextBox = new System.Windows.Forms.TextBox();
            this.SuspendLayout();
            // 
            // showTextBox
            // 
            this.showTextBox.BackColor = System.Drawing.SystemColors.InactiveCaptionText;
            this.showTextBox.Font = new System.Drawing.Font("맑은 고딕", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.showTextBox.ForeColor = System.Drawing.Color.White;
            this.showTextBox.Location = new System.Drawing.Point(12, 12);
            this.showTextBox.Name = "showTextBox";
            this.showTextBox.ReadOnly = true;
            this.showTextBox.Size = new System.Drawing.Size(1200, 810);
            this.showTextBox.TabIndex = 0;
            this.showTextBox.Text = "";
            // 
            // chatSendButton
            // 
            this.chatSendButton.BackColor = System.Drawing.SystemColors.ButtonHighlight;
            this.chatSendButton.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.chatSendButton.Font = new System.Drawing.Font("굴림", 15F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.chatSendButton.ForeColor = System.Drawing.SystemColors.ActiveCaptionText;
            this.chatSendButton.Location = new System.Drawing.Point(1083, 829);
            this.chatSendButton.Name = "chatSendButton";
            this.chatSendButton.Size = new System.Drawing.Size(129, 67);
            this.chatSendButton.TabIndex = 1;
            this.chatSendButton.Text = "전 송";
            this.chatSendButton.UseVisualStyleBackColor = false;
            this.chatSendButton.Click += new System.EventHandler(this.button1_Click);
            // 
            // inputTextBox
            // 
            this.inputTextBox.BackColor = System.Drawing.Color.White;
            this.inputTextBox.Location = new System.Drawing.Point(13, 829);
            this.inputTextBox.Multiline = true;
            this.inputTextBox.Name = "inputTextBox";
            this.inputTextBox.Size = new System.Drawing.Size(1064, 67);
            this.inputTextBox.TabIndex = 2;
            this.inputTextBox.KeyUp += new System.Windows.Forms.KeyEventHandler(this.textBox2_KeyUp);
            // 
            // ChatForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.SystemColors.ActiveCaptionText;
            this.ClientSize = new System.Drawing.Size(1224, 908);
            this.Controls.Add(this.inputTextBox);
            this.Controls.Add(this.chatSendButton);
            this.Controls.Add(this.showTextBox);
            this.Name = "ChatForm";
            this.Text = "Chat";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.ChatForm_FormClosing);
            this.Shown += new System.EventHandler(this.MainForm_Loaded);
            this.SizeChanged += new System.EventHandler(this.ChatForm_SizeChanged);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.RichTextBox showTextBox;
        private System.Windows.Forms.Button chatSendButton;
        private System.Windows.Forms.TextBox inputTextBox;
    }
}