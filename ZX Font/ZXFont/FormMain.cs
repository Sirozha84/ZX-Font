using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;

namespace ZXFont
{
    public partial class FormMain : Form
    {
        const int PixelSize = 32;
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
        Color Ink;
        Color Paper;
        string ImportFile;

        //Инициализация параметров
        public FormMain()
        {
            InitializeComponent();
            //Загрузка настроек
            Left = Properties.Settings.Default.X;
            Top = Properties.Settings.Default.Y;
            Width = Properties.Settings.Default.Width;
            Height = Properties.Settings.Default.Height;
            splitter1.SplitPosition = Properties.Settings.Default.Splitter;
            CodeInHex.Checked = Properties.Settings.Default.Hex;
            Grid.Checked = Properties.Settings.Default.Grid;
            Borders.Checked = Properties.Settings.Default.Borders;
            menunew_Click(null, null);
        }
        //Создание нового файла
        private void menunew_Click(object sender, EventArgs e)
        {
            if (!SaveQuestion()) return;
            CurrentProject.NewProject(false);
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
            openFileDialog1.Filter = Program.FileType;
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
            if (Project.EditName == Program.FileUnnamed && !FileSaveAs()) return false;
            if (!CurrentProject.Save()) return false;
            Change(true);
            return true;
        }
        //Сохранение файла как
        bool FileSaveAs()
        {
            saveFileDialog1.FileName = "";
            saveFileDialog1.Filter = Program.FileType;
            if (saveFileDialog1.ShowDialog() == DialogResult.OK) { Project.EditName = saveFileDialog1.FileName; Change(true); return true; }
            return false;
        }
        //Отмена
        private void menuundo_Click(object sender, EventArgs e)
        {
            if (HistoryNumber < 2) return;
            HistoryNumber--;
            CurrentProject.Copy(History[HistoryNumber - 1]);
            InitBitmaps();
            DrawDocument();
        }
        //Возврат
        private void menuredo_Click(object sender, EventArgs e)
        {
            if (HistoryNumber == History.Count) return;
            HistoryNumber++;
            CurrentProject.Copy(History[HistoryNumber - 1]);
            InitBitmaps();
            DrawDocument();
        }
        //Вырезать
        private void menucut_Click(object sender, EventArgs e)
        {
            Bitmap buf = new Bitmap(CurrentProject.SizeX, CurrentProject.SizeY);
            for (int i = 0; i < CurrentProject.SizeX; i++)
                for (int j = 0; j < CurrentProject.SizeY; j++)
                {
                    buf.SetPixel(i, j, CurrentProject.Font[CurrentSymbol, j, i] == 0 ? Color.White : Color.Black);
                    CurrentProject.Font[CurrentSymbol, j, i] = 0;
                }
            Clipboard.SetImage(buf);
            DrawSymbol();
            DrawDocument();
            Change(false);
        }
        //Копировать
        private void menucopy_Click(object sender, EventArgs e)
        {
            Bitmap buf = new Bitmap(CurrentProject.SizeX, CurrentProject.SizeY);
            for (int i = 0; i < CurrentProject.SizeX; i++)
                for (int j = 0; j < CurrentProject.SizeY; j++)
                    buf.SetPixel(i, j, CurrentProject.Font[CurrentSymbol, j, i] == 0 ? Color.White : Color.Black);
            Clipboard.SetImage(buf);
        }
        //Вставить
        private void menupaste_Click(object sender, EventArgs e)
        {
            Image im = Clipboard.GetImage();
            if (im == null) return;
            Bitmap buf = new Bitmap(Clipboard.GetImage());
            for (int i = 0; i < CurrentProject.SizeX; i++)
                for (int j = 0; j < CurrentProject.SizeY; j++)
                {
                    if (i < buf.Width & j < buf.Height)
                    {
                        Color p = buf.GetPixel(i, j);
                        CurrentProject.Font[CurrentSymbol, j, i] = (byte)((p.R + p.G + p.B < 384) ? 1 : 0);
                    }
                }
            DrawSymbol();
            DrawDocument();
        }
        //Закрытие программы
        private void Form1_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (!SaveQuestion()) e.Cancel = true;
            //Запись настроек
            Properties.Settings.Default.X = Left;
            Properties.Settings.Default.Y = Top;
            Properties.Settings.Default.Width = Width;
            Properties.Settings.Default.Height = Height;
            Properties.Settings.Default.Splitter = splitter1.SplitPosition;
            Properties.Settings.Default.Hex = CodeInHex.Checked;
            Properties.Settings.Default.Grid = Grid.Checked;
            Properties.Settings.Default.Borders = Borders.Checked;
            Properties.Settings.Default.Save();
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
            Color InkB = Program.ZXColor[Properties.Settings.Default.Ink + 8];
            Color PaperB = Program.ZXColor[Properties.Settings.Default.Paper + 8];

