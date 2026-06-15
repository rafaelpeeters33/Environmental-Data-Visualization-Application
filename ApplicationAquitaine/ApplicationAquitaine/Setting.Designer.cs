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
            numericUpDownStart = new NumericUpDown();
            numericUpDownEnd = new NumericUpDown();
            labelSetting = new Label();
            buttonValidate = new Button();
            labelPeriod = new Label();
            buttonRollBack = new Button();
            labelTitle = new Label();
            labelLegend = new Label();
            buttonCompare = new Button();
            buttonDownload = new Button();
            buttonRegion = new Button();
            comboBoxMunicipality = new ComboBox();
            comboBoxDepartment = new ComboBox();
            labelTypeGraph = new Label();
            comboBoxTypeGraph = new ComboBox();
            label2 = new Label();
            label3 = new Label();
            label4 = new Label();
            label6 = new Label();
            label7 = new Label();
            label5 = new Label();
            airQualityButton = new RadioButton();
            floodButton = new RadioButton();
            label9 = new Label();
            fireButton = new RadioButton();
            climateButton = new RadioButton();
            stormButton = new RadioButton();
            regionButton = new RadioButton();
            panel1 = new Panel();
            municipalityButton = new RadioButton();
            departmentButton = new RadioButton();
            pictureBox = new PictureBox();
            ((System.ComponentModel.ISupportInitialize)numericUpDownStart).BeginInit();
            ((System.ComponentModel.ISupportInitialize)numericUpDownEnd).BeginInit();
            panel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)pictureBox).BeginInit();
            SuspendLayout();
            // 
            // numericUpDownStart
            // 
            numericUpDownStart.Font = new Font("Segoe UI", 20F);
            numericUpDownStart.Location = new Point(159, 758);
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
            numericUpDownEnd.Location = new Point(347, 761);
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
            labelSetting.Font = new Font("Segoe UI", 36F, FontStyle.Regular, GraphicsUnit.Point, 0);
            labelSetting.ForeColor = SystemColors.ControlLightLight;
            labelSetting.Location = new Point(23, 180);
            labelSetting.Name = "labelSetting";
            labelSetting.Size = new Size(294, 65);
            labelSetting.TabIndex = 7;
            labelSetting.Text = "Paramétrage";
            // 
            // buttonValidate
            // 
            buttonValidate.BackColor = Color.FromArgb(194, 226, 196);
            buttonValidate.Font = new Font("Segoe UI", 18F);
            buttonValidate.Location = new Point(1258, 896);
            buttonValidate.Name = "buttonValidate";
            buttonValidate.Size = new Size(198, 44);
            buttonValidate.TabIndex = 8;
            buttonValidate.Text = "Valider";
            buttonValidate.UseVisualStyleBackColor = false;
            buttonValidate.Click += buttonValidate_Click;
            // 
            // labelPeriod
            // 
            labelPeriod.AutoSize = true;
            labelPeriod.BackColor = Color.FromArgb(169, 24, 50);
            labelPeriod.Font = new Font("Segoe UI", 20F);
            labelPeriod.ForeColor = SystemColors.ControlLightLight;
            labelPeriod.Location = new Point(30, 761);
            labelPeriod.Name = "labelPeriod";
            labelPeriod.Size = new Size(107, 37);
            labelPeriod.TabIndex = 9;
            labelPeriod.Text = "Période";
            // 
            // buttonRollBack
            // 
            buttonRollBack.BackColor = Color.FromArgb(194, 226, 196);
            buttonRollBack.Font = new Font("Segoe UI", 18F);
            buttonRollBack.Location = new Point(26, 923);
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
            labelTitle.Font = new Font("Segoe UI", 26.25F, FontStyle.Regular, GraphicsUnit.Point, 0);
            labelTitle.ForeColor = SystemColors.ControlLightLight;
            labelTitle.Location = new Point(773, 198);
            labelTitle.Name = "labelTitle";
            labelTitle.Size = new Size(106, 47);
            labelTitle.TabIndex = 11;
            labelTitle.Text = "Titre :";
            // 
            // labelLegend
            // 
            labelLegend.AutoSize = true;
            labelLegend.BackColor = Color.FromArgb(169, 24, 50);
            labelLegend.Font = new Font("Segoe UI", 20F);
            labelLegend.ForeColor = SystemColors.ControlLightLight;
            labelLegend.Location = new Point(1527, 310);
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
            buttonRegion.Font = new Font("Segoe UI", 15.75F, FontStyle.Regular, GraphicsUnit.Point, 0);
            buttonRegion.Location = new Point(30, 606);
            buttonRegion.Name = "buttonRegion";
            buttonRegion.Size = new Size(217, 38);
            buttonRegion.TabIndex = 16;
            buttonRegion.Text = "Nouvelle-Aquitaine";
            buttonRegion.UseVisualStyleBackColor = true;
            // 
            // comboBoxMunicipality
            // 
            comboBoxMunicipality.Font = new Font("Segoe UI", 15.75F, FontStyle.Regular, GraphicsUnit.Point, 0);
            comboBoxMunicipality.FormattingEnabled = true;
            comboBoxMunicipality.Location = new Point(508, 606);
            comboBoxMunicipality.Name = "comboBoxMunicipality";
            comboBoxMunicipality.Size = new Size(221, 38);
            comboBoxMunicipality.TabIndex = 46;
            comboBoxMunicipality.Text = "Commune";
            // 
            // comboBoxDepartment
            // 
            comboBoxDepartment.AutoCompleteMode = AutoCompleteMode.SuggestAppend;
            comboBoxDepartment.AutoCompleteSource = AutoCompleteSource.ListItems;
            comboBoxDepartment.Font = new Font("Segoe UI", 15.75F, FontStyle.Regular, GraphicsUnit.Point, 0);
            comboBoxDepartment.FormattingEnabled = true;
            comboBoxDepartment.Location = new Point(275, 606);
            comboBoxDepartment.Name = "comboBoxDepartment";
            comboBoxDepartment.Size = new Size(205, 38);
            comboBoxDepartment.TabIndex = 45;
            comboBoxDepartment.TabStop = false;
            comboBoxDepartment.Text = "Département";
            comboBoxDepartment.Leave += comboBoxDepartment_Leave;
            // 
            // labelTypeGraph
            // 
            labelTypeGraph.AutoSize = true;
            labelTypeGraph.BackColor = Color.FromArgb(169, 24, 50);
            labelTypeGraph.Font = new Font("Segoe UI", 20F);
            labelTypeGraph.ForeColor = SystemColors.ControlLightLight;
            labelTypeGraph.Location = new Point(648, 895);
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
            comboBoxTypeGraph.Items.AddRange(new object[] { "Diagramme baton" });
            comboBoxTypeGraph.Location = new Point(944, 895);
            comboBoxTypeGraph.Name = "comboBoxTypeGraph";
            comboBoxTypeGraph.Size = new Size(281, 45);
            comboBoxTypeGraph.TabIndex = 50;
            // 
            // label2
            // 
            label2.Location = new Point(26, 350);
            label2.Name = "label2";
            label2.Size = new Size(654, 10);
            label2.TabIndex = 58;
            // 
            // label3
            // 
            label3.AutoSize = true;
            label3.BackColor = Color.FromArgb(169, 24, 50);
            label3.Font = new Font("Segoe UI", 26.25F, FontStyle.Regular, GraphicsUnit.Point, 0);
            label3.ForeColor = SystemColors.ControlLightLight;
            label3.Location = new Point(23, 284);
            label3.Name = "label3";
            label3.Size = new Size(110, 47);
            label3.TabIndex = 59;
            label3.Text = "Filtres";
            // 
            // label4
            // 
            label4.AutoSize = true;
            label4.BackColor = Color.FromArgb(169, 24, 50);
            label4.Font = new Font("Segoe UI", 20F);
            label4.ForeColor = SystemColors.ControlLightLight;
            label4.Location = new Point(22, 377);
            label4.Name = "label4";
            label4.Size = new Size(121, 37);
            label4.TabIndex = 60;
            label4.Text = "Données";
            // 
            // label6
            // 
            label6.AutoSize = true;
            label6.BackColor = Color.FromArgb(169, 24, 50);
            label6.Font = new Font("Segoe UI", 20F);
            label6.ForeColor = SystemColors.ControlLightLight;
            label6.Location = new Point(26, 555);
            label6.Name = "label6";
            label6.Size = new Size(100, 37);
            label6.TabIndex = 62;
            label6.Text = "Échelle";
            // 
            // label7
            // 
            label7.Location = new Point(30, 734);
            label7.Name = "label7";
            label7.Size = new Size(650, 2);
            label7.TabIndex = 63;
            // 
            // label5
            // 
            label5.Location = new Point(27, 831);
            label5.Name = "label5";
            label5.Size = new Size(654, 10);
            label5.TabIndex = 61;
            // 
            // airQualityButton
            // 
            airQualityButton.Appearance = Appearance.Button;
            airQualityButton.BackColor = Color.FromArgb(194, 226, 196);
            airQualityButton.FlatAppearance.BorderColor = Color.White;
            airQualityButton.FlatAppearance.CheckedBackColor = Color.FromArgb(155, 181, 157);
            airQualityButton.FlatStyle = FlatStyle.Flat;
            airQualityButton.Font = new Font("Segoe UI", 18F, FontStyle.Regular, GraphicsUnit.Point, 0);
            airQualityButton.ForeColor = Color.Black;
            airQualityButton.Location = new Point(155, 409);
            airQualityButton.Name = "airQualityButton";
            airQualityButton.Size = new Size(187, 43);
            airQualityButton.TabIndex = 68;
            airQualityButton.TabStop = true;
            airQualityButton.Text = "Qualité de l'air";
            airQualityButton.TextAlign = ContentAlignment.MiddleCenter;
            airQualityButton.UseVisualStyleBackColor = false;
            // 
            // floodButton
            // 
            floodButton.Appearance = Appearance.Button;
            floodButton.BackColor = Color.FromArgb(194, 226, 196);
            floodButton.FlatAppearance.BorderColor = Color.White;
            floodButton.FlatAppearance.CheckedBackColor = Color.FromArgb(155, 181, 157);
            floodButton.FlatStyle = FlatStyle.Flat;
            floodButton.Font = new Font("Segoe UI", 18F, FontStyle.Regular, GraphicsUnit.Point, 0);
            floodButton.ForeColor = Color.Black;
            floodButton.Location = new Point(261, 468);
            floodButton.Name = "floodButton";
            floodButton.Size = new Size(187, 43);
            floodButton.TabIndex = 72;
            floodButton.TabStop = true;
            floodButton.Text = "Inondations";
            floodButton.TextAlign = ContentAlignment.MiddleCenter;
            floodButton.UseVisualStyleBackColor = false;
            // 
            // label9
            // 
            label9.Location = new Point(22, 539);
            label9.Name = "label9";
            label9.Size = new Size(650, 2);
            label9.TabIndex = 74;
            // 
            // fireButton
            // 
            fireButton.Appearance = Appearance.Button;
            fireButton.BackColor = Color.FromArgb(194, 226, 196);
            fireButton.FlatAppearance.BorderColor = Color.White;
            fireButton.FlatAppearance.CheckedBackColor = Color.FromArgb(155, 181, 157);
            fireButton.FlatStyle = FlatStyle.Flat;
            fireButton.Font = new Font("Segoe UI", 18F, FontStyle.Regular, GraphicsUnit.Point, 0);
            fireButton.ForeColor = Color.Black;
            fireButton.Location = new Point(30, 466);
            fireButton.Name = "fireButton";
            fireButton.Size = new Size(187, 43);
            fireButton.TabIndex = 76;
            fireButton.TabStop = true;
            fireButton.Text = "Incendies";
            fireButton.TextAlign = ContentAlignment.MiddleCenter;
            fireButton.UseVisualStyleBackColor = false;
            // 
            // climateButton
            // 
            climateButton.Appearance = Appearance.Button;
            climateButton.BackColor = Color.FromArgb(194, 226, 196);
            climateButton.FlatAppearance.BorderColor = Color.White;
            climateButton.FlatAppearance.CheckedBackColor = Color.FromArgb(155, 181, 157);
            climateButton.FlatStyle = FlatStyle.Flat;
            climateButton.Font = new Font("Segoe UI", 18F, FontStyle.Regular, GraphicsUnit.Point, 0);
            climateButton.ForeColor = Color.Black;
            climateButton.Location = new Point(406, 409);
            climateButton.Name = "climateButton";
            climateButton.Size = new Size(187, 43);
            climateButton.TabIndex = 77;
            climateButton.TabStop = true;
            climateButton.Text = "Climat";
            climateButton.TextAlign = ContentAlignment.MiddleCenter;
            climateButton.UseVisualStyleBackColor = false;
            // 
            // stormButton
            // 
            stormButton.Appearance = Appearance.Button;
            stormButton.BackColor = Color.FromArgb(194, 226, 196);
            stormButton.FlatAppearance.BorderColor = Color.White;
            stormButton.FlatAppearance.CheckedBackColor = Color.FromArgb(155, 181, 157);
            stormButton.FlatStyle = FlatStyle.Flat;
            stormButton.Font = new Font("Segoe UI", 18F, FontStyle.Regular, GraphicsUnit.Point, 0);
            stormButton.ForeColor = Color.Black;
            stormButton.Location = new Point(493, 468);
            stormButton.Name = "stormButton";
            stormButton.Size = new Size(187, 43);
            stormButton.TabIndex = 78;
            stormButton.TabStop = true;
            stormButton.Text = "Tempêtes";
            stormButton.TextAlign = ContentAlignment.MiddleCenter;
            stormButton.UseVisualStyleBackColor = false;
            // 
            // regionButton
            // 
            regionButton.Appearance = Appearance.Button;
            regionButton.BackColor = Color.FromArgb(194, 226, 196);
            regionButton.FlatAppearance.BorderColor = Color.White;
            regionButton.FlatAppearance.CheckedBackColor = Color.FromArgb(155, 181, 157);
            regionButton.FlatStyle = FlatStyle.Flat;
            regionButton.Font = new Font("Segoe UI", 12F, FontStyle.Regular, GraphicsUnit.Point, 0);
            regionButton.ForeColor = Color.Black;
            regionButton.ImageAlign = ContentAlignment.TopCenter;
            regionButton.Location = new Point(28, 3);
            regionButton.Name = "regionButton";
            regionButton.Size = new Size(159, 29);
            regionButton.TabIndex = 80;
            regionButton.TabStop = true;
            regionButton.Text = "Valider";
            regionButton.TextAlign = ContentAlignment.MiddleCenter;
            regionButton.UseVisualStyleBackColor = false;
            // 
            // panel1
            // 
            panel1.BackColor = Color.FromArgb(169, 24, 50);
            panel1.Controls.Add(municipalityButton);
            panel1.Controls.Add(departmentButton);
            panel1.Controls.Add(regionButton);
            panel1.Location = new Point(30, 650);
            panel1.Name = "panel1";
            panel1.Size = new Size(699, 38);
            panel1.TabIndex = 80;
            // 
            // municipalityButton
            // 
            municipalityButton.Appearance = Appearance.Button;
            municipalityButton.BackColor = Color.FromArgb(194, 226, 196);
            municipalityButton.FlatAppearance.BorderColor = Color.White;
            municipalityButton.FlatAppearance.CheckedBackColor = Color.FromArgb(155, 181, 157);
            municipalityButton.FlatStyle = FlatStyle.Flat;
            municipalityButton.Font = new Font("Segoe UI", 12F, FontStyle.Regular, GraphicsUnit.Point, 0);
            municipalityButton.ForeColor = Color.Black;
            municipalityButton.ImageAlign = ContentAlignment.TopCenter;
            municipalityButton.Location = new Point(506, 3);
            municipalityButton.Name = "municipalityButton";
            municipalityButton.Size = new Size(159, 29);
            municipalityButton.TabIndex = 81;
            municipalityButton.TabStop = true;
            municipalityButton.Text = "Valider";
            municipalityButton.TextAlign = ContentAlignment.MiddleCenter;
            municipalityButton.UseVisualStyleBackColor = false;
            // 
            // departmentButton
            // 
            departmentButton.Appearance = Appearance.Button;
            departmentButton.BackColor = Color.FromArgb(194, 226, 196);
            departmentButton.FlatAppearance.BorderColor = Color.White;
            departmentButton.FlatAppearance.CheckedBackColor = Color.FromArgb(155, 181, 157);
            departmentButton.FlatStyle = FlatStyle.Flat;
            departmentButton.Font = new Font("Segoe UI", 12F, FontStyle.Regular, GraphicsUnit.Point, 0);
            departmentButton.ForeColor = Color.Black;
            departmentButton.ImageAlign = ContentAlignment.TopCenter;
            departmentButton.Location = new Point(259, 3);
            departmentButton.Name = "departmentButton";
            departmentButton.Size = new Size(159, 29);
            departmentButton.TabIndex = 81;
            departmentButton.TabStop = true;
            departmentButton.Text = "Valider";
            departmentButton.TextAlign = ContentAlignment.MiddleCenter;
            departmentButton.UseVisualStyleBackColor = false;
            // 
            // pictureBox
            // 
            pictureBox.Location = new Point(773, 261);
            pictureBox.Name = "pictureBox";
            pictureBox.Size = new Size(650, 580);
            pictureBox.TabIndex = 81;
            pictureBox.TabStop = false;
            // 
            // Setting
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            BackgroundImage = Properties.Resources.Background;
            ClientSize = new Size(1924, 1061);
            Controls.Add(pictureBox);
            Controls.Add(panel1);
            Controls.Add(stormButton);
            Controls.Add(climateButton);
            Controls.Add(fireButton);
            Controls.Add(label9);
            Controls.Add(floodButton);
            Controls.Add(airQualityButton);
            Controls.Add(label7);
            Controls.Add(label6);
            Controls.Add(label5);
            Controls.Add(label4);
            Controls.Add(label3);
            Controls.Add(label2);
            Controls.Add(labelTypeGraph);
            Controls.Add(comboBoxTypeGraph);
            Controls.Add(comboBoxMunicipality);
            Controls.Add(comboBoxDepartment);
            Controls.Add(buttonRegion);
            Controls.Add(buttonDownload);
            Controls.Add(buttonCompare);
            Controls.Add(labelLegend);
            Controls.Add(labelTitle);
            Controls.Add(buttonRollBack);
            Controls.Add(labelPeriod);
            Controls.Add(buttonValidate);
            Controls.Add(labelSetting);
            Controls.Add(numericUpDownEnd);
            Controls.Add(numericUpDownStart);
            Name = "Setting";
            Text = "Setting";
            WindowState = FormWindowState.Maximized;
            ((System.ComponentModel.ISupportInitialize)numericUpDownStart).EndInit();
            ((System.ComponentModel.ISupportInitialize)numericUpDownEnd).EndInit();
            panel1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)pictureBox).EndInit();
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion
        private Button buttonRollBack;
        private Button buttonGraphType;
        private NumericUpDown numericUpDownStart;
        private NumericUpDown numericUpDownEnd;
        private Label labelSetting;
        private Button buttonValidate;
        private Label labelPeriod;
        private Label labelTitle;
        private Label labelLegend;
        private Button buttonCompare;
        private Button buttonDownload;
        private Button buttonRegion;
        private ComboBox comboBoxMunicipality;
        private ComboBox comboBoxDepartment;
        private Label labelTypeGraph;
        private ComboBox comboBoxTypeGraph;
        private Label label2;
        private Label label3;
        private Label label4;
        private Label label6;
        private Label label7;
        private Label label5;
        private RadioButton airQualityButton;
        private RadioButton floodButton;
        private Label label9;
        private RadioButton fireButton;
        private RadioButton climateButton;
        private RadioButton stormButton;
        private RadioButton regionButton;
        private Panel panel1;
        private RadioButton municipalityButton;
        private RadioButton departmentButton;
        private PictureBox pictureBox;
    }
}