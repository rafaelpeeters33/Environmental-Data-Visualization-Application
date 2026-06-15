using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ApplicationAquitaine
{
    public partial class Setting : Form
    {
        public Setting()
        {
            InitializeComponent();
            ChargerDepartements();
        }

        private void buttonCompare_Click(object sender, EventArgs e)
        {
            Compare compareForm = new Compare();
            compareForm.Show();
            this.Hide();
        }

        private void buttonRollBack_Click(object sender, EventArgs e)
        {
            Main MainForm = new Main();
            MainForm.Show();
            this.Hide();
        }

        private void buttonAirQuality_Click(object sender, EventArgs e)
        {

        }

        private void buttonClimate_Click(object sender, EventArgs e)
        {

        }

        private void buttonValidate_Click(object sender, EventArgs e)
        {
            string cheminFichier = @"..\..\..\Data\incendie_Pessac_max.png";
            string cheminAbsolu = Path.GetFullPath(cheminFichier);
            if (File.Exists(cheminAbsolu))
            {
                if (pictureBox.Image != null) pictureBox.Image.Dispose();

                using (FileStream fs = new FileStream(cheminAbsolu, FileMode.Open, FileAccess.Read))
                {
                    pictureBox.Image = Image.FromStream(fs);
                }
                pictureBox.SizeMode = PictureBoxSizeMode.Zoom;
            }
            else
            {
                MessageBox.Show("Le fichier n'est pas trouvé ici : \n" + cheminAbsolu);
            }
        }

        private void buttonDownload_Click(object sender, EventArgs e)
        {

        }
        private string ExecuterPython(string commande)
        {
            ProcessStartInfo start = new ProcessStartInfo();
            start.FileName = "python";
            start.Arguments = @"..\..\..\Data\listes.py";
            start.RedirectStandardInput = true; 
            start.RedirectStandardOutput = true; 
            start.UseShellExecute = false;
            start.CreateNoWindow = true;

            using (Process process = Process.Start(start))
            { 
                process.StandardInput.WriteLine(commande);
                process.StandardInput.Close(); 
                string resultat = process.StandardOutput.ReadToEnd();
                process.WaitForExit();

                return resultat;
            }
        }

        private void ChargerDepartements()
        {
            comboBoxDepartment.Items.Clear();
            comboBoxMunicipality.Enabled = false; 
            string resultat = ExecuterPython("departements");
            string[] lignes = resultat.Split(new[] { '\r', '\n' }, StringSplitOptions.RemoveEmptyEntries);
            comboBoxDepartment.Items.AddRange(lignes);
        }
        private void comboBoxDepartment_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (comboBoxDepartment.SelectedItem == null) return;
            string dptChoisi = comboBoxDepartment.SelectedItem.ToString();
            comboBoxMunicipality.Items.Clear();
            comboBoxMunicipality.Text = "Chargement...";
            string resultat = ExecuterPython($"communes|{dptChoisi}");
            string[] lignes = resultat.Split(new[] { '\r', '\n' }, StringSplitOptions.RemoveEmptyEntries);
            comboBoxMunicipality.Items.AddRange(lignes);
            comboBoxMunicipality.Text = "Commune";
            comboBoxMunicipality.Enabled = true; 
        }

        private void comboBoxDepartment_Leave(object sender, EventArgs e)
        {
            int index = comboBoxDepartment.FindStringExact(comboBoxDepartment.Text);
            if (index == -1)
            {
                comboBoxDepartment.Text = "Département";
                comboBoxDepartment.SelectedIndex = -1;
                comboBoxMunicipality.Enabled = false;
                comboBoxMunicipality.Items.Clear();
            }
            else
            {
                comboBoxDepartment.SelectedIndex = index;
                comboBoxDepartment_SelectedIndexChanged(sender, e);
            }
        }
    }
}
