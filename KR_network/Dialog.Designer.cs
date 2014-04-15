namespace KR_network
{
    partial class Dialog
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
            this.richTextBox1 = new System.Windows.Forms.RichTextBox();
            this.exitBtn = new System.Windows.Forms.Button();
            this.sendBtn = new System.Windows.Forms.Button();
            this.messages = new System.Windows.Forms.ListBox();
            this.info = new System.Windows.Forms.Label();
            this.info_text = new System.Windows.Forms.TextBox();
            this.SuspendLayout();
            // 
            // richTextBox1
            // 
            this.richTextBox1.Location = new System.Drawing.Point(14, 260);
            this.richTextBox1.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.richTextBox1.MaxLength = 255;
            this.richTextBox1.Name = "richTextBox1";
            this.richTextBox1.Size = new System.Drawing.Size(347, 84);
            this.richTextBox1.TabIndex = 0;
            this.richTextBox1.Text = "";
            this.richTextBox1.KeyDown += new System.Windows.Forms.KeyEventHandler(this.keyDown);
            // 
            // exitBtn
            // 
            this.exitBtn.Location = new System.Drawing.Point(14, 410);
            this.exitBtn.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.exitBtn.Name = "exitBtn";
            this.exitBtn.Size = new System.Drawing.Size(145, 88);
            this.exitBtn.TabIndex = 4;
            this.exitBtn.Text = "Выйти";
            this.exitBtn.UseVisualStyleBackColor = true;
            this.exitBtn.Click += new System.EventHandler(this.exitBtn_Click);
            // 
            // sendBtn
            // 
            this.sendBtn.Location = new System.Drawing.Point(165, 410);
            this.sendBtn.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.sendBtn.Name = "sendBtn";
            this.sendBtn.Size = new System.Drawing.Size(196, 88);
            this.sendBtn.TabIndex = 3;
            this.sendBtn.Text = "Отправить";
            this.sendBtn.UseVisualStyleBackColor = true;
            this.sendBtn.Click += new System.EventHandler(this.sendBtn_Click);
            // 
            // messages
            // 
            this.messages.FormattingEnabled = true;
            this.messages.ItemHeight = 20;
            this.messages.Location = new System.Drawing.Point(15, 16);
            this.messages.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.messages.Name = "messages";
            this.messages.Size = new System.Drawing.Size(346, 224);
            this.messages.TabIndex = 5;
            // 
            // info
            // 
            this.info.AutoSize = true;
            this.info.Location = new System.Drawing.Point(10, 361);
            this.info.Name = "info";
            this.info.Size = new System.Drawing.Size(91, 20);
            this.info.TabIndex = 6;
            this.info.Text = "Состояние";
            // 
            // info_text
            // 
            this.info_text.Enabled = false;
            this.info_text.Location = new System.Drawing.Point(107, 361);
            this.info_text.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.info_text.Name = "info_text";
            this.info_text.Size = new System.Drawing.Size(252, 26);
            this.info_text.TabIndex = 7;
            // 
            // Dialog
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(9F, 20F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(372, 512);
            this.Controls.Add(this.info_text);
            this.Controls.Add(this.info);
            this.Controls.Add(this.messages);
            this.Controls.Add(this.exitBtn);
            this.Controls.Add(this.sendBtn);
            this.Controls.Add(this.richTextBox1);
            this.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.Name = "Dialog";
            this.Text = "Диалог";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        public System.Windows.Forms.RichTextBox richTextBox1;
        public System.Windows.Forms.Button exitBtn;
        public System.Windows.Forms.Button sendBtn;
        public System.Windows.Forms.ListBox messages;
        public System.Windows.Forms.Label info;
        public System.Windows.Forms.TextBox info_text;
    }
}