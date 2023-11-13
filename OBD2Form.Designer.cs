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
            this.label1.Location = new System.Drawing.Point(2, 44);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(50, 15);
            this.label1.TabIndex = 1;
            this.label1.Text = "OBD2 IP";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(17, 15);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(35, 15);
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
            this.initConnButton.Location = new System.Drawing.Point(12, 70);
            this.initConnButton.Name = "initConnButton";
            this.initConnButton.Size = new System.Drawing.Size(183, 23);
            this.initConnButton.TabIndex = 4;
            this.initConnButton.Text = "Init TCP Connection";
            this.initConnButton.UseVisualStyleBackColor = true;
            this.initConnButton.Click += new System.EventHandler(this.initConnButton_Click);
            // 
            // display
            // 
            this.display.Location = new System.Drawing.Point(241, 12);
            this.display.Name = "display";
            this.display.Size = new System.Drawing.Size(500, 152);
            this.display.TabIndex = 5;
            this.display.Text = "";
            // 
            // askPIDButton
            // 
            this.askPIDButton.Location = new System.Drawing.Point(13, 101);
            this.askPIDButton.Name = "askPIDButton";
            this.askPIDButton.Size = new System.Drawing.Size(96, 23);
            this.askPIDButton.TabIndex = 6;
            this.askPIDButton.Text = "Ask PID";
            this.askPIDButton.UseVisualStyleBackColor = true;
            this.askPIDButton.Click += new System.EventHandler(this.askPIDButton_Click);
            // 
            // buttonStopListening
            // 
            this.buttonStopListening.Location = new System.Drawing.Point(609, 170);
            this.buttonStopListening.Name = "buttonStopListening";
            this.buttonStopListening.Size = new System.Drawing.Size(132, 23);
            this.buttonStopListening.TabIndex = 7;
            this.buttonStopListening.Text = "Close Connection";
            this.buttonStopListening.UseVisualStyleBackColor = true;
            this.buttonStopListening.Click += new System.EventHandler(this.buttonStopListening_Click);
            // 
            // OBD2Form
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(753, 429);
            this.Controls.Add(this.buttonStopListening);
            this.Controls.Add(this.askPIDButton);
            this.Controls.Add(this.display);
            this.Controls.Add(this.initConnButton);
            this.Controls.Add(this.ipTextBox);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.portTextBox);
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
    }
}