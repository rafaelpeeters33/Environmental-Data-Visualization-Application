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

        private readonly String dataPath;

        public Setting()
        {
            InitializeComponent();
            dataPath = @"..\..\..\Data\";

            try
            {
                remplire_CB_Dep();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Erreur dans remplire_CB_Dep : \n" + ex.Message);
            }

        }

        private void remplire_CB_Dep()
        {
            List<string> departements = lancerScriptGetAllDepartement();
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
                List<string> communes = lancerScriptDetAllCommune(dpt);

                comboBoxMunicipality.Items.Clear();
                foreach (string commun in communes)
                {

                    comboBoxMunicipality.Items.Add(commun.Trim());

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
            this.Close();
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
          
        }

        private void comboBoxDepartment_Leave(object sender, EventArgs e)
        {
            
        }

        private void comboBoxDepartment_SelectedIndexChanged_1(object sender, EventArgs e)
        {
            remplire_CB_Com();
        }


        private List<string> lancerScriptDetAllCommune(string nomDpt)
        {
            string scriptPath = Path.GetFullPath(dataPath + "script_Get_Communes.py");
            string dossierData = Path.GetFullPath(dataPath);

            ProcessStartInfo startInfo = new ProcessStartInfo();
            startInfo.FileName = "python";

            startInfo.Arguments = $"\"{scriptPath}\" \"{nomDpt}\" \"{dossierData}\"";
            startInfo.CreateNoWindow = true;
            startInfo.UseShellExecute = false;
            startInfo.RedirectStandardError = true;
            startInfo.RedirectStandardOutput = true;

            Process python = new Process();
            python.StartInfo = startInfo;
            python.Start();

            string erreurs = python.StandardError.ReadToEnd();
            python.WaitForExit();

            if (erreurs != "")
            {
                MessageBox.Show("Erreur Python : " + erreurs);
                return new List<string>();
            }

            string cheminFichier = Path.Combine(dossierData, "Dep_All_Com.txt");
            if (!File.Exists(cheminFichier))
            {
                MessageBox.Show("Fichier introuvable : " + cheminFichier);
                return new List<string>();
            }
            return File.ReadAllLines(cheminFichier, Encoding.UTF8).ToList();
        }

        private List<string> lancerScriptGetAllDepartement()
        {
            // permet d'avoir le chemin absolue du script 
            string scriptPath = Path.GetFullPath(dataPath + "script_Get_All_Departement.py");

            // permet d'avoire le chemin absolue du dossier 
            string dossierData = Path.GetFullPath(dataPath);

            // On dit d'ouvrir le programme Python
            ProcessStartInfo startInfo = new ProcessStartInfo();
            startInfo.FileName = "python";

            // on donne les arguments 
            startInfo.Arguments = $"\"{scriptPath}\" \"{dossierData}\"";

            // on dit de ne pas afficher la fenêtre de terminal 
            startInfo.CreateNoWindow = true;
            startInfo.UseShellExecute = false;
            startInfo.RedirectStandardError = true;
            startInfo.RedirectStandardOutput = true;

            Process python = new Process();
            python.StartInfo = startInfo;
            // on lance le script en arierre plan 
            python.Start();

            // permet d'avoir les erreurs 
            string erreurs = python.StandardError.ReadToEnd();
            python.WaitForExit();

            // s'il y a une erreur on l'affiche 
            if (erreurs != "")
            {
                MessageBox.Show("Erreur Python : " + erreurs);
                return new List<string>();
            }
            // on prend le chemin du fichier 
            string cheminFichier = Path.Combine(dossierData, "All_Dep.txt");
            // s'il n'existe pas on affiche une erreur 
            if (!File.Exists(cheminFichier))
            {
                MessageBox.Show("Fichier introuvable : " + cheminFichier);
                return new List<string>();
            }

            // on renvoie la liste de tout les élèment qu'elle contient 
            return File.ReadAllLines(cheminFichier, Encoding.UTF8).ToList();
        }

        private void comboBoxMunicipality_SelectedIndexChanged(object sender, EventArgs e)
        {

        }
    }
}
