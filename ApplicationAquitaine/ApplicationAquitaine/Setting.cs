using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Runtime.Intrinsics.Arm;
using System.Security.Policy;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace ApplicationAquitaine
{
    public partial class Setting : Form
    {
        private string theData;
        private string theDep;
        private string theCom;
        private string theEnd;
        private string theStart;
        private string theGraphique;
        private string echelle;
        private string agregation;
        private string nom_zone;
        private readonly string dataPath;


        public Setting()
        {
            InitializeComponent();
            dataPath = @"..\..\..\Data\";

            theStart = dateTimePickerStart.Value.ToString("yyyy-MM-dd");
            theEnd = dateTimePickerEnd.Value.ToString("yyyy-MM-dd");

            remplire_CB_Dep();
            remplire_CB_Data();
            comboBoxMunicipality.Enabled = false;
            comboBoxTypeGraph.Items.Clear();
            comboBoxTypeGraph.Items.Add("boite a moustaches");
            comboBoxTypeGraph.Items.Add("ligne");

            comboBoxAgregation.Items.Clear();
            comboBoxAgregation.Items.Add("avg");
            comboBoxAgregation.Items.Add("sum");
            comboBoxAgregation.Items.Add("count");

            pictureBox.SizeMode = PictureBoxSizeMode.Zoom;


        }

        private void remplire_CB_Data()
        {
            List<string> datas = lancerScriptGetAllData();
            DataComboBox.Items.Clear();
            foreach (string data in datas)
            {
                string data_minuscule = data.ToLower();
                DataComboBox.Items.Add(data_minuscule);
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
            if (!string.IsNullOrEmpty(dpt))
            {
                List<string> communes = lancerScriptDetAllCommune(dpt);
                comboBoxMunicipality.Items.Clear();
                foreach (string commun in communes)
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
            this.Close();
        }


        private void buttonValidate_Click(object sender, EventArgs e)
        {
            if (theGraphique == null)
            {
                MessageBox.Show("Vous devez choisir un type de graphe à afficher");
                return;
            }
            if (theData == null)
            {
                MessageBox.Show("Vous devez choisir une donnée à afficher");
                return;
            }
            if (theGraphique == "ligne" && agregation == null)
            {
                MessageBox.Show("Vous devez choisir une agrégation si vous voulez afficher un graphique en ligne");
                return;
            }

            if (theGraphique == "boite a moustaches")
                boite_A_Moustache_Create();
            else if (theGraphique == "ligne")
                ligne_Graphique_Create();
        }

        private void ligne_Graphique_Create()
        {

            Process python = new();
            if (theDep == null)
            {
                echelle = "region";
                nom_zone = "";
            }

            else if (theCom == null)
            {
                echelle = "departement";
                nom_zone = theDep;
            }
            else
            {
                echelle = "commune";
                nom_zone = theCom;
            }

            python.StartInfo.FileName = "python";

            python.StartInfo.ArgumentList.Add(dataPath + "script_Generer_Graphique_Ligne.py");
            python.StartInfo.ArgumentList.Add(theStart.ToString());
            python.StartInfo.ArgumentList.Add(theEnd.ToString());
            python.StartInfo.ArgumentList.Add(theData);
            python.StartInfo.ArgumentList.Add(echelle);
            python.StartInfo.ArgumentList.Add(agregation);
            python.StartInfo.ArgumentList.Add(nom_zone);

            python.StartInfo.CreateNoWindow = true;
            python.StartInfo.UseShellExecute = false;
            python.Start();
            python.WaitForExit();

            string erreurs = python.StandardError.ReadToEnd();
            python.WaitForExit();

            if (!string.IsNullOrEmpty(erreurs))
            {
                MessageBox.Show("Erreur Python : " + erreurs);
                return;
            }

            DisplayResult(dataPath + "setting.png");
        }


        private void boite_A_Moustache_Create()
        {
            if (theDep == null)
            {
                echelle = "region";
                nom_zone = "Aquitaine";
            }
            else if (theCom == null)
            {
                echelle = "departement";
                nom_zone = theDep;
            }
            else
            {
                echelle = "commune";
                nom_zone = theCom;
            }

            ProcessStartInfo startInfo = new ProcessStartInfo
            {
                FileName = "python",
                CreateNoWindow = true,
                UseShellExecute = false,
                RedirectStandardError = true,
                RedirectStandardOutput = true
            };

            startInfo.ArgumentList.Add(dataPath + "boite_moustache.py");
            startInfo.ArgumentList.Add(theStart);
            startInfo.ArgumentList.Add(theEnd);
            startInfo.ArgumentList.Add(theData);
            startInfo.ArgumentList.Add(echelle);
            startInfo.ArgumentList.Add(nom_zone);
            startInfo.ArgumentList.Add("false");

            Process python = new Process { StartInfo = startInfo };
            python.Start();

            string erreurs = python.StandardError.ReadToEnd();
            python.WaitForExit();

            if (python.ExitCode != 0)
            {
                MessageBox.Show("Erreur Python : " + erreurs);
                return;
            }


            DisplayResult(dataPath + "setting.png");
        }

        private void DisplayResult(string fileNameImage)
        {
            if (!File.Exists(fileNameImage))
            {
                MessageBox.Show("Image Introuvable : " + fileNameImage);
                return;
            }

            Image? precedente = pictureBox.Image;

            byte[] bytes = File.ReadAllBytes(fileNameImage);
            using (MemoryStream ms = new MemoryStream(bytes))
            {
                pictureBox.Image = new Bitmap(ms);
            }

            precedente?.Dispose();
        }


        private void buttonDownload_Click(object sender, EventArgs e)
        {

        }


        private void comboBoxDepartment_Leave(object sender, EventArgs e)
        {

        }

        private void comboBoxDepartment_SelectedIndexChanged_1(object sender, EventArgs e)
        {
            remplire_CB_Com();
            comboBoxMunicipality.Enabled = true;
            comboBoxMunicipality.Text = null;
            theDep = comboBoxDepartment.Text;

        }


        private List<string> lancerScriptDetAllCommune(string nomDpt)
        {
            string scriptPath = Path.GetFullPath(dataPath + "script_Get_Communes.py");
            string dossierData = Path.GetFullPath(dataPath);

            ProcessStartInfo startInfo = new ProcessStartInfo();
            startInfo.FileName = "python";
            //Guillemets autour des chemins
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
            string scriptPath = Path.GetFullPath(dataPath + "script_Get_All_Departement.py");
            string dossierData = Path.GetFullPath(dataPath);

            ProcessStartInfo startInfo = new ProcessStartInfo();
            startInfo.FileName = "python";
            startInfo.Arguments = $"\"{scriptPath}\" \"{dossierData}\"";
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

            string cheminFichier = Path.Combine(dossierData, "All_Dep.txt");
            if (!File.Exists(cheminFichier))
            {
                MessageBox.Show("Fichier introuvable : " + cheminFichier);
                return new List<string>();
            }

            return File.ReadAllLines(cheminFichier, Encoding.UTF8).ToList();
        }

        private List<string> lancerScriptGetAllData()
        {
            string scriptPath = Path.GetFullPath(dataPath + "script_Get_All_Categorie.py");
            string dossierData = Path.GetFullPath(dataPath);

            ProcessStartInfo startInfo = new ProcessStartInfo();
            startInfo.FileName = "python";
            //  Guillemets autour des chemins pour gérer les espaces
            startInfo.Arguments = $"\"{scriptPath}\" \"{dossierData}\"";
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

            string cheminFichier = Path.Combine(dossierData, "All_CTG.txt");
            if (!File.Exists(cheminFichier))
            {
                MessageBox.Show("Fichier introuvable : " + cheminFichier);
                return new List<string>();
            }

            return File.ReadAllLines(cheminFichier, Encoding.UTF8).ToList();
        }

        private void DataComboBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            theData = DataComboBox.Text;
        }

        private void comboBoxMunicipality_SelectedIndexChanged(object sender, EventArgs e)
        {
            theCom = comboBoxMunicipality.Text;
        }



        private void comboBoxTypeGraph_SelectedIndexChanged(object sender, EventArgs e)
        {
            theGraphique = comboBoxTypeGraph.Text;
        }

        private void dateTimePickerStart_ValueChanged(object sender, EventArgs e)
        {
            theStart = theStart = dateTimePickerStart.Value.ToString("yyyy-MM-dd");
        }

        private void dateTimePickerEnd_ValueChanged(object sender, EventArgs e)
        {
            theEnd = dateTimePickerEnd.Value.ToString("yyyy-MM-dd");
        }

        private void comboBoxAgregation_SelectedIndexChanged(object sender, EventArgs e)
        {
            agregation = comboBoxAgregation.Text;
        }
    }
}
