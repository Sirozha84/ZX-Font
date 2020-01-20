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
    public partial class FormColors : Form
    {
        public FormColors()
        {
            InitializeComponent();
            comboBoxInk.SelectedIndex = Properties.Settings.Default.Ink;
            comboBoxPaper.SelectedIndex = Properties.Settings.Default.Paper;
        }

        private void button2_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            Properties.Settings.Default.Ink = comboBoxInk.SelectedIndex;
            Properties.Settings.Default.Paper = comboBoxPaper.SelectedIndex;
            Close();
        }

        private void comboBoxThemes_SelectedIndexChanged(object sender, EventArgs e)
        {
            switch (comboBoxThemes.SelectedIndex)
            {
                case 0:
                    comboBoxInk.SelectedIndex = 0;
                    comboBoxPaper.SelectedIndex = 7;
                    break;
                case 1:
                    comboBoxInk.SelectedIndex = 7;
                    comboBoxPaper.SelectedIndex = 0;
                    break;
                case 2:
                    comboBoxInk.SelectedIndex = 5;
                    comboBoxPaper.SelectedIndex = 1;
                    break;
                case 3:
                    comboBoxInk.SelectedIndex = 4;
                    comboBoxPaper.SelectedIndex = 0;
                    break;
                case 4:
                    comboBoxInk.SelectedIndex = 2;
                    comboBoxPaper.SelectedIndex = 0;
                    break;
                case 5:
                    comboBoxInk.SelectedIndex = 6;
                    comboBoxPaper.SelectedIndex = 4;
                    break;
            }
        }

        private void comboBoxInk_SelectedIndexChanged(object sender, EventArgs e)
        {
            ChangeColor();
        }

        private void comboBoxPaper_SelectedIndexChanged(object sender, EventArgs e)
        {
            ChangeColor();
        }

        void ChangeColor()
        {
            labelStupid.Visible = comboBoxInk.SelectedIndex == comboBoxPaper.SelectedIndex;
            if (comboBoxInk.SelectedIndex == 0 & comboBoxPaper.SelectedIndex == 7)
            {
                comboBoxThemes.SelectedIndex = 0;
                return;
            }
            if (comboBoxInk.SelectedIndex == 7 & comboBoxPaper.SelectedIndex == 0)
            {
                comboBoxThemes.SelectedIndex = 1;
                return;
            }
            if (comboBoxInk.SelectedIndex == 5 & comboBoxPaper.SelectedIndex == 1)
            {
                comboBoxThemes.SelectedIndex = 2;
                return;
            }
            if (comboBoxInk.SelectedIndex == 4 & comboBoxPaper.SelectedIndex == 0)
            {
                comboBoxThemes.SelectedIndex = 3;
                return;
            }
            if (comboBoxInk.SelectedIndex == 2 & comboBoxPaper.SelectedIndex == 0)
            {
                comboBoxThemes.SelectedIndex = 4;
                return;
            }
            if (comboBoxInk.SelectedIndex == 6 & comboBoxPaper.SelectedIndex == 4)
            {
                comboBoxThemes.SelectedIndex = 5;
                return;
            }
            comboBoxThemes.SelectedIndex = 6;
        }
    }
}
