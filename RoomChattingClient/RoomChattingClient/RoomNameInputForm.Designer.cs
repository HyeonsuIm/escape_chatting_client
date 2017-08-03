namespace RoomChattingClient
{
    partial class RoomNameInputForm
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
            this.label1 = new System.Windows.Forms.Label();
            this.textBox1 = new System.Windows.Forms.TextBox();
            this.button = new System.Windows.Forms.Button();
            this.ipAddressInput = new IPAddressControlLib.IPAddressControl();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(43, 29);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(125, 12);
            this.label1.TabIndex = 0;
            this.label1.Text = "방 이름을 입력하세요.";
            // 
            // textBox1
            // 
            this.textBox1.Location = new System.Drawing.Point(45, 59);
            this.textBox1.Name = "textBox1";
            this.textBox1.Size = new System.Drawing.Size(170, 21);
            this.textBox1.TabIndex = 1;
            this.textBox1.KeyDown += new System.Windows.Forms.KeyEventHandler(this.textBox1_KeyDown);
            // 
            // button
            // 
            this.button.Location = new System.Drawing.Point(140, 148);
            this.button.Name = "button";
            this.button.Size = new System.Drawing.Size(75, 23);
            this.button.TabIndex = 3;
            this.button.Text = "확 인";
            this.button.UseVisualStyleBackColor = true;
            this.button.Click += new System.EventHandler(this.button_Click);
            // 
            // ipAddressInput
            // 
            this.ipAddressInput.AllowInternalTab = false;
            this.ipAddressInput.AutoHeight = true;
            this.ipAddressInput.BackColor = System.Drawing.SystemColors.Window;
            this.ipAddressInput.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.ipAddressInput.Cursor = System.Windows.Forms.Cursors.IBeam;
            this.ipAddressInput.Location = new System.Drawing.Point(45, 104);
            this.ipAddressInput.Name = "ipAddressInput";
            this.ipAddressInput.ReadOnly = false;
            this.ipAddressInput.Size = new System.Drawing.Size(170, 19);
            this.ipAddressInput.TabIndex = 2;
            this.ipAddressInput.Text = "...";
            // 
            // RoomNameInputForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(272, 198);
            this.Controls.Add(this.button);
            this.Controls.Add(this.textBox1);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.ipAddressInput);
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "RoomNameInputForm";
            this.Text = "RoomNameInputForm";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox textBox1;
        private System.Windows.Forms.Button button;
        private IPAddressControlLib.IPAddressControl ipAddressInput;
    }
}