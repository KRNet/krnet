namespace KR_network
{
    partial class MainMenu
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
            this.connectBtn = new System.Windows.Forms.Button();
            this.exitBtn = new System.Windows.Forms.Button();
            this.info_text = new System.Windows.Forms.TextBox();
            this.info = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.parity = new System.Windows.Forms.ComboBox();
            this.label3 = new System.Windows.Forms.Label();
            this.stopBytes = new System.Windows.Forms.ComboBox();
            this.label2 = new System.Windows.Forms.Label();
            this.speed = new System.Windows.Forms.ComboBox();
            this.label1 = new System.Windows.Forms.Label();
            this.portName = new System.Windows.Forms.ComboBox();
            this.nickname = new System.Windows.Forms.TextBox();
            this.label6 = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // connectBtn
            // 
            this.connectBtn.Location = new System.Drawing.Point(278, 545);
            this.connectBtn.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.connectBtn.Name = "connectBtn";
            this.connectBtn.Size = new System.Drawing.Size(298, 88);
            this.connectBtn.TabIndex = 1;
            this.connectBtn.Text = "Подключиться";
            this.connectBtn.UseVisualStyleBackColor = true;
            this.connectBtn.Click += new System.EventHandler(this.connectBtn_Click);
            // 
            // exitBtn
            // 
            this.exitBtn.Location = new System.Drawing.Point(15, 542);
            this.exitBtn.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.exitBtn.Name = "exitBtn";
            this.exitBtn.Size = new System.Drawing.Size(257, 88);
            this.exitBtn.TabIndex = 2;
            this.exitBtn.Text = "Выйти";
            this.exitBtn.UseVisualStyleBackColor = true;
            this.exitBtn.Click += new System.EventHandler(this.exitBtn_Click);
            // 
            // info_text
            // 
            this.info_text.Enabled = false;
            this.info_text.Location = new System.Drawing.Point(111, 480);
            this.info_text.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.info_text.Name = "info_text";
            this.info_text.Size = new System.Drawing.Size(469, 26);
            this.info_text.TabIndex = 9;
            this.info_text.Text = "Соединение не установлено";
            // 
            // info
            // 
            this.info.AutoSize = true;
            this.info.Location = new System.Drawing.Point(16, 484);
            this.info.Name = "info";
            this.info.Size = new System.Drawing.Size(91, 20);
            this.info.TabIndex = 8;
            this.info.Text = "Состояние";
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(99, 283);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(83, 20);
            this.label5.TabIndex = 37;
            this.label5.Text = "Чётность";
            // 
            // parity
            // 
            this.parity.FormattingEnabled = true;
            this.parity.Location = new System.Drawing.Point(213, 279);
            this.parity.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.parity.Name = "parity";
            this.parity.Size = new System.Drawing.Size(280, 28);
            this.parity.TabIndex = 36;
            this.parity.SelectedIndexChanged += new System.EventHandler(this.parity_SelectedIndexChanged);
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(82, 229);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(98, 20);
            this.label3.TabIndex = 35;
            this.label3.Text = "Стоп байты";
            // 
            // stopBytes
            // 
            this.stopBytes.FormattingEnabled = true;
            this.stopBytes.Location = new System.Drawing.Point(213, 225);
            this.stopBytes.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.stopBytes.Name = "stopBytes";
            this.stopBytes.Size = new System.Drawing.Size(280, 28);
            this.stopBytes.TabIndex = 34;
            this.stopBytes.SelectedIndexChanged += new System.EventHandler(this.stopBytes_SelectedIndexChanged);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(100, 181);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(81, 20);
            this.label2.TabIndex = 33;
            this.label2.Text = "Скорость";
            // 
            // speed
            // 
            this.speed.FormattingEnabled = true;
            this.speed.Location = new System.Drawing.Point(213, 177);
            this.speed.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.speed.Name = "speed";
            this.speed.Size = new System.Drawing.Size(280, 28);
            this.speed.TabIndex = 32;
            this.speed.SelectedIndexChanged += new System.EventHandler(this.speed_SelectedIndexChanged);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(132, 128);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(48, 20);
            this.label1.TabIndex = 31;
            this.label1.Text = "Порт";
            // 
            // portName
            // 
            this.portName.FormattingEnabled = true;
            this.portName.Location = new System.Drawing.Point(213, 124);
            this.portName.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.portName.Name = "portName";
            this.portName.Size = new System.Drawing.Size(280, 28);
            this.portName.TabIndex = 30;
            this.portName.SelectedIndexChanged += new System.EventHandler(this.portName_SelectedIndexChanged);
            // 
            // nickname
            // 
            this.nickname.Location = new System.Drawing.Point(213, 69);
            this.nickname.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.nickname.MaxLength = 10;
            this.nickname.Name = "nickname";
            this.nickname.Size = new System.Drawing.Size(280, 26);
            this.nickname.TabIndex = 29;
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(144, 72);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(38, 20);
            this.label6.TabIndex = 28;
            this.label6.Text = "Ник";
            // 
            // MainMenu
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(9F, 20F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(592, 646);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.parity);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.stopBytes);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.speed);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.portName);
            this.Controls.Add(this.nickname);
            this.Controls.Add(this.label6);
            this.Controls.Add(this.info_text);
            this.Controls.Add(this.info);
            this.Controls.Add(this.exitBtn);
            this.Controls.Add(this.connectBtn);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "MainMenu";
            this.Text = "Главное меню";
            this.VisibleChanged += new System.EventHandler(this.visibleChange);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button connectBtn;
        private System.Windows.Forms.Button exitBtn;
        public System.Windows.Forms.TextBox info_text;
        public System.Windows.Forms.Label info;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.ComboBox parity;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.ComboBox stopBytes;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.ComboBox speed;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.ComboBox portName;
        private System.Windows.Forms.TextBox nickname;
        private System.Windows.Forms.Label label6;
    }
}