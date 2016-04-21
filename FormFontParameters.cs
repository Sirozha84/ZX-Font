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
            if (FormMain.CurrentProject.Symbols == 96) comboBoxCount.SelectedIndex = 0;
            if (FormMain.CurrentProject.Symbols == 224) comboBoxCount.SelectedIndex = 1;
            if (FormMain.CurrentProject.Symbols == 256) comboBoxCount.SelectedIndex = 2;
            numericUpDownWidth.Value = FormMain.CurrentProject.SizeX;
            numericUpDownHeight.Value = FormMain.CurrentProject.SizeY;
        }

        private void buttonCancel_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void buttonOK_Click(object sender, EventArgs e)
        {
            if (comboBoxCount.SelectedIndex == 0) { FormMain.CurrentProject.Symbols = 96; FormMain.CurrentProject.ADD = 32; }
            if (comboBoxCount.SelectedIndex == 1) { FormMain.CurrentProject.Symbols = 224; FormMain.CurrentProject.ADD = 32; }
            if (comboBoxCount.SelectedIndex == 2) { FormMain.CurrentProject.Symbols = 256; FormMain.CurrentProject.ADD = 0; }
            FormMain.CurrentProject.SizeX = (byte)numericUpDownWidth.Value;
            FormMain.CurrentProject.SizeY = (byte)numericUpDownHeight.Value;
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
            DialogResult = DialogResult.OK;
            Close();
        }
    }
}
