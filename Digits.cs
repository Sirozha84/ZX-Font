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

        public static string ToHex(int num)
        {
            return "00";
        }
    }
}
