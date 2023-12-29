namespace OBD_II_WiFi
{
    partial class OBD2Form
    {
        /// <summary>
        ///  Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        ///  Clean up any resources being used.
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
        ///  Required method for Designer support - do not modify
        ///  the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            components = new System.ComponentModel.Container();
            portTextBox = new TextBox();
            label1 = new Label();
            label2 = new Label();
            ipTextBox = new TextBox();
            initConnButton = new Button();
            display = new RichTextBox();
            askPIDButton = new Button();
            buttonStopListening = new Button();
            buttonFuelData = new Button();
            label3 = new Label();
            label4 = new Label();
            ecoButton = new Button();
            normalButton = new Button();
            sportButton = new Button();
            highwayButton = new Button();
            extraButton = new Button();
            urbanButton = new Button();
            groupBox1 = new GroupBox();
            groupBox2 = new GroupBox();
            videoSyncButton = new Button();
            evalDriveButton = new Button();
            chart_speed = new ScottPlot.FormsPlot();
            chart_rpm = new ScottPlot.FormsPlot();
            chart_load = new ScottPlot.FormsPlot();
            groupBox3 = new GroupBox();
            videoSyncTimer = new System.Windows.Forms.Timer(components);
            syncPanel = new Panel();
            groupBox1.SuspendLayout();
            groupBox2.SuspendLayout();
            groupBox3.SuspendLayout();
            SuspendLayout();
            // 
            // portTextBox
            // 
            portTextBox.Location = new Point(70, 41);
            portTextBox.Name = "portTextBox";
            portTextBox.Size = new Size(136, 23);
            portTextBox.TabIndex = 0;
            portTextBox.Text = "35000";
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.BackColor = Color.Transparent;
            label1.Font = new Font("Franklin Gothic Medium Cond", 9F, FontStyle.Regular, GraphicsUnit.Point);
            label1.ForeColor = SystemColors.ControlLightLight;
            label1.Location = new Point(19, 44);
            label1.Name = "label1";
            label1.Size = new Size(45, 16);
            label1.TabIndex = 1;
            label1.Text = "OBD2 IP";
            // 
            // label2
            // 
            label2.AutoSize = true;
            label2.BackColor = Color.Transparent;
            label2.Font = new Font("Franklin Gothic Medium Cond", 9F, FontStyle.Regular, GraphicsUnit.Point);
            label2.ForeColor = SystemColors.ControlLightLight;
            label2.Location = new Point(34, 15);
            label2.Name = "label2";
            label2.Size = new Size(31, 16);
            label2.TabIndex = 2;
            label2.Text = "PORT";
            // 
            // ipTextBox
            // 
            ipTextBox.Location = new Point(70, 12);
            ipTextBox.Name = "ipTextBox";
            ipTextBox.RightToLeft = RightToLeft.No;
            ipTextBox.Size = new Size(136, 23);
            ipTextBox.TabIndex = 3;
            ipTextBox.Text = "192.168.0.10";
            // 
            // initConnButton
            // 
            initConnButton.BackColor = Color.White;
            initConnButton.Cursor = Cursors.Hand;
            initConnButton.FlatAppearance.BorderColor = Color.Silver;
            initConnButton.FlatStyle = FlatStyle.Flat;
            initConnButton.Font = new Font("Franklin Gothic Medium Cond", 9F, FontStyle.Regular, GraphicsUnit.Point);
            initConnButton.Location = new Point(212, 12);
            initConnButton.Name = "initConnButton";
            initConnButton.Size = new Size(76, 52);
            initConnButton.TabIndex = 4;
            initConnButton.Text = "Init TCP Connection";
            initConnButton.UseVisualStyleBackColor = false;
            initConnButton.Click += initConnButton_Click;
            // 
            // display
            // 
            display.BackColor = Color.FromArgb(7, 102, 173);
            display.BorderStyle = BorderStyle.None;
            display.Font = new Font("Cascadia Code Light", 8.25F, FontStyle.Regular, GraphicsUnit.Point);
            display.ForeColor = SystemColors.HighlightText;
            display.Location = new Point(7, 15);
            display.Margin = new Padding(3, 3, 10, 3);
            display.Name = "display";
            display.Size = new Size(265, 240);
            display.TabIndex = 5;
            display.Text = "";
            // 
            // askPIDButton
            // 
            askPIDButton.BackColor = Color.White;
            askPIDButton.Cursor = Cursors.Hand;
            askPIDButton.FlatAppearance.BorderColor = Color.Silver;
            askPIDButton.FlatStyle = FlatStyle.Flat;
            askPIDButton.Font = new Font("Franklin Gothic Medium Cond", 9F, FontStyle.Regular, GraphicsUnit.Point);
            askPIDButton.ForeColor = SystemColors.ActiveCaptionText;
            askPIDButton.Location = new Point(6, 22);
            askPIDButton.Name = "askPIDButton";
            askPIDButton.Size = new Size(120, 23);
            askPIDButton.TabIndex = 6;
            askPIDButton.Text = "Ask PID";
            askPIDButton.UseVisualStyleBackColor = false;
            askPIDButton.Click += askPIDButton_Click;
            // 
            // buttonStopListening
            // 
            buttonStopListening.Cursor = Cursors.Hand;
            buttonStopListening.Location = new Point(1070, 532);
            buttonStopListening.Name = "buttonStopListening";
            buttonStopListening.Size = new Size(132, 23);
            buttonStopListening.TabIndex = 7;
            buttonStopListening.Text = "Close Connection";
            buttonStopListening.UseVisualStyleBackColor = true;
            buttonStopListening.Click += buttonStopListening_Click;
            // 
            // buttonFuelData
            // 
            buttonFuelData.BackColor = Color.White;
            buttonFuelData.FlatAppearance.BorderColor = Color.Silver;
            buttonFuelData.FlatStyle = FlatStyle.Flat;
            buttonFuelData.Font = new Font("Franklin Gothic Medium Cond", 9F, FontStyle.Regular, GraphicsUnit.Point);
            buttonFuelData.ForeColor = SystemColors.ActiveCaptionText;
            buttonFuelData.Location = new Point(151, 22);
            buttonFuelData.Name = "buttonFuelData";
            buttonFuelData.Size = new Size(120, 23);
            buttonFuelData.TabIndex = 9;
            buttonFuelData.Text = "Read Data";
            buttonFuelData.UseVisualStyleBackColor = false;
            buttonFuelData.Click += buttonFuelData_Click;
            // 
            // label3
            // 
            label3.AutoSize = true;
            label3.BackColor = Color.Transparent;
            label3.Font = new Font("Franklin Gothic Medium Cond", 9F, FontStyle.Regular, GraphicsUnit.Point);
            label3.ForeColor = SystemColors.ControlLightLight;
            label3.Location = new Point(6, 52);
            label3.Name = "label3";
            label3.Size = new Size(55, 16);
            label3.TabIndex = 10;
            label3.Text = "Drive Style:";
            // 
            // label4
            // 
            label4.AutoSize = true;
            label4.BackColor = Color.Transparent;
            label4.Font = new Font("Franklin Gothic Medium Cond", 9F, FontStyle.Regular, GraphicsUnit.Point);
            label4.ForeColor = SystemColors.ControlLightLight;
            label4.Location = new Point(6, 101);
            label4.Name = "label4";
            label4.Size = new Size(56, 16);
            label4.TabIndex = 11;
            label4.Text = "Road Type:";
            // 
            // ecoButton
            // 
            ecoButton.BackColor = Color.LightGreen;
            ecoButton.FlatAppearance.BorderColor = Color.Green;
            ecoButton.FlatStyle = FlatStyle.Flat;
            ecoButton.Font = new Font("Franklin Gothic Medium Cond", 9F, FontStyle.Regular, GraphicsUnit.Point);
            ecoButton.ForeColor = SystemColors.ActiveCaptionText;
            ecoButton.Location = new Point(6, 73);
            ecoButton.Margin = new Padding(0);
            ecoButton.Name = "ecoButton";
            ecoButton.Size = new Size(80, 24);
            ecoButton.TabIndex = 12;
            ecoButton.Text = "Eco";
            ecoButton.UseVisualStyleBackColor = false;
            // 
            // normalButton
            // 
            normalButton.BackColor = SystemColors.ActiveCaption;
            normalButton.FlatAppearance.BorderColor = Color.Navy;
            normalButton.FlatStyle = FlatStyle.Flat;
            normalButton.Font = new Font("Franklin Gothic Medium Cond", 9F, FontStyle.Regular, GraphicsUnit.Point);
            normalButton.ForeColor = SystemColors.ActiveCaptionText;
            normalButton.Location = new Point(98, 73);
            normalButton.Name = "normalButton";
            normalButton.Size = new Size(80, 24);
            normalButton.TabIndex = 13;
            normalButton.Text = "Normal";
            normalButton.UseVisualStyleBackColor = false;
            // 
            // sportButton
            // 
            sportButton.BackColor = Color.IndianRed;
            sportButton.FlatAppearance.BorderColor = Color.Maroon;
            sportButton.FlatStyle = FlatStyle.Flat;
            sportButton.Font = new Font("Franklin Gothic Medium Cond", 9F, FontStyle.Regular, GraphicsUnit.Point);
            sportButton.ForeColor = SystemColors.ActiveCaptionText;
            sportButton.Location = new Point(190, 73);
            sportButton.Name = "sportButton";
            sportButton.Size = new Size(80, 24);
            sportButton.TabIndex = 14;
            sportButton.Text = "Sport";
            sportButton.UseVisualStyleBackColor = false;
            // 
            // highwayButton
            // 
            highwayButton.BackColor = Color.YellowGreen;
            highwayButton.FlatAppearance.BorderColor = Color.Green;
            highwayButton.FlatStyle = FlatStyle.Flat;
            highwayButton.Font = new Font("Franklin Gothic Medium Cond", 9F, FontStyle.Regular, GraphicsUnit.Point);
            highwayButton.ForeColor = SystemColors.ActiveCaptionText;
            highwayButton.Location = new Point(190, 120);
            highwayButton.Name = "highwayButton";
            highwayButton.Size = new Size(80, 24);
            highwayButton.TabIndex = 17;
            highwayButton.Text = "Highway";
            highwayButton.UseVisualStyleBackColor = false;
            // 
            // extraButton
            // 
            extraButton.BackColor = Color.Teal;
            extraButton.FlatAppearance.BorderColor = Color.FromArgb(0, 64, 64);
            extraButton.FlatStyle = FlatStyle.Flat;
            extraButton.Font = new Font("Franklin Gothic Medium Cond", 9F, FontStyle.Regular, GraphicsUnit.Point);
            extraButton.ForeColor = SystemColors.ActiveCaptionText;
            extraButton.Location = new Point(98, 120);
            extraButton.Name = "extraButton";
            extraButton.Size = new Size(80, 24);
            extraButton.TabIndex = 16;
            extraButton.Text = "Suburban";
            extraButton.UseVisualStyleBackColor = false;
            // 
            // urbanButton
            // 
            urbanButton.BackColor = SystemColors.Info;
            urbanButton.FlatAppearance.BorderColor = Color.Gray;
            urbanButton.FlatStyle = FlatStyle.Flat;
            urbanButton.Font = new Font("Franklin Gothic Medium Cond", 9F, FontStyle.Regular, GraphicsUnit.Point);
            urbanButton.ForeColor = SystemColors.ActiveCaptionText;
            urbanButton.Location = new Point(6, 120);
            urbanButton.Name = "urbanButton";
            urbanButton.Size = new Size(80, 24);
            urbanButton.TabIndex = 15;
            urbanButton.Text = "Urban";
            urbanButton.UseVisualStyleBackColor = false;
            // 
            // groupBox1
            // 
            groupBox1.BackColor = Color.Transparent;
            groupBox1.Controls.Add(askPIDButton);
            groupBox1.Controls.Add(highwayButton);
            groupBox1.Controls.Add(buttonFuelData);
            groupBox1.Controls.Add(extraButton);
            groupBox1.Controls.Add(ecoButton);
            groupBox1.Controls.Add(urbanButton);
            groupBox1.Controls.Add(label3);
            groupBox1.Controls.Add(sportButton);
            groupBox1.Controls.Add(label4);
            groupBox1.Controls.Add(normalButton);
            groupBox1.ForeColor = SystemColors.ButtonHighlight;
            groupBox1.Location = new Point(7, 70);
            groupBox1.Name = "groupBox1";
            groupBox1.Size = new Size(281, 161);
            groupBox1.TabIndex = 19;
            groupBox1.TabStop = false;
            groupBox1.Text = "Logging Data";
            // 
            // groupBox2
            // 
            groupBox2.BackColor = Color.Transparent;
            groupBox2.Controls.Add(videoSyncButton);
            groupBox2.Controls.Add(evalDriveButton);
            groupBox2.ForeColor = SystemColors.ButtonHighlight;
            groupBox2.Location = new Point(7, 237);
            groupBox2.Name = "groupBox2";
            groupBox2.Size = new Size(281, 57);
            groupBox2.TabIndex = 20;
            groupBox2.TabStop = false;
            groupBox2.Text = "Active Monitoring";
            // 
            // videoSyncButton
            // 
            videoSyncButton.BackColor = Color.White;
            videoSyncButton.Cursor = Cursors.Hand;
            videoSyncButton.FlatAppearance.BorderColor = Color.Silver;
            videoSyncButton.FlatStyle = FlatStyle.Flat;
            videoSyncButton.Font = new Font("Franklin Gothic Medium Cond", 9F, FontStyle.Regular, GraphicsUnit.Point);
            videoSyncButton.ForeColor = SystemColors.ActiveCaptionText;
            videoSyncButton.Location = new Point(151, 22);
            videoSyncButton.Name = "videoSyncButton";
            videoSyncButton.Size = new Size(120, 23);
            videoSyncButton.TabIndex = 26;
            videoSyncButton.Text = "Video Sync";
            videoSyncButton.UseVisualStyleBackColor = false;
            videoSyncButton.Click += videoSyncButton_Click;
            // 
            // evalDriveButton
            // 
            evalDriveButton.BackColor = Color.White;
            evalDriveButton.Cursor = Cursors.Hand;
            evalDriveButton.FlatAppearance.BorderColor = Color.Silver;
            evalDriveButton.FlatStyle = FlatStyle.Flat;
            evalDriveButton.Font = new Font("Franklin Gothic Medium Cond", 9F, FontStyle.Regular, GraphicsUnit.Point);
            evalDriveButton.ForeColor = SystemColors.ActiveCaptionText;
            evalDriveButton.Location = new Point(6, 22);
            evalDriveButton.Name = "evalDriveButton";
            evalDriveButton.Size = new Size(120, 23);
            evalDriveButton.TabIndex = 7;
            evalDriveButton.Text = "Evaluate Drive Style";
            evalDriveButton.UseVisualStyleBackColor = false;
            evalDriveButton.Click += evalDriveButton_Click;
            // 
            // chart_speed
            // 
            chart_speed.BackColor = Color.FromArgb(7, 102, 173);
            chart_speed.Enabled = false;
            chart_speed.ForeColor = SystemColors.ActiveCaptionText;
            chart_speed.Location = new Point(315, -5);
            chart_speed.Margin = new Padding(4, 3, 4, 3);
            chart_speed.Name = "chart_speed";
            chart_speed.Size = new Size(903, 187);
            chart_speed.TabIndex = 22;
            // 
            // chart_rpm
            // 
            chart_rpm.BackColor = Color.FromArgb(7, 102, 173);
            chart_rpm.Enabled = false;
            chart_rpm.ForeColor = SystemColors.ActiveCaptionText;
            chart_rpm.Location = new Point(315, 172);
            chart_rpm.Margin = new Padding(4, 3, 4, 3);
            chart_rpm.Name = "chart_rpm";
            chart_rpm.Size = new Size(903, 187);
            chart_rpm.TabIndex = 23;
            // 
            // chart_load
            // 
            chart_load.BackColor = Color.FromArgb(7, 102, 173);
            chart_load.Enabled = false;
            chart_load.ForeColor = SystemColors.ActiveCaptionText;
            chart_load.Location = new Point(315, 337);
            chart_load.Margin = new Padding(4, 3, 4, 3);
            chart_load.Name = "chart_load";
            chart_load.Size = new Size(903, 187);
            chart_load.TabIndex = 24;
            // 
            // groupBox3
            // 
            groupBox3.Controls.Add(display);
            groupBox3.ForeColor = SystemColors.ButtonHighlight;
            groupBox3.Location = new Point(6, 300);
            groupBox3.Name = "groupBox3";
            groupBox3.Size = new Size(282, 264);
            groupBox3.TabIndex = 25;
            groupBox3.TabStop = false;
            groupBox3.Text = "Message Display";
            // 
            // videoSyncTimer
            // 
            videoSyncTimer.Interval = 2000;
            // 
            // syncPanel
            // 
            syncPanel.Location = new Point(291, 12);
            syncPanel.Name = "syncPanel";
            syncPanel.Size = new Size(27, 552);
            syncPanel.TabIndex = 26;
            // 
            // OBD2Form
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            BackColor = Color.FromArgb(7, 102, 173);
            BackgroundImageLayout = ImageLayout.Center;
            ClientSize = new Size(1214, 567);
            Controls.Add(syncPanel);
            Controls.Add(groupBox3);
            Controls.Add(chart_load);
            Controls.Add(chart_rpm);
            Controls.Add(chart_speed);
            Controls.Add(buttonStopListening);
            Controls.Add(groupBox2);
            Controls.Add(groupBox1);
            Controls.Add(initConnButton);
            Controls.Add(ipTextBox);
            Controls.Add(label2);
            Controls.Add(label1);
            Controls.Add(portTextBox);
            ForeColor = SystemColors.ControlText;
            FormBorderStyle = FormBorderStyle.FixedSingle;
            Name = "OBD2Form";
            Text = "OBD II - Logger";
            groupBox1.ResumeLayout(false);
            groupBox1.PerformLayout();
            groupBox2.ResumeLayout(false);
            groupBox3.ResumeLayout(false);
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private TextBox portTextBox;
        private Label label1;
        private Label label2;
        private TextBox ipTextBox;
        private Button initConnButton;
        private RichTextBox display;
        private Button askPIDButton;
        private Button buttonStopListening;
        private Button buttonFuelData;
        private Label label3;
        private Label label4;
        private Button ecoButton;
        private Button normalButton;
        private Button sportButton;
        private Button highwayButton;
        private Button extraButton;
        private Button urbanButton;
        private GroupBox groupBox1;
        private GroupBox groupBox2;
        private Button evalDriveButton;
        private ScottPlot.FormsPlot chart_speed;
        private ScottPlot.FormsPlot chart_rpm;
        private ScottPlot.FormsPlot chart_load;
        private GroupBox groupBox3;
        private Button videoSyncButton;
        private System.Windows.Forms.Timer videoSyncTimer;
        private Panel syncPanel;
    }
}