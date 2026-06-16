using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Runtime.Intrinsics.Arm;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ApplicationAquitaine
{
    public partial class Setting : Form
    {
        
        private readonly String scriptListe;
        private readonly String dataPath;
        private readonly String resultImageName;

        public Setting()
        {
            InitializeComponent();
            dataPath = @"..\..\..\Data\";
            scriptListe = dataPath + "listes.py";

            remplire_CB_Dep();
            remplire_CB_Com();
        }

        private void remplire_CB_Dep()
        {
            List<string> departements = LancerScript(Scripts_Enum.script_Get_All_Departement);
            comboBoxDepartment.Items.Clear(); // vide la liste avant de remplir
            foreach (string dept in departements)
            {
                comboBoxDepartment.Items.Add(dept);
            }
        }

        private void remplire_CB_Com()
        {
            string dpt = comboBoxDepartment.Text;
            if (!(dpt == ""))
            {
                List<string> communes = LancerScript(Scripts_Enum.script_Get_Communnes, dpt);
                comboBoxMunicipality.Items.Clear();
                foreach(string commun in communes)
                {
                    comboBoxMunicipality.Items.Add(commun);
                }
            }
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

        private void comboBoxDepartment_SelectedIndexChanged_1(object sender, EventArgs e)
        {
            remplire_CB_Com();
        }

        private List<string> LancerScript(Scripts_Enum scripts, string argument = "")
        {


            Process python = new();
            python.StartInfo.FileName = "python";
            python.StartInfo.Arguments = scriptListe + " " + scripts.ToString()  + (argument != "" ? " " + argument : "");
            python.StartInfo.CreateNoWindow = true;
            python.StartInfo.UseShellExecute = false;
            python.Start();
            python.WaitForExit();


           

            List<string> liste =  new List<string>();

            switch (scripts)
            {
                case Scripts_Enum.script_Get_All_Departement:
                    liste = lancerScriptGetAllDepartement();

                    break;

                case Scripts_Enum.script_Get_Communnes:
                    liste = lancerScriptDetAllCommune(argument);
                    break;

                default:
                    return new List<string>();
            }
            return liste;

        }

        private List<string> lancerScriptDetAllCommune(String nomDpt)
        {
            string cheminFichier = Path.Combine(Path.GetDirectoryName(scriptListe), "Dep_All_Com.txt");


            if (!File.Exists(cheminFichier))
            {
                MessageBox.Show("Fichier introuvable : " + cheminFichier);
                return new List<string>();
            }

            List<string> resultats = File.ReadAllLines(cheminFichier, Encoding.UTF8).ToList();

            return resultats;
        }

        private List<string> lancerScriptGetAllDepartement()
        {
            string cheminFichier = Path.Combine(Path.GetDirectoryName(scriptListe), "All_Dep.txt");


            if (!File.Exists(cheminFichier))
            {
                MessageBox.Show("Fichier introuvable : " + cheminFichier);
                return new List<string>();
            }

            List<string> resultats = File.ReadAllLines(cheminFichier, Encoding.UTF8).ToList();

            return resultats;
        }
    }
}
