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
            this.tabs = new System.Windows.Forms.TabControl();
            this.tabPage1 = new System.Windows.Forms.TabPage();
            this.nickname = new System.Windows.Forms.TextBox();
            this.label6 = new System.Windows.Forms.Label();
            this.tabPage2 = new System.Windows.Forms.TabPage();
            this.label5 = new System.Windows.Forms.Label();
            this.parity = new System.Windows.Forms.ComboBox();
            this.label3 = new System.Windows.Forms.Label();
            this.stopBytes = new System.Windows.Forms.ComboBox();
            this.label2 = new System.Windows.Forms.Label();
            this.speed = new System.Windows.Forms.ComboBox();
            this.label1 = new System.Windows.Forms.Label();
            this.portName = new System.Windows.Forms.ComboBox();
            this.connectBtn = new System.Windows.Forms.Button();
            this.exitBtn = new System.Windows.Forms.Button();
            this.label4 = new System.Windows.Forms.Label();
            this.tabs.SuspendLayout();
            this.tabPage1.SuspendLayout();
            this.tabPage2.SuspendLayout();
            this.SuspendLayout();
            // 
            // tabs
            // 
            this.tabs.Controls.Add(this.tabPage1);
            this.tabs.Controls.Add(this.tabPage2);
            this.tabs.Location = new System.Drawing.Point(15, 16);
            this.tabs.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.tabs.Name = "tabs";
            this.tabs.SelectedIndex = 0;
            this.tabs.Size = new System.Drawing.Size(357, 440);
            this.tabs.TabIndex = 0;
            // 
            // tabPage1
            // 
            this.tabPage1.Controls.Add(this.nickname);
            this.tabPage1.Controls.Add(this.label6);
            this.tabPage1.Location = new System.Drawing.Point(4, 29);
            this.tabPage1.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.tabPage1.Name = "tabPage1";
            this.tabPage1.Padding = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.tabPage1.Size = new System.Drawing.Size(349, 407);
            this.tabPage1.TabIndex = 0;
            this.tabPage1.Text = "Ник";
            this.tabPage1.UseVisualStyleBackColor = true;
            // 
            // nickname
            // 
            this.nickname.Location = new System.Drawing.Point(87, 98);
            this.nickname.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.nickname.Name = "nickname";
            this.nickname.Size = new System.Drawing.Size(222, 26);
            this.nickname.TabIndex = 1;
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(26, 98);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(38, 20);
            this.label6.TabIndex = 0;
            this.label6.Text = "Ник";
            // 
            // tabPage2
            // 
            this.tabPage2.Controls.Add(this.label5);
            this.tabPage2.Controls.Add(this.parity);
            this.tabPage2.Controls.Add(this.label3);
            this.tabPage2.Controls.Add(this.stopBytes);
            this.tabPage2.Controls.Add(this.label2);
            this.tabPage2.Controls.Add(this.speed);
            this.tabPage2.Controls.Add(this.label1);
            this.tabPage2.Controls.Add(this.portName);
            this.tabPage2.Location = new System.Drawing.Point(4, 29);
            this.tabPage2.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.tabPage2.Name = "tabPage2";
            this.tabPage2.Padding = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.tabPage2.Size = new System.Drawing.Size(349, 407);
            this.tabPage2.TabIndex = 1;
            this.tabPage2.Text = "Настройки порта";
            this.tabPage2.UseVisualStyleBackColor = true;
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(38, 180);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(83, 20);
            this.label5.TabIndex = 19;
            this.label5.Text = "Чётность";
            // 
            // parity
            // 
            this.parity.FormattingEnabled = true;
            this.parity.Location = new System.Drawing.Point(152, 176);
            this.parity.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.parity.Name = "parity";
            this.parity.Size = new System.Drawing.Size(178, 28);
            this.parity.TabIndex = 18;
            this.parity.SelectedIndexChanged += new System.EventHandler(this.parity_SelectedIndexChanged);
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(21, 126);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(98, 20);
            this.label3.TabIndex = 15;
            this.label3.Text = "Стоп байты";
            // 
            // stopBytes
            // 
            this.stopBytes.FormattingEnabled = true;
            this.stopBytes.Location = new System.Drawing.Point(152, 122);
            this.stopBytes.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.stopBytes.Name = "stopBytes";
            this.stopBytes.Size = new System.Drawing.Size(178, 28);
            this.stopBytes.TabIndex = 14;
            this.stopBytes.SelectedIndexChanged += new System.EventHandler(this.stopBytes_SelectedIndexChanged);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(39, 78);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(81, 20);
            this.label2.TabIndex = 13;
            this.label2.Text = "Скорость";
            // 
            // speed
            // 
            this.speed.FormattingEnabled = true;
            this.speed.Location = new System.Drawing.Point(152, 74);
            this.speed.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.speed.Name = "speed";
            this.speed.Size = new System.Drawing.Size(178, 28);
            this.speed.TabIndex = 12;
            this.speed.SelectedIndexChanged += new System.EventHandler(this.speed_SelectedIndexChanged);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(71, 25);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(48, 20);
            this.label1.TabIndex = 11;
            this.label1.Text = "Порт";
            // 
            // portName
            // 
            this.portName.FormattingEnabled = true;
            this.portName.Location = new System.Drawing.Point(152, 21);
            this.portName.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.portName.Name = "portName";
            this.portName.Size = new System.Drawing.Size(178, 28);
            this.portName.TabIndex = 10;
            this.portName.SelectedIndexChanged += new System.EventHandler(this.portName_SelectedIndexChanged);
            // 
            // connectBtn
            // 
            this.connectBtn.Location = new System.Drawing.Point(172, 488);
            this.connectBtn.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.connectBtn.Name = "connectBtn";
            this.connectBtn.Size = new System.Drawing.Size(196, 88);
            this.connectBtn.TabIndex = 1;
            this.connectBtn.Text = "Подключиться";
            this.connectBtn.UseVisualStyleBackColor = true;
            this.connectBtn.Click += new System.EventHandler(this.connectBtn_Click);
            // 
            // exitBtn
            // 
            this.exitBtn.Location = new System.Drawing.Point(15, 488);
            this.exitBtn.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.exitBtn.Name = "exitBtn";
            this.exitBtn.Size = new System.Drawing.Size(145, 88);
            this.exitBtn.TabIndex = 2;
            this.exitBtn.Text = "Выйти";
            this.exitBtn.UseVisualStyleBackColor = true;
            this.exitBtn.Click += new System.EventHandler(this.exitBtn_Click);
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(186, 460);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(169, 20);
            this.label4.TabIndex = 2;
            this.label4.Text = "Идет подключение...";
            this.label4.Visible = false;
            // 
            // MainMenu
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(9F, 20F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(388, 589);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.exitBtn);
            this.Controls.Add(this.connectBtn);
            this.Controls.Add(this.tabs);
            this.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.Name = "MainMenu";
            this.Text = "Главное меню";
            this.tabs.ResumeLayout(false);
            this.tabPage1.ResumeLayout(false);
            this.tabPage1.PerformLayout();
            this.tabPage2.ResumeLayout(false);
            this.tabPage2.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TabControl tabs;
        private System.Windows.Forms.TabPage tabPage1;
        private System.Windows.Forms.TabPage tabPage2;
        private System.Windows.Forms.Button connectBtn;
        private System.Windows.Forms.Button exitBtn;
        private System.Windows.Forms.TextBox nickname;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.ComboBox parity;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.ComboBox stopBytes;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.ComboBox speed;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.ComboBox portName;
        private System.Windows.Forms.Label label4;
    }
}