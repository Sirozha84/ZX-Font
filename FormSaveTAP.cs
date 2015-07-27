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
    public partial class FormSaveTAP : Form
    {
        public FormSaveTAP()
        {
            InitializeComponent();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void FormSaveTAP_Load(object sender, EventArgs e)
        {
            saveFileDialog1.FileName = "";
            saveFileDialog1.Title = "Сохранение шрифта в TAP-файл";
            saveFileDialog1.Filter = "Образ ленты (*.tap)|*.tap|Все файлы(*.*)|*.*";
            if (saveFileDialog1.ShowDialog() == DialogResult.Cancel) Close();
            label2.Text = saveFileDialog1.FileName;
            textBox1.Text = System.IO.Path.GetFileNameWithoutExtension(label2.Text);
        }

        private void button4_Click(object sender, EventArgs e)
        {
            if (saveFileDialog1.ShowDialog() == DialogResult.OK)
                label2.Text = saveFileDialog1.FileName;
        }
        //Сохраняем ТАПушку.....
        private void button3_Click(object sender, EventArgs e)
        {
            //Делаем массив байт с шрифтом
            List<byte> Bytes = new List<byte>();
            Bytes.Add(255);
            if (checkBox1.Checked)
            {
                //Построчно
                for (int l = 0; l < FormMain.CurrentProject.SizeY; l++)
                    for (int s = 0; s < FormMain.CurrentProject.Symbols; s++)
                    {
                        byte bb = 0;
                        for (int b = 0; b < FormMain.CurrentProject.SizeX; b++)
                        {
                            bb += (byte)add(s, l, b);
                            if (b == 7 || b == FormMain.CurrentProject.SizeX - 1) { Bytes.Add(bb); bb = 0; }
                        }
                    }
            }
            else
            {
                //Стандартно
                for (int s = 0; s < FormMain.CurrentProject.Symbols; s++)
                    for (int l = 0; l < FormMain.CurrentProject.SizeY; l++)
                    {
                        byte bb = 0;
                        for (int b = 0; b < FormMain.CurrentProject.SizeX; b++)
                        {
                            bb += (byte)add(s, l, b);
                            if (b == 7 || b == FormMain.CurrentProject.SizeX - 1) { Bytes.Add(bb); bb = 0; }
                        }
                    }
            }
            //Считаем CRC
            byte CRC = 0;
            foreach (byte b in Bytes) CRC = (byte)(CRC ^ b);
            Bytes.Add(CRC);
            //Готовим заголовок
            List<byte> Title = new List<byte>();
            Title.Add(0);
            Title.Add(3);
            for (int i = 0; i < 10; i++)
                if (i < textBox1.Text.Length) Title.Add((byte)textBox1.Text[i]); else Title.Add(32);
            Title.Add((byte)((Bytes.Count() - 2) % 256)); //Размер
            Title.Add((byte)((Bytes.Count() - 2) / 256));
            Title.Add((byte)(numericUpDown1.Value % 256)); //Адрес
            Title.Add((byte)(numericUpDown1.Value / 256));
            Title.Add(0); //Забыл чё...
            Title.Add(0);
            //Считаем CRC
            CRC = 0;
            foreach (byte b in Title) CRC = (byte)(CRC ^ b);
            Title.Add(CRC);
            //Пишем файл
            System.IO.BinaryWriter file = new System.IO.BinaryWriter(new System.IO.FileStream(label2.Text, System.IO.FileMode.Create));
            file.Write((ushort)Title.Count());
            file.Write(Title.ToArray());
            file.Write((ushort)Bytes.Count());
            file.Write(Bytes.ToArray());
            file.Close();
        }
        int add(int s, int l, int b)
        {
            if (b >= FormMain.CurrentProject.SizeX) return 0;
            if (b == 0) return FormMain.CurrentProject.Font[s + FormMain.CurrentProject.ADD, l, b] * 128;
            if (b == 1) return FormMain.CurrentProject.Font[s + FormMain.CurrentProject.ADD, l, b] * 64;
            if (b == 2) return FormMain.CurrentProject.Font[s + FormMain.CurrentProject.ADD, l, b] * 32;
            if (b == 3) return FormMain.CurrentProject.Font[s + FormMain.CurrentProject.ADD, l, b] * 16;
            if (b == 4) return FormMain.CurrentProject.Font[s + FormMain.CurrentProject.ADD, l, b] * 8;
            if (b == 5) return FormMain.CurrentProject.Font[s + FormMain.CurrentProject.ADD, l, b] * 4;
            if (b == 6) return FormMain.CurrentProject.Font[s + FormMain.CurrentProject.ADD, l, b] * 2;
            if (b == 7) return FormMain.CurrentProject.Font[s + FormMain.CurrentProject.ADD, l, b] * 1;
            if (b == 8) return FormMain.CurrentProject.Font[s + FormMain.CurrentProject.ADD, l, b] * 128;
            if (b == 9) return FormMain.CurrentProject.Font[s + FormMain.CurrentProject.ADD, l, b] * 64;
            if (b == 10) return FormMain.CurrentProject.Font[s + FormMain.CurrentProject.ADD, l, b] * 32;
            if (b == 11) return FormMain.CurrentProject.Font[s + FormMain.CurrentProject.ADD, l, b] * 16;
            if (b == 12) return FormMain.CurrentProject.Font[s + FormMain.CurrentProject.ADD, l, b] * 8;
            if (b == 13) return FormMain.CurrentProject.Font[s + FormMain.CurrentProject.ADD, l, b] * 4;
            if (b == 14) return FormMain.CurrentProject.Font[s + FormMain.CurrentProject.ADD, l, b] * 2;
            if (b == 15) return FormMain.CurrentProject.Font[s + FormMain.CurrentProject.ADD, l, b] * 1;
            return 0;
        }
    }
}
