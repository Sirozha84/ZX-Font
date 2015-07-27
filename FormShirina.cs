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
    public partial class FormShirina : Form
    {
        public FormShirina()
        {
            InitializeComponent();
            label3.Text = FormMain.ShirinaX.ToString();
            hScrollBar1.Value = FormMain.ShirinaX;
            label4.Text = FormMain.ShirinaY.ToString();
            hScrollBar2.Value = FormMain.ShirinaY;
        }

        private void hScrollBar1_Scroll(object sender, ScrollEventArgs e)
        {
            label3.Text = hScrollBar1.Value.ToString();
        }

        private void hScrollBar2_Scroll(object sender, ScrollEventArgs e)
        {
            label4.Text = hScrollBar2.Value.ToString();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            FormMain.ShirinaX = hScrollBar1.Value;
            FormMain.ShirinaY = hScrollBar2.Value;
            DialogResult = DialogResult.OK;
            Close();
        }
    }
}
