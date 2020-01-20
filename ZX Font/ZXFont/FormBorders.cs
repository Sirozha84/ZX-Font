using System;
using System.Windows.Forms;

namespace ZXFont
{
    public partial class FormBorders : Form
    {
        int WidthBefore = FormMain.CurrentProject.SizeX;
        int HeightBefore = FormMain.CurrentProject.SizeY;

        public FormBorders()
        {
            InitializeComponent();
            numericUpDownTop.Value = Properties.Settings.Default.BorderTop;
            numericUpDownTopP.Value = Properties.Settings.Default.BorderTopP;
            numericUpDownLeft.Value = Properties.Settings.Default.BorderLeft;
            numericUpDownRight.Value = Properties.Settings.Default.BorderRight;
            numericUpDownBottom.Value = Properties.Settings.Default.BorderBottom;
        }

        private void buttonCancel_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void buttonOK_Click(object sender, EventArgs e)
        {
            Properties.Settings.Default.BorderTop = (int)numericUpDownTop.Value;
            Properties.Settings.Default.BorderTopP = (int)numericUpDownTopP.Value;
            Properties.Settings.Default.BorderLeft = (int)numericUpDownLeft.Value;
            Properties.Settings.Default.BorderRight = (int)numericUpDownRight.Value;
            Properties.Settings.Default.BorderBottom = (int)numericUpDownBottom.Value;
            Close();
        }
    }
}
