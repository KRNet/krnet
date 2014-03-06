namespace KR_NetworkCSharp
{
    partial class Recieved
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
            this.recievedMessages = new System.Windows.Forms.ListBox();
            this.SuspendLayout();
            // 
            // recievedMessages
            // 
            this.recievedMessages.FormattingEnabled = true;
            this.recievedMessages.ItemHeight = 16;
            this.recievedMessages.Location = new System.Drawing.Point(13, 13);
            this.recievedMessages.Name = "recievedMessages";
            this.recievedMessages.Size = new System.Drawing.Size(457, 420);
            this.recievedMessages.TabIndex = 0;
            // 
            // Recieved
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(482, 453);
            this.Controls.Add(this.recievedMessages);
            this.Name = "Recieved";
            this.Text = "Полученные сообщения";
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.ListBox recievedMessages;
    }
}