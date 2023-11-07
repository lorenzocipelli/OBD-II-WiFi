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
            portTextBox = new TextBox();
            label1 = new Label();
            label2 = new Label();
            ipTextBox = new TextBox();
            initConnButton = new Button();
            display = new RichTextBox();
            SuspendLayout();
            // 
            // portTextBox
            // 
            portTextBox.Location = new Point(58, 41);
            portTextBox.Name = "portTextBox";
            portTextBox.Size = new Size(137, 23);
            portTextBox.TabIndex = 0;
            portTextBox.Text = "35000";
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.Location = new Point(2, 44);
            label1.Name = "label1";
            label1.Size = new Size(50, 15);
            label1.TabIndex = 1;
            label1.Text = "OBD2 IP";
            // 
            // label2
            // 
            label2.AutoSize = true;
            label2.Location = new Point(17, 15);
            label2.Name = "label2";
            label2.Size = new Size(35, 15);
            label2.TabIndex = 2;
            label2.Text = "PORT";
            // 
            // ipTextBox
            // 
            ipTextBox.Location = new Point(58, 12);
            ipTextBox.Name = "ipTextBox";
            ipTextBox.Size = new Size(137, 23);
            ipTextBox.TabIndex = 3;
            ipTextBox.Text = "192.168.0.149";
            // 
            // initConnButton
            // 
            initConnButton.Location = new Point(12, 70);
            initConnButton.Name = "initConnButton";
            initConnButton.Size = new Size(183, 23);
            initConnButton.TabIndex = 4;
            initConnButton.Text = "Init TCP Connection";
            initConnButton.UseVisualStyleBackColor = true;
            initConnButton.Click += button1_Click;
            // 
            // display
            // 
            display.Location = new Point(201, 12);
            display.Name = "display";
            display.Size = new Size(782, 81);
            display.TabIndex = 5;
            display.Text = "";
            // 
            // OBD2Form
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(995, 525);
            Controls.Add(display);
            Controls.Add(initConnButton);
            Controls.Add(ipTextBox);
            Controls.Add(label2);
            Controls.Add(label1);
            Controls.Add(portTextBox);
            Name = "OBD2Form";
            Text = "OBD II - Logger";
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
    }
}