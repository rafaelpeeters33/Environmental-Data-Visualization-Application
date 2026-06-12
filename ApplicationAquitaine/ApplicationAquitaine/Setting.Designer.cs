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
            numericUpDownStart = new NumericUpDown();
            numericUpDownEnd = new NumericUpDown();
            labelSetting = new Label();
            buttonValidate = new Button();
            labelFilter = new Label();
            buttonRollBack = new Button();
            labelTitle = new Label();
            labelLegend = new Label();
            buttonCompare = new Button();
            buttonDownload = new Button();
            buttonRegion = new Button();
            labelGraph = new Label();
            comboBoxMunicipality = new ComboBox();
            comboBoxDepartment = new ComboBox();
            labelTypeGraph = new Label();
            comboBoxTypeGraph = new ComboBox();
            labelRisk = new Label();
            comboBox1 = new ComboBox();
            ((System.ComponentModel.ISupportInitialize)numericUpDownStart).BeginInit();
            ((System.ComponentModel.ISupportInitialize)numericUpDownEnd).BeginInit();
            SuspendLayout();
            // 
            // buttonAirQuality
            // 
            buttonAirQuality.BackColor = Color.FromArgb(194, 226, 196);
            buttonAirQuality.Font = new Font("Segoe UI", 20F);
            buttonAirQuality.Location = new Point(12, 496);
            buttonAirQuality.Name = "buttonAirQuality";
            buttonAirQuality.Size = new Size(234, 48);
            buttonAirQuality.TabIndex = 0;
            buttonAirQuality.Text = "Qualité de l'air";
            buttonAirQuality.UseVisualStyleBackColor = false;
            buttonAirQuality.Click += buttonAirQuality_Click;
            // 
            // buttonClimate
            // 
            buttonClimate.BackColor = Color.FromArgb(194, 226, 196);
            buttonClimate.Font = new Font("Segoe UI", 20F);
            buttonClimate.Location = new Point(12, 589);
            buttonClimate.Name = "buttonClimate";
            buttonClimate.Size = new Size(234, 48);
            buttonClimate.TabIndex = 3;
            buttonClimate.Text = "Climat";
            buttonClimate.UseVisualStyleBackColor = false;
            buttonClimate.Click += buttonClimate_Click;
            // 
            // numericUpDownStart
            // 
            numericUpDownStart.Font = new Font("Segoe UI", 20F);
            numericUpDownStart.Location = new Point(12, 411);
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
            numericUpDownEnd.Location = new Point(173, 411);
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
            labelSetting.BackColor = Color.FromArgb(169, 24, 50);
            labelSetting.Font = new Font("Segoe UI", 30F);
            labelSetting.ForeColor = SystemColors.ControlLightLight;
            labelSetting.Location = new Point(47, 197);
            labelSetting.Name = "labelSetting";
            labelSetting.Size = new Size(246, 54);
            labelSetting.TabIndex = 7;
            labelSetting.Text = "Parametrage";
            // 
            // buttonValidate
            // 
            buttonValidate.BackColor = Color.FromArgb(194, 226, 196);
            buttonValidate.Font = new Font("Segoe UI", 18F);
            buttonValidate.Location = new Point(461, 774);
            buttonValidate.Name = "buttonValidate";
            buttonValidate.Size = new Size(105, 44);
            buttonValidate.TabIndex = 8;
            buttonValidate.Text = "Valider";
            buttonValidate.UseVisualStyleBackColor = false;
            buttonValidate.Click += buttonValidate_Click;
            // 
            // labelFilter
            // 
            labelFilter.AutoSize = true;
            labelFilter.BackColor = Color.FromArgb(169, 24, 50);
            labelFilter.Font = new Font("Segoe UI", 20F);
            labelFilter.ForeColor = SystemColors.ControlLightLight;
            labelFilter.Location = new Point(12, 336);
            labelFilter.Name = "labelFilter";
            labelFilter.Size = new Size(87, 37);
            labelFilter.TabIndex = 9;
            labelFilter.Text = "Filtres";
            // 
            // buttonRollBack
            // 
            buttonRollBack.BackColor = Color.FromArgb(194, 226, 196);
            buttonRollBack.Font = new Font("Segoe UI", 18F);
            buttonRollBack.Location = new Point(47, 923);
            buttonRollBack.Name = "buttonRollBack";
            buttonRollBack.Size = new Size(133, 68);
            buttonRollBack.TabIndex = 10;
            buttonRollBack.Text = "Retour";
            buttonRollBack.UseVisualStyleBackColor = false;
            buttonRollBack.Click += buttonRollBack_Click;
            // 
            // labelTitle
            // 
            labelTitle.AutoSize = true;
            labelTitle.BackColor = Color.FromArgb(169, 24, 50);
            labelTitle.Font = new Font("Segoe UI", 20F);
            labelTitle.ForeColor = SystemColors.ControlLightLight;
            labelTitle.Location = new Point(686, 260);
            labelTitle.Name = "labelTitle";
            labelTitle.Size = new Size(83, 37);
            labelTitle.TabIndex = 11;
            labelTitle.Text = "Titre :";
            // 
            // labelLegend
            // 
            labelLegend.AutoSize = true;
            labelLegend.BackColor = Color.FromArgb(169, 24, 50);
            labelLegend.Font = new Font("Segoe UI", 20F);
            labelLegend.ForeColor = SystemColors.ControlLightLight;
            labelLegend.Location = new Point(1532, 260);
            labelLegend.Name = "labelLegend";
            labelLegend.Size = new Size(143, 37);
            labelLegend.TabIndex = 12;
            labelLegend.Text = "Légendes :";
            // 
            // buttonCompare
            // 
            buttonCompare.BackColor = Color.FromArgb(194, 226, 196);
            buttonCompare.Font = new Font("Segoe UI", 18F);
            buttonCompare.Location = new Point(1668, 923);
            buttonCompare.Name = "buttonCompare";
            buttonCompare.Size = new Size(133, 68);
            buttonCompare.TabIndex = 13;
            buttonCompare.Text = "Comparer";
            buttonCompare.UseVisualStyleBackColor = false;
            buttonCompare.Click += buttonCompare_Click;
            // 
            // buttonDownload
            // 
            buttonDownload.Font = new Font("Segoe UI", 20F);
            buttonDownload.Location = new Point(1668, 844);
            buttonDownload.Name = "buttonDownload";
            buttonDownload.Size = new Size(234, 48);
            buttonDownload.TabIndex = 15;
            buttonDownload.Text = "Télécharger";
            buttonDownload.UseVisualStyleBackColor = true;
            buttonDownload.Click += buttonDownload_Click;
            // 
            // buttonRegion
            // 
            buttonRegion.Font = new Font("Segoe UI", 20F);
            buttonRegion.Location = new Point(315, 496);
            buttonRegion.Name = "buttonRegion";
            buttonRegion.Size = new Size(313, 48);
            buttonRegion.TabIndex = 16;
            buttonRegion.Text = "Voir données régionales";
            buttonRegion.UseVisualStyleBackColor = true;
            // 
            // labelGraph
            // 
            labelGraph.AutoSize = true;
            labelGraph.Location = new Point(1031, 485);
            labelGraph.Name = "labelGraph";
            labelGraph.Size = new Size(39, 15);
            labelGraph.TabIndex = 19;
            labelGraph.Text = "Graph";
            // 
            // comboBoxMunicipality
            // 
            comboBoxMunicipality.Font = new Font("Segoe UI", 20F);
            comboBoxMunicipality.FormattingEnabled = true;
            comboBoxMunicipality.Location = new Point(315, 682);
            comboBoxMunicipality.Name = "comboBoxMunicipality";
            comboBoxMunicipality.Size = new Size(281, 45);
            comboBoxMunicipality.TabIndex = 46;
            comboBoxMunicipality.Text = "Choisir commune";
            // 
            // comboBoxDepartment
            // 
            comboBoxDepartment.Font = new Font("Segoe UI", 20F);
            comboBoxDepartment.FormattingEnabled = true;
            comboBoxDepartment.Location = new Point(315, 589);
            comboBoxDepartment.Name = "comboBoxDepartment";
            comboBoxDepartment.Size = new Size(281, 45);
            comboBoxDepartment.TabIndex = 45;
            comboBoxDepartment.Text = "Choisir département";
            // 
            // labelTypeGraph
            // 
            labelTypeGraph.AutoSize = true;
            labelTypeGraph.BackColor = Color.FromArgb(169, 24, 50);
            labelTypeGraph.Font = new Font("Segoe UI", 20F);
            labelTypeGraph.ForeColor = SystemColors.ControlLightLight;
            labelTypeGraph.Location = new Point(646, 883);
            labelTypeGraph.Name = "labelTypeGraph";
            labelTypeGraph.Size = new Size(255, 37);
            labelTypeGraph.TabIndex = 51;
            labelTypeGraph.Text = "Type de Graphique :";
            // 
            // comboBoxTypeGraph
            // 
            comboBoxTypeGraph.DropDownStyle = ComboBoxStyle.DropDownList;
            comboBoxTypeGraph.Font = new Font("Segoe UI", 20F);
            comboBoxTypeGraph.FormattingEnabled = true;
            comboBoxTypeGraph.Location = new Point(916, 883);
            comboBoxTypeGraph.Name = "comboBoxTypeGraph";
            comboBoxTypeGraph.Size = new Size(281, 45);
            comboBoxTypeGraph.TabIndex = 50;
            // 
            // labelRisk
            // 
            labelRisk.AutoSize = true;
            labelRisk.BackColor = Color.FromArgb(169, 24, 50);
            labelRisk.Font = new Font("Segoe UI", 20F);
            labelRisk.ForeColor = SystemColors.ControlLightLight;
            labelRisk.Location = new Point(12, 642);
            labelRisk.Name = "labelRisk";
            labelRisk.Size = new Size(256, 37);
            labelRisk.TabIndex = 55;
            labelRisk.Text = "Type de catastrophe";
            // 
            // comboBox1
            // 
            comboBox1.BackColor = Color.FromArgb(194, 226, 196);
            comboBox1.DropDownStyle = ComboBoxStyle.DropDownList;
            comboBox1.Font = new Font("Segoe UI", 20F);
            comboBox1.FormattingEnabled = true;
            comboBox1.Location = new Point(12, 682);
            comboBox1.Name = "comboBox1";
            comboBox1.Size = new Size(281, 45);
            comboBox1.TabIndex = 53;
            // 
            // Setting
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            BackgroundImage = Properties.Resources.Background;
            ClientSize = new Size(1924, 1061);
            Controls.Add(labelRisk);
            Controls.Add(comboBox1);
            Controls.Add(labelTypeGraph);
            Controls.Add(comboBoxTypeGraph);
            Controls.Add(comboBoxMunicipality);
            Controls.Add(comboBoxDepartment);
            Controls.Add(labelGraph);
            Controls.Add(buttonRegion);
            Controls.Add(buttonDownload);
            Controls.Add(buttonCompare);
            Controls.Add(labelLegend);
            Controls.Add(labelTitle);
            Controls.Add(buttonRollBack);
            Controls.Add(labelFilter);
            Controls.Add(buttonValidate);
            Controls.Add(labelSetting);
            Controls.Add(numericUpDownEnd);
            Controls.Add(numericUpDownStart);
            Controls.Add(buttonClimate);
            Controls.Add(buttonAirQuality);
            Name = "Setting";
            Text = "Setting";
            WindowState = FormWindowState.Maximized;
            ((System.ComponentModel.ISupportInitialize)numericUpDownStart).EndInit();
            ((System.ComponentModel.ISupportInitialize)numericUpDownEnd).EndInit();
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private Button buttonAirQuality;
        private Button buttonRollBack;
        private Button buttonGraphType;
        private Button buttonClimate;
        private NumericUpDown numericUpDownStart;
        private NumericUpDown numericUpDownEnd;
        private Label labelSetting;
        private Button buttonValidate;
        private Label labelFilter;
        private Label labelTitle;
        private Label labelLegend;
        private Button buttonCompare;
        private Button buttonDownload;
        private Button buttonRegion;
        private Label labelGraph;
        private ComboBox comboBoxMunicipality;
        private ComboBox comboBoxDepartment;
        private Label labelTypeGraph;
        private ComboBox comboBoxTypeGraph;
        private Label labelRisk;
        private ComboBox comboBox1;
    }
}