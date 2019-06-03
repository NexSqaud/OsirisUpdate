using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace OsirisUpdater
{
    public partial class Form3 : Form
    {
        private Config config = new Config();
        public Form3(Config config)
        {
            InitializeComponent();
            comboBox1.SelectedIndex = config.injectType;
            textBox1.Text = config.pathToDll;
            this.config = config;
        }
        private void Button1_Click(object sender, EventArgs e)
        {
            Form1.SaveConfig(config);
            Close();
        }

        private void ComboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            config.injectType = comboBox1.SelectedIndex;
        }
    }
}
