namespace ModbusTcpClient
{
    partial class Form1
    {
        /// <summary>
        /// 必需的设计器变量。
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// 清理所有正在使用的资源。
        /// </summary>
        /// <param name="disposing">如果应释放托管资源，为 true；否则为 false。</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows 窗体设计器生成的代码

        /// <summary>
        /// 设计器支持所需的方法 - 不要修改
        /// 使用代码编辑器修改此方法的内容。
        /// </summary>
        private void InitializeComponent()
        {
            this.IpAddress = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.IpAddressTextBox = new System.Windows.Forms.TextBox();
            this.PortTextBox = new System.Windows.Forms.TextBox();
            this.ConnectButton = new System.Windows.Forms.Button();
            this.DisconnectButton = new System.Windows.Forms.Button();
            this.label2 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.FunctionTextBox = new System.Windows.Forms.TextBox();
            this.StartAddressTextBox = new System.Windows.Forms.TextBox();
            this.CountTextBox = new System.Windows.Forms.TextBox();
            this.Writebutton = new System.Windows.Forms.Button();
            this.dataShowArea = new System.Windows.Forms.DataGridView();
            this.ShowAllData = new System.Windows.Forms.Button();
            this.NextRow = new System.Windows.Forms.Button();
            this.UpRow = new System.Windows.Forms.Button();
            this.radioButton1 = new System.Windows.Forms.RadioButton();
            this.radioButton2 = new System.Windows.Forms.RadioButton();
            this.backgroundWorker1 = new System.ComponentModel.BackgroundWorker();
            this.backgroundWorker2 = new System.ComponentModel.BackgroundWorker();
            this.textBox1 = new System.Windows.Forms.TextBox();
            ((System.ComponentModel.ISupportInitialize)(this.dataShowArea)).BeginInit();
            this.SuspendLayout();
            // 
            // IpAddress
            // 
            this.IpAddress.AutoSize = true;
            this.IpAddress.Location = new System.Drawing.Point(25, 35);
            this.IpAddress.Name = "IpAddress";
            this.IpAddress.Size = new System.Drawing.Size(68, 15);
            this.IpAddress.TabIndex = 0;
            this.IpAddress.Text = "IP地址：";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(280, 35);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(67, 15);
            this.label1.TabIndex = 1;
            this.label1.Text = "端口号：";
            // 
            // IpAddressTextBox
            // 
            this.IpAddressTextBox.Location = new System.Drawing.Point(99, 32);
            this.IpAddressTextBox.Name = "IpAddressTextBox";
            this.IpAddressTextBox.Size = new System.Drawing.Size(140, 25);
            this.IpAddressTextBox.TabIndex = 2;
            this.IpAddressTextBox.Text = "127.0.0.1";
            // 
            // PortTextBox
            // 
            this.PortTextBox.Location = new System.Drawing.Point(353, 32);
            this.PortTextBox.Name = "PortTextBox";
            this.PortTextBox.Size = new System.Drawing.Size(140, 25);
            this.PortTextBox.TabIndex = 3;
            this.PortTextBox.Text = "502";
            // 
            // ConnectButton
            // 
            this.ConnectButton.Location = new System.Drawing.Point(555, 35);
            this.ConnectButton.Name = "ConnectButton";
            this.ConnectButton.Size = new System.Drawing.Size(75, 23);
            this.ConnectButton.TabIndex = 4;
            this.ConnectButton.Text = "Connect";
            this.ConnectButton.UseVisualStyleBackColor = true;
            this.ConnectButton.Click += new System.EventHandler(this.ConnectButton_Click);
            // 
            // DisconnectButton
            // 
            this.DisconnectButton.Location = new System.Drawing.Point(676, 34);
            this.DisconnectButton.Name = "DisconnectButton";
            this.DisconnectButton.Size = new System.Drawing.Size(97, 23);
            this.DisconnectButton.TabIndex = 5;
            this.DisconnectButton.Text = "Disconnect";
            this.DisconnectButton.UseVisualStyleBackColor = true;
            this.DisconnectButton.Click += new System.EventHandler(this.DisconnectButton_Click);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(10, 101);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(79, 15);
            this.label2.TabIndex = 6;
            this.label2.Text = "Function:";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(243, 101);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(111, 15);
            this.label3.TabIndex = 7;
            this.label3.Text = "StartAddress:";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(511, 104);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(95, 15);
            this.label4.TabIndex = 8;
            this.label4.Text = "Cont/Value:";
            // 
            // FunctionTextBox
            // 
            this.FunctionTextBox.Location = new System.Drawing.Point(95, 98);
            this.FunctionTextBox.Name = "FunctionTextBox";
            this.FunctionTextBox.Size = new System.Drawing.Size(139, 25);
            this.FunctionTextBox.TabIndex = 9;
            // 
            // StartAddressTextBox
            // 
            this.StartAddressTextBox.Location = new System.Drawing.Point(360, 98);
            this.StartAddressTextBox.Name = "StartAddressTextBox";
            this.StartAddressTextBox.Size = new System.Drawing.Size(139, 25);
            this.StartAddressTextBox.TabIndex = 10;
            // 
            // CountTextBox
            // 
            this.CountTextBox.Location = new System.Drawing.Point(612, 101);
            this.CountTextBox.Name = "CountTextBox";
            this.CountTextBox.Size = new System.Drawing.Size(139, 25);
            this.CountTextBox.TabIndex = 11;
            // 
            // Writebutton
            // 
            this.Writebutton.Location = new System.Drawing.Point(246, 146);
            this.Writebutton.Name = "Writebutton";
            this.Writebutton.Size = new System.Drawing.Size(75, 23);
            this.Writebutton.TabIndex = 13;
            this.Writebutton.Text = "Write";
            this.Writebutton.UseVisualStyleBackColor = true;
            this.Writebutton.Click += new System.EventHandler(this.Writebutton_Click);
            // 
            // dataShowArea
            // 
            this.dataShowArea.BackgroundColor = System.Drawing.SystemColors.Control;
            this.dataShowArea.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataShowArea.Location = new System.Drawing.Point(-3, 194);
            this.dataShowArea.Name = "dataShowArea";
            this.dataShowArea.ReadOnly = true;
            this.dataShowArea.RowHeadersWidth = 51;
            this.dataShowArea.RowTemplate.Height = 27;
            this.dataShowArea.Size = new System.Drawing.Size(496, 360);
            this.dataShowArea.TabIndex = 14;
            this.dataShowArea.CellMouseDoubleClick += new System.Windows.Forms.DataGridViewCellMouseEventHandler(this.DataGridViewListCellMouseDoubleClick);
            // 
            // ShowAllData
            // 
            this.ShowAllData.Location = new System.Drawing.Point(353, 147);
            this.ShowAllData.Name = "ShowAllData";
            this.ShowAllData.Size = new System.Drawing.Size(88, 22);
            this.ShowAllData.TabIndex = 15;
            this.ShowAllData.Text = "ShowData";
            this.ShowAllData.UseVisualStyleBackColor = true;
            this.ShowAllData.Click += new System.EventHandler(this.ShowAllData_Click);
            // 
            // NextRow
            // 
            this.NextRow.Location = new System.Drawing.Point(38, 146);
            this.NextRow.Name = "NextRow";
            this.NextRow.Size = new System.Drawing.Size(75, 23);
            this.NextRow.TabIndex = 16;
            this.NextRow.Text = "Down";
            this.NextRow.UseVisualStyleBackColor = true;
            this.NextRow.Click += new System.EventHandler(this.NextRow_Click);
            // 
            // UpRow
            // 
            this.UpRow.Location = new System.Drawing.Point(145, 146);
            this.UpRow.Name = "UpRow";
            this.UpRow.Size = new System.Drawing.Size(75, 23);
            this.UpRow.TabIndex = 17;
            this.UpRow.Text = "Up";
            this.UpRow.UseVisualStyleBackColor = true;
            this.UpRow.Click += new System.EventHandler(this.UpRow_Click);
            // 
            // radioButton1
            // 
            this.radioButton1.AutoSize = true;
            this.radioButton1.Location = new System.Drawing.Point(518, 149);
            this.radioButton1.Name = "radioButton1";
            this.radioButton1.Size = new System.Drawing.Size(88, 19);
            this.radioButton1.TabIndex = 18;
            this.radioButton1.TabStop = true;
            this.radioButton1.Text = "字寄存器";
            this.radioButton1.UseVisualStyleBackColor = true;
            this.radioButton1.Click += new System.EventHandler(this.radioButton1_Click);
            // 
            // radioButton2
            // 
            this.radioButton2.AutoSize = true;
            this.radioButton2.Location = new System.Drawing.Point(627, 150);
            this.radioButton2.Name = "radioButton2";
            this.radioButton2.Size = new System.Drawing.Size(88, 19);
            this.radioButton2.TabIndex = 19;
            this.radioButton2.TabStop = true;
            this.radioButton2.Text = "位寄存器";
            this.radioButton2.UseVisualStyleBackColor = true;
            this.radioButton2.Click += new System.EventHandler(this.radioButton2_Click);
            // 
            // textBox1
            // 
            this.textBox1.BackColor = System.Drawing.SystemColors.Menu;
            this.textBox1.Location = new System.Drawing.Point(494, 194);
            this.textBox1.Multiline = true;
            this.textBox1.Name = "textBox1";
            this.textBox1.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.textBox1.Size = new System.Drawing.Size(307, 360);
            this.textBox1.TabIndex = 20;
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(802, 553);
            this.Controls.Add(this.textBox1);
            this.Controls.Add(this.radioButton1);
            this.Controls.Add(this.radioButton2);
            this.Controls.Add(this.UpRow);
            this.Controls.Add(this.NextRow);
            this.Controls.Add(this.ShowAllData);
            this.Controls.Add(this.dataShowArea);
            this.Controls.Add(this.Writebutton);
            this.Controls.Add(this.CountTextBox);
            this.Controls.Add(this.StartAddressTextBox);
            this.Controls.Add(this.FunctionTextBox);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.DisconnectButton);
            this.Controls.Add(this.ConnectButton);
            this.Controls.Add(this.PortTextBox);
            this.Controls.Add(this.IpAddressTextBox);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.IpAddress);
            this.MaximizeBox = false;
            this.Name = "Form1";
            this.Text = "Modbus Client";
            ((System.ComponentModel.ISupportInitialize)(this.dataShowArea)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label IpAddress;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox IpAddressTextBox;
        private System.Windows.Forms.TextBox PortTextBox;
        private System.Windows.Forms.Button ConnectButton;
        private System.Windows.Forms.Button DisconnectButton;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.TextBox FunctionTextBox;
        private System.Windows.Forms.TextBox StartAddressTextBox;
        private System.Windows.Forms.TextBox CountTextBox;
        private System.Windows.Forms.Button Writebutton;
        private System.Windows.Forms.DataGridView dataShowArea;
        private System.Windows.Forms.Button ShowAllData;
        private System.Windows.Forms.Button NextRow;
        private System.Windows.Forms.Button UpRow;
        private System.Windows.Forms.RadioButton radioButton1;
        private System.Windows.Forms.RadioButton radioButton2;
        private System.ComponentModel.BackgroundWorker backgroundWorker1;
        private System.ComponentModel.BackgroundWorker backgroundWorker2;
        private System.Windows.Forms.TextBox textBox1;
        /*        private System.Windows.Forms.Timer timer;*/
    }
}

