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
    public partial class FormLoadTAP : Form
    {
        byte[, ,] FONT = new byte[256, 16, 16];
        List<byte[]> Blocks = new List<byte[]>();
        List<int> Adr = new List<int>();

        public FormLoadTAP()
        {
            InitializeComponent();
            comboBox1.SelectedIndex = 0;
        }

        private void button2_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            //Перекачка найденного в проект
            FormMain.CurrentProject.SizeX = (byte)numericUpDown1.Value;
            FormMain.CurrentProject.SizeY = (byte)numericUpDown2.Value;
            FormMain.CurrentProject.Symbols = 96;
            FormMain.CurrentProject.ADD = 32;
            if (comboBox1.SelectedIndex == 1) FormMain.CurrentProject.Symbols = 224;
            if (comboBox1.SelectedIndex == 2) { FormMain.CurrentProject.Symbols = 256; FormMain.CurrentProject.ADD = 0; }
            for (int s = 0; s < 256; s++)
                for (int l = 0; l < 16; l++)
                    for (int b = 0; b < 16; b++)
                        FormMain.CurrentProject.Font[s, l, b] = FONT[s, l, b];
            DialogResult = DialogResult.OK;
            Close();
        }
        //Загрузка тапки
        private void FormLoadTAP_Load(object sender, EventArgs e)
        {
            openFileDialog1.FileName = "";
            openFileDialog1.Title = "Импорт из TAP-файла.";
            openFileDialog1.Filter = "Образ ленты (*.tap)|*.tap|Все файлы (*.*)|*.*";
            if (openFileDialog1.ShowDialog() != DialogResult.OK) Close();
            else
            {
                try
                {
                    System.IO.BinaryReader file = new System.IO.BinaryReader(new System.IO.FileStream(openFileDialog1.FileName, System.IO.FileMode.Open));
                    while (file.BaseStream.Position < file.BaseStream.Length)
                    {
                        int LEN = file.ReadUInt16();
                        byte FB = file.ReadByte();
                        byte[] Bytes = file.ReadBytes(LEN - 2);
                        file.ReadByte();
                        if (FB > 0)
                        {
                            Blocks.Add(Bytes);
                            listBox1.Items.Add("Блок в " + Bytes.Length + " байт(а)");
                        }
                    }
                    file.Close();
                    listBox1.SelectedIndex = 0;
                }
                catch
                {
                    Editor.Error("Произошла ошибка при загрузке файла.");
                    Close();
                }
            }
        }
        //Автоматический поиск шрифта в блоке...
        private void listBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (Blocks[listBox1.SelectedIndex].Count() < 400) return;
            Adr.Clear();
            listBox2.Items.Clear();
            for (int i = 0; i < Blocks[listBox1.SelectedIndex].Count() - 300; i++)
            {
                bool nul = true;
                for (int j = 0; j < 8; j++) if (Blocks[listBox1.SelectedIndex][i + j] > 0) nul = false;
                if (nul & Blocks[listBox1.SelectedIndex][i + 8] > 0)
                {
                    Adr.Add(i);
                    listBox2.Items.Add("Байт №" + i.ToString());
                }
            }
            numericUpDown3.Value = 0;
            Drawfont();
        }

        private void listBox2_SelectedIndexChanged(object sender, EventArgs e)
        {
            numericUpDown3.Value = Adr[listBox2.SelectedIndex];
        }

        private void numericUpDown3_ValueChanged(object sender, EventArgs e)
        {
            Drawfont();
        }
        //Загрузка и рисование шрифта
        void Drawfont()
        {
            int SM = 96;
            int ADD = 32;
            int SX = (int)numericUpDown1.Value;
            int SY = (int)numericUpDown2.Value;
            int CB = listBox1.SelectedIndex;
            if (CB < 0) return;
            FONT = new byte[256, 16, 16];
            if (comboBox1.SelectedIndex == 1) SM = 224;
            if (comboBox1.SelectedIndex == 2) { SM = 256; ADD = 0; }
            //Чтение шрифта из блока
            int B = (int)numericUpDown3.Value;
            if (!checkBox1.Checked)
            {
                for (int s = 0; s < SM; s++)
                    for (int l = 0; l < SY; l++)
                    {
                        for (int b = 0; b < SX; b++)
                        {
                            if (b == 0 && B < Blocks[CB].Count() && (Blocks[CB][B] & 128) == 128) FONT[s + ADD, l, b] = 1;
                            if (b == 1 && B < Blocks[CB].Count() && (Blocks[CB][B] & 64) == 64) FONT[s + ADD, l, b] = 1;
                            if (b == 2 && B < Blocks[CB].Count() && (Blocks[CB][B] & 32) == 32) FONT[s + ADD, l, b] = 1;
                            if (b == 3 && B < Blocks[CB].Count() && (Blocks[CB][B] & 16) == 16) FONT[s + ADD, l, b] = 1;
                            if (b == 4 && B < Blocks[CB].Count() && (Blocks[CB][B] & 8) == 8) FONT[s + ADD, l, b] = 1;
                            if (b == 5 && B < Blocks[CB].Count() && (Blocks[CB][B] & 4) == 4) FONT[s + ADD, l, b] = 1;
                            if (b == 6 && B < Blocks[CB].Count() && (Blocks[CB][B] & 2) == 2) FONT[s + ADD, l, b] = 1;
                            if (b == 7 && B < Blocks[CB].Count() && (Blocks[CB][B] & 1) == 1) FONT[s + ADD, l, b] = 1;
                            if (b == 8) B++;
                            if (b == 8 && B < Blocks[CB].Count() && (Blocks[CB][B] & 128) == 128) FONT[s + ADD, l, b] = 1;
                            if (b == 9 && B < Blocks[CB].Count() && (Blocks[CB][B] & 64) == 64) FONT[s + ADD, l, b] = 1;
                            if (b == 10 && B < Blocks[CB].Count() && (Blocks[CB][B] & 32) == 32) FONT[s + ADD, l, b] = 1;
                            if (b == 11 && B < Blocks[CB].Count() && (Blocks[CB][B] & 16) == 16) FONT[s + ADD, l, b] = 1;
                            if (b == 12 && B < Blocks[CB].Count() && (Blocks[CB][B] & 8) == 8) FONT[s + ADD, l, b] = 1;
                            if (b == 13 && B < Blocks[CB].Count() && (Blocks[CB][B] & 4) == 4) FONT[s + ADD, l, b] = 1;
                            if (b == 14 && B < Blocks[CB].Count() && (Blocks[CB][B] & 2) == 2) FONT[s + ADD, l, b] = 1;
                            if (b == 15 && B < Blocks[CB].Count() && (Blocks[CB][B] & 1) == 1) FONT[s + ADD, l, b] = 1;
                        }
                        B++;
                    }
            }
            else
            {
                for (int l = 0; l < SY; l++)
                    for (int s = 0; s < SM; s++)
                    {
                        for (int b = 0; b < SX; b++)
                        {
                            if (b == 0 && B < Blocks[CB].Count() && (Blocks[CB][B] & 128) == 128) FONT[s + ADD, l, b] = 1;
                            if (b == 1 && B < Blocks[CB].Count() && (Blocks[CB][B] & 64) == 64) FONT[s + ADD, l, b] = 1;
                            if (b == 2 && B < Blocks[CB].Count() && (Blocks[CB][B] & 32) == 32) FONT[s + ADD, l, b] = 1;
                            if (b == 3 && B < Blocks[CB].Count() && (Blocks[CB][B] & 16) == 16) FONT[s + ADD, l, b] = 1;
                            if (b == 4 && B < Blocks[CB].Count() && (Blocks[CB][B] & 8) == 8) FONT[s + ADD, l, b] = 1;
                            if (b == 5 && B < Blocks[CB].Count() && (Blocks[CB][B] & 4) == 4) FONT[s + ADD, l, b] = 1;
                            if (b == 6 && B < Blocks[CB].Count() && (Blocks[CB][B] & 2) == 2) FONT[s + ADD, l, b] = 1;
                            if (b == 7 && B < Blocks[CB].Count() && (Blocks[CB][B] & 1) == 1) FONT[s + ADD, l, b] = 1;
                            if (b == 8) B++;
                            if (b == 8 && B < Blocks[CB].Count() && (Blocks[CB][B] & 128) == 128) FONT[s + ADD, l, b] = 1;
                            if (b == 9 && B < Blocks[CB].Count() && (Blocks[CB][B] & 64) == 64) FONT[s + ADD, l, b] = 1;
                            if (b == 10 && B < Blocks[CB].Count() && (Blocks[CB][B] & 32) == 32) FONT[s + ADD, l, b] = 1;
                            if (b == 11 && B < Blocks[CB].Count() && (Blocks[CB][B] & 16) == 16) FONT[s + ADD, l, b] = 1;
                            if (b == 12 && B < Blocks[CB].Count() && (Blocks[CB][B] & 8) == 8) FONT[s + ADD, l, b] = 1;
                            if (b == 13 && B < Blocks[CB].Count() && (Blocks[CB][B] & 4) == 4) FONT[s + ADD, l, b] = 1;
                            if (b == 14 && B < Blocks[CB].Count() && (Blocks[CB][B] & 2) == 2) FONT[s + ADD, l, b] = 1;
                            if (b == 15 && B < Blocks[CB].Count() && (Blocks[CB][B] & 1) == 1) FONT[s + ADD, l, b] = 1;
                        }
                        B++;
                    }
            }
            //Рисование того, что получилось
            Bitmap buffer = new Bitmap(SX * 32, SY * SM / 32);
            buffer.SetPixel(0, 0, Color.Black);
            for (int s = 0; s < SM; s++)
                for (int l = 0; l < SY; l++)
                    for (int b = 0; b < SX; b++)
                    {
                        Color pix = Color.White;
                        if (FONT[s + ADD, l, b] == 1) pix = Color.Black;
                        int x = (s % 32) * SX + b;
                        int y = (s / 32) * SY + l;
                        buffer.SetPixel(x, y, pix);
                    }
            pictureBox1.Image = buffer;
            //Ух ты! Даже что-то получилось! Больше всего боялся это делать, но, получилось!
        }

        private void numericUpDown1_ValueChanged(object sender, EventArgs e)
        {
            Drawfont();
        }

        private void numericUpDown2_ValueChanged(object sender, EventArgs e)
        {
            Drawfont();
        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            Drawfont();
        }

        private void checkBox1_CheckedChanged(object sender, EventArgs e)
        {
            Drawfont();
        }
    }
}
