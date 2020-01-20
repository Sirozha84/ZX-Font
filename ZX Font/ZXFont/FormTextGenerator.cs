using System;
using System.Windows.Forms;
using System.IO;

namespace ZXFont
{
    public partial class FormTextGenerator : Form
    {
        string Str;
        int Code;
        public FormTextGenerator()
        {
            InitializeComponent();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            Close();
        }
        //Сохранение полученного текста
        private void button1_Click(object sender, EventArgs e)
        {
            SaveFileDialog dialog = new SaveFileDialog();
            dialog.FileName = "";
            dialog.Title = "Сохранение текстового файла";
            dialog.Filter = "Текстовый файл|*.txt|Все файлы|*.*";
            if (dialog.ShowDialog() == DialogResult.OK)
                try
                {
                    File.WriteAllText(dialog.FileName, textBoxText.Text);
                }
                catch
                {
                    Program.Error("Ошибка при сохранении файла. Файл не сохранён.");
                }
        }

        void DrawCode()
        {
            Str = "";
            Code = 0;
            if (checkBox1.Checked)
            {
                //Построчно
                for (int l = 0; l < FormMain.CurrentProject.SizeY; l++)
                    for (int s = 0; s < FormMain.CurrentProject.Symbols; s++)
                        AddByte(s + FormMain.CurrentProject.ADD, l);
            }
            else
            {
                //Стандартно
                for (int s = 0; s < FormMain.CurrentProject.Symbols; s++)
                    for (int l = 0; l < FormMain.CurrentProject.SizeY; l++)
                        AddByte(s + FormMain.CurrentProject.ADD, l);
            }
            textBoxText.Text = Str;
        }
        void AddByte(int s, int l)
        {
            byte bb = 0;
            for (int b = 0; b < FormMain.CurrentProject.SizeX; b++)
            {
                if (b == 0 & FormMain.CurrentProject.Font[s, l, b] == 1) bb += 128;
                if (b == 1 & FormMain.CurrentProject.Font[s, l, b] == 1) bb += 64;
                if (b == 2 & FormMain.CurrentProject.Font[s, l, b] == 1) bb += 32;
                if (b == 3 & FormMain.CurrentProject.Font[s, l, b] == 1) bb += 16;
                if (b == 4 & FormMain.CurrentProject.Font[s, l, b] == 1) bb += 8;
                if (b == 5 & FormMain.CurrentProject.Font[s, l, b] == 1) bb += 4;
                if (b == 6 & FormMain.CurrentProject.Font[s, l, b] == 1) bb += 2;
                if (b == 7 & FormMain.CurrentProject.Font[s, l, b] == 1) bb += 1;
                if (b == 8 & FormMain.CurrentProject.Font[s, l, b] == 1) bb += 128;
                if (b == 9 & FormMain.CurrentProject.Font[s, l, b] == 1) bb += 64;
                if (b == 10 & FormMain.CurrentProject.Font[s, l, b] == 1) bb += 32;
                if (b == 11 & FormMain.CurrentProject.Font[s, l, b] == 1) bb += 16;
                if (b == 12 & FormMain.CurrentProject.Font[s, l, b] == 1) bb += 8;
                if (b == 13 & FormMain.CurrentProject.Font[s, l, b] == 1) bb += 4;
                if (b == 14 & FormMain.CurrentProject.Font[s, l, b] == 1) bb += 2;
                if (b == 15 & FormMain.CurrentProject.Font[s, l, b] == 1) bb += 1;
                if (b == 8 | b == FormMain.CurrentProject.SizeX - 1)
                {
                    //Добавляем начало (DEFB или Запятую)
                    if (Code == 0)
                        Str += comboBoxStart.Text;
                    else
                        Str += comboBoxSeparator.Text;
                    //Добавляем, собственно, сам байт
                    if (checkBoxHex.Checked)
                        Str += Digits.ToHex(bb);
                    else
                        Str += bb.ToString();
                    //Проверяем, стоит ли переходить на следущую строку
                    bb = 0;
                    Code++;
                    if (Code >= numericUpDownCodes.Value)
                    {
                        Code = 0;
                        Str += comboBoxEnd.Text + Environment.NewLine;
                    }
                }
            }
            //Code++;
        }

        private void FormASM_Load(object sender, EventArgs e)
        {
            DrawCode();
        }

        private void numericUpDownCodes_ValueChanged(object sender, EventArgs e)
        {
            DrawCode();
        }

        private void checkBox1_CheckedChanged(object sender, EventArgs e)
        {
            DrawCode();
        }

        private void checkBoxHex_CheckedChanged(object sender, EventArgs e)
        {
            DrawCode();
        }

        private void comboBoxStart_TextChanged(object sender, EventArgs e)
        {
            DrawCode();
        }

        private void comboBoxSeparator_TextChanged(object sender, EventArgs e)
        {
            DrawCode();
        }

        private void comboBoxEnd_TextChanged(object sender, EventArgs e)
        {
            DrawCode();
        }
    }
}
