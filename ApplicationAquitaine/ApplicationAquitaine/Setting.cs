using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Diagnostics.Metrics;
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
        public string theStart;
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
            comboBoxTypeGraph.Items.Add("Air empilée");
            comboBoxTypeGraph.Items.Add("nuage de points");
            comboBoxTypeGraph.Items.Add("regression linéaire");
            comboBoxTypeGraph.Items.Add("violon");
            comboBoxTypeGraph.Items.Add("Histogramme");


            comboBoxAgregation.Items.Clear();
            comboBoxAgregation.Items.Add("avg");
            comboBoxAgregation.Items.Add("sum");
            comboBoxAgregation.Items.Add("count");

            comboBoxAgregation.Enabled = false;

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
            Compare compareForm = new Compare(theStart, theEnd);
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
            if ((theGraphique == "ligne" || theGraphique == "Air empilée" || theGraphique == "violon") && agregation == null)
            {
                MessageBox.Show("Vous devez choisir une agrégation si vous voulez afficher ce graphique");
                return;
            }

            Graphique_Create(theGraphique);


        }

        private void Graphique_Create(string graphique)
        {
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

            ProcessStartInfo startInfo = new ProcessStartInfo
            {
                FileName = "python",
                CreateNoWindow = true,
                UseShellExecute = false,
                RedirectStandardError = true,
                RedirectStandardOutput = true
            };

            switch (theGraphique)
            {
                case "boite a moustaches":
                    startInfo.ArgumentList.Add(dataPath + "boite_moustache.py");
                    break;
                case "ligne":
                    startInfo.ArgumentList.Add(dataPath + "trace_ligne.py");
                    break;
                case "Air empilée":
                    startInfo.ArgumentList.Add(dataPath + "aire_empilee.py");
                    break;
                case "nuage de points":
                    startInfo.ArgumentList.Add(dataPath + "nuage_de_points.py");
                    break;
                case "regression linéaire":
                    startInfo.ArgumentList.Add(dataPath + "regression_lineaire.py");
                    break;
                case "violon":
                    startInfo.ArgumentList.Add(dataPath + "violon.py");
                    break;
                case "Histogramme":
                    startInfo.ArgumentList.Add(dataPath + "histogramme.py");
                    break;

                default:
                    break;
            }


            startInfo.ArgumentList.Add(theStart.ToString());
            startInfo.ArgumentList.Add(theEnd.ToString());
            startInfo.ArgumentList.Add(theData);
            startInfo.ArgumentList.Add(echelle);

            if (theGraphique == "ligne" || theGraphique == "Air empilée" || theGraphique == "violon")
            {
                startInfo.ArgumentList.Add(agregation);
            }

            startInfo.ArgumentList.Add(nom_zone);
            startInfo.ArgumentList.Add("false");

            Process python = new Process { StartInfo = startInfo };
            
            python.Start();
            string erreurs = python.StandardError.ReadToEnd();
            //string info = python.StandardOutput.ReadToEnd();

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
            if (theGraphique == "ligne" || theGraphique == "Air empilée" || theGraphique == "violon")
            {
                comboBoxAgregation.Enabled = true;
            }
            else
            {
                comboBoxAgregation.Enabled = false;
            }
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

        private void pictureBox_Click(object sender, EventArgs e)
        {

        }
    }
}
