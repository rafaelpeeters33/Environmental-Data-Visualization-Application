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
            label9 = new Label();
            pictureBox = new PictureBox();
            DataComboBox = new ComboBox();
            dateTimePickerStart = new DateTimePicker();
            dateTimePickerEnd = new DateTimePicker();
            labelAgregation = new Label();
            comboBoxAgregation = new ComboBox();
            ((System.ComponentModel.ISupportInitialize)pictureBox).BeginInit();
            SuspendLayout();
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
            comboBoxMunicipality.SelectedIndexChanged += comboBoxMunicipality_SelectedIndexChanged;
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
            comboBoxDepartment.SelectedIndexChanged += comboBoxDepartment_SelectedIndexChanged_1;
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
            comboBoxTypeGraph.SelectedIndexChanged += comboBoxTypeGraph_SelectedIndexChanged;
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
            // label9
            // 
            label9.Location = new Point(22, 539);
            label9.Name = "label9";
            label9.Size = new Size(650, 2);
            label9.TabIndex = 74;
            // 
            // pictureBox
            // 
            pictureBox.Location = new Point(773, 261);
            pictureBox.Name = "pictureBox";
            pictureBox.Size = new Size(650, 580);
            pictureBox.TabIndex = 81;
            pictureBox.TabStop = false;
            pictureBox.Click += pictureBox_Click;
            // 
            // DataComboBox
            // 
            DataComboBox.Font = new Font("Segoe UI", 15.75F);
            DataComboBox.FormattingEnabled = true;
            DataComboBox.Location = new Point(149, 380);
            DataComboBox.Name = "DataComboBox";
            DataComboBox.Size = new Size(205, 38);
            DataComboBox.TabIndex = 82;
            DataComboBox.SelectedIndexChanged += DataComboBox_SelectedIndexChanged;
            // 
            // dateTimePickerStart
            // 
            dateTimePickerStart.Location = new Point(187, 775);
            dateTimePickerStart.Name = "dateTimePickerStart";
            dateTimePickerStart.Size = new Size(200, 23);
            dateTimePickerStart.TabIndex = 83;
            dateTimePickerStart.ValueChanged += dateTimePickerStart_ValueChanged;
            // 
            // dateTimePickerEnd
            // 
            dateTimePickerEnd.Location = new Point(436, 773);
            dateTimePickerEnd.Name = "dateTimePickerEnd";
            dateTimePickerEnd.Size = new Size(200, 23);
            dateTimePickerEnd.TabIndex = 84;
            dateTimePickerEnd.ValueChanged += dateTimePickerEnd_ValueChanged;
            // 
            // labelAgregation
            // 
            labelAgregation.AutoSize = true;
            labelAgregation.BackColor = Color.FromArgb(169, 24, 50);
            labelAgregation.Font = new Font("Segoe UI", 20F);
            labelAgregation.ForeColor = SystemColors.ControlLightLight;
            labelAgregation.Location = new Point(255, 895);
            labelAgregation.Name = "labelAgregation";
            labelAgregation.Size = new Size(163, 37);
            labelAgregation.TabIndex = 85;
            labelAgregation.Text = "Agregation :";
            // 
            // comboBoxAgregation
            // 
            comboBoxAgregation.DropDownStyle = ComboBoxStyle.DropDownList;
            comboBoxAgregation.Font = new Font("Segoe UI", 20F);
            comboBoxAgregation.FormattingEnabled = true;
            comboBoxAgregation.Items.AddRange(new object[] { "Diagramme baton" });
            comboBoxAgregation.Location = new Point(424, 896);
            comboBoxAgregation.Name = "comboBoxAgregation";
            comboBoxAgregation.Size = new Size(212, 45);
            comboBoxAgregation.TabIndex = 86;
            comboBoxAgregation.SelectedIndexChanged += comboBoxAgregation_SelectedIndexChanged;
            // 
            // Setting
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            BackgroundImage = Properties.Resources.Background;
            ClientSize = new Size(1924, 1061);
            Controls.Add(comboBoxAgregation);
            Controls.Add(labelAgregation);
            Controls.Add(dateTimePickerEnd);
            Controls.Add(dateTimePickerStart);
            Controls.Add(DataComboBox);
            Controls.Add(pictureBox);
            Controls.Add(label9);
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
            Name = "Setting";
            Text = "Setting";
            WindowState = FormWindowState.Maximized;
            ((System.ComponentModel.ISupportInitialize)pictureBox).EndInit();
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion
        private Button buttonRollBack;
        private Button buttonGraphType;
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
        private RadioButton climateButton;
        private RadioButton stormButton;
        private PictureBox pictureBox;
        private ComboBox DataComboBox;
        private DateTimePicker dateTimePickerStart;
        private DateTimePicker dateTimePickerEnd;
        private Label labelAgregation;
        private ComboBox comboBoxAgregation;
    }
}