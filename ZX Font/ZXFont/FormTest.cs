using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

//using System.Threading;

namespace ZXFont
{
    public partial class FormTest : Form
    {
        Bitmap Screen = new Bitmap(512, 384);
        bool Flash = true;
        string Test = Properties.Settings.Default.TestString;
        Color Ink = Program.ZXColor[Properties.Settings.Default.Ink];
        Color Paper = Program.ZXColor[Properties.Settings.Default.Paper];
        int Width = 256 / FormMain.CurrentProject.SizeX;
        int Height = 192 / FormMain.CurrentProject.SizeY;
        int Max;

        public FormTest()
        {
            InitializeComponent();
            Max = Width * Height - 1;
            //Заполняем весь фон пэпером... мало ли текущий шрифт будет рисоваться не везде
            for (int i = 0; i < 512; i++)
                for (int j = 0; j < 384; j++)
                    Screen.SetPixel(i, j, Paper);
            DrawText(0, Max);
            //Если надо, укорачиваем строку, а то мало ли не влезет...
            if (Test.Length > Max) Test = Test.Substring(0, Max);
        }

        private void timerCursor_Tick(object sender, EventArgs e)
        {
            Flash ^= true;
            DrawText(Test.Length, Test.Length + 1);
        }

        void DrawText(int Start, int End)
        {
            for (int i = Start; i < End; i++)
            {
                //Находим координаты знакоместа
                int x = i % Width;
                int y = i / Width;
                int xscale = FormMain.CurrentProject.SizeX * 2;
                int yscale = FormMain.CurrentProject.SizeY * 2;
                for (int xx = 0; xx < FormMain.CurrentProject.SizeX; xx++)
                    for (int yy = 0; yy < FormMain.CurrentProject.SizeY; yy++)
                    {
                        Color pix = Paper;
                        if (i < Test.Length && FormMain.CurrentProject.Font[Test[i], yy, xx] == 1)
                            pix = Ink;
                        if (i == Test.Length) pix = Flash ? Ink : Paper;
                        Screen.SetPixel(x * xscale + xx * 2, y * yscale + yy * 2, pix);
                        Screen.SetPixel(x * xscale + xx * 2 + 1, y * yscale + yy * 2, pix);
                        Screen.SetPixel(x * xscale + xx * 2, y * yscale + yy * 2 + 1, pix);
                        Screen.SetPixel(x * xscale + xx * 2 + 1, y * yscale + yy * 2 + 1, pix);
                    }
            }
            pictureBox1.Image = Screen;
        }

        private void FormTest_KeyDown(object sender, KeyEventArgs e)
        {
            int key = Letters.KeyByKeuboard(e);
            if (key > 0 & Test.Length < Max)
            {
                Test += (char)key;
                DrawText(Test.Length - 1, Test.Length + 1);
            }
            if (e.KeyCode == Keys.Back & Test.Length > 0)
            {
                Test = Test.Substring(0, Test.Length - 1);
                DrawText(Test.Length, Test.Length + 2);
            }
            if (e.KeyCode == Keys.Delete)
            {
                Test = "";
                DrawText(0, Max);
            }
            if (e.KeyCode == Keys.Escape) Close();
        }

        private void FormTest_FormClosed(object sender, FormClosedEventArgs e)
        {
            Properties.Settings.Default.TestString = Test;
        }
    }
}
