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
    public partial class FormMain : Form
    {
        static public Project CurrentProject = new Project();
        static public int ShirinaX;
        static public int ShirinaY;
        static public int Naklon;
        List<Project> History = new List<Project>();
        int HistoryNumber;
        bool ProgramTextChange = true;
        System.Diagnostics.Process Help = new System.Diagnostics.Process();
        //Глобальные переменные редактора
        public int CurrentSymbol = 0;
        byte Tool = 0;
        public const byte Maximumsize = 16;
        byte[,] Buffer = new byte[Maximumsize, Maximumsize];
        Bitmap BitmapFont;
        Bitmap BitmapSymbol;
        Graphics Canvas;
        public FormMain()
        {
            InitializeComponent();
            Editor.init();
            Left = WindowsPosition.X;
            Top = WindowsPosition.Y;
            Width = WindowsPosition.Width;
            Height = WindowsPosition.Heidht;
            if (WindowsPosition.Max) WindowState = FormWindowState.Maximized; else WindowState = FormWindowState.Normal;
            splitter1.SplitPosition = WindowsPosition.Splitter;
            menunew_Click(null, null);
        }
        //Создание нового файла
        private void menunew_Click(object sender, EventArgs e)
        {
            if (!SaveQuestion()) return;
            CurrentProject.NewProject();
            CurrentSymbol = CurrentProject.ADD;
            InitBitmaps();
            DrawDocument();
            ResetHistory();
            Change(true);
        }
        //Открытие файла
        void OpenFile()
        {
            if (!SaveQuestion()) return;
            openFileDialog1.FileName = "";
            openFileDialog1.Filter = Editor.FileType;
            if (openFileDialog1.ShowDialog() != DialogResult.OK) return;
            if (!CurrentProject.Open(openFileDialog1.FileName)) return;
            Project.EditName = openFileDialog1.FileName;
            CurrentSymbol = CurrentProject.ADD;
            InitBitmaps();
            DrawDocument();
            ResetHistory();
            Change(true);
        }
        //Сохранение файла
        bool FileSave()
        {
            if (Project.EditName == Editor.FileUnnamed && !FileSaveAs()) return false;
            if (!CurrentProject.Save()) return false;
            Change(true);
            return true;
        }
        //Сохранение файла как
        bool FileSaveAs()
        {
            saveFileDialog1.FileName = "";
            saveFileDialog1.Filter = Editor.FileType;
            if (saveFileDialog1.ShowDialog() == DialogResult.OK) { Project.EditName = saveFileDialog1.FileName; Change(true); return true; }
            return false;
        }
        //Отмена
        private void menuundo_Click(object sender, EventArgs e)
        {
            if (HistoryNumber < 2) return;
            HistoryNumber--;
            CurrentProject.Copy(History[HistoryNumber - 1]);
            DrawDocument();
        }
        //Возврат
        private void menuredo_Click(object sender, EventArgs e)
        {
            if (HistoryNumber == History.Count) return;
            HistoryNumber++;
            CurrentProject.Copy(History[HistoryNumber - 1]);
            DrawDocument();
            //Change(false);
        }
        //Вырезать
        private void menucut_Click(object sender, EventArgs e)
        {
            for (int i = 0; i < CurrentProject.SizeX; i++)
                for (int j = 0; j < CurrentProject.SizeY; j++)
                {
                    Buffer[i, j] = CurrentProject.Font[CurrentSymbol, i, j];
                    CurrentProject.Font[CurrentSymbol, i, j] = 0;
                }
            DrawSymbol();
            DrawDocument();
            Change(false);
        }
        //Копировать
        private void menucopy_Click(object sender, EventArgs e)
        {
            for (int i = 0; i < CurrentProject.SizeX; i++)
                for (int j = 0; j < CurrentProject.SizeY; j++)
                    Buffer[i, j] = CurrentProject.Font[CurrentSymbol, i, j];
        }
        //Вставить
        private void menupaste_Click(object sender, EventArgs e)
        {
            for (int i = 0; i < CurrentProject.SizeX; i++)
                for (int j = 0; j < CurrentProject.SizeY; j++)
                    CurrentProject.Font[CurrentSymbol, i, j] = Buffer[i, j];
            DrawSymbol();
            DrawDocument();
        }
        //Вызов справки
        private void menuhelp_Click(object sender, EventArgs e)
        {
            try { HelpClose(); Help.StartInfo.FileName = "help.chm"; Help.Start(); }
            catch { Editor.Error("Файл справки не найден."); } 
        }
        //Закрытие программы
        private void Form1_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (!SaveQuestion()) e.Cancel = true;
            if (WindowState == FormWindowState.Maximized) WindowsPosition.Max = true;
            else WindowsPosition.Max = false;
            Editor.saveconfig();
            HelpClose();
        }

        /// <summary>
        /// Инициализация битмапов (вызывается при изменении размеров)
        /// </summary>
        void InitBitmaps()
        {
            int mn = 1; //Множитель, удваивает размер пикселей в микрошрифтах
            int px = 16;
            if (CurrentProject.SizeX <= 8) mn = 2;
            BitmapFont = new Bitmap(CurrentProject.SizeX * 32 * mn, CurrentProject.SizeY * CurrentProject.Symbols / 32 * mn);
            BitmapSymbol = new Bitmap(CurrentProject.SizeX * px, CurrentProject.SizeY * px);
            Canvas = Graphics.FromImage(BitmapSymbol);
        }

        //Рисование шрифта
        void DrawDocument()
        {
            ProgramTextChange = true;
            int mn = 1; //Множитель, удваивает размер пикселей в микрошрифтах
            if (CurrentProject.SizeX <= 8) mn = 2;
            BitmapFont.SetPixel(0, 0, Color.Black);
            for (int s = 0; s < CurrentProject.Symbols; s++)
                for (int l = 0; l < CurrentProject.SizeY; l++)
                    for (int b = 0; b < CurrentProject.SizeX; b++)
                    {
                        Color pix = Color.White;
                        if (s + CurrentProject.ADD == CurrentSymbol) pix = Color.Yellow;
                        if (CurrentProject.Symbols == 256) CurrentProject.ADD = 0;
                        if (CurrentProject.Font[s + CurrentProject.ADD, l, b] == 1) pix = Color.Black;
                        int x = (s % 32) * CurrentProject.SizeX + b;
                        int y = (s / 32) * CurrentProject.SizeY + l;

                        BitmapFont.SetPixel(x * mn, y * mn, pix);
                        if (mn > 1)
                        {
                            BitmapFont.SetPixel(x * mn, y * mn + 1, pix);
                            BitmapFont.SetPixel(x * mn + 1, y * mn, pix);
                            BitmapFont.SetPixel(x * mn + 1, y * mn + 1, pix);
                        }
                    }
            pictureBox1.Image = BitmapFont;
            DrawSymbol();
        }
        //Рисование символа
        void DrawSymbol()
        {
            const int px = 16;
            //Рисуем ограничивающую сетку
            for (int i = 0; i < CurrentProject.SizeX; i++)
            {
                if (i < Project.XL)
                    for (int j = 0; j < CurrentProject.SizeY; j++)
                        Canvas.FillRectangle(Brushes.Silver, i * px, j * px, px, px);
                if (i >= Project.XL & i < CurrentProject.SizeX - Project.XR)
                {
                    for (int j = 0; j < CurrentProject.SizeY; j++)
                        Canvas.FillRectangle(Brushes.White, i * px, j * px, px, px);
                    for (int j = 0; j < Project.Yt; j++)
                        Canvas.FillRectangle(Brushes.LightGray, i * px, j * px, px, px);
                    for (int j = 0; j < Project.YT; j++)
                        Canvas.FillRectangle(Brushes.Silver, i * px, j * px, px, px);
                    for (int j = CurrentProject.SizeY - Project.YB; j < CurrentProject.SizeY; j++)
                        Canvas.FillRectangle(Brushes.Silver, i * px, j * px, px, px);
                }
                if (i >= CurrentProject.SizeX - Project.XR)
                    for (int j = 0; j < CurrentProject.SizeY; j++)
                        Canvas.FillRectangle(Brushes.Silver, i * px, j * px, px, px);
            }
            //Рисуем символ
            for (int y = 0; y < CurrentProject.SizeY; y++) for (int x = 0; x < CurrentProject.SizeX; x++)
                    if (CurrentProject.Font[CurrentSymbol, y, x] != 0)
                        Canvas.FillRectangle(Brushes.Black, x * px, y * px, px, px);
            for (int i = px; i < CurrentProject.SizeX * px; i += px)
                Canvas.DrawLine(Pens.Gray, i, 0, i, CurrentProject.SizeY * px);
            for (int i = px; i < CurrentProject.SizeY * px; i += px)
                Canvas.DrawLine(Pens.Gray, 0, i, CurrentProject.SizeX * px + 0, i);
            pictureBox2.Image = BitmapSymbol;
            label2.Text = CurrentSymbol.ToString();
            label3.Text = "" + (char)CurrentSymbol;
            //Рисование кодов
            if (CurrentProject.SizeY > 0) label6.Text = Codes(CurrentSymbol, 0); else label6.Text = "";
            if (CurrentProject.SizeY > 1) label7.Text = Codes(CurrentSymbol, 1); else label7.Text = "";
            if (CurrentProject.SizeY > 2) label8.Text = Codes(CurrentSymbol, 2); else label8.Text = "";
            if (CurrentProject.SizeY > 3) label9.Text = Codes(CurrentSymbol, 3); else label9.Text = "";
            if (CurrentProject.SizeY > 4) label10.Text = Codes(CurrentSymbol, 4); else label10.Text = "";
            if (CurrentProject.SizeY > 5) label11.Text = Codes(CurrentSymbol, 5); else label11.Text = "";
            if (CurrentProject.SizeY > 6) label12.Text = Codes(CurrentSymbol, 6); else label12.Text = "";
            if (CurrentProject.SizeY > 7) label13.Text = Codes(CurrentSymbol, 7); else label13.Text = "";
            if (CurrentProject.SizeY > 8) label14.Text = Codes(CurrentSymbol, 8); else label14.Text = "";
            if (CurrentProject.SizeY > 9) label15.Text = Codes(CurrentSymbol, 9); else label15.Text = "";
            if (CurrentProject.SizeY > 10) label16.Text = Codes(CurrentSymbol, 10); else label16.Text = "";
            if (CurrentProject.SizeY > 11) label17.Text = Codes(CurrentSymbol, 11); else label17.Text = "";
            if (CurrentProject.SizeY > 12) label18.Text = Codes(CurrentSymbol, 12); else label18.Text = "";
            if (CurrentProject.SizeY > 13) label19.Text = Codes(CurrentSymbol, 13); else label19.Text = "";
            if (CurrentProject.SizeY > 14) label20.Text = Codes(CurrentSymbol, 14); else label20.Text = "";
            if (CurrentProject.SizeY > 15) label21.Text = Codes(CurrentSymbol, 15); else label21.Text = "";
        }
        //Превращение пикселей в коды
        string Codes(int s, int l)
        {
            byte Byte1 = 0;
            byte Byte2 = 0;
            string st = "";
            for (int b = 0; b < CurrentProject.SizeX; b++)
            {
                if (b == 0) Byte1 += (byte)(CurrentProject.Font[s, l, 0] * 128);
                if (b == 1) Byte1 += (byte)(CurrentProject.Font[s, l, 1] * 64);
                if (b == 2) Byte1 += (byte)(CurrentProject.Font[s, l, 2] * 32);
                if (b == 3) Byte1 += (byte)(CurrentProject.Font[s, l, 3] * 16);
                if (b == 4) Byte1 += (byte)(CurrentProject.Font[s, l, 4] * 8);
                if (b == 5) Byte1 += (byte)(CurrentProject.Font[s, l, 5] * 4);
                if (b == 6) Byte1 += (byte)(CurrentProject.Font[s, l, 6] * 2);
                if (b == 7) Byte1 += (byte)(CurrentProject.Font[s, l, 7] * 1);
                if (b == 0) Byte2 += (byte)(CurrentProject.Font[s, l, 8] * 128);
                if (b == 1) Byte2 += (byte)(CurrentProject.Font[s, l, 9] * 64);
                if (b == 2) Byte2 += (byte)(CurrentProject.Font[s, l, 10] * 32);
                if (b == 3) Byte2 += (byte)(CurrentProject.Font[s, l, 11] * 16);
                if (b == 4) Byte2 += (byte)(CurrentProject.Font[s, l, 12] * 8);
                if (b == 5) Byte2 += (byte)(CurrentProject.Font[s, l, 13] * 4);
                if (b == 6) Byte2 += (byte)(CurrentProject.Font[s, l, 14] * 2);
                if (b == 7) Byte2 += (byte)(CurrentProject.Font[s, l, 15] * 1);
            }
            if (CurrentProject.SizeX <= 8)
                st = Byte1.ToString();
            else
                st = Byte1.ToString() + ", "+ Byte2.ToString();
            return st;
        }
        //Рисование имени файла и программы
        void SetFormText()
        {
            string star = ""; 
            if (Project.Changed) star = "*";
            Text = System.IO.Path.GetFileNameWithoutExtension(Project.EditName) + star + " - " + Editor.ProgramName; 
        }
        //Регистрация изменений
        void Change(bool Reset) 
        { 
            if (Reset) Project.Changed = false; 
            else Project.Changed = true; 
            SetFormText();
            CreateUndo(); 
        }
        //Создание отмены
        void CreateUndo()
        {
            while (HistoryNumber < History.Count) History.RemoveAt(History.Count - 1);
            Project Copy = new Project();
            Copy.Copy(CurrentProject);
            History.Add(Copy);
            HistoryNumber++;
        }
        //регистрация изменений окна
        private void MainForm_ResizeEnd(object sender, EventArgs e)
        {
            WindowsPosition.X = Left;
            WindowsPosition.Y = Top;
            WindowsPosition.Width = Width;
            WindowsPosition.Heidht = Height;
            WindowsPosition.Max = false;
        }
        //Вопрос перед уничтожением проекта
        public bool SaveQuestion()
        {
            if (!Project.Changed) return true;
            //switch (MessageBox.Show("Сохранить изменения в файле \"" + System.IO.Path.GetFileNameWithoutExtension(Project.FileName) + "\"?", "Файл изменён",
            switch (MessageBox.Show("Сохранить изменения в файле \"" + System.IO.Path.GetFileNameWithoutExtension(Project.EditName)+ "\"?", "Файл изменён",
                MessageBoxButtons.YesNoCancel, MessageBoxIcon.Question))
            {
                case DialogResult.Yes: return FileSave();
                case DialogResult.No: return true;
                case DialogResult.Cancel: return false;
            }
            return false;
        }
        //Изменение в тексте (Главное что надо будет делать при изменении своих проектов, это регистрировать их с помощью Change(false);
        private void textBox1_TextChanged(object sender, EventArgs e)
        {
            if (ProgramTextChange) { ProgramTextChange = false; return; } //Наверное не понадобится
            //CurrentProject.Text = textBox1.Text; //Это тоже вряд ли понадобится
            Change(false);
        }
        //Прочие процедурки
        void ResetHistory() { History.Clear(); HistoryNumber = 0; }
        void HelpClose() { try { Help.Kill(); } catch { } }
        //Меню и панель инструментов
        private void menusave_Click(object sender, EventArgs e) { FileSave(); }
        private void menusaveas_Click(object sender, EventArgs e) { if (FileSaveAs()) FileSave(); }
        private void menuexit_Click(object sender, EventArgs e) { this.Close(); }
        private void menuabout_Click(object sender, EventArgs e) { FormAbout form = new FormAbout(); form.ShowDialog(); }
        private void toolnew_Click(object sender, EventArgs e) { menunew_Click(null, null); }
        private void toolopen_Click(object sender, EventArgs e) { menuopen_Click(null, null); }
        private void toolsave_Click(object sender, EventArgs e) { menusave_Click(null, null); }
        private void toolcut_Click(object sender, EventArgs e) { menucut_Click(null, null); }
        private void toolcopy_Click(object sender, EventArgs e) { menucopy_Click(null, null); }
        private void toolpaste_Click(object sender, EventArgs e) { menupaste_Click(null, null); }
        private void toolundo_Click(object sender, EventArgs e) { menuundo_Click(null, null); }
        private void toolredo_Click(object sender, EventArgs e) { menuredo_Click(null, null); }
        private void toolStripStatusLabel1_Click(object sender, EventArgs e) { System.Diagnostics.Process.Start("http://www.sg-software.ru"); }
        private void menuopen_Click(object sender, EventArgs e) { OpenFile(); }

        private void pictureBox2_MouseDown(object sender, MouseEventArgs e)
        {
            if (e.Button == System.Windows.Forms.MouseButtons.Left) Tool = 1;
            if (e.Button == System.Windows.Forms.MouseButtons.Right) Tool = 2;
            PixelOnPicture(pictureBox1.Width, pictureBox1.Height, 32, CurrentProject.Symbols / 32, e.Location);
            SetPixel(PixelOnPicture(pictureBox2.Width, pictureBox2.Height, CurrentProject.SizeX, CurrentProject.SizeY, e.Location));
        }

        private void SetPixel(Point Coord)
        {
            if (Tool == 1) CurrentProject.Font[CurrentSymbol, Coord.Y, Coord.X] = 1;
            if (Tool == 2) CurrentProject.Font[CurrentSymbol, Coord.Y, Coord.X] = 0;
            DrawSymbol();
        }

        private void pictureBox2_MouseUp(object sender, MouseEventArgs e) 
        {
            if (Tool > 0)
            {
                Tool = 0;
                DrawDocument();
                Change(false);
            }
        }

        private void pictureBox2_MouseMove(object sender, MouseEventArgs e)
        {
            SetPixel(PixelOnPicture(pictureBox2.Width, pictureBox2.Height, CurrentProject.SizeX, CurrentProject.SizeY, e.Location));
        }

        //Вычисляем нажатый пиксель на картинке пикчербокса
        Point PixelOnPicture(int Width, int Height, int PixWidth, int PixHeight,Point Mouse)
        {
            //Пришлось вот самому писать данную процедурку, так как не нашёл стандартного решения...
            //Процедура вычисляет координаты картинки в пикчербоксе, тыкнутой мышью
            int x;
            int y;
            if ((float)Width / Height > PixWidth / PixHeight)
            {
                //Сверху и снизу
                float k = (float)Height / PixHeight;
                x = (int)((Mouse.X - (Width - k * PixWidth) / 2) / k);
                y = (int)(Mouse.Y / k);
            }
            else
            {
                //Слева и справа
                float k = (float)Width / PixWidth;
                x = (int)(Mouse.X / k);
                y = (int)((Mouse.Y - (Height - k * PixHeight) / 2) / k);
            }
            //Корректеровка вылета за грань рисунка (полезная штука, не надо более об этом думать)
            if (x < 0) x = 0;
            if (x >= PixWidth) x = PixWidth - 1;
            if (y < 0) y = 0;
            if (y >= PixHeight) y = PixHeight - 1;
            return new Point(x, y);
        }

        private void FormMain_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.PageUp & CurrentSymbol > CurrentProject.ADD)
            {
                CurrentSymbol--;
                DrawSymbol();
            }
            if (e.KeyCode == Keys.PageDown & CurrentSymbol < (CurrentProject.Symbols+CurrentProject.ADD-1))
            {
                CurrentSymbol++;
                DrawSymbol();
            }
            if (e.KeyCode == Keys.Up) скроллВверхToolStripMenuItem_Click(null, null);
            if (e.KeyCode == Keys.Left) скроллВлевоToolStripMenuItem_Click(null, null);
            if (e.KeyCode == Keys.Down) скроллВнизToolStripMenuItem_Click(null, null);
            if (e.KeyCode == Keys.Right) скроллВправоToolStripMenuItem_Click(null, null);
            if (e.KeyCode == Keys.Space & !e.Shift) CurrentSymbol = 32;
            if (e.KeyCode == Keys.D1 & e.Shift) CurrentSymbol = 33;
            if (e.KeyCode == Keys.Oem7 & e.Shift) CurrentSymbol = 34;
            if (e.KeyCode == Keys.D3 & e.Shift) CurrentSymbol = 35;
            if (e.KeyCode == Keys.D4 & e.Shift) CurrentSymbol = 36;
            if (e.KeyCode == Keys.D5 & e.Shift) CurrentSymbol = 37;
            if (e.KeyCode == Keys.D7 & e.Shift) CurrentSymbol = 38;
            if (e.KeyCode == Keys.Oemtilde & !e.Shift) CurrentSymbol = 39;
            if (e.KeyCode == Keys.D9 & e.Shift) CurrentSymbol = 40;
            if (e.KeyCode == Keys.D0 & e.Shift) CurrentSymbol = 41;
            if (e.KeyCode == Keys.D8 & e.Shift) CurrentSymbol = 42;
            if (e.KeyCode == Keys.Oemplus & e.Shift) CurrentSymbol = 43;
            if (e.KeyCode == Keys.Oemcomma & !e.Shift) CurrentSymbol = 44;
            if (e.KeyCode == Keys.OemMinus & !e.Shift) CurrentSymbol = 45;
            if (e.KeyCode == Keys.OemPeriod & !e.Shift) CurrentSymbol = 46;
            if (e.KeyCode == Keys.OemQuestion & !e.Shift) CurrentSymbol = 47;
            if (e.KeyCode == Keys.D0 & !e.Shift) CurrentSymbol = 48;
            if (e.KeyCode == Keys.D1 & !e.Shift) CurrentSymbol = 49;
            if (e.KeyCode == Keys.D2 & !e.Shift) CurrentSymbol = 50;
            if (e.KeyCode == Keys.D3 & !e.Shift) CurrentSymbol = 51;
            if (e.KeyCode == Keys.D4 & !e.Shift) CurrentSymbol = 52;
            if (e.KeyCode == Keys.D5 & !e.Shift) CurrentSymbol = 53;
            if (e.KeyCode == Keys.D6 & !e.Shift) CurrentSymbol = 54;
            if (e.KeyCode == Keys.D7 & !e.Shift) CurrentSymbol = 55;
            if (e.KeyCode == Keys.D8 & !e.Shift) CurrentSymbol = 56;
            if (e.KeyCode == Keys.D9 & !e.Shift) CurrentSymbol = 57;

            if (e.KeyCode == Keys.Oem1 & e.Shift) CurrentSymbol = 58;
            if (e.KeyCode == Keys.Oem1 & !e.Shift) CurrentSymbol = 59;
            if (e.KeyCode == Keys.Oemcomma & e.Shift) CurrentSymbol = 60;
            if (e.KeyCode == Keys.Oemplus & !e.Shift) CurrentSymbol = 61;
            if (e.KeyCode == Keys.OemPeriod & e.Shift) CurrentSymbol = 62;
            if (e.KeyCode == Keys.OemQuestion & e.Shift) CurrentSymbol = 63;
            if (e.KeyCode == Keys.D2 & e.Shift) CurrentSymbol = 64;

            if (e.KeyCode == Keys.A & e.Shift) CurrentSymbol = 65;
            if (e.KeyCode == Keys.B & e.Shift) CurrentSymbol = 66;
            if (e.KeyCode == Keys.C & e.Shift) CurrentSymbol = 67;
            if (e.KeyCode == Keys.D & e.Shift) CurrentSymbol = 68;
            if (e.KeyCode == Keys.E & e.Shift) CurrentSymbol = 69;
            if (e.KeyCode == Keys.F & e.Shift) CurrentSymbol = 70;
            if (e.KeyCode == Keys.G & e.Shift) CurrentSymbol = 71;
            if (e.KeyCode == Keys.H & e.Shift) CurrentSymbol = 72;
            if (e.KeyCode == Keys.I & e.Shift) CurrentSymbol = 73;
            if (e.KeyCode == Keys.J & e.Shift) CurrentSymbol = 74;
            if (e.KeyCode == Keys.K & e.Shift) CurrentSymbol = 75;
            if (e.KeyCode == Keys.L & e.Shift) CurrentSymbol = 76;
            if (e.KeyCode == Keys.M & e.Shift) CurrentSymbol = 77;
            if (e.KeyCode == Keys.N & e.Shift) CurrentSymbol = 78;
            if (e.KeyCode == Keys.O & e.Shift) CurrentSymbol = 79;
            if (e.KeyCode == Keys.P & e.Shift) CurrentSymbol = 80;
            if (e.KeyCode == Keys.Q & e.Shift) CurrentSymbol = 81;
            if (e.KeyCode == Keys.R & e.Shift) CurrentSymbol = 82;
            if (e.KeyCode == Keys.S & e.Shift) CurrentSymbol = 83;
            if (e.KeyCode == Keys.T & e.Shift) CurrentSymbol = 84;
            if (e.KeyCode == Keys.U & e.Shift) CurrentSymbol = 85;
            if (e.KeyCode == Keys.V & e.Shift) CurrentSymbol = 86;
            if (e.KeyCode == Keys.W & e.Shift) CurrentSymbol = 87;
            if (e.KeyCode == Keys.X & e.Shift) CurrentSymbol = 88;
            if (e.KeyCode == Keys.Y & e.Shift) CurrentSymbol = 89;
            if (e.KeyCode == Keys.Z & e.Shift) CurrentSymbol = 90;

            if (e.KeyCode == Keys.OemOpenBrackets & !e.Shift) CurrentSymbol = 91;
            if (e.KeyCode == Keys.Oem5 & !e.Shift) CurrentSymbol = 92;
            if (e.KeyCode == Keys.Oem6 & !e.Shift) CurrentSymbol = 93;
            if (e.KeyCode == Keys.D6 & e.Shift) CurrentSymbol = 94;
            if (e.KeyCode == Keys.OemMinus & e.Shift) CurrentSymbol = 95;

            if (e.KeyCode == Keys.A & !e.Shift)  CurrentSymbol = 97;
            if (e.KeyCode == Keys.B & !e.Shift)  CurrentSymbol = 98;
            if (e.KeyCode == Keys.C & !e.Shift)  CurrentSymbol = 99;
            if (e.KeyCode == Keys.D & !e.Shift)  CurrentSymbol = 100;
            if (e.KeyCode == Keys.E & !e.Shift)  CurrentSymbol = 101;
            if (e.KeyCode == Keys.F & !e.Shift)  CurrentSymbol = 102;
            if (e.KeyCode == Keys.G & !e.Shift)  CurrentSymbol = 103;
            if (e.KeyCode == Keys.H & !e.Shift)  CurrentSymbol = 104;
            if (e.KeyCode == Keys.I & !e.Shift)  CurrentSymbol = 105;
            if (e.KeyCode == Keys.J & !e.Shift)  CurrentSymbol = 106;
            if (e.KeyCode == Keys.K & !e.Shift)  CurrentSymbol = 107;
            if (e.KeyCode == Keys.L & !e.Shift)  CurrentSymbol = 108;
            if (e.KeyCode == Keys.M & !e.Shift)  CurrentSymbol = 109;
            if (e.KeyCode == Keys.N & !e.Shift)  CurrentSymbol = 110;
            if (e.KeyCode == Keys.O & !e.Shift)  CurrentSymbol = 111;
            if (e.KeyCode == Keys.P & !e.Shift)  CurrentSymbol = 112;
            if (e.KeyCode == Keys.Q & !e.Shift)  CurrentSymbol = 113;
            if (e.KeyCode == Keys.R & !e.Shift)  CurrentSymbol = 114;
            if (e.KeyCode == Keys.S & !e.Shift)  CurrentSymbol = 115;
            if (e.KeyCode == Keys.T & !e.Shift)  CurrentSymbol = 116;
            if (e.KeyCode == Keys.U & !e.Shift)  CurrentSymbol = 117;
            if (e.KeyCode == Keys.V & !e.Shift)  CurrentSymbol = 118;
            if (e.KeyCode == Keys.W & !e.Shift)  CurrentSymbol = 119;
            if (e.KeyCode == Keys.X & !e.Shift)  CurrentSymbol = 120;
            if (e.KeyCode == Keys.Y & !e.Shift)  CurrentSymbol = 121;
            if (e.KeyCode == Keys.Z & !e.Shift)  CurrentSymbol = 122;

            if (e.KeyCode == Keys.OemOpenBrackets & e.Shift) CurrentSymbol = 123;
            if (e.KeyCode == Keys.Oem5 & e.Shift) CurrentSymbol = 124;
            if (e.KeyCode == Keys.Oem6 & e.Shift) CurrentSymbol = 125;
            if (e.KeyCode == Keys.Oemtilde & e.Shift) CurrentSymbol = 126;
            DrawDocument();
        }

        private void pictureBox1_MouseDown(object sender, MouseEventArgs e)
        {
            Point P = PixelOnPicture(pictureBox1.Width, pictureBox1.Height, 32 * CurrentProject.SizeX, CurrentProject.Symbols / 32 * CurrentProject.SizeY, e.Location);
            CurrentSymbol = P.X / CurrentProject.SizeX + P.Y / CurrentProject.SizeY * 32 + CurrentProject.ADD;
            DrawSymbol();
        }

        private void параметрыToolStripMenuItem_Click(object sender, EventArgs e)
        {
            int temp = CurrentProject.Symbols;
            FormFontParameters form = new FormFontParameters();
            if (form.ShowDialog() == DialogResult.Cancel) return;
            Change(false);
            if (temp != CurrentProject.Symbols) CurrentSymbol = CurrentProject.ADD;
            InitBitmaps();
            DrawDocument();
            DrawSymbol();
        }

        private void pictureBox1_MouseUp(object sender, MouseEventArgs e)
        {
            DrawDocument();
        }

        private void скроллВверхToolStripMenuItem_Click(object sender, EventArgs e)
        {
            ScrollUp(CurrentSymbol);
            DrawDocument();
            Change(false);
        }

        private void скроллВлевоToolStripMenuItem_Click(object sender, EventArgs e)
        {
            ScrollLeft(CurrentSymbol);
            DrawDocument();
            Change(false);
        }

        private void скроллВнизToolStripMenuItem_Click(object sender, EventArgs e)
        {
            ScrollDown(CurrentSymbol);
            DrawDocument();
            Change(false);
        }

        private void скроллВправоToolStripMenuItem_Click(object sender, EventArgs e)
        {
            ScrollRight(CurrentSymbol);
            DrawDocument();
            Change(false);
        }        //Скролл вверх
        void ScrollUp(int num)
        {
            for (int i = 0; i < CurrentProject.SizeX; i++)
            {
                byte temp = CurrentProject.Font[num, 0, i];
                for (int j = 0; j < CurrentProject.SizeY - 1; j++)
                    CurrentProject.Font[num, j, i] = CurrentProject.Font[num, j + 1, i];
                CurrentProject.Font[num, CurrentProject.SizeY - 1, i] = temp;
            }
        }
        //Скролл вниз
        void ScrollDown(int num)
        {
            for (int i = 0; i < CurrentProject.SizeX; i++)
            {
                byte temp = CurrentProject.Font[num, CurrentProject.SizeY - 1, i];
                for (int j = CurrentProject.SizeY - 2; j >= 0; j--)
                    CurrentProject.Font[num, j + 1, i] = CurrentProject.Font[num, j, i];
                CurrentProject.Font[num, 0, i] = temp;
            }
        }
        //Скролл влево
        void ScrollLeft(int num)
        {
            for (int i = 0; i < CurrentProject.SizeY; i++)
            {
                byte temp = CurrentProject.Font[num, i, 0];
                for (int j = 0; j < CurrentProject.SizeX - 1; j++)
                    CurrentProject.Font[num, i, j] = CurrentProject.Font[num, i, j + 1];
                CurrentProject.Font[num, i, CurrentProject.SizeX - 1] = temp;
            }
        }
        //Скролл вправо
        void ScrollRight(int num)
        {
            for (int i = 0; i < CurrentProject.SizeY; i++)
            {
                byte temp = CurrentProject.Font[num, i, CurrentProject.SizeX - 1];
                for (int j = CurrentProject.SizeX - 2; j >= 0; j--)
                    CurrentProject.Font[num, i, j + 1] = CurrentProject.Font[num, i, j];
                CurrentProject.Font[num, i, 0] = temp;
            }
        }

        private void поворотПоЧасовойСтрелкеToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Rotate(CurrentSymbol, true);
            DrawDocument();
            Change(false);
        }

        private void поворотПротивЧасовойСтрелкеToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Rotate(CurrentSymbol, false);
            DrawDocument();
            Change(false);
        }
        //Поворот
        void Rotate(int nm, bool Clock)
        {
            int min = CurrentProject.SizeX;
            if (CurrentProject.SizeY < min) min = CurrentProject.SizeY;
            byte[,] temp = new byte[min, min];
            for (int i = 0; i < min; i++)
                for (int j = 0; j < min; j++)
                    temp[i, j] = CurrentProject.Font[nm, i, j];
            for (int i = 0; i < min; i++)
                for (int j = 0; j < min; j++)
                    if (Clock)
                        CurrentProject.Font[nm, i, j] = temp[min - j - 1, i];
                    else
                        CurrentProject.Font[nm, i, j] = temp[j, min - i - 1];
                    
        }
        //Удаление
        private void очиститьToolStripMenuItem_Click(object sender, EventArgs e)
        {
            for (int l = 0; l < CurrentProject.SizeY; l++)
                for (int b = 0; b < CurrentProject.SizeX; b++)
                    CurrentProject.Font[CurrentSymbol, l, b] = 0;
            DrawDocument();
            Change(false);
        }
        //Инвертирование
        private void негативToolStripMenuItem_Click(object sender, EventArgs e)
        {
            for (int l = 0; l < CurrentProject.SizeY; l++)
                for (int b = 0; b < CurrentProject.SizeX; b++)
                    CurrentProject.Font[CurrentSymbol, l, b] = (byte)(1 - CurrentProject.Font[CurrentSymbol, l, b]);
            DrawDocument();
            Change(false);
        }
        //Вертикальное зеркало
        private void вертикальноеЗеркалоToolStripMenuItem_Click(object sender, EventArgs e)
        {
            byte temp;
            for (int i = 0; i < CurrentProject.SizeX / 2; i++) 
                for (int j = 0; j < CurrentProject.SizeY; j++)
                {
                    temp = CurrentProject.Font[CurrentSymbol, j, i];
                    CurrentProject.Font[CurrentSymbol, j, i] = CurrentProject.Font[CurrentSymbol, j, CurrentProject.SizeX - i - 1];
                    CurrentProject.Font[CurrentSymbol, j, CurrentProject.SizeX - i - 1] = temp;
                }
            DrawDocument();
            Change(false);
        }
        //Горизонтальное зеркало
        private void горизонтальноеЗеркалоToolStripMenuItem_Click(object sender, EventArgs e)
        {
            byte temp;
            for (int i = 0; i < CurrentProject.SizeY / 2; i++)
                for (int j = 0; j < CurrentProject.SizeX; j++)
                {
                    temp = CurrentProject.Font[CurrentSymbol, i, j];
                    CurrentProject.Font[CurrentSymbol, i, j] = CurrentProject.Font[CurrentSymbol, CurrentProject.SizeY - i - 1, j];
                    CurrentProject.Font[CurrentSymbol, CurrentProject.SizeY - i - 1, j] = temp;
                }
            DrawDocument();
            Change(false);
        }
        //Движение сплиттера
        private void splitter1_SplitterMoved(object sender, SplitterEventArgs e) { WindowsPosition.Splitter = splitter1.SplitPosition; }

        private void жирностьToolStripMenuItem_Click(object sender, EventArgs e)
        {
            FormShirina form = new FormShirina();
            if (form.ShowDialog() == DialogResult.Cancel) return;
            if (ShirinaX == -2) { Uzhe(true); Uzhe(true); }
            if (ShirinaX == -1) Uzhe(true);
            if (ShirinaX == 1) Shire(true);
            if (ShirinaX == 2) { Shire(true); Shire(true); }
            if (ShirinaY == -2) { Uzhe(false); Uzhe(false); }
            if (ShirinaY == -1) Uzhe(false);
            if (ShirinaY == 1) Shire(false);
            if (ShirinaY == 2) { Shire(false); Shire(false); }
            DrawDocument();
            Change(false);
        }
        //Делаем символы ширe
        void Shire(bool gor)
        {
            if (gor)
            {
                for (int s = 0; s < CurrentProject.Symbols; s++)
                    for (int i = CurrentProject.SizeX - 1; i > 0; i--)
                        for (int j = 0; j < CurrentProject.SizeY; j++)
                            if (CurrentProject.Font[s + CurrentProject.ADD, j, i - 1] == 1)
                                CurrentProject.Font[s + CurrentProject.ADD, j, i] = 1;
            }
            else
            {
                for (int s = 0; s < CurrentProject.Symbols; s++)
                    for (int i = CurrentProject.SizeY - 1; i > 0; i--)
                        for (int j = 0; j < CurrentProject.SizeX; j++)
                            if (CurrentProject.Font[s + CurrentProject.ADD, i - 1, j] == 1)
                                CurrentProject.Font[s + CurrentProject.ADD, i, j] = 1;
            }
        }
        //Делаем символы уже
        void Uzhe(bool gor)
        {
            if (gor)
            {
                for (int s = 0; s < CurrentProject.Symbols; s++)
                    for (int j = 0; j < CurrentProject.SizeY; j++)
                    {
                        for (int i = 0; i < CurrentProject.SizeX - 1; i++)
                            if (CurrentProject.Font[s + CurrentProject.ADD, j, i + 1] == 0)
                                CurrentProject.Font[s + CurrentProject.ADD, j, i] = 0;
                        CurrentProject.Font[s + CurrentProject.ADD, j, CurrentProject.SizeX - 1] = 0;
                    }
            }
            else
            {
                for (int s = 0; s < CurrentProject.Symbols; s++)
                {
                    for (int j = 0; j < CurrentProject.SizeX; j++)
                    {
                        for (int i = 0; i < CurrentProject.SizeY - 1; i++)
                            if (CurrentProject.Font[s + CurrentProject.ADD, i + 1, j] == 0)
                                CurrentProject.Font[s + CurrentProject.ADD, i, j] = 0;
                        CurrentProject.Font[s + CurrentProject.ADD, CurrentProject.SizeY - 1, j] = 0;
                    }
                }
            }
        }
        //Наклон символов
        private void наклонToolStripMenuItem_Click(object sender, EventArgs e)
        {
            FormNaklon form = new FormNaklon();
            if (form.ShowDialog() == DialogResult.Cancel) return;
            //Наклонама!!!
            if (Naklon > 0)
                for (int s = 0; s < CurrentProject.Symbols; s++)
                    for (int l = 0; l < CurrentProject.SizeY; l++)
                        for (int raz = 0; raz < Math.Round((float)(CurrentProject.SizeY - l) / CurrentProject.SizeY * Naklon); raz++)
                        {
                            for (int i = CurrentProject.SizeX - 1; i > 0; i--)
                                CurrentProject.Font[s + CurrentProject.ADD, l, i] = CurrentProject.Font[s + CurrentProject.ADD, l, i - 1];
                            CurrentProject.Font[s + CurrentProject.ADD, l, 0] = 0;
                        }
            else
                for (int s = 0; s < CurrentProject.Symbols; s++)
                    for (int l = 0; l < CurrentProject.SizeY; l++)
                        for (int raz = 0; raz < Math.Abs(Math.Round((float)(CurrentProject.SizeY - l) / CurrentProject.SizeY * Naklon)); raz++)
                        {
                            for (int i = 0; i < CurrentProject.SizeX - 1; i++)
                                CurrentProject.Font[s + CurrentProject.ADD, l, i] = CurrentProject.Font[s + CurrentProject.ADD, l, i + 1];
                            CurrentProject.Font[s + CurrentProject.ADD, l, CurrentProject.SizeX - 1] = 0;
                        }
            DrawDocument();
            Change(false);
        }

        private void вBMPизображениеToolStripMenuItem_Click(object sender, EventArgs e)
        {
            saveFileDialog1.Title = "Экспорт шрифта в BMP-файл";
            saveFileDialog1.FileName = "";
            saveFileDialog1.Filter = "Точечный рисунок (*.bmp)|*.bmp|Все файлы (*.*)|*.*";
            if (saveFileDialog1.ShowDialog() != DialogResult.OK) return;
            //Рисуем изображение
            Bitmap buffer = new Bitmap((int)(CurrentProject.SizeX * 32), (int)(CurrentProject.SizeY * CurrentProject.Symbols / 32));
            buffer.SetPixel(0, 0, Color.Black);
            for (int s = 0; s < CurrentProject.Symbols; s++)
                for (int l = 0; l < CurrentProject.SizeY; l++)
                    for (int b = 0; b < CurrentProject.SizeX; b++)
                    {
                        Color pix = Color.White;
                        if (CurrentProject.Symbols == 256) CurrentProject.ADD = 0;
                        if (CurrentProject.Font[s + CurrentProject.ADD, l, b] == 1) pix = Color.Black;
                        int x = (s % 32) * CurrentProject.SizeX + b;
                        int y = (s / 32) * CurrentProject.SizeY + l;
                        buffer.SetPixel(x, y, pix);
                    }
            buffer.Save(saveFileDialog1.FileName);
        }

        private void калькуляторPOKEToolStripMenuItem_Click(object sender, EventArgs e)
        {
            FormPoke form = new FormPoke();
            form.ShowDialog();
        }

        private void вTAPфайлToolStripMenuItem_Click(object sender, EventArgs e)
        {
            FormSaveTAP form = new FormSaveTAP();
            form.ShowDialog();
        }

        private void изBMPToolStripMenuItem_Click(object sender, EventArgs e)
        {
            FormLoadBMP form = new FormLoadBMP();
            if (form.ShowDialog() == DialogResult.OK)
            {
                Change(false);
                InitBitmaps();
                DrawDocument();
            }
        }

        private void изTAPToolStripMenuItem_Click(object sender, EventArgs e)
        {
            FormLoadTAP form = new FormLoadTAP();
            if (form.ShowDialog() == DialogResult.OK)
            {
                Change(false);
                InitBitmaps();
                DrawDocument();
            }
        }

        private void генерироватьКодAssemblerаToolStripMenuItem_Click(object sender, EventArgs e)
        {
            FormASM form = new FormASM();
            form.ShowDialog();
        }

        private void toolStripMenuItem8_Click(object sender, EventArgs e)
        {
            for (int i = 0; i < CurrentProject.Symbols; i++)
                ScrollUp(i + CurrentProject.ADD);
            DrawDocument();
            Change(false);
        }

        private void toolStripMenuItem9_Click(object sender, EventArgs e)
        {
            for (int i = 0; i < CurrentProject.Symbols; i++)
                ScrollDown(i + CurrentProject.ADD);
            DrawDocument();
            Change(false);
        }

        private void toolStripMenuItem10_Click(object sender, EventArgs e)
        {
            for (int i = 0; i < CurrentProject.Symbols; i++)
                ScrollLeft(i + CurrentProject.ADD);
            DrawDocument();
            Change(false);
        }

        private void toolStripMenuItem11_Click(object sender, EventArgs e)
        {
            for (int i = 0; i < CurrentProject.Symbols; i++)
                ScrollRight(i + CurrentProject.ADD);
            DrawDocument();
            Change(false);
        }

        private void toolStripButton1_Click(object sender, EventArgs e) { параметрыToolStripMenuItem_Click(null, null); }
        private void toolStripButton2_Click(object sender, EventArgs e) { скроллВверхToolStripMenuItem_Click(null, null); }
        private void toolStripButton3_Click(object sender, EventArgs e) { скроллВнизToolStripMenuItem_Click(null, null); }
        private void toolStripButton4_Click(object sender, EventArgs e) { скроллВлевоToolStripMenuItem_Click(null, null); }
        private void toolStripButton5_Click(object sender, EventArgs e) { скроллВправоToolStripMenuItem_Click(null, null); }
        private void toolStripButton6_Click(object sender, EventArgs e) { поворотПоЧасовойСтрелкеToolStripMenuItem_Click(null, null); }
        private void toolStripButton7_Click(object sender, EventArgs e) { поворотПротивЧасовойСтрелкеToolStripMenuItem_Click(null, null); }
        private void toolStripButton8_Click(object sender, EventArgs e) { вертикальноеЗеркалоToolStripMenuItem_Click(null, null); }
        private void toolStripButton9_Click(object sender, EventArgs e) { горизонтальноеЗеркалоToolStripMenuItem_Click(null, null); }
        private void toolStripButton11_Click(object sender, EventArgs e) { негативToolStripMenuItem_Click(null, null); }
        private void toolStripButton10_Click(object sender, EventArgs e) { очиститьToolStripMenuItem_Click(null, null); }
        private void toolStripButton12_Click(object sender, EventArgs e) { жирностьToolStripMenuItem_Click(null, null); }
        private void toolStripButton13_Click(object sender, EventArgs e) { наклонToolStripMenuItem_Click(null, null); }

        //Открытие файла, указанного в аргументах
        private void FormMain_Load(object sender, EventArgs e)
        {

            string[] args = Environment.GetCommandLineArgs();

            //string[] args = { "123", "c:\\Users\\Sergey\\Desktop\\ZX Font\\Spectrum (Hires).SpecCHR" };

            if (args.Count() == 1) return;
            string file = args[1];
            string ext = System.IO.Path.GetExtension(file).ToLower();
            if (ext == ".specchr")
            {
                if (!CurrentProject.Open(args[1])) return;
                Project.EditName = args[1];
                CurrentSymbol = CurrentProject.ADD;
                InitBitmaps();
                DrawDocument();
                ResetHistory();
                Change(true);
                return;
            }
            MessageBox.Show("Файл не поддерживается", Editor.ProgramName);
        }

        //Drag-n-Drop
        private void FormMain_DragEnter(object sender, DragEventArgs e)
        {
            if (e.Data.GetDataPresent(DataFormats.FileDrop, false))
            {
                e.Effect = DragDropEffects.All;
            }
        }

        private void FormMain_DragDrop(object sender, DragEventArgs e)
        {
            string[] files = (string[])e.Data.GetData(DataFormats.FileDrop);
            string file = files[0];
            string ext = System.IO.Path.GetExtension(file).ToLower();
            if (ext == ".specchr" & SaveQuestion())
            {
                if (!CurrentProject.Open(file)) return;
                Project.EditName = file;
                CurrentSymbol = CurrentProject.ADD;
                InitBitmaps();
                DrawDocument();
                ResetHistory();
                Change(true);
                return;
            }
            MessageBox.Show("Файл не поддерживается", Editor.ProgramName);
        }
    }
}
