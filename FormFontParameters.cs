using System;
using System.Windows.Forms;

namespace ZXFont
{
    public partial class FormFontParameters : Form
    {
        int WidthBefore = FormMain.CurrentProject.SizeX;
        int HeightBefore = FormMain.CurrentProject.SizeY;

        public FormFontParameters()
        {
            InitializeComponent();
            if (FormMain.CurrentProject.Symbols == 96) comboBox1.SelectedIndex = 0;
            if (FormMain.CurrentProject.Symbols == 224) comboBox1.SelectedIndex = 1;
            if (FormMain.CurrentProject.Symbols == 256) comboBox1.SelectedIndex = 2;
            numericUpDown1.Value = FormMain.CurrentProject.SizeX;
            numericUpDown2.Value = FormMain.CurrentProject.SizeY;
            numericUpDown3.Value = Properties.Settings.Default.BorderTop;
            numericUpDown4.Value = Properties.Settings.Default.BorderTopP;
            numericUpDown5.Value = Properties.Settings.Default.BorderLeft;
            numericUpDown6.Value = Properties.Settings.Default.BorderRight;
            numericUpDown7.Value = Properties.Settings.Default.BorderBottom;
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
            //Если надо растянуть
            if (checkBoxScale.Checked)
            {
                byte[,] New = new byte[FormMain.CurrentProject.SizeY, FormMain.CurrentProject.SizeX];
                //Надо высчитать какие-то коэффициенты скейла
                float Xs = (float)WidthBefore / FormMain.CurrentProject.SizeX;
                float Ys = (float)HeightBefore / FormMain.CurrentProject.SizeY;
                for (int s = 0; s < 255; s++)
                {
                    //Вводим во временную память изменённый символ
                    for (int i = 0; i < FormMain.CurrentProject.SizeY; i++)
                        for (int j = 0; j < FormMain.CurrentProject.SizeX; j++)
                        {
                            New[i, j] = FormMain.CurrentProject.Font[s, (int)(i * Ys), (int)(j * Xs)];
                        }
                    //Запихиваем его обратно
                    for (int i = 0; i < FormMain.CurrentProject.SizeY; i++)
                        for (int j = 0; j < FormMain.CurrentProject.SizeX; j++)
                        {
                            FormMain.CurrentProject.Font[s, i, j] = New[i, j];
                        }
                }
            }
            Properties.Settings.Default.BorderTop = (byte)numericUpDown3.Value;
            Properties.Settings.Default.BorderTopP = (byte)numericUpDown4.Value;
            Properties.Settings.Default.BorderLeft = (byte)numericUpDown5.Value;
            Properties.Settings.Default.BorderRight = (byte)numericUpDown6.Value;
            Properties.Settings.Default.BorderBottom = (byte)numericUpDown7.Value;
            DialogResult = DialogResult.OK;
            Close();
        }
    }
}
