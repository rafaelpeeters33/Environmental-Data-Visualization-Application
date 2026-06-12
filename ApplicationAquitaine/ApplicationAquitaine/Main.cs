using System.Drawing.Drawing2D;

namespace ApplicationAquitaine
{
    public partial class Main : Form
    {
        public Main()
        {
            InitializeComponent();
            RoundButton(buttonContinue, 20);
            RoundButton(buttonQuit, 20);
        }

        private void RoundButton(Button button, int radius)
        {
            GraphicsPath path = new GraphicsPath();
            int diameter = radius * 2;

            path.AddArc(new Rectangle(0, 0, diameter, diameter), 180, 90);
            path.AddArc(new Rectangle(button.Width - diameter, 0, diameter, diameter), 270, 90);
            path.AddArc(new Rectangle(button.Width - diameter, button.Height - diameter, diameter, diameter), 0, 90);
            path.AddArc(new Rectangle(0, button.Height - diameter, diameter, diameter), 90, 90);
            path.CloseAllFigures();

            button.Region = new Region(path);
        }

        private void buttonContinue_Click(object sender, EventArgs e)
        {
            Setting SettingForm = new Setting();
            SettingForm.Show();
            this.Hide();
        }

        private void buttonQuit_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }
    }
}
