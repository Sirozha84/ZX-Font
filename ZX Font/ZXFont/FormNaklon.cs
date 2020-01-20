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
    public partial class FormNaklon : Form
    {
        public FormNaklon()
        {
            InitializeComponent();
            label3.Text = FormMain.Naklon.ToString();
            hScrollBar1.Value = FormMain.Naklon;
        }

        private void button2_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            DialogResult = DialogResult.OK;
            FormMain.Naklon = hScrollBar1.Value;
            Close();
        }

        private void hScrollBar1_Scroll(object sender, ScrollEventArgs e)
        {
            label3.Text = hScrollBar1.Value.ToString();
        }
    }
}
