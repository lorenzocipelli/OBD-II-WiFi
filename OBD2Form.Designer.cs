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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(OBD2Form));
            this.portTextBox = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.ipTextBox = new System.Windows.Forms.TextBox();
            this.initConnButton = new System.Windows.Forms.Button();
            this.display = new System.Windows.Forms.RichTextBox();
            this.askPIDButton = new System.Windows.Forms.Button();
            this.buttonStopListening = new System.Windows.Forms.Button();
            this.button1 = new System.Windows.Forms.Button();
            this.buttonFuelData = new System.Windows.Forms.Button();
            this.label3 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.ecoButton = new System.Windows.Forms.Button();
            this.normalButton = new System.Windows.Forms.Button();
            this.sportButton = new System.Windows.Forms.Button();
            this.highwayButton = new System.Windows.Forms.Button();
            this.extraButton = new System.Windows.Forms.Button();
            this.urbanButton = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // portTextBox
            // 
            this.portTextBox.Location = new System.Drawing.Point(58, 41);
            this.portTextBox.Name = "portTextBox";
            this.portTextBox.Size = new System.Drawing.Size(137, 23);
            this.portTextBox.TabIndex = 0;
            this.portTextBox.Text = "35000";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.BackColor = System.Drawing.Color.Transparent;
            this.label1.Font = new System.Drawing.Font("Franklin Gothic Medium Cond", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.label1.ForeColor = System.Drawing.SystemColors.ControlLightLight;
            this.label1.Location = new System.Drawing.Point(7, 44);
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
            this.label2.Location = new System.Drawing.Point(22, 15);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(31, 16);
            this.label2.TabIndex = 2;
            this.label2.Text = "PORT";
            // 
            // ipTextBox
            // 
            this.ipTextBox.Location = new System.Drawing.Point(58, 12);
            this.ipTextBox.Name = "ipTextBox";
            this.ipTextBox.Size = new System.Drawing.Size(137, 23);
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
            this.initConnButton.Location = new System.Drawing.Point(12, 70);
            this.initConnButton.Name = "initConnButton";
            this.initConnButton.Size = new System.Drawing.Size(183, 23);
            this.initConnButton.TabIndex = 4;
            this.initConnButton.Text = "Init TCP Connection";
            this.initConnButton.UseVisualStyleBackColor = false;
            this.initConnButton.Click += new System.EventHandler(this.initConnButton_Click);
            // 
            // display
            // 
            this.display.BackColor = System.Drawing.Color.White;
            this.display.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.display.Font = new System.Drawing.Font("Cascadia Code Light", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.display.ForeColor = System.Drawing.SystemColors.InfoText;
            this.display.Location = new System.Drawing.Point(241, 12);
            this.display.Margin = new System.Windows.Forms.Padding(3, 3, 10, 3);
            this.display.Name = "display";
            this.display.Size = new System.Drawing.Size(500, 376);
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
            this.askPIDButton.Location = new System.Drawing.Point(13, 101);
            this.askPIDButton.Name = "askPIDButton";
            this.askPIDButton.Size = new System.Drawing.Size(85, 23);
            this.askPIDButton.TabIndex = 6;
            this.askPIDButton.Text = "Ask PID";
            this.askPIDButton.UseVisualStyleBackColor = false;
            this.askPIDButton.Click += new System.EventHandler(this.askPIDButton_Click);
            // 
            // buttonStopListening
            // 
            this.buttonStopListening.Cursor = System.Windows.Forms.Cursors.Hand;
            this.buttonStopListening.Location = new System.Drawing.Point(609, 394);
            this.buttonStopListening.Name = "buttonStopListening";
            this.buttonStopListening.Size = new System.Drawing.Size(132, 23);
            this.buttonStopListening.TabIndex = 7;
            this.buttonStopListening.Text = "Close Connection";
            this.buttonStopListening.UseVisualStyleBackColor = true;
            this.buttonStopListening.Click += new System.EventHandler(this.buttonStopListening_Click_1);
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(517, 394);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(75, 23);
            this.button1.TabIndex = 8;
            this.button1.Text = "button1";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // buttonFuelData
            // 
            this.buttonFuelData.BackColor = System.Drawing.Color.White;
            this.buttonFuelData.FlatAppearance.BorderColor = System.Drawing.Color.Silver;
            this.buttonFuelData.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.buttonFuelData.Font = new System.Drawing.Font("Franklin Gothic Medium Cond", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.buttonFuelData.Location = new System.Drawing.Point(104, 101);
            this.buttonFuelData.Name = "buttonFuelData";
            this.buttonFuelData.Size = new System.Drawing.Size(91, 23);
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
            this.label3.Location = new System.Drawing.Point(17, 137);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(71, 16);
            this.label3.TabIndex = 10;
            this.label3.Text = "Stile di Guida:";
            this.label3.Click += new System.EventHandler(this.label3_Click);
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.BackColor = System.Drawing.Color.Transparent;
            this.label4.Font = new System.Drawing.Font("Franklin Gothic Medium Cond", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.label4.ForeColor = System.Drawing.SystemColors.ControlLightLight;
            this.label4.Location = new System.Drawing.Point(17, 192);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(105, 16);
            this.label4.TabIndex = 11;
            this.label4.Text = "Tipologia di Percorso:";
            // 
            // ecoButton
            // 
            this.ecoButton.BackColor = System.Drawing.Color.LightGreen;
            this.ecoButton.FlatAppearance.BorderColor = System.Drawing.Color.Green;
            this.ecoButton.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.ecoButton.Font = new System.Drawing.Font("Franklin Gothic Medium Cond", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.ecoButton.Location = new System.Drawing.Point(13, 155);
            this.ecoButton.Margin = new System.Windows.Forms.Padding(0);
            this.ecoButton.Name = "ecoButton";
            this.ecoButton.Size = new System.Drawing.Size(52, 24);
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
            this.normalButton.Location = new System.Drawing.Point(71, 155);
            this.normalButton.Name = "normalButton";
            this.normalButton.Size = new System.Drawing.Size(62, 24);
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
            this.sportButton.ForeColor = System.Drawing.SystemColors.ControlText;
            this.sportButton.Location = new System.Drawing.Point(139, 155);
            this.sportButton.Name = "sportButton";
            this.sportButton.Size = new System.Drawing.Size(52, 24);
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
            this.highwayButton.Location = new System.Drawing.Point(151, 210);
            this.highwayButton.Name = "highwayButton";
            this.highwayButton.Size = new System.Drawing.Size(66, 24);
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
            this.extraButton.Location = new System.Drawing.Point(71, 210);
            this.extraButton.Name = "extraButton";
            this.extraButton.Size = new System.Drawing.Size(74, 24);
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
            this.urbanButton.Location = new System.Drawing.Point(13, 210);
            this.urbanButton.Name = "urbanButton";
            this.urbanButton.Size = new System.Drawing.Size(52, 24);
            this.urbanButton.TabIndex = 15;
            this.urbanButton.Text = "Urban";
            this.urbanButton.UseVisualStyleBackColor = false;
            this.urbanButton.Click += new System.EventHandler(this.urbanButton_Click);
            // 
            // OBD2Form
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.Lavender;
            this.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("$this.BackgroundImage")));
            this.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Center;
            this.ClientSize = new System.Drawing.Size(776, 429);
            this.Controls.Add(this.highwayButton);
            this.Controls.Add(this.extraButton);
            this.Controls.Add(this.urbanButton);
            this.Controls.Add(this.sportButton);
            this.Controls.Add(this.normalButton);
            this.Controls.Add(this.ecoButton);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.buttonFuelData);
            this.Controls.Add(this.button1);
            this.Controls.Add(this.buttonStopListening);
            this.Controls.Add(this.askPIDButton);
            this.Controls.Add(this.display);
            this.Controls.Add(this.initConnButton);
            this.Controls.Add(this.ipTextBox);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.portTextBox);
            this.ForeColor = System.Drawing.SystemColors.ControlText;
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.Name = "OBD2Form";
            this.Text = "OBD II - Logger";
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
        private Button button1;
        private Button buttonFuelData;
        private Label label3;
        private Label label4;
        private Button ecoButton;
        private Button normalButton;
        private Button sportButton;
        private Button highwayButton;
        private Button extraButton;
        private Button urbanButton;
    }
}