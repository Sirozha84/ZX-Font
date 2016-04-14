using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;

namespace ZXFont
{
    static class Program
    {
        //Константы программы
        public const string Name = "ZX Font";
        public const string Version = "3.2 - 14 апреля 2016 года";
        public const string Autor = "Сергей Гордеев";
        public const string FileUnnamed = "Безымянный";
        public const string FileType = "Шрифт ZX Spectrum|*.SpecCHR|Все файлы|*.*";
        static string ParametersFolder = Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData) + "\\SG\\ZX Font"; //Папка с конфигурацией программы
        static string ParametersFile = Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData) + "\\SG\\ZX Font\\config.cfg"; //Файл конфигурации программы

        //Злобное сообщение об ошибке
        public static void Error(string message)
        {
            MessageBox.Show(message, "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
        }
        //Не злобное сообщение
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
