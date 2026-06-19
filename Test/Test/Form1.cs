using System.Diagnostics;

namespace Test
{
    public partial class Form1 : Form
    {
        private readonly String simpleScriptName;
        private readonly String resultImageName;
        private readonly String dataPath;
        private String tab;

        public Form1()
        {
            InitializeComponent();
            // permet d'avoir le chemin ou est enregistrer le script python et les images 
            dataPath = @"..\..\..\Data\";
            //permet d'avoire le chemin du script python 
            simpleScriptName = dataPath + "scripte_teste_BD.py";
            // permet d'avoir le chemein de l'image 
            resultImageName = dataPath + "test.png";

            // permet de bien dimentionner l'image 
            res.SizeMode = PictureBoxSizeMode.Zoom;
        }


        private void res_Click(object sender, EventArgs e)
        {

        }

        private void DisplayResult(string fileNameImage)
        {
            // s'il n'y a pas d'image 
            if (!File.Exists(fileNameImage))
            {
                MessageBox.Show("Image introuvable : " + fileNameImage);
                return;
            }

            // ce morceau de code permet d'empécher la fuite de mémoire et de réécrirde au prochain clique ( using) 
            // si il y a une image précédente on la sauvegarde 
            Image? precedente = res.Image;
            //on charge la nouvelle image 
            using Image img = Image.FromFile(fileNameImage);
            // on affiche la nouvelle image 
            res.Image = new Bitmap(img); 
            //on libere l'image précédente de la mémoire 
            precedente?.Dispose();
        }

        private void buttonGo_Click(object sender, EventArgs e)
        {
            // si il n'y a pas d'argument 
            if (string.IsNullOrWhiteSpace(tab))
            {
                MessageBox.Show("Veuillez entrer une valeur dans le champ texte.");
                return;
            }

            // crée un nouveau programme qu'on va lancer 
            Process python = new();

            // on dit que le programme à lancer est en python 
            python.StartInfo.FileName = "python";

            // on passe les argument à python ( comme si on le lancer dans un terminal) 
            python.StartInfo.Arguments = simpleScriptName + " " + tab;

            // permet de ne pas avoir la fenêtre du terminale 
            python.StartInfo.CreateNoWindow = true;

            //on lance python sans passer par cdm 
            python.StartInfo.UseShellExecute = false;

            // on lance le scripte python 
            python.Start();

            // on attend que le scripte python ait terminer 
            python.WaitForExit(); 

            // on affiche l'image 
            DisplayResult(resultImageName);
        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {
            //on supprime les espaces accidentels en début/fin
            tab = textBox1.Text.Trim(); 
        }
    }
}