            SetColors();
            ProgramTextChange = true;
            int mn = 1; //Множитель, удваивает размер пикселей в микрошрифтах
            if (CurrentProject.SizeX <= 8) mn = 2;
            BitmapFont.SetPixel(0, 0, Ink);
            for (int s = 0; s < CurrentProject.Symbols; s++)
                for (int l = 0; l < CurrentProject.SizeY; l++)
                    for (int b = 0; b < CurrentProject.SizeX; b++)
                    {
                        Color pix = Paper;
                        if (s + CurrentProject.ADD == CurrentSymbol) pix = PaperB;
                        if (CurrentProject.Symbols == 256) CurrentProject.ADD = 0;
                        if (CurrentProject.Font[s + CurrentProject.ADD, l, b] == 1)
                        {
                            pix = Ink;
                            if (s + CurrentProject.ADD == CurrentSymbol) pix = InkB;
                        }
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
            pictureBoxFont.Image = BitmapFont;
            DrawSymbol();
        }
        //Рисование символа
        void DrawSymbol()
        {
            const int PixelSize = 32;
            Brush INK = new SolidBrush(Ink);
            Brush PAPER = new SolidBrush(Paper);
            Color AvColor = Color.FromArgb((Program.ZXColor[Properties.Settings.Default.Ink].R +
                                      Program.ZXColor[Properties.Settings.Default.Paper].R) / 2,
                                     (Program.ZXColor[Properties.Settings.Default.Ink].G +
                                      Program.ZXColor[Properties.Settings.Default.Paper].G) / 2,
                                     (Program.ZXColor[Properties.Settings.Default.Ink].B +
                                      Program.ZXColor[Properties.Settings.Default.Paper].B) / 2);
            Color AvColorLow = Color.FromArgb((Program.ZXColor[Properties.Settings.Default.Ink].R +
                                         Program.ZXColor[Properties.Settings.Default.Paper].R * 2) / 3,
                                        (Program.ZXColor[Properties.Settings.Default.Ink].G +
                                         Program.ZXColor[Properties.Settings.Default.Paper].G * 2) / 3,
                                        (Program.ZXColor[Properties.Settings.Default.Ink].B +
                                         Program.ZXColor[Properties.Settings.Default.Paper].B * 2) / 3);
            Color AvColorLowwww = Color.FromArgb((Program.ZXColor[Properties.Settings.Default.Ink].R +
                                         Program.ZXColor[Properties.Settings.Default.Paper].R * 3) / 4,
                                        (Program.ZXColor[Properties.Settings.Default.Ink].G +
                                         Program.ZXColor[Properties.Settings.Default.Paper].G * 3) / 4,
                                        (Program.ZXColor[Properties.Settings.Default.Ink].B +
                                         Program.ZXColor[Properties.Settings.Default.Paper].B * 3) / 4); 


            Brush AVColor = new SolidBrush(AvColor);
            Brush AVColorL = new SolidBrush(AvColorLow);
            Brush AVColorLL = new SolidBrush(AvColorLowwww);
            const int px = 16;
            //Рисуем, если надо, поля шаблона
            Canvas.FillRectangle(PAPER, 0, 0, CurrentProject.SizeX * 16, CurrentProject.SizeY * 16);
            if (Borders.Checked)
            {
                int Width = CurrentProject.SizeX;
                int Height = CurrentProject.SizeY;
                Canvas.FillRectangle(AVColorLL, 0, 0, Width * 16, Properties.Settings.Default.BorderTopP * 16);
                Canvas.FillRectangle(AVColorL, 0, 0, Width * 16, Properties.Settings.Default.BorderTop * 16);
                Canvas.FillRectangle(AVColorL, 0, (Height - Properties.Settings.Default.BorderBottom) * 16,
                    Width * 16, Properties.Settings.Default.BorderBottom * 16);
                Canvas.FillRectangle(AVColorL, 0, 0, Properties.Settings.Default.BorderLeft * 16, Width * 16);
                Canvas.FillRectangle(AVColorL, (Width - Properties.Settings.Default.BorderRight) * 16, 0,
                    Properties.Settings.Default.BorderRight * 16, Width * 16);
            }
            //Рисуем символ
            for (int y = 0; y < CurrentProject.SizeY; y++)
                for (int x = 0; x < CurrentProject.SizeX; x++)
                    if (CurrentProject.Font[CurrentSymbol, y, x] != 0)
                        Canvas.FillRectangle(INK, x * px, y * px, px, px);
            //Сетка
            if (Grid.Checked)
            {
                for (int i = px; i < CurrentProject.SizeX * px; i += px)
                    Canvas.DrawLine(new Pen(AvColor), i, 0, i, CurrentProject.SizeY * px);
                for (int i = px; i < CurrentProject.SizeY * px; i += px)
                    Canvas.DrawLine(new Pen(AvColor), 0, i, CurrentProject.SizeX * px + 0, i);
            }
            pictureBoxSumbol.Image = BitmapSymbol;
            labelCode.Text =  Digits.ToString((byte)CurrentSymbol, CodeInHex.Checked);
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
                st = Digits.ToString(Byte1, CodeInHex.Checked);
            else
            {
                st = Digits.ToString(Byte1, CodeInHex.Checked);
                if (!CodeInHex.Checked) st += ", ";
                st += Digits.ToString(Byte2, CodeInHex.Checked);
            }
            return st;
        }

