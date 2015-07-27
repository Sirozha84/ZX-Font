using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace ZXFont
{
    public partial class FormFontParameters : Form
    {
        public FormFontParameters()
        {
            InitializeComponent();
            if (FormMain.CurrentProject.Symbols == 96) comboBox1.SelectedIndex = 0;
            if (FormMain.CurrentProject.Symbols == 224) comboBox1.SelectedIndex = 1;
            if (FormMain.CurrentProject.Symbols == 256) comboBox1.SelectedIndex = 2;
            numericUpDown1.Value = FormMain.CurrentProject.SizeX;
            numericUpDown2.Value = FormMain.CurrentProject.SizeY;
            numericUpDown3.Value = Project.YT;
            numericUpDown4.Value = Project.Yt;
            numericUpDown5.Value = Project.XL;
            numericUpDown6.Value = Project.XR;
            numericUpDown7.Value = Project.YB;
        }

        private void button2_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (comboBox1.SelectedIndex == 0) { FormMain.CurrentProject.Symbols = 96; FormMain.CurrentProject.ADD = 32; }
            if (comboBox1.SelectedIndex == 1) { FormMain.CurrentProject.Symbols = 224; FormMain.CurrentProject.ADD = 32; }
            if (comboBox1.SelectedIndex == 2) { FormMain.CurrentProject.Symbols = 256; FormMain.CurrentProject.ADD = 0; }
            FormMain.CurrentProject.SizeX = (byte)numericUpDown1.Value;
            FormMain.CurrentProject.SizeY = (byte)numericUpDown2.Value;
            Project.YT = (byte)numericUpDown3.Value;
            Project.Yt = (byte)numericUpDown4.Value;
            Project.XL = (byte)numericUpDown5.Value;
            Project.XR = (byte)numericUpDown6.Value;
            Project.YB = (byte)numericUpDown7.Value;
            DialogResult = DialogResult.OK; //Надеюсь в следующий раз не забуду это, когда форма будет возвращаьб Cancel!
            Close();
        }
    }
}
