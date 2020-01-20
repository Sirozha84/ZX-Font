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
    public partial class FormPoke : Form
    {
        public FormPoke()
        {
            InitializeComponent();
            Calculate();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            Close();
        }

        void Calculate()
        {
            label2.Text = "POKE 23606, " + ((numericUpDown1.Value ) % 256).ToString();
            label3.Text = "POKE 23607, " + (((int)numericUpDown1.Value ) / 256 - 1).ToString();
        }

        private void numericUpDown1_ValueChanged(object sender, EventArgs e)
        {
            Calculate();
        }
    }
}
