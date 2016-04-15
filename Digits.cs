namespace ZXFont
{
    class Digits
    {
        public static string Numeration(int num)
        {
            string Bait = " байт";
            string Baita = " байта";
            string Baitov = " байтов";
            string Num = num.ToString();
            if (Num.Length > 1 && Num[Num.Length - 2] == '1') return num + Baitov;
            switch (Num[Num.Length - 1])
            {
                case '1':
                    return num + Bait;
                case '2':
                case '3':
                case '4':
                    return num + Baita;
                default:
                    return num + Baitov;
            }
        }

        /// <summary>
        /// Перевод человеческого числа в 16-и ричное представление
        /// </summary>
        /// <param name="num">Код</param>
        /// <returns></returns>
        public static string ToHex(byte num)
        {
            return HexLit(num / 16) + HexLit(num % 16);
        }

        /// <summary>
        /// Перевод числа в строку
        /// </summary>
        /// <param name="num">Код</param>
        /// <param name="inHex">В Hex?</param>
        /// <returns></returns>
        public static string ToString(byte num, bool inHex)
        {
            if (inHex) return ToHex(num);
            return num.ToString();
        }

        static string HexLit(int b)
        {
            switch (b)
            {
                case 0: return "0";
                case 1: return "1";
                case 2: return "2";
                case 3: return "3";
                case 4: return "4";
                case 5: return "5";
                case 6: return "6";
                case 7: return "7";
                case 8: return "8";
                case 9: return "9";
                case 10: return "A";
                case 11: return "B";
                case 12: return "C";
                case 13: return "D";
                case 14: return "E";
                case 15: return "F";
            }
            return "*";
        }
    }
}
