namespace ApplicationAquitaine
{
    partial class Main
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
            buttonContinue = new Button();
            SuspendLayout();
            // 
            // buttonContinue
            // 
            buttonContinue.Font = new Font("Segoe UI", 30F);
            buttonContinue.Location = new Point(130, 600);
            buttonContinue.Name = "buttonContinue";
            buttonContinue.Size = new Size(576, 90);
            buttonContinue.TabIndex = 0;
            buttonContinue.Text = "Continuer    ->";
            buttonContinue.UseVisualStyleBackColor = true;
            buttonContinue.Click += buttonContinue_Click;
            // 
            // Main
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            BackgroundImage = Properties.Resources.Accueil;
            ClientSize = new Size(1924, 1061);
            Controls.Add(buttonContinue);
            Name = "Main";
            Text = "Form1";
            WindowState = FormWindowState.Maximized;
            ResumeLayout(false);
        }

        #endregion

        private Button buttonContinue;
    }
}
