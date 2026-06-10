namespace ApplicationAquitaine
{
    partial class Setting
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
            buttonAirQuality = new Button();
            buttonClimate = new Button();
            domainUpDown1 = new DomainUpDown();
            numericUpDownStart = new NumericUpDown();
            numericUpDownEnd = new NumericUpDown();
            labelSetting = new Label();
            ((System.ComponentModel.ISupportInitialize)numericUpDownStart).BeginInit();
            ((System.ComponentModel.ISupportInitialize)numericUpDownEnd).BeginInit();
            SuspendLayout();
            // 
            // buttonAirQuality
            // 
            buttonAirQuality.Font = new Font("Segoe UI", 20F);
            buttonAirQuality.Location = new Point(47, 496);
            buttonAirQuality.Name = "buttonAirQuality";
            buttonAirQuality.Size = new Size(234, 48);
            buttonAirQuality.TabIndex = 0;
            buttonAirQuality.Text = "Qualité de l'air";
            buttonAirQuality.UseVisualStyleBackColor = true;
            // 
            // buttonClimate
            // 
            buttonClimate.Font = new Font("Segoe UI", 20F);
            buttonClimate.Location = new Point(47, 589);
            buttonClimate.Name = "buttonClimate";
            buttonClimate.Size = new Size(234, 48);
            buttonClimate.TabIndex = 3;
            buttonClimate.Text = "Climat";
            buttonClimate.UseVisualStyleBackColor = true;
            buttonClimate.Click += button3_Click;
            // 
            // domainUpDown1
            // 
            domainUpDown1.Font = new Font("Segoe UI", 20F);
            domainUpDown1.Location = new Point(47, 681);
            domainUpDown1.Name = "domainUpDown1";
            domainUpDown1.Size = new Size(281, 43);
            domainUpDown1.TabIndex = 4;
            domainUpDown1.Text = "Catastrophe naturelle";
            // 
            // numericUpDownStart
            // 
            numericUpDownStart.Font = new Font("Segoe UI", 20F);
            numericUpDownStart.Location = new Point(47, 411);
            numericUpDownStart.Maximum = new decimal(new int[] { 2025, 0, 0, 0 });
            numericUpDownStart.Minimum = new decimal(new int[] { 1945, 0, 0, 0 });
            numericUpDownStart.Name = "numericUpDownStart";
            numericUpDownStart.Size = new Size(120, 43);
            numericUpDownStart.TabIndex = 5;
            numericUpDownStart.Value = new decimal(new int[] { 1945, 0, 0, 0 });
            // 
            // numericUpDownEnd
            // 
            numericUpDownEnd.Font = new Font("Segoe UI", 20F);
            numericUpDownEnd.Location = new Point(208, 411);
            numericUpDownEnd.Maximum = new decimal(new int[] { 2026, 0, 0, 0 });
            numericUpDownEnd.Minimum = new decimal(new int[] { 1946, 0, 0, 0 });
            numericUpDownEnd.Name = "numericUpDownEnd";
            numericUpDownEnd.Size = new Size(120, 43);
            numericUpDownEnd.TabIndex = 6;
            numericUpDownEnd.Value = new decimal(new int[] { 1946, 0, 0, 0 });
            // 
            // labelSetting
            // 
            labelSetting.AutoSize = true;
            labelSetting.Location = new Point(127, 304);
            labelSetting.Name = "labelSetting";
            labelSetting.Size = new Size(74, 15);
            labelSetting.TabIndex = 7;
            labelSetting.Text = "Parametrage";
            // 
            // Setting
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            BackgroundImage = Properties.Resources.Background;
            ClientSize = new Size(1924, 1061);
            Controls.Add(labelSetting);
            Controls.Add(numericUpDownEnd);
            Controls.Add(numericUpDownStart);
            Controls.Add(domainUpDown1);
            Controls.Add(buttonClimate);
            Controls.Add(buttonAirQuality);
            Name = "Setting";
            Text = "Setting";
            ((System.ComponentModel.ISupportInitialize)numericUpDownStart).EndInit();
            ((System.ComponentModel.ISupportInitialize)numericUpDownEnd).EndInit();
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private Button buttonAirQuality;
        private Button button1;
        private Button button2;
        private Button buttonClimate;
        private DomainUpDown domainUpDown1;
        private NumericUpDown numericUpDownStart;
        private NumericUpDown numericUpDownEnd;
        private Label labelSetting;
    }
}