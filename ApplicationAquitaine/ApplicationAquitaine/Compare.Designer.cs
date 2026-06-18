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
            buttonRegion = new Button();
            buttonDownload = new Button();
            labelLegend = new Label();
            labelTitle = new Label();
            buttonRollBack = new Button();
            labelFilter = new Label();
            buttonValidate = new Button();
            labelCompare = new Label();
            comboBoxTypeGraph = new ComboBox();
            labelTypeGraph = new Label();
            comboBoxAgregation = new ComboBox();
            labelAgregation = new Label();
            pictureBox = new PictureBox();
            checkedListBoxData = new CheckedListBox();
            checkedListBoxDepartment = new CheckedListBox();
            checkedListBoxMunicipality = new CheckedListBox();
            buttonValidateDep = new Button();
            ((System.ComponentModel.ISupportInitialize)pictureBox).BeginInit();
            SuspendLayout();
            // 
            // buttonRegion
            // 
            buttonRegion.Font = new Font("Segoe UI", 20F);
            buttonRegion.Location = new Point(316, 416);
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
            labelTitle.Location = new Point(690, 216);
            labelTitle.Name = "labelTitle";
            labelTitle.Size = new Size(83, 37);
            labelTitle.TabIndex = 29;
            labelTitle.Text = "Titre :";
            labelTitle.Click += labelTitle_Click;
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
            buttonValidate.Location = new Point(1336, 899);
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
            // comboBoxTypeGraph
            // 
            comboBoxTypeGraph.DropDownStyle = ComboBoxStyle.DropDownList;
            comboBoxTypeGraph.Font = new Font("Segoe UI", 20F);
            comboBoxTypeGraph.FormattingEnabled = true;
            comboBoxTypeGraph.Location = new Point(903, 895);
            comboBoxTypeGraph.Name = "comboBoxTypeGraph";
            comboBoxTypeGraph.Size = new Size(281, 45);
            comboBoxTypeGraph.TabIndex = 45;
            comboBoxTypeGraph.SelectedIndexChanged += comboBoxTypeGraph_SelectedIndexChanged;
            // 
            // labelTypeGraph
            // 
            labelTypeGraph.AutoSize = true;
            labelTypeGraph.BackColor = Color.FromArgb(169, 24, 50);
            labelTypeGraph.Font = new Font("Segoe UI", 20F);
            labelTypeGraph.ForeColor = SystemColors.ControlLightLight;
            labelTypeGraph.Location = new Point(607, 895);
            labelTypeGraph.Name = "labelTypeGraph";
            labelTypeGraph.Size = new Size(255, 37);
            labelTypeGraph.TabIndex = 46;
            labelTypeGraph.Text = "Type de Graphique :";
            // 
            // comboBoxAgregation
            // 
            comboBoxAgregation.DropDownStyle = ComboBoxStyle.DropDownList;
            comboBoxAgregation.Font = new Font("Segoe UI", 20F);
            comboBoxAgregation.FormattingEnabled = true;
            comboBoxAgregation.Items.AddRange(new object[] { "Diagramme baton" });
            comboBoxAgregation.Location = new Point(357, 880);
            comboBoxAgregation.Name = "comboBoxAgregation";
            comboBoxAgregation.Size = new Size(212, 45);
            comboBoxAgregation.TabIndex = 87;
            comboBoxAgregation.SelectedIndexChanged += comboBoxAgregation_SelectedIndexChanged;
            // 
            // labelAgregation
            // 
            labelAgregation.AutoSize = true;
            labelAgregation.BackColor = Color.FromArgb(169, 24, 50);
            labelAgregation.Font = new Font("Segoe UI", 20F);
            labelAgregation.ForeColor = SystemColors.ControlLightLight;
            labelAgregation.Location = new Point(188, 880);
            labelAgregation.Name = "labelAgregation";
            labelAgregation.Size = new Size(163, 37);
            labelAgregation.TabIndex = 88;
            labelAgregation.Text = "Agregation :";
            // 
            // pictureBox
            // 
            pictureBox.Location = new Point(690, 266);
            pictureBox.Name = "pictureBox";
            pictureBox.Size = new Size(650, 580);
            pictureBox.TabIndex = 89;
            pictureBox.TabStop = false;
            pictureBox.Click += pictureBox_Click;
            // 
            // checkedListBoxData
            // 
            checkedListBoxData.FormattingEnabled = true;
            checkedListBoxData.Location = new Point(29, 416);
            checkedListBoxData.Name = "checkedListBoxData";
            checkedListBoxData.Size = new Size(205, 94);
            checkedListBoxData.TabIndex = 90;
            checkedListBoxData.SelectedIndexChanged += checkedListBoxData_SelectedIndexChanged;
            // 
            // checkedListBoxDepartment
            // 
            checkedListBoxDepartment.FormattingEnabled = true;
            checkedListBoxDepartment.Location = new Point(327, 621);
            checkedListBoxDepartment.Name = "checkedListBoxDepartment";
            checkedListBoxDepartment.Size = new Size(281, 94);
            checkedListBoxDepartment.TabIndex = 91;
            checkedListBoxDepartment.SelectedIndexChanged += checkedListBoxDepartment_SelectedIndexChanged;
            // 
            // checkedListBoxMunicipality
            // 
            checkedListBoxMunicipality.FormattingEnabled = true;
            checkedListBoxMunicipality.Location = new Point(327, 491);
            checkedListBoxMunicipality.Name = "checkedListBoxMunicipality";
            checkedListBoxMunicipality.Size = new Size(281, 94);
            checkedListBoxMunicipality.TabIndex = 92;
            checkedListBoxMunicipality.SelectedIndexChanged += checkedListBoxMunicipality_SelectedIndexChanged;
            // 
            // buttonValidateDep
            // 
            buttonValidateDep.BackColor = Color.FromArgb(194, 226, 196);
            buttonValidateDep.Font = new Font("Segoe UI", 18F);
            buttonValidateDep.Location = new Point(357, 738);
            buttonValidateDep.Name = "buttonValidateDep";
            buttonValidateDep.Size = new Size(198, 44);
            buttonValidateDep.TabIndex = 93;
            buttonValidateDep.Text = "Valider";
            buttonValidateDep.UseVisualStyleBackColor = false;
            buttonValidateDep.Click += buttonValidateDep_Click;
            // 
            // Compare
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            BackgroundImage = Properties.Resources.Background;
            ClientSize = new Size(1924, 1061);
            Controls.Add(buttonValidateDep);
            Controls.Add(checkedListBoxMunicipality);
            Controls.Add(checkedListBoxDepartment);
            Controls.Add(checkedListBoxData);
            Controls.Add(pictureBox);
            Controls.Add(labelAgregation);
            Controls.Add(comboBoxAgregation);
            Controls.Add(labelTypeGraph);
            Controls.Add(comboBoxTypeGraph);
            Controls.Add(buttonRegion);
            Controls.Add(buttonDownload);
            Controls.Add(labelLegend);
            Controls.Add(labelTitle);
            Controls.Add(buttonRollBack);
            Controls.Add(labelFilter);
            Controls.Add(buttonValidate);
            Controls.Add(labelCompare);
            Name = "Compare";
            Text = "Compare";
            WindowState = FormWindowState.Maximized;
            ((System.ComponentModel.ISupportInitialize)pictureBox).EndInit();
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion
        private Button buttonRegion;
        private Button buttonDownload;
        private DomainUpDown domainUpDownTypeGraph;
        private Label labelLegend;
        private Label labelTitle;
        private Button buttonRollBack;
        private Label labelFilter;
        private Button buttonValidate;
        private Label labelCompare;
        private ComboBox comboBoxTypeGraph;
        private Label labelTypeGraph;
        private ComboBox comboBoxAgregation;
        private Label labelAgregation;
        private PictureBox pictureBox;
        private CheckedListBox checkedListBoxData;
        private CheckedListBox checkedListBoxDepartment;
        private CheckedListBox checkedListBoxMunicipality;
        private Button buttonValidateDepartement;
        private Button buttonValidateDep;
    }
}