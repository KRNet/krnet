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
            this.info = new System.Windows.Forms.Label();
            this.info_text = new System.Windows.Forms.TextBox();
            this.messages = new System.Windows.Forms.RichTextBox();
            this.SuspendLayout();
            // 
            // richTextBox1
            // 
            this.richTextBox1.Location = new System.Drawing.Point(12, 208);
            this.richTextBox1.MaxLength = 110;
            this.richTextBox1.Name = "richTextBox1";
            this.richTextBox1.Size = new System.Drawing.Size(401, 68);
            this.richTextBox1.TabIndex = 0;
            this.richTextBox1.Text = "";
            this.richTextBox1.KeyDown += new System.Windows.Forms.KeyEventHandler(this.keyDown);
            // 
            // exitBtn
            // 
            this.exitBtn.Location = new System.Drawing.Point(12, 282);
            this.exitBtn.Name = "exitBtn";
            this.exitBtn.Size = new System.Drawing.Size(129, 70);
            this.exitBtn.TabIndex = 4;
            this.exitBtn.Text = "Выйти";
            this.exitBtn.UseVisualStyleBackColor = true;
            this.exitBtn.Click += new System.EventHandler(this.exitBtn_Click);
            // 
            // sendBtn
            // 
            this.sendBtn.Location = new System.Drawing.Point(419, 208);
            this.sendBtn.Name = "sendBtn";
            this.sendBtn.Size = new System.Drawing.Size(174, 70);
            this.sendBtn.TabIndex = 3;
            this.sendBtn.Text = "Отправить";
            this.sendBtn.UseVisualStyleBackColor = true;
            this.sendBtn.Click += new System.EventHandler(this.sendBtn_Click);
            // 
            // info
            // 
            this.info.AutoSize = true;
            this.info.Location = new System.Drawing.Point(284, 309);
            this.info.Name = "info";
            this.info.Size = new System.Drawing.Size(79, 17);
            this.info.TabIndex = 6;
            this.info.Text = "Состояние";
            // 
            // info_text
            // 
            this.info_text.Enabled = false;
            this.info_text.Location = new System.Drawing.Point(369, 306);
            this.info_text.Name = "info_text";
            this.info_text.Size = new System.Drawing.Size(224, 22);
            this.info_text.TabIndex = 7;
            // 
            // messages
            // 
            this.messages.Location = new System.Drawing.Point(12, 12);
            this.messages.MaxLength = 110;
            this.messages.Name = "messages";
            this.messages.ReadOnly = true;
            this.messages.Size = new System.Drawing.Size(581, 180);
            this.messages.TabIndex = 8;
            this.messages.Text = "";
            // 
            // Dialog
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(605, 364);
            this.Controls.Add(this.messages);
            this.Controls.Add(this.info_text);
            this.Controls.Add(this.info);
            this.Controls.Add(this.exitBtn);
            this.Controls.Add(this.sendBtn);
            this.Controls.Add(this.richTextBox1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "Dialog";
            this.Text = "Диалог";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.formClosing);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        public System.Windows.Forms.RichTextBox richTextBox1;
        public System.Windows.Forms.Button exitBtn;
        public System.Windows.Forms.Button sendBtn;
        public System.Windows.Forms.Label info;
        public System.Windows.Forms.TextBox info_text;
        public System.Windows.Forms.RichTextBox messages;
    }
}