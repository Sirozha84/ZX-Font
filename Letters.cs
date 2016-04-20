using System;
using System.Windows.Forms;

namespace ZXFont
{
    class Letters
    {
        /// <summary>
        /// Возврат кода символа по нажатой клавише
        /// </summary>
        /// <param name="e">Событие клавиатуры</param>
        /// <returns></returns>
        public static int KeyByKeuboard(KeyEventArgs e)
        {
            //Знаки и цифры
            if (e.KeyCode == Keys.Space & !e.Shift) return 32;
            if (e.KeyCode == Keys.D1 & e.Shift) return 33;
            if (e.KeyCode == Keys.Oem7 & e.Shift) return 34;
            if (e.KeyCode == Keys.D3 & e.Shift) return 35;
            if (e.KeyCode == Keys.D4 & e.Shift) return 36;
            if (e.KeyCode == Keys.D5 & e.Shift) return 37;
            if (e.KeyCode == Keys.D7 & e.Shift) return 38;
            if (e.KeyCode == Keys.Oemtilde & !e.Shift) return 39;
            if (e.KeyCode == Keys.D9 & e.Shift) return 40;
            if (e.KeyCode == Keys.D0 & e.Shift) return 41;
            if (e.KeyCode == Keys.D8 & e.Shift) return 42;
            if (e.KeyCode == Keys.Oemplus & e.Shift) return 43;
            if (e.KeyCode == Keys.Oemcomma & !e.Shift) return 44;
            if (e.KeyCode == Keys.OemMinus & !e.Shift) return 45;
            if (e.KeyCode == Keys.OemPeriod & !e.Shift) return 46;
            if (e.KeyCode == Keys.OemQuestion & !e.Shift) return 47;
            if (e.KeyCode == Keys.D0 & !e.Shift) return 48;
            if (e.KeyCode == Keys.D1 & !e.Shift) return 49;
            if (e.KeyCode == Keys.D2 & !e.Shift) return 50;
            if (e.KeyCode == Keys.D3 & !e.Shift) return 51;
            if (e.KeyCode == Keys.D4 & !e.Shift) return 52;
            if (e.KeyCode == Keys.D5 & !e.Shift) return 53;
            if (e.KeyCode == Keys.D6 & !e.Shift) return 54;
            if (e.KeyCode == Keys.D7 & !e.Shift) return 55;
            if (e.KeyCode == Keys.D8 & !e.Shift) return 56;
            if (e.KeyCode == Keys.D9 & !e.Shift) return 57;
            if (e.KeyCode == Keys.Oem1 & e.Shift) return 58;
            if (e.KeyCode == Keys.Oem1 & !e.Shift) return 59;
            if (e.KeyCode == Keys.Oemcomma & e.Shift) return 60;
            if (e.KeyCode == Keys.Oemplus & !e.Shift) return 61;
            if (e.KeyCode == Keys.OemPeriod & e.Shift) return 62;
            if (e.KeyCode == Keys.OemQuestion & e.Shift) return 63;
            if (e.KeyCode == Keys.D2 & e.Shift) return 64;
            if (e.KeyCode == Keys.OemOpenBrackets & !e.Shift) return 91;
            if (e.KeyCode == Keys.Oem5 & !e.Shift) return 92;
            if (e.KeyCode == Keys.Oem6 & !e.Shift) return 93;
            if (e.KeyCode == Keys.D6 & e.Shift) return 94;
            if (e.KeyCode == Keys.OemMinus & e.Shift) return 95;
            if (e.KeyCode == Keys.OemOpenBrackets & e.Shift) return 123;
            if (e.KeyCode == Keys.Oem5 & e.Shift) return 124;
            if (e.KeyCode == Keys.Oem6 & e.Shift) return 125;
            if (e.KeyCode == Keys.Oemtilde & e.Shift) return 126;
            //Нажат ли Shift или Caps Lock, или и то и другое
            bool Shift = e.Shift | Console.CapsLock;
            if (e.Shift & Console.CapsLock) Shift = false;
            //И тут уже проверяем буквы
            if (e.KeyCode == Keys.A) return Shift ? 65 : 97;
            if (e.KeyCode == Keys.B) return Shift ? 66 : 98;
            if (e.KeyCode == Keys.C) return Shift ? 67 : 99;
            if (e.KeyCode == Keys.D) return Shift ? 68 : 100;
            if (e.KeyCode == Keys.E) return Shift ? 69 : 101;
            if (e.KeyCode == Keys.F) return Shift ? 70 : 102;
            if (e.KeyCode == Keys.G) return Shift ? 71 : 103;
            if (e.KeyCode == Keys.H) return Shift ? 72 : 104;
            if (e.KeyCode == Keys.I) return Shift ? 73 : 105;
            if (e.KeyCode == Keys.J) return Shift ? 74 : 106;
            if (e.KeyCode == Keys.K) return Shift ? 75 : 107;
            if (e.KeyCode == Keys.L) return Shift ? 76 : 108;
            if (e.KeyCode == Keys.M) return Shift ? 77 : 109;
            if (e.KeyCode == Keys.N) return Shift ? 78 : 110;
            if (e.KeyCode == Keys.O) return Shift ? 79 : 111;
            if (e.KeyCode == Keys.P) return Shift ? 80 : 112;
            if (e.KeyCode == Keys.Q) return Shift ? 81 : 113;
            if (e.KeyCode == Keys.R) return Shift ? 82 : 114;
            if (e.KeyCode == Keys.S) return Shift ? 83 : 115;
            if (e.KeyCode == Keys.T) return Shift ? 84 : 116;
            if (e.KeyCode == Keys.U) return Shift ? 85 : 117;
            if (e.KeyCode == Keys.V) return Shift ? 86 : 118;
            if (e.KeyCode == Keys.W) return Shift ? 87 : 119;
            if (e.KeyCode == Keys.X) return Shift ? 88 : 120;
            if (e.KeyCode == Keys.Y) return Shift ? 89 : 121;
            if (e.KeyCode == Keys.Z) return Shift ? 90 : 122;
            return 0;
        }
    }
}
