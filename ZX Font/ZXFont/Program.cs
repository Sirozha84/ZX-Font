using System;
using System.Windows.Forms;
using System.Drawing;

namespace ZXFont
{
    static class Program
    {
        //Константы программы
        public const string Name = "ZX Font";
        public const string Version = "3.3 - 20 января 2020 года";
        public const string Autor = "Сергей Гордеев";
        public const string FileUnnamed = "Безымянный";
        public const string FileType = "Шрифт ZX Spectrum|*.SpecCHR|Все файлы|*.*";
        public static Color[] ZXColor = { Color.FromArgb(0, 0, 0),
                                          Color.FromArgb(0, 0, 192),
                                          Color.FromArgb(192, 0, 0),
                                          Color.FromArgb(192, 0, 192),
                                          Color.FromArgb(0, 180, 0),
                                          Color.FromArgb(0, 192, 192),
                                          Color.FromArgb(192, 192, 0),
                                          Color.FromArgb(192, 192, 192),
                                          Color.FromArgb(64, 64, 64),
                                          Color.FromArgb(64, 64, 255),
                                          Color.FromArgb(255, 64, 64),
                                          Color.FromArgb(255, 64, 255),
                                          Color.FromArgb(64, 255, 64),
                                          Color.FromArgb(64, 255, 255),
                                          Color.FromArgb(255, 255, 0),
                                          Color.FromArgb(255, 255, 255)};
        /// <summary>
        /// Злобное сообщение об ошибке
        /// </summary>
        /// <param name="message"></param>
        public static void Error(string message)
        {
            MessageBox.Show(message, "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
        }

        /// <summary>
        /// Не злобное сообщение
        /// </summary>
        /// <param name="message"></param>
        public static void Message(string message)
        {
            MessageBox.Show(message, Name);
        }

        /// <summary>
        /// Главная точка входа для приложения.
        /// </summary>
        [STAThread]
        static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            Application.Run(new FormMain());
        }
    }
}
