namespace KR_NetworkCSharp
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
            this.textBox1 = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.tabPage2 = new System.Windows.Forms.TabPage();
            this.OkBtn = new System.Windows.Forms.Button();
            this.ExitBtn = new System.Windows.Forms.Button();
            this.parity = new System.Windows.Forms.ComboBox();
            this.stopBits = new System.Windows.Forms.ComboBox();
            this.dataBits = new System.Windows.Forms.ComboBox();
            this.portSpeed = new System.Windows.Forms.ComboBox();
            this.portName = new System.Windows.Forms.ComboBox();
            this.label6 = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.tabs.SuspendLayout();
            this.tabPage1.SuspendLayout();
            this.tabPage2.SuspendLayout();
            this.SuspendLayout();
            // 
            // tabs
            // 
            this.tabs.Controls.Add(this.tabPage1);
            this.tabs.Controls.Add(this.tabPage2);
            this.tabs.Location = new System.Drawing.Point(12, 12);
            this.tabs.Name = "tabs";
            this.tabs.SelectedIndex = 0;
            this.tabs.Size = new System.Drawing.Size(358, 385);
            this.tabs.TabIndex = 0;
            // 
            // tabPage1
            // 
            this.tabPage1.Controls.Add(this.textBox1);
            this.tabPage1.Controls.Add(this.label1);
            this.tabPage1.Location = new System.Drawing.Point(4, 25);
            this.tabPage1.Name = "tabPage1";
            this.tabPage1.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage1.Size = new System.Drawing.Size(350, 356);
            this.tabPage1.TabIndex = 0;
            this.tabPage1.Text = "Ник";
            this.tabPage1.UseVisualStyleBackColor = true;
            this.tabPage1.Click += new System.EventHandler(this.tabPage1_Click);
            // 
            // textBox1
            // 
            this.textBox1.Location = new System.Drawing.Point(113, 63);
            this.textBox1.Name = "textBox1";
            this.textBox1.Size = new System.Drawing.Size(158, 22);
            this.textBox1.TabIndex = 1;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(37, 66);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(33, 17);
            this.label1.TabIndex = 0;
            this.label1.Text = "Ник";
            this.label1.Click += new System.EventHandler(this.label1_Click);
            // 
            // tabPage2
            // 
            this.tabPage2.Controls.Add(this.parity);
            this.tabPage2.Controls.Add(this.stopBits);
            this.tabPage2.Controls.Add(this.dataBits);
            this.tabPage2.Controls.Add(this.portSpeed);
            this.tabPage2.Controls.Add(this.portName);
            this.tabPage2.Controls.Add(this.label6);
            this.tabPage2.Controls.Add(this.label5);
            this.tabPage2.Controls.Add(this.label4);
            this.tabPage2.Controls.Add(this.label3);
            this.tabPage2.Controls.Add(this.label2);
            this.tabPage2.Location = new System.Drawing.Point(4, 25);
            this.tabPage2.Name = "tabPage2";
            this.tabPage2.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage2.Size = new System.Drawing.Size(350, 356);
            this.tabPage2.TabIndex = 1;
            this.tabPage2.Text = "Параметры COM порта";
            this.tabPage2.UseVisualStyleBackColor = true;
            // 
            // OkBtn
            // 
            this.OkBtn.Location = new System.Drawing.Point(187, 403);
            this.OkBtn.Name = "OkBtn";
            this.OkBtn.Size = new System.Drawing.Size(179, 38);
            this.OkBtn.TabIndex = 1;
            this.OkBtn.Text = "OK";
            this.OkBtn.UseVisualStyleBackColor = true;
            this.OkBtn.Click += new System.EventHandler(this.OkBtn_Click);
            // 
            // ExitBtn
            // 
            this.ExitBtn.Location = new System.Drawing.Point(16, 403);
            this.ExitBtn.Name = "ExitBtn";
            this.ExitBtn.Size = new System.Drawing.Size(165, 38);
            this.ExitBtn.TabIndex = 2;
            this.ExitBtn.Text = "Exit";
            this.ExitBtn.UseVisualStyleBackColor = true;
            // 
            // parity
            // 
            this.parity.FormattingEnabled = true;
            this.parity.Location = new System.Drawing.Point(171, 279);
            this.parity.Name = "parity";
            this.parity.Size = new System.Drawing.Size(121, 24);
            this.parity.TabIndex = 29;
            // 
            // stopBits
            // 
            this.stopBits.FormattingEnabled = true;
            this.stopBits.Location = new System.Drawing.Point(171, 224);
            this.stopBits.Name = "stopBits";
            this.stopBits.Size = new System.Drawing.Size(121, 24);
            this.stopBits.TabIndex = 28;
            // 
            // dataBits
            // 
            this.dataBits.FormattingEnabled = true;
            this.dataBits.Location = new System.Drawing.Point(171, 165);
            this.dataBits.Name = "dataBits";
            this.dataBits.Size = new System.Drawing.Size(121, 24);
            this.dataBits.TabIndex = 27;
            // 
            // portSpeed
            // 
            this.portSpeed.FormattingEnabled = true;
            this.portSpeed.Location = new System.Drawing.Point(171, 107);
            this.portSpeed.Name = "portSpeed";
            this.portSpeed.Size = new System.Drawing.Size(121, 24);
            this.portSpeed.TabIndex = 26;
            // 
            // portName
            // 
            this.portName.Location = new System.Drawing.Point(171, 57);
            this.portName.Name = "portName";
            this.portName.Size = new System.Drawing.Size(121, 24);
            this.portName.TabIndex = 20;
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(59, 282);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(70, 17);
            this.label6.TabIndex = 25;
            this.label6.Text = "Чётность";
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(59, 227);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(77, 17);
            this.label5.TabIndex = 24;
            this.label5.Text = "Стоп биты";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(59, 168);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(94, 17);
            this.label4.TabIndex = 23;
            this.label4.Text = "Биты данных";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(59, 110);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(69, 17);
            this.label3.TabIndex = 22;
            this.label3.Text = "Скорость";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(59, 57);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(74, 17);
            this.label2.TabIndex = 21;
            this.label2.Text = "COM порт";
            // 
            // MainMenu
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(382, 453);
            this.Controls.Add(this.ExitBtn);
            this.Controls.Add(this.OkBtn);
            this.Controls.Add(this.tabs);
            this.Name = "MainMenu";
            this.Text = "Главное меню";
            this.tabs.ResumeLayout(false);
            this.tabPage1.ResumeLayout(false);
            this.tabPage1.PerformLayout();
            this.tabPage2.ResumeLayout(false);
            this.tabPage2.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.TabControl tabs;
        private System.Windows.Forms.TabPage tabPage1;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TabPage tabPage2;
        private System.Windows.Forms.TextBox textBox1;
        private System.Windows.Forms.Button OkBtn;
        private System.Windows.Forms.Button ExitBtn;
        private System.Windows.Forms.ComboBox parity;
        private System.Windows.Forms.ComboBox stopBits;
        private System.Windows.Forms.ComboBox dataBits;
        private System.Windows.Forms.ComboBox portSpeed;
        private System.Windows.Forms.ComboBox portName;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label2;
    }
}