        //Рисование имени файла и программы
        void SetFormText()
        {
            string star = ""; 
            if (Project.Changed) star = "*";
            Text = System.IO.Path.GetFileNameWithoutExtension(Project.EditName) + star + " - " + Application.ProductName;
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

        private void SetPixel(Point Coord)
        {
            if (Tool == 1) CurrentProject.Font[CurrentSymbol, Coord.Y, Coord.X] = 1;
            if (Tool == 2) CurrentProject.Font[CurrentSymbol, Coord.Y, Coord.X] = 0;
            DrawSymbol();
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
            if (e.KeyCode == Keys.PageUp) CurrentSymbol--;
            if (e.KeyCode == Keys.PageDown) CurrentSymbol++;
            if (e.KeyCode == Keys.Up) CurrentSymbol -= 32;
            if (e.KeyCode == Keys.Left) CurrentSymbol--;
            if (e.KeyCode == Keys.Down) CurrentSymbol += 32;
            if (e.KeyCode == Keys.Right) CurrentSymbol++;
            if (e.KeyCode == Keys.Home) CurrentSymbol = CurrentProject.ADD;
            if (e.KeyCode == Keys.End) CurrentSymbol = CurrentProject.Symbols + CurrentProject.ADD - 1;
            if (CurrentSymbol < CurrentProject.ADD)
                CurrentSymbol = CurrentProject.ADD;
            if (CurrentSymbol > CurrentProject.Symbols + CurrentProject.ADD - 1)
                CurrentSymbol = CurrentProject.Symbols + CurrentProject.ADD - 1;

            int key = Letters.KeyByKeuboard(e);
            if (key > 0) CurrentSymbol = key;
            DrawDocument();
        }

        private void pictureBox1_MouseDown(object sender, MouseEventArgs e)
        {
            Point P = PixelOnPicture(pictureBoxFont.Width, pictureBoxFont.Height, 32 * CurrentProject.SizeX, CurrentProject.Symbols / 32 * CurrentProject.SizeY, e.Location);
            CurrentSymbol = P.X / CurrentProject.SizeX + P.Y / CurrentProject.SizeY * 32 + CurrentProject.ADD;
            DrawSymbol();
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
        }        
        
        //Скролл вверх
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
            ImportFromFile(FormLoadTAP.ImportTypes.Tap);
        }

        private void изБинарногоФайлаToolStripMenuItem_Click(object sender, EventArgs e)
        {
            ImportFromFile(FormLoadTAP.ImportTypes.Bin);
        }

        //Импорт из файла
        void ImportFromFile(FormLoadTAP.ImportTypes type)
        {
            FormLoadTAP form = new FormLoadTAP(type);
            if (form.ShowDialog() == DialogResult.OK)
            {
                Change(false);
                InitBitmaps();
                DrawDocument();
            }
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

        private void toolStripButton1_Click(object sender, EventArgs e) { параметрыШрифтаToolStripMenuItem_Click(null, null); }
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
            Program.Message("Файл не поддерживается");
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
            ImportFile = files[0];
            timerForImport.Enabled = true;
        }

