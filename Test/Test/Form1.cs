using System.Diagnostics;

namespace Test
{
    public partial class Form1 : Form
    {
        private readonly String cmdStr;
        private readonly String simpleScriptName;
        private readonly String resultImageName;
        private readonly String dataPath;
        private String tab;
        public Form1()
        {
            InitializeComponent();
            cmdStr = "cmd.exe";
            dataPath = @"..\..\..\Data\";
            simpleScriptName = dataPath + "scripte_teste_BD.py";
            resultImageName = dataPath + "test.png";


        }


        private void res_Click(object sender, EventArgs e)
        {

        }

        private void DisplayResult(string fileNameImage)
        {
            using Image img = Image.FromFile(fileNameImage);
            res.Image = new Bitmap(img, res.Size);
        }

        private void buttonGo_Click(object sender, EventArgs e)
        {
            Process cmd = GenerateCmd();
            CmdAction("python" + simpleScriptName + tab, cmd);
            DisplayResult(resultImageName);
        }


        private Process GenerateCmd()
        {
            Process cmd = new();
            cmd.StartInfo.FileName = cmdStr;
            cmd.StartInfo.RedirectStandardInput = true;
            cmd.StartInfo.RedirectStandardOutput = true;
            cmd.StartInfo.CreateNoWindow = true;
            cmd.StartInfo.UseShellExecute = false;
            cmd.Start();
            return cmd;
        }

        private static void CmdAction(string line, Process cmd)
        {
            cmd.StandardInput.WriteLine(line);
            cmd.StandardInput.Flush();
            cmd.StandardInput.Close();
            cmd.WaitForExit();
        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {
            tab = textBox1.Text;
        }
    }
}
