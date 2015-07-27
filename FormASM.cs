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
    public partial class FormASM : Form
    {
        int InString = 0;
        string ASM = "";
        string Str = "";
        public FormASM()
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
            saveFileDialog1.FileName = "";
            saveFileDialog1.Title = "Сохранение кода Assembler'а";
            saveFileDialog1.Filter = "Код Assembler'а (*.asm)|*.asm|Все файлы(*.*)|*.*";
            if (saveFileDialog1.ShowDialog() == DialogResult.OK)
                try
                {
                    System.IO.TextWriter file = new System.IO.StreamWriter(saveFileDialog1.FileName);
                    file.Write(textBox2.Text);
                    file.Close();
                }
                catch
                {
                    Editor.Error("Ошибка при сохранении файла. Файл не сохранён.");
                }
        }

        void DrawCode()
        {
            ASM = "";
            InString = 0;
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
            if (InString != 0) ASM += Str;
            textBox2.Text = ASM;
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
                    if (InString == 0)
                    {
                        Str = "";
                        for (int i = 0; i < numericUpDown1.Value; i++) Str += " ";
                        Str += textBox1.Text + " ";
                        InString = 1;
                    }
                    else
                        Str += ", ";
                    //Добавляем, собственно, сам байт
                    Str += bb.ToString();
                    //Проверяем, стоит ли переходить на следущую строку
                    if (Str.Length > numericUpDown2.Value - 5)
                    {
                        ASM += Str + (char)13 + (char)10;
                        InString = 0;
                    }
                    bb = 0;
                }
            }
        }

        private void FormASM_Load(object sender, EventArgs e)
        {
            DrawCode();
        }

        private void numericUpDown1_ValueChanged(object sender, EventArgs e)
        {
            DrawCode();
        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {
            DrawCode();
        }

        private void numericUpDown2_ValueChanged(object sender, EventArgs e)
        {
            DrawCode();
        }

        private void checkBox1_CheckedChanged(object sender, EventArgs e)
        {
            DrawCode();
        }
    }
}