        private void timerForImport_Tick(object sender, EventArgs e)
        {
            timerForImport.Enabled = false;
            string ext = System.IO.Path.GetExtension(ImportFile).ToLower();
            if (ext == ".specchr" && SaveQuestion())
            {
                if (!CurrentProject.Open(ImportFile)) return;
                Project.EditName = ImportFile;
                CurrentSymbol = CurrentProject.ADD;
                InitBitmaps();
                DrawDocument();
                ResetHistory();
                Change(true);
                return;
            }
            if (ext == ".tap")
            {
                FormLoadTAP form = new FormLoadTAP(FormLoadTAP.ImportTypes.Tap, ImportFile);
                if (form.ShowDialog() == DialogResult.OK)
                {
                    InitBitmaps();
                    DrawDocument();
                    Change(false);
                }
                return;
            }
            if (ext == ".bmp" | ext == ".png" | ext == ".jpg" | ext == ".jpeg" | ext == ".gif")
            {
                FormLoadBMP form = new FormLoadBMP(ImportFile);
                if (form.ShowDialog() == DialogResult.OK)
                {
                    Change(false);
                    InitBitmaps();
                    DrawDocument();
                }
                return;
            }
            //Если ничего не подошло, импортируем как бинарник
            using (FormLoadTAP form = new FormLoadTAP(FormLoadTAP.ImportTypes.Bin, ImportFile))
                if (form.ShowDialog() == DialogResult.OK)
                {
                    InitBitmaps();
                    DrawDocument();
                    Change(false);
                }
        }

        private void pictureBoxSumbol_MouseDown(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left) Tool = 1;
            if (e.Button == MouseButtons.Right) Tool = 2;
            PixelOnPicture(pictureBoxFont.Width, pictureBoxFont.Height, 32, CurrentProject.Symbols / 32, e.Location);
            SetPixel(PixelOnPicture(pictureBoxSumbol.Width, pictureBoxSumbol.Height, CurrentProject.SizeX, CurrentProject.SizeY, e.Location));
        }

        private void pictureBoxSumbol_MouseMove(object sender, MouseEventArgs e)
        {
            SetPixel(PixelOnPicture(pictureBoxSumbol.Width, pictureBoxSumbol.Height, CurrentProject.SizeX, CurrentProject.SizeY, e.Location));
        }

        private void pictureBoxSumbol_MouseUp(object sender, MouseEventArgs e)
        {
            if (Tool > 0)
            {
                Tool = 0;
                Change(false);
                DrawDocument();
            }
        }

        private void кодыВHEXToolStripMenuItem_Click(object sender, EventArgs e)
        {
            CodeInHex.Checked ^= true;
            DrawSymbol();
        }

        private void текстовыйГенераторToolStripMenuItem_Click(object sender, EventArgs e)
        {
            FormTextGenerator form = new FormTextGenerator();
            form.ShowDialog();
        }

        private void калькуляторPOKEToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            FormPoke form = new FormPoke();
            form.ShowDialog();
        }

        private void параметрыШрифтаToolStripMenuItem_Click(object sender, EventArgs e)
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

        //Создание нового чистого шрифта
        private void новыйЧистыйToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (!SaveQuestion()) return;
            CurrentProject.NewProject(true);
            CurrentSymbol = CurrentProject.ADD;
            InitBitmaps();
            DrawDocument();
            ResetHistory();
            Change(true);
        }

        private void сеткаToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Grid.Checked ^= true;
            DrawSymbol();
        }

        private void цветаToolStripMenuItem_Click(object sender, EventArgs e)
        {
            FormColors form = new FormColors();
            form.ShowDialog();
            DrawDocument();
        }

        void SetColors()
        {
            Ink = Program.ZXColor[Properties.Settings.Default.Ink];
            Paper = Program.ZXColor[Properties.Settings.Default.Paper];
       }

        private void тестШрифтаToolStripMenuItem_Click(object sender, EventArgs e)
        {
            FormTest form = new FormTest();
            form.ShowDialog();
            form.Dispose();
        }

        private void ограничивающиеКонторыToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Borders.Checked ^= true;
            DrawSymbol();
        }

        private void параметрыОграничивающихКонтуровToolStripMenuItem_Click(object sender, EventArgs e)
        {
            FormBorders form = new FormBorders();
            form.ShowDialog();
            DrawSymbol();
        }

    }
}
