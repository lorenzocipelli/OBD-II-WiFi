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
            this.portTextBox = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.ipTextBox = new System.Windows.Forms.TextBox();
            this.initConnButton = new System.Windows.Forms.Button();
            this.display = new System.Windows.Forms.RichTextBox();
            this.askPIDButton = new System.Windows.Forms.Button();
            this.buttonStopListening = new System.Windows.Forms.Button();
            this.buttonFuelData = new System.Windows.Forms.Button();
            this.label3 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.ecoButton = new System.Windows.Forms.Button();
            this.normalButton = new System.Windows.Forms.Button();
            this.sportButton = new System.Windows.Forms.Button();
            this.highwayButton = new System.Windows.Forms.Button();
            this.extraButton = new System.Windows.Forms.Button();
            this.urbanButton = new System.Windows.Forms.Button();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.evalDriveButton = new System.Windows.Forms.Button();
            this.chart_speed = new ScottPlot.FormsPlot();
            this.chart_rpm = new ScottPlot.FormsPlot();
            this.chart_load = new ScottPlot.FormsPlot();
            this.groupBox3 = new System.Windows.Forms.GroupBox();
            this.label5 = new System.Windows.Forms.Label();
            this.displayDriverStyle = new System.Windows.Forms.Label();
            this.CSVbutton = new System.Windows.Forms.Button();
            this.groupBox1.SuspendLayout();
            this.groupBox2.SuspendLayout();
            this.groupBox3.SuspendLayout();
            this.SuspendLayout();
            // 
            // portTextBox
            // 
            this.portTextBox.Location = new System.Drawing.Point(70, 41);
            this.portTextBox.Name = "portTextBox";
            this.portTextBox.Size = new System.Drawing.Size(136, 23);
            this.portTextBox.TabIndex = 0;
            this.portTextBox.Text = "35000";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.BackColor = System.Drawing.Color.Transparent;
            this.label1.Font = new System.Drawing.Font("Franklin Gothic Medium Cond", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.label1.ForeColor = System.Drawing.SystemColors.ControlLightLight;
            this.label1.Location = new System.Drawing.Point(19, 44);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(45, 16);
            this.label1.TabIndex = 1;
            this.label1.Text = "OBD2 IP";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.BackColor = System.Drawing.Color.Transparent;
            this.label2.Font = new System.Drawing.Font("Franklin Gothic Medium Cond", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.label2.ForeColor = System.Drawing.SystemColors.ControlLightLight;
            this.label2.Location = new System.Drawing.Point(34, 15);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(31, 16);
            this.label2.TabIndex = 2;
            this.label2.Text = "PORT";
            // 
            // ipTextBox
            // 
            this.ipTextBox.Location = new System.Drawing.Point(70, 12);
            this.ipTextBox.Name = "ipTextBox";
            this.ipTextBox.RightToLeft = System.Windows.Forms.RightToLeft.No;
            this.ipTextBox.Size = new System.Drawing.Size(136, 23);
            this.ipTextBox.TabIndex = 3;
            this.ipTextBox.Text = "192.168.0.10";
            // 
            // initConnButton
            // 
            this.initConnButton.BackColor = System.Drawing.Color.White;
            this.initConnButton.Cursor = System.Windows.Forms.Cursors.Hand;
            this.initConnButton.FlatAppearance.BorderColor = System.Drawing.Color.Silver;
            this.initConnButton.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.initConnButton.Font = new System.Drawing.Font("Franklin Gothic Medium Cond", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.initConnButton.Location = new System.Drawing.Point(212, 12);
            this.initConnButton.Name = "initConnButton";
            this.initConnButton.Size = new System.Drawing.Size(76, 52);
            this.initConnButton.TabIndex = 4;
            this.initConnButton.Text = "Init TCP Connection";
            this.initConnButton.UseVisualStyleBackColor = false;
            this.initConnButton.Click += new System.EventHandler(this.initConnButton_Click);
            // 
            // display
            // 
            this.display.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(7)))), ((int)(((byte)(102)))), ((int)(((byte)(173)))));
            this.display.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.display.Font = new System.Drawing.Font("Cascadia Code Light", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.display.ForeColor = System.Drawing.SystemColors.HighlightText;
            this.display.Location = new System.Drawing.Point(7, 15);
            this.display.Margin = new System.Windows.Forms.Padding(3, 3, 10, 3);
            this.display.Name = "display";
            this.display.Size = new System.Drawing.Size(265, 240);
            this.display.TabIndex = 5;
            this.display.Text = "";
            // 
            // askPIDButton
            // 
            this.askPIDButton.BackColor = System.Drawing.Color.White;
            this.askPIDButton.Cursor = System.Windows.Forms.Cursors.Hand;
            this.askPIDButton.FlatAppearance.BorderColor = System.Drawing.Color.Silver;
            this.askPIDButton.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.askPIDButton.Font = new System.Drawing.Font("Franklin Gothic Medium Cond", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.askPIDButton.ForeColor = System.Drawing.SystemColors.ActiveCaptionText;
            this.askPIDButton.Location = new System.Drawing.Point(6, 22);
            this.askPIDButton.Name = "askPIDButton";
            this.askPIDButton.Size = new System.Drawing.Size(120, 23);
            this.askPIDButton.TabIndex = 6;
            this.askPIDButton.Text = "Ask PID";
            this.askPIDButton.UseVisualStyleBackColor = false;
            this.askPIDButton.Click += new System.EventHandler(this.askPIDButton_Click);
            // 
            // buttonStopListening
            // 
            this.buttonStopListening.Cursor = System.Windows.Forms.Cursors.Hand;
            this.buttonStopListening.Location = new System.Drawing.Point(1070, 532);
            this.buttonStopListening.Name = "buttonStopListening";
            this.buttonStopListening.Size = new System.Drawing.Size(132, 23);
            this.buttonStopListening.TabIndex = 7;
            this.buttonStopListening.Text = "Close Connection";
            this.buttonStopListening.UseVisualStyleBackColor = true;
            this.buttonStopListening.Click += new System.EventHandler(this.buttonStopListening_Click);
            // 
            // buttonFuelData
            // 
            this.buttonFuelData.BackColor = System.Drawing.Color.White;
            this.buttonFuelData.FlatAppearance.BorderColor = System.Drawing.Color.Silver;
            this.buttonFuelData.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.buttonFuelData.Font = new System.Drawing.Font("Franklin Gothic Medium Cond", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.buttonFuelData.ForeColor = System.Drawing.SystemColors.ActiveCaptionText;
            this.buttonFuelData.Location = new System.Drawing.Point(151, 22);
            this.buttonFuelData.Name = "buttonFuelData";
            this.buttonFuelData.Size = new System.Drawing.Size(120, 23);
            this.buttonFuelData.TabIndex = 9;
            this.buttonFuelData.Text = "Read Data";
            this.buttonFuelData.UseVisualStyleBackColor = false;
            this.buttonFuelData.Click += new System.EventHandler(this.buttonFuelData_Click);
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.BackColor = System.Drawing.Color.Transparent;
            this.label3.Font = new System.Drawing.Font("Franklin Gothic Medium Cond", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.label3.ForeColor = System.Drawing.SystemColors.ControlLightLight;
            this.label3.Location = new System.Drawing.Point(6, 52);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(55, 16);
            this.label3.TabIndex = 10;
            this.label3.Text = "Drive Style:";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.BackColor = System.Drawing.Color.Transparent;
            this.label4.Font = new System.Drawing.Font("Franklin Gothic Medium Cond", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.label4.ForeColor = System.Drawing.SystemColors.ControlLightLight;
            this.label4.Location = new System.Drawing.Point(6, 101);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(56, 16);
            this.label4.TabIndex = 11;
            this.label4.Text = "Road Type:";
            // 
            // ecoButton
            // 
            this.ecoButton.BackColor = System.Drawing.Color.LightGreen;
            this.ecoButton.FlatAppearance.BorderColor = System.Drawing.Color.Green;
            this.ecoButton.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.ecoButton.Font = new System.Drawing.Font("Franklin Gothic Medium Cond", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.ecoButton.ForeColor = System.Drawing.SystemColors.ActiveCaptionText;
            this.ecoButton.Location = new System.Drawing.Point(6, 73);
            this.ecoButton.Margin = new System.Windows.Forms.Padding(0);
            this.ecoButton.Name = "ecoButton";
            this.ecoButton.Size = new System.Drawing.Size(80, 24);
            this.ecoButton.TabIndex = 12;
            this.ecoButton.Text = "Eco";
            this.ecoButton.UseVisualStyleBackColor = false;
            this.ecoButton.Click += new System.EventHandler(this.ecoButton_Click);
            // 
            // normalButton
            // 
            this.normalButton.BackColor = System.Drawing.SystemColors.ActiveCaption;
            this.normalButton.FlatAppearance.BorderColor = System.Drawing.Color.Navy;
            this.normalButton.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.normalButton.Font = new System.Drawing.Font("Franklin Gothic Medium Cond", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.normalButton.ForeColor = System.Drawing.SystemColors.ActiveCaptionText;
            this.normalButton.Location = new System.Drawing.Point(98, 73);
            this.normalButton.Name = "normalButton";
            this.normalButton.Size = new System.Drawing.Size(80, 24);
            this.normalButton.TabIndex = 13;
            this.normalButton.Text = "Normal";
            this.normalButton.UseVisualStyleBackColor = false;
            this.normalButton.Click += new System.EventHandler(this.normalButton_Click);
            // 
            // sportButton
            // 
            this.sportButton.BackColor = System.Drawing.Color.IndianRed;
            this.sportButton.FlatAppearance.BorderColor = System.Drawing.Color.Maroon;
            this.sportButton.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.sportButton.Font = new System.Drawing.Font("Franklin Gothic Medium Cond", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.sportButton.ForeColor = System.Drawing.SystemColors.ActiveCaptionText;
            this.sportButton.Location = new System.Drawing.Point(190, 73);
            this.sportButton.Name = "sportButton";
            this.sportButton.Size = new System.Drawing.Size(80, 24);
            this.sportButton.TabIndex = 14;
            this.sportButton.Text = "Sport";
            this.sportButton.UseVisualStyleBackColor = false;
            this.sportButton.Click += new System.EventHandler(this.sportButton_Click);
            // 
            // highwayButton
            // 
            this.highwayButton.BackColor = System.Drawing.Color.YellowGreen;
            this.highwayButton.FlatAppearance.BorderColor = System.Drawing.Color.Green;
            this.highwayButton.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.highwayButton.Font = new System.Drawing.Font("Franklin Gothic Medium Cond", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.highwayButton.ForeColor = System.Drawing.SystemColors.ActiveCaptionText;
            this.highwayButton.Location = new System.Drawing.Point(190, 120);
            this.highwayButton.Name = "highwayButton";
            this.highwayButton.Size = new System.Drawing.Size(80, 24);
            this.highwayButton.TabIndex = 17;
            this.highwayButton.Text = "Highway";
            this.highwayButton.UseVisualStyleBackColor = false;
            this.highwayButton.Click += new System.EventHandler(this.highwayButton_Click);
            // 
            // extraButton
            // 
            this.extraButton.BackColor = System.Drawing.Color.Teal;
            this.extraButton.FlatAppearance.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            this.extraButton.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.extraButton.Font = new System.Drawing.Font("Franklin Gothic Medium Cond", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.extraButton.ForeColor = System.Drawing.SystemColors.ActiveCaptionText;
            this.extraButton.Location = new System.Drawing.Point(98, 120);
            this.extraButton.Name = "extraButton";
            this.extraButton.Size = new System.Drawing.Size(80, 24);
            this.extraButton.TabIndex = 16;
            this.extraButton.Text = "Suburban";
            this.extraButton.UseVisualStyleBackColor = false;
            this.extraButton.Click += new System.EventHandler(this.extraButton_Click);
            // 
            // urbanButton
            // 
            this.urbanButton.BackColor = System.Drawing.SystemColors.Info;
            this.urbanButton.FlatAppearance.BorderColor = System.Drawing.Color.Gray;
            this.urbanButton.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.urbanButton.Font = new System.Drawing.Font("Franklin Gothic Medium Cond", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.urbanButton.ForeColor = System.Drawing.SystemColors.ActiveCaptionText;
            this.urbanButton.Location = new System.Drawing.Point(6, 120);
            this.urbanButton.Name = "urbanButton";
            this.urbanButton.Size = new System.Drawing.Size(80, 24);
            this.urbanButton.TabIndex = 15;
            this.urbanButton.Text = "Urban";
            this.urbanButton.UseVisualStyleBackColor = false;
            this.urbanButton.Click += new System.EventHandler(this.urbanButton_Click);
            // 
            // groupBox1
            // 
            this.groupBox1.BackColor = System.Drawing.Color.Transparent;
            this.groupBox1.Controls.Add(this.askPIDButton);
            this.groupBox1.Controls.Add(this.highwayButton);
            this.groupBox1.Controls.Add(this.buttonFuelData);
            this.groupBox1.Controls.Add(this.extraButton);
            this.groupBox1.Controls.Add(this.ecoButton);
            this.groupBox1.Controls.Add(this.urbanButton);
            this.groupBox1.Controls.Add(this.label3);
            this.groupBox1.Controls.Add(this.sportButton);
            this.groupBox1.Controls.Add(this.label4);
            this.groupBox1.Controls.Add(this.normalButton);
            this.groupBox1.ForeColor = System.Drawing.SystemColors.ButtonHighlight;
            this.groupBox1.Location = new System.Drawing.Point(7, 70);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(281, 161);
            this.groupBox1.TabIndex = 19;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Logging Data";
            // 
            // groupBox2
            // 
            this.groupBox2.BackColor = System.Drawing.Color.Transparent;
            this.groupBox2.Controls.Add(this.CSVbutton);
            this.groupBox2.Controls.Add(this.evalDriveButton);
            this.groupBox2.ForeColor = System.Drawing.SystemColors.ButtonHighlight;
            this.groupBox2.Location = new System.Drawing.Point(7, 237);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(281, 57);
            this.groupBox2.TabIndex = 20;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "Active Monitoring";
            // 
            // evalDriveButton
            // 
            this.evalDriveButton.BackColor = System.Drawing.Color.White;
            this.evalDriveButton.Cursor = System.Windows.Forms.Cursors.Hand;
            this.evalDriveButton.FlatAppearance.BorderColor = System.Drawing.Color.Silver;
            this.evalDriveButton.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.evalDriveButton.Font = new System.Drawing.Font("Franklin Gothic Medium Cond", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.evalDriveButton.ForeColor = System.Drawing.SystemColors.ActiveCaptionText;
            this.evalDriveButton.Location = new System.Drawing.Point(6, 22);
            this.evalDriveButton.Name = "evalDriveButton";
            this.evalDriveButton.Size = new System.Drawing.Size(120, 23);
            this.evalDriveButton.TabIndex = 7;
            this.evalDriveButton.Text = "Evaluate Drive Style";
            this.evalDriveButton.UseVisualStyleBackColor = false;
            this.evalDriveButton.Click += new System.EventHandler(this.evalDriveButton_Click);
            // 
            // chart_speed
            // 
            this.chart_speed.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(7)))), ((int)(((byte)(102)))), ((int)(((byte)(173)))));
            this.chart_speed.Enabled = false;
            this.chart_speed.ForeColor = System.Drawing.SystemColors.ActiveCaptionText;
            this.chart_speed.Location = new System.Drawing.Point(315, -5);
            this.chart_speed.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            this.chart_speed.Name = "chart_speed";
            this.chart_speed.Size = new System.Drawing.Size(903, 187);
            this.chart_speed.TabIndex = 22;
            // 
            // chart_rpm
            // 
            this.chart_rpm.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(7)))), ((int)(((byte)(102)))), ((int)(((byte)(173)))));
            this.chart_rpm.Enabled = false;
            this.chart_rpm.ForeColor = System.Drawing.SystemColors.ActiveCaptionText;
            this.chart_rpm.Location = new System.Drawing.Point(315, 172);
            this.chart_rpm.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            this.chart_rpm.Name = "chart_rpm";
            this.chart_rpm.Size = new System.Drawing.Size(903, 187);
            this.chart_rpm.TabIndex = 23;
            // 
            // chart_load
            // 
            this.chart_load.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(7)))), ((int)(((byte)(102)))), ((int)(((byte)(173)))));
            this.chart_load.Enabled = false;
            this.chart_load.ForeColor = System.Drawing.SystemColors.ActiveCaptionText;
            this.chart_load.Location = new System.Drawing.Point(315, 337);
            this.chart_load.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            this.chart_load.Name = "chart_load";
            this.chart_load.Size = new System.Drawing.Size(903, 187);
            this.chart_load.TabIndex = 24;
            // 
            // groupBox3
            // 
            this.groupBox3.Controls.Add(this.display);
            this.groupBox3.ForeColor = System.Drawing.SystemColors.ButtonHighlight;
            this.groupBox3.Location = new System.Drawing.Point(6, 300);
            this.groupBox3.Name = "groupBox3";
            this.groupBox3.Size = new System.Drawing.Size(282, 264);
            this.groupBox3.TabIndex = 25;
            this.groupBox3.TabStop = false;
            this.groupBox3.Text = "Message Display";
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Font = new System.Drawing.Font("Segoe UI", 19.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.label5.ForeColor = System.Drawing.SystemColors.ButtonHighlight;
            this.label5.Location = new System.Drawing.Point(351, 520);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(155, 37);
            this.label5.TabIndex = 27;
            this.label5.Text = "Drive Style: ";
            // 
            // displayDriverStyle
            // 
            this.displayDriverStyle.AutoSize = true;
            this.displayDriverStyle.Font = new System.Drawing.Font("Segoe UI", 19.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.displayDriverStyle.ForeColor = System.Drawing.SystemColors.ButtonHighlight;
            this.displayDriverStyle.Location = new System.Drawing.Point(515, 520);
            this.displayDriverStyle.Name = "displayDriverStyle";
            this.displayDriverStyle.Size = new System.Drawing.Size(0, 37);
            this.displayDriverStyle.TabIndex = 28;
            // 
            // CSVbutton
            // 
            this.CSVbutton.BackColor = System.Drawing.Color.White;
            this.CSVbutton.Cursor = System.Windows.Forms.Cursors.Hand;
            this.CSVbutton.FlatAppearance.BorderColor = System.Drawing.Color.Silver;
            this.CSVbutton.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.CSVbutton.Font = new System.Drawing.Font("Franklin Gothic Medium Cond", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.CSVbutton.ForeColor = System.Drawing.SystemColors.ActiveCaptionText;
            this.CSVbutton.Location = new System.Drawing.Point(151, 22);
            this.CSVbutton.Name = "CSVbutton";
            this.CSVbutton.Size = new System.Drawing.Size(120, 23);
            this.CSVbutton.TabIndex = 8;
            this.CSVbutton.Text = "Write on CSV";
            this.CSVbutton.UseVisualStyleBackColor = false;
            this.CSVbutton.Click += new System.EventHandler(this.CSVbutton_Click);
            // 
            // OBD2Form
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(7)))), ((int)(((byte)(102)))), ((int)(((byte)(173)))));
            this.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Center;
            this.ClientSize = new System.Drawing.Size(1215, 571);
            this.Controls.Add(this.displayDriverStyle);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.groupBox3);
            this.Controls.Add(this.chart_load);
            this.Controls.Add(this.chart_rpm);
            this.Controls.Add(this.chart_speed);
            this.Controls.Add(this.buttonStopListening);
            this.Controls.Add(this.groupBox2);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.initConnButton);
            this.Controls.Add(this.ipTextBox);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.portTextBox);
            this.ForeColor = System.Drawing.SystemColors.ControlText;
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.Name = "OBD2Form";
            this.Text = "OBD II - Logger";
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.groupBox2.ResumeLayout(false);
            this.groupBox3.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

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
        private Label label5;
        private Label displayDriverStyle;
        private Button CSVbutton;
    }
}