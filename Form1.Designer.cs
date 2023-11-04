namespace OBD_II_WiFi
{
    partial class Form1
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
            textBox1 = new TextBox();
            label1 = new Label();
            label2 = new Label();
            textBox2 = new TextBox();
            button1 = new Button();
            display = new RichTextBox();
            SuspendLayout();
            // 
            // textBox1
            // 
            textBox1.Location = new Point(58, 41);
            textBox1.Name = "textBox1";
            textBox1.Size = new Size(137, 23);
            textBox1.TabIndex = 0;
            textBox1.Text = "35000";
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
            // textBox2
            // 
            textBox2.Location = new Point(58, 12);
            textBox2.Name = "textBox2";
            textBox2.Size = new Size(137, 23);
            textBox2.TabIndex = 3;
            textBox2.Text = "192.168.0.149";
            // 
            // button1
            // 
            button1.Location = new Point(12, 70);
            button1.Name = "button1";
            button1.Size = new Size(183, 23);
            button1.TabIndex = 4;
            button1.Text = "Connect";
            button1.UseVisualStyleBackColor = true;
            button1.Click += button1_Click;
            // 
            // display
            // 
            display.Location = new Point(201, 12);
            display.Name = "display";
            display.Size = new Size(782, 81);
            display.TabIndex = 5;
            display.Text = "";
            // 
            // Form1
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(995, 525);
            Controls.Add(display);
            Controls.Add(button1);
            Controls.Add(textBox2);
            Controls.Add(label2);
            Controls.Add(label1);
            Controls.Add(textBox1);
            Name = "Form1";
            Text = "OBD II - Logger";
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private TextBox textBox1;
        private Label label1;
        private Label label2;
        private TextBox textBox2;
        private Button button1;
        private RichTextBox display;
    }
}