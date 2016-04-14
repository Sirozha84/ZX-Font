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
    public partial class FormLoadBMP : Form
    {
        Bitmap BMP;
        string FileName;

        public FormLoadBMP()
        {
            InitializeComponent();
        }

        public FormLoadBMP(string FileName)
        {
            InitializeComponent();
            this.FileName = FileName;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            byte SizeX = (byte)numericUpDown1.Value;
            byte SizeY = (byte)numericUpDown2.Value;
            DialogResult = DialogResult.OK;
            FormMain.CurrentProject.SizeX = SizeX;
            FormMain.CurrentProject.SizeY = SizeY;
            FormMain.CurrentProject.ADD = 32;
            if (comboBox1.SelectedIndex == 1) FormMain.CurrentProject.ADD = 0;
            //Теперь почти всё тоже самое что и при рисовании сетки
            int i = 0;
            for (int y = 0; y < BMP.Height - SizeY + 1; y += SizeY)
                for (int x = 0; x < BMP.Width - SizeX + 1; x += SizeX)
                {
                    for (int yy = 0; yy < SizeY; yy++)
                        for (int xx = 0; xx < SizeX; xx++)
                            if (i + FormMain.CurrentProject.ADD <= 255)
                            {
                                FormMain.CurrentProject.Font[i + FormMain.CurrentProject.ADD, yy, xx] = 0;
                                int brig = BMP.GetPixel(x + xx, y + yy).R +
                                    BMP.GetPixel(x + xx, y + yy).G +
                                    BMP.GetPixel(x + xx, y + yy).B;
                                if (brig < 382)
                                    FormMain.CurrentProject.Font[i + FormMain.CurrentProject.ADD, yy, xx] = 1;
                            }
                    i++;
                }
            FormMain.CurrentProject.Symbols = 96;
            if (i > 96) FormMain.CurrentProject.Symbols = 224;
            if (i > 224 & FormMain.CurrentProject.ADD==0) FormMain.CurrentProject.Symbols = 256;
        }

        void DrawBMP()
        {
            Bitmap Buffer = new Bitmap(BMP);
            Graphics Canvas = Graphics.FromImage(Buffer);
            //Рисуем сетку
            int i = 0;
            for (int y = 0; y < BMP.Height - (int)numericUpDown2.Value + 1; y += (int)numericUpDown2.Value)
                for (int x = 0; x < BMP.Width - (int)numericUpDown1.Value + 1; x += (int)numericUpDown1.Value)
                {
                    Canvas.DrawRectangle(Pens.Silver, x, y, (int)numericUpDown1.Value, (int)numericUpDown2.Value);
                    i++;
                }
            label5.Text = i.ToString();
            pictureBox1.Image = Buffer;
        }

        private void numericUpDown1_ValueChanged(object sender, EventArgs e)
        {
            DrawBMP();
        }

        private void numericUpDown2_ValueChanged(object sender, EventArgs e)
        {
            DrawBMP();
        }

        private void FormLoadBMP_Load(object sender, EventArgs e)
        {
            bool OK;
            if (FileName == null)
                using (OpenFileDialog dialog = new OpenFileDialog())
                {
                    comboBox1.SelectedIndex = 0;
                    dialog.FileName = "";
                    dialog.Title = "Импорт из картинки";
                    dialog.Filter = "Файлы изображений|*.bmp;*.png;*.jpg;*.jpeg;*.gif|Все файлы|*.*";
                    OK = dialog.ShowDialog() == DialogResult.OK;
                    FileName = dialog.FileName;
                }
            else
                OK = true;
            if (OK)
            {
                try
                {
                    BMP = new Bitmap(FileName);
                    DrawBMP();
                }
                catch
                {
                    Program.Error("Произошла ошибка при загрузке файла.");
                    Dispose();
                }
            }
            else
                Dispose();
        }
    }
}
