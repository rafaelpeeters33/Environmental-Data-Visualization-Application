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
    public partial class Compare : Form
    {
        // theDep, theCom et theData deviennent des listes car la sélection est maintenant multiple
        private List<string> theData = new List<string>();
        private List<string> theDep = new List<string>();
        private List<string> theCom = new List<string>();
        private string theEnd;
        private string theStart;
        private string theGraphique;
        private string echelle;
        private string agregation;
        private readonly string dataPath;
        private string scriptPath;

        public Compare(string date_debut, string date_fin)
        {
            InitializeComponent();
            dataPath = @"..\..\..\Data\";

            theStart = date_debut;
            theEnd = date_fin;

            comboBoxTypeGraph.Items.Clear();
            comboBoxTypeGraph.Items.Add("boite a moustaches");
            comboBoxTypeGraph.Items.Add("ligne");
            comboBoxTypeGraph.Items.Add("Air empilée");
            comboBoxTypeGraph.Items.Add("nuage de points");
            comboBoxTypeGraph.Items.Add("regression linéaire");
            comboBoxTypeGraph.Items.Add("violon");

            comboBoxAgregation.Items.Clear();
            comboBoxAgregation.Items.Add("avg");
            comboBoxAgregation.Items.Add("sum");
            comboBoxAgregation.Items.Add("count");

            remplire_CB_Data();
            remplire_CB_Dep();

            comboBoxAgregation.Enabled = false;

            pictureBox.SizeMode = PictureBoxSizeMode.Zoom;
        }

        private void remplire_CB_Data()
        {
            List<string> datas = lancerScriptGetAllData();
            checkedListBoxData.Items.Clear();
            foreach (string data in datas)
            {
                checkedListBoxData.Items.Add(data);
            }
        }

        private void remplire_CB_Dep()
        {
            List<string> departements = lancerScriptGetAllDepartement();
            checkedListBoxDepartment.Items.Clear(); // vide la liste avant de remplir
            foreach (string dept in departements)
            {
                checkedListBoxDepartment.Items.Add(dept);
            }
        }

        private void remplire_CB_Com(List<string> departementsSelectionnes)
        {
            checkedListBoxMunicipality.Items.Clear();

            

            HashSet<string> communesUniques = new HashSet<string>();
            foreach (string dpt in departementsSelectionnes)
            {
                List<string> communes = lancerScriptDetAllCommune(dpt);
                foreach (string commune in communes)
                {
                    communesUniques.Add(commune);
                }
            }

            foreach (string commune in communesUniques.OrderBy(c => c))
            {
                checkedListBoxMunicipality.Items.Add(commune);
            }
        }

        private List<string> GetCheckedItems(CheckedListBox clb)
        {
            List<string> items = new List<string>();
            foreach (var item in clb.CheckedItems)
            {
                items.Add(item.ToString());
            }
            return items;
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


        private void buttonRollBack_Click(object sender, EventArgs e)
        {
            Setting SettingForm = new Setting();
            SettingForm.Show();
            this.Close();
        }

        private void buttonRegion_Click(object sender, EventArgs e)
        {

        }

        private void buttonDownload_Click(object sender, EventArgs e)
        {

        }

        private void buttonValidate_Click(object sender, EventArgs e)
        {
            theCom.Clear();
            foreach (var item in checkedListBoxMunicipality.CheckedItems)
            {
                theCom.Add(item.ToString());
            }


            if (theData.Count == 0)
            {
                MessageBox.Show("Sélectionne au moins une donnée à afficher.");
                return;
            }

            if (string.IsNullOrEmpty(comboBoxAgregation.Text))
            {
                MessageBox.Show("Sélectionne un type d'agrégation (avg, sum, count).");
                return;
            }
            agregation = comboBoxAgregation.Text;

            List<string> zonesSelectionnees;
            if (theCom.Count > 0)
            {
                echelle = "commune";
                zonesSelectionnees = theCom;
            }
            else if (theDep.Count > 0)
            {
                echelle = "departement";
                zonesSelectionnees = theDep;
            }
            else
            {
                echelle = "region";
                zonesSelectionnees = new List<string>();
            }

            string argCategories = string.Join("|", theData);
            string argZones = string.Join("|", zonesSelectionnees);

            lancerScriptGraphique(theStart, theEnd, argCategories, echelle, agregation, argZones);

        }

        private void lancerScriptGraphique(string dateDebut, string dateFin, string categories,
                                           string echelleChoisie, string typeAgregation, string zones)
        {
            switch (theGraphique)
            {
                case "boite a moustaches":
                    scriptPath = Path.GetFullPath(dataPath + "boite_moustache.py");
                    break;
                case "ligne":
                    scriptPath = Path.GetFullPath(dataPath + "trace_ligne.py");
                    break;
                case "Air empilée":
                    scriptPath = Path.GetFullPath(dataPath + "aire_empilee.py");
                    break;
                case "nuage de points":
                    scriptPath = Path.GetFullPath(dataPath + "nuage_de_points.py");
                    break;
                case "regression linéaire":
                    scriptPath = Path.GetFullPath(dataPath + "regression_lineaire.py");
                    break;
                case "violon":
                    scriptPath = Path.GetFullPath(dataPath + "violon.py");
                    break;
                default:
                    break;
            }


            string dossierData = Path.GetFullPath(dataPath);

            ProcessStartInfo startInfo = new ProcessStartInfo();
            startInfo.FileName = "python";
            startInfo.Arguments = $"\"{scriptPath}\" \"{dateDebut}\" \"{dateFin}\" \"{categories}\" \"{echelleChoisie}\" \"{typeAgregation}\" \"{zones}\" \"false\"";
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
                return;
            }

            string cheminImage = Path.Combine(dossierData, "setting.png");
            if (!File.Exists(cheminImage))
            {
                MessageBox.Show("Image introuvable : " + cheminImage);
                return;
            }

            DisplayResult(cheminImage);
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

        


        

        private void comboBoxAgregation_SelectedIndexChanged(object sender, EventArgs e)
        {
            agregation = comboBoxAgregation.Text;
        }

        private void labelTitle_Click(object sender, EventArgs e)
        {

        }

        private void pictureBox_Click(object sender, EventArgs e)
        {

        }

        private void checkedListBoxMunicipality_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void checkedListBoxDepartment_SelectedIndexChanged(object sender, EventArgs e)
        {


        }

        private void checkedListBoxData_SelectedIndexChanged(object sender, EventArgs e)
        {
            theData.Add(checkedListBoxData.Text);
        }

        private void comboBoxTypeGraph_SelectedIndexChanged(object sender, EventArgs e)
        {
            theGraphique = comboBoxTypeGraph.Text;
            comboBoxAgregation.Enabled = true;
        }

        private void buttonValidateDep_Click(object sender, EventArgs e)
        {
            theDep.Clear();
            foreach (var item in checkedListBoxDepartment.CheckedItems)
            {
                theDep.Add(item.ToString());
            }
            remplire_CB_Com(theDep);
            

        }
    }
}