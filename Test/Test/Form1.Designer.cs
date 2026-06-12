namespace Test
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
            buttonGo = new Button();
            res = new PictureBox();
            textBox1 = new TextBox();
            ((System.ComponentModel.ISupportInitialize)res).BeginInit();
            SuspendLayout();
            // 
            // buttonGo
            // 
            buttonGo.Location = new Point(85, 52);
            buttonGo.Name = "buttonGo";
            buttonGo.Size = new Size(75, 23);
            buttonGo.TabIndex = 0;
            buttonGo.Text = "GO";
            buttonGo.UseVisualStyleBackColor = true;
            buttonGo.Click += buttonGo_Click;
            // 
            // res
            // 
            res.Location = new Point(262, 103);
            res.Name = "res";
            res.Size = new Size(293, 196);
            res.TabIndex = 1;
            res.TabStop = false;
            res.Click += res_Click;
            // 
            // textBox1
            // 
            textBox1.Location = new Point(208, 52);
            textBox1.Name = "textBox1";
            textBox1.Size = new Size(100, 23);
            textBox1.TabIndex = 2;
            textBox1.TextChanged += textBox1_TextChanged;
            // 
            // Form1
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(800, 450);
            Controls.Add(textBox1);
            Controls.Add(res);
            Controls.Add(buttonGo);
            Name = "Form1";
            Text = "Form1";
            ((System.ComponentModel.ISupportInitialize)res).EndInit();
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private Button buttonGo;
        private PictureBox res;
        private TextBox textBox1;
    }
}
