using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ApplicationAquitaine
{
    public partial class Compare : Form
    {
        public Compare()
        {
            InitializeComponent();
        }

        private void buttonRollBack_Click(object sender, EventArgs e)
        {
            Setting SettingForm = new Setting();
            SettingForm.Show();
            this.Hide();
        }

        private void buttonAirQuality_Click(object sender, EventArgs e)
        {

        }

        private void buttonClimate_Click(object sender, EventArgs e)
        {

        }

        private void buttonRegion_Click(object sender, EventArgs e)
        {

        }

        private void buttonDownload_Click(object sender, EventArgs e)
        {

        }

        private void buttonValidate_Click(object sender, EventArgs e)
        {

        }
    }
}
