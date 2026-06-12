namespace ApplicationAquitaine
{
    partial class Compare
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
            labelGraph = new Label();
            buttonRegion = new Button();
            buttonDownload = new Button();
            labelLegend = new Label();
            labelTitle = new Label();
            buttonRollBack = new Button();
            labelFilter = new Label();
            buttonValidate = new Button();
            labelCompare = new Label();
            numericUpDownEnd = new NumericUpDown();
            numericUpDownStart = new NumericUpDown();
            buttonClimate = new Button();
            buttonAirQuality = new Button();
            numericUpDownNbCompare = new NumericUpDown();
            labelNbCompare = new Label();
            comboBoxDepartment = new ComboBox();
            comboBoxMunicipality = new ComboBox();
            comboBoxSelectedData = new ComboBox();
            comboBoxRisk = new ComboBox();
            comboBoxTypeGraph = new ComboBox();
            labelTypeGraph = new Label();
            labelSelectedData = new Label();
            labelRisk = new Label();
            ((System.ComponentModel.ISupportInitialize)numericUpDownEnd).BeginInit();
            ((System.ComponentModel.ISupportInitialize)numericUpDownStart).BeginInit();
            ((System.ComponentModel.ISupportInitialize)numericUpDownNbCompare).BeginInit();
            SuspendLayout();
            // 
            // labelGraph
            // 
            labelGraph.AutoSize = true;
            labelGraph.Location = new Point(1035, 491);
            labelGraph.Name = "labelGraph";
            labelGraph.Size = new Size(39, 15);
            labelGraph.TabIndex = 37;
            labelGraph.Text = "Graph";
            // 
            // buttonRegion
            // 
            buttonRegion.Font = new Font("Segoe UI", 20F);
            buttonRegion.Location = new Point(327, 414);
            buttonRegion.Name = "buttonRegion";
            buttonRegion.Size = new Size(313, 48);
            buttonRegion.TabIndex = 34;
            buttonRegion.Text = "Voir données régionales";
            buttonRegion.UseVisualStyleBackColor = true;
            buttonRegion.Click += buttonRegion_Click;
            // 
            // buttonDownload
            // 
            buttonDownload.Font = new Font("Segoe UI", 20F);
            buttonDownload.Location = new Point(1678, 895);
            buttonDownload.Name = "buttonDownload";
            buttonDownload.Size = new Size(234, 48);
            buttonDownload.TabIndex = 33;
            buttonDownload.Text = "Télécharger";
            buttonDownload.UseVisualStyleBackColor = true;
            buttonDownload.Click += buttonDownload_Click;
            // 
            // labelLegend
            // 
            labelLegend.AutoSize = true;
            labelLegend.BackColor = Color.FromArgb(169, 24, 50);
            labelLegend.Font = new Font("Segoe UI", 20F);
            labelLegend.ForeColor = SystemColors.ControlLightLight;
            labelLegend.Location = new Point(1536, 266);
            labelLegend.Name = "labelLegend";
            labelLegend.Size = new Size(143, 37);
            labelLegend.TabIndex = 30;
            labelLegend.Text = "Légendes :";
            // 
            // labelTitle
            // 
            labelTitle.AutoSize = true;
            labelTitle.BackColor = Color.FromArgb(169, 24, 50);
            labelTitle.Font = new Font("Segoe UI", 20F);
            labelTitle.ForeColor = SystemColors.ControlLightLight;
            labelTitle.Location = new Point(690, 266);
            labelTitle.Name = "labelTitle";
            labelTitle.Size = new Size(83, 37);
            labelTitle.TabIndex = 29;
            labelTitle.Text = "Titre :";
            // 
            // buttonRollBack
            // 
            buttonRollBack.BackColor = Color.FromArgb(194, 226, 196);
            buttonRollBack.Font = new Font("Segoe UI", 18F);
            buttonRollBack.Location = new Point(51, 929);
            buttonRollBack.Name = "buttonRollBack";
            buttonRollBack.Size = new Size(133, 68);
            buttonRollBack.TabIndex = 28;
            buttonRollBack.Text = "Retour";
            buttonRollBack.UseVisualStyleBackColor = false;
            buttonRollBack.Click += buttonRollBack_Click;
            // 
            // labelFilter
            // 
            labelFilter.AutoSize = true;
            labelFilter.BackColor = Color.FromArgb(169, 24, 50);
            labelFilter.Font = new Font("Segoe UI", 20F);
            labelFilter.ForeColor = SystemColors.ControlLightLight;
            labelFilter.Location = new Point(16, 342);
            labelFilter.Name = "labelFilter";
            labelFilter.Size = new Size(87, 37);
            labelFilter.TabIndex = 27;
            labelFilter.Text = "Filtres";
            // 
            // buttonValidate
            // 
            buttonValidate.BackColor = Color.FromArgb(194, 226, 196);
            buttonValidate.Font = new Font("Segoe UI", 18F);
            buttonValidate.Location = new Point(417, 862);
            buttonValidate.Name = "buttonValidate";
            buttonValidate.Size = new Size(105, 44);
            buttonValidate.TabIndex = 26;
            buttonValidate.Text = "Valider";
            buttonValidate.UseVisualStyleBackColor = false;
            buttonValidate.Click += buttonValidate_Click;
            // 
            // labelCompare
            // 
            labelCompare.AutoSize = true;
            labelCompare.BackColor = Color.FromArgb(169, 24, 50);
            labelCompare.Font = new Font("Segoe UI", 30F);
            labelCompare.ForeColor = SystemColors.ControlLightLight;
            labelCompare.Location = new Point(51, 203);
            labelCompare.Name = "labelCompare";
            labelCompare.Size = new Size(256, 54);
            labelCompare.TabIndex = 25;
            labelCompare.Text = "Comparaison";
            // 
            // numericUpDownEnd
            // 
            numericUpDownEnd.Font = new Font("Segoe UI", 20F);
            numericUpDownEnd.Location = new Point(177, 417);
            numericUpDownEnd.Maximum = new decimal(new int[] { 2026, 0, 0, 0 });
            numericUpDownEnd.Minimum = new decimal(new int[] { 1946, 0, 0, 0 });
            numericUpDownEnd.Name = "numericUpDownEnd";
            numericUpDownEnd.Size = new Size(120, 43);
            numericUpDownEnd.TabIndex = 24;
            numericUpDownEnd.Value = new decimal(new int[] { 1946, 0, 0, 0 });
            // 
            // numericUpDownStart
            // 
            numericUpDownStart.Font = new Font("Segoe UI", 20F);
            numericUpDownStart.Location = new Point(16, 417);
            numericUpDownStart.Maximum = new decimal(new int[] { 2025, 0, 0, 0 });
            numericUpDownStart.Minimum = new decimal(new int[] { 1945, 0, 0, 0 });
            numericUpDownStart.Name = "numericUpDownStart";
            numericUpDownStart.Size = new Size(120, 43);
            numericUpDownStart.TabIndex = 23;
            numericUpDownStart.Value = new decimal(new int[] { 1945, 0, 0, 0 });
            // 
            // buttonClimate
            // 
            buttonClimate.BackColor = Color.FromArgb(194, 226, 196);
            buttonClimate.Font = new Font("Segoe UI", 20F);
            buttonClimate.Location = new Point(16, 595);
            buttonClimate.Name = "buttonClimate";
            buttonClimate.Size = new Size(234, 48);
            buttonClimate.TabIndex = 21;
            buttonClimate.Text = "Climat";
            buttonClimate.UseVisualStyleBackColor = false;
            buttonClimate.Click += buttonClimate_Click;
            // 
            // buttonAirQuality
            // 
            buttonAirQuality.BackColor = Color.FromArgb(194, 226, 196);
            buttonAirQuality.Font = new Font("Segoe UI", 20F);
            buttonAirQuality.Location = new Point(16, 502);
            buttonAirQuality.Name = "buttonAirQuality";
            buttonAirQuality.Size = new Size(234, 48);
            buttonAirQuality.TabIndex = 20;
            buttonAirQuality.Text = "Qualité de l'air";
            buttonAirQuality.UseVisualStyleBackColor = false;
            buttonAirQuality.Click += buttonAirQuality_Click;
            // 
            // numericUpDownNbCompare
            // 
            numericUpDownNbCompare.Font = new Font("Segoe UI", 20F);
            numericUpDownNbCompare.Location = new Point(355, 288);
            numericUpDownNbCompare.Name = "numericUpDownNbCompare";
            numericUpDownNbCompare.RightToLeft = RightToLeft.No;
            numericUpDownNbCompare.Size = new Size(120, 43);
            numericUpDownNbCompare.TabIndex = 38;
            // 
            // labelNbCompare
            // 
            labelNbCompare.AutoSize = true;
            labelNbCompare.BackColor = Color.FromArgb(169, 24, 50);
            labelNbCompare.Font = new Font("Segoe UI", 20F);
            labelNbCompare.ForeColor = SystemColors.ControlLightLight;
            labelNbCompare.Location = new Point(16, 290);
            labelNbCompare.Name = "labelNbCompare";
            labelNbCompare.Size = new Size(323, 37);
            labelNbCompare.TabIndex = 39;
            labelNbCompare.Text = "Nombre de comparaisons";
            // 
            // comboBoxDepartment
            // 
            comboBoxDepartment.Font = new Font("Segoe UI", 20F);
            comboBoxDepartment.FormattingEnabled = true;
            comboBoxDepartment.Location = new Point(327, 502);
            comboBoxDepartment.Name = "comboBoxDepartment";
            comboBoxDepartment.Size = new Size(281, 45);
            comboBoxDepartment.TabIndex = 41;
            comboBoxDepartment.Text = "Choisir département";
            // 
            // comboBoxMunicipality
            // 
            comboBoxMunicipality.Font = new Font("Segoe UI", 20F);
            comboBoxMunicipality.FormattingEnabled = true;
            comboBoxMunicipality.Location = new Point(327, 595);
            comboBoxMunicipality.Name = "comboBoxMunicipality";
            comboBoxMunicipality.Size = new Size(281, 45);
            comboBoxMunicipality.TabIndex = 42;
            comboBoxMunicipality.Text = "Choisir commune";
            // 
            // comboBoxSelectedData
            // 
            comboBoxSelectedData.DropDownStyle = ComboBoxStyle.DropDownList;
            comboBoxSelectedData.Font = new Font("Segoe UI", 20F);
            comboBoxSelectedData.FormattingEnabled = true;
            comboBoxSelectedData.Location = new Point(327, 687);
            comboBoxSelectedData.Name = "comboBoxSelectedData";
            comboBoxSelectedData.Size = new Size(281, 45);
            comboBoxSelectedData.TabIndex = 43;
            // 
            // comboBoxRisk
            // 
            comboBoxRisk.BackColor = Color.FromArgb(194, 226, 196);
            comboBoxRisk.DropDownStyle = ComboBoxStyle.DropDownList;
            comboBoxRisk.Font = new Font("Segoe UI", 20F);
            comboBoxRisk.FormattingEnabled = true;
            comboBoxRisk.Location = new Point(16, 687);
            comboBoxRisk.Name = "comboBoxRisk";
            comboBoxRisk.Size = new Size(281, 45);
            comboBoxRisk.TabIndex = 44;
            // 
            // comboBoxTypeGraph
            // 
            comboBoxTypeGraph.DropDownStyle = ComboBoxStyle.DropDownList;
            comboBoxTypeGraph.Font = new Font("Segoe UI", 20F);
            comboBoxTypeGraph.FormattingEnabled = true;
            comboBoxTypeGraph.Location = new Point(884, 880);
            comboBoxTypeGraph.Name = "comboBoxTypeGraph";
            comboBoxTypeGraph.Size = new Size(281, 45);
            comboBoxTypeGraph.TabIndex = 45;
            // 
            // labelTypeGraph
            // 
            labelTypeGraph.AutoSize = true;
            labelTypeGraph.BackColor = Color.FromArgb(169, 24, 50);
            labelTypeGraph.Font = new Font("Segoe UI", 20F);
            labelTypeGraph.ForeColor = SystemColors.ControlLightLight;
            labelTypeGraph.Location = new Point(614, 880);
            labelTypeGraph.Name = "labelTypeGraph";
            labelTypeGraph.Size = new Size(255, 37);
            labelTypeGraph.TabIndex = 46;
            labelTypeGraph.Text = "Type de Graphique :";
            // 
            // labelSelectedData
            // 
            labelSelectedData.AutoSize = true;
            labelSelectedData.BackColor = Color.FromArgb(169, 24, 50);
            labelSelectedData.Font = new Font("Segoe UI", 20F);
            labelSelectedData.ForeColor = SystemColors.ControlLightLight;
            labelSelectedData.Location = new Point(327, 647);
            labelSelectedData.Name = "labelSelectedData";
            labelSelectedData.Size = new Size(203, 37);
            labelSelectedData.TabIndex = 47;
            labelSelectedData.Text = "Données saisies";
            // 
            // labelRisk
            // 
            labelRisk.AutoSize = true;
            labelRisk.BackColor = Color.FromArgb(169, 24, 50);
            labelRisk.Font = new Font("Segoe UI", 20F);
            labelRisk.ForeColor = SystemColors.ControlLightLight;
            labelRisk.Location = new Point(16, 647);
            labelRisk.Name = "labelRisk";
            labelRisk.Size = new Size(256, 37);
            labelRisk.TabIndex = 48;
            labelRisk.Text = "Type de catastrophe";
            // 
            // Compare
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            BackgroundImage = Properties.Resources.Background;
            ClientSize = new Size(1924, 1061);
            Controls.Add(labelRisk);
            Controls.Add(labelSelectedData);
            Controls.Add(labelTypeGraph);
            Controls.Add(comboBoxTypeGraph);
            Controls.Add(comboBoxRisk);
            Controls.Add(comboBoxSelectedData);
            Controls.Add(comboBoxMunicipality);
            Controls.Add(comboBoxDepartment);
            Controls.Add(labelNbCompare);
            Controls.Add(numericUpDownNbCompare);
            Controls.Add(labelGraph);
            Controls.Add(buttonRegion);
            Controls.Add(buttonDownload);
            Controls.Add(labelLegend);
            Controls.Add(labelTitle);
            Controls.Add(buttonRollBack);
            Controls.Add(labelFilter);
            Controls.Add(buttonValidate);
            Controls.Add(labelCompare);
            Controls.Add(numericUpDownEnd);
            Controls.Add(numericUpDownStart);
            Controls.Add(buttonClimate);
            Controls.Add(buttonAirQuality);
            Name = "Compare";
            Text = "Compare";
            WindowState = FormWindowState.Maximized;
            ((System.ComponentModel.ISupportInitialize)numericUpDownEnd).EndInit();
            ((System.ComponentModel.ISupportInitialize)numericUpDownStart).EndInit();
            ((System.ComponentModel.ISupportInitialize)numericUpDownNbCompare).EndInit();
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private Label labelGraph;
        private Button buttonRegion;
        private Button buttonDownload;
        private DomainUpDown domainUpDownTypeGraph;
        private Label labelLegend;
        private Label labelTitle;
        private Button buttonRollBack;
        private Label labelFilter;
        private Button buttonValidate;
        private Label labelCompare;
        private NumericUpDown numericUpDownEnd;
        private NumericUpDown numericUpDownStart;
        private Button buttonClimate;
        private Button buttonAirQuality;
        private NumericUpDown numericUpDownNbCompare;
        private Label labelNbCompare;
        private ComboBox comboBoxDepartment;
        private ComboBox comboBoxMunicipality;
        private ComboBox comboBoxSelectedData;
        private ComboBox comboBoxRisk;
        private ComboBox comboBoxTypeGraph;
        private Label labelTypeGraph;
        private Label labelSelectedData;
        private Label labelRisk;
    }
}