namespace KR_NetworkCSharp
{
    partial class Sended
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
            this.listBox1 = new System.Windows.Forms.ListBox();
            this.sendedMessages = new System.Windows.Forms.ListBox();
            this.messageToSend = new System.Windows.Forms.RichTextBox();
            this.sendBtn = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // listBox1
            // 
            this.listBox1.FormattingEnabled = true;
            this.listBox1.ItemHeight = 16;
            this.listBox1.Location = new System.Drawing.Point(22, 621);
            this.listBox1.Name = "listBox1";
            this.listBox1.Size = new System.Drawing.Size(120, 84);
            this.listBox1.TabIndex = 0;
            // 
            // sendedMessages
            // 
            this.sendedMessages.FormattingEnabled = true;
            this.sendedMessages.ItemHeight = 16;
            this.sendedMessages.Location = new System.Drawing.Point(13, 13);
            this.sendedMessages.Name = "sendedMessages";
            this.sendedMessages.Size = new System.Drawing.Size(457, 324);
            this.sendedMessages.TabIndex = 1;
            // 
            // messageToSend
            // 
            this.messageToSend.Location = new System.Drawing.Point(12, 360);
            this.messageToSend.Name = "messageToSend";
            this.messageToSend.Size = new System.Drawing.Size(328, 81);
            this.messageToSend.TabIndex = 2;
            this.messageToSend.Text = "";
            // 
            // sendBtn
            // 
            this.sendBtn.Location = new System.Drawing.Point(346, 374);
            this.sendBtn.Name = "sendBtn";
            this.sendBtn.Size = new System.Drawing.Size(124, 51);
            this.sendBtn.TabIndex = 3;
            this.sendBtn.Text = "Отправить";
            this.sendBtn.UseVisualStyleBackColor = true;
            // 
            // Sended
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(482, 453);
            this.Controls.Add(this.sendBtn);
            this.Controls.Add(this.messageToSend);
            this.Controls.Add(this.sendedMessages);
            this.Controls.Add(this.listBox1);
            this.Name = "Sended";
            this.Text = "Отправленные сообщения";
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.ListBox listBox1;
        private System.Windows.Forms.ListBox sendedMessages;
        private System.Windows.Forms.RichTextBox messageToSend;
        private System.Windows.Forms.Button sendBtn;
    }
}