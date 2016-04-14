using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

namespace ZXFont
{
    public class Propertiess
    {
        static public int X = 100;
        static public int Y = 100;
        static public int Width = 800;
        static public int Heidht = 800;
        static public bool Max = false;
        static public int Splitter = 500;

        public static void init()
        {
            //try
            //{
            //    //Пробуем загрузить настройки, если они были сохранены
            //    BinaryReader file = new BinaryReader(new FileStream(Program.ParametersFile, FileMode.Open));
            //    X = file.ReadInt32();
            //    Y = file.ReadInt32();
            //    Width = file.ReadInt32();
            //    Heidht = file.ReadInt32();
            //    Max = file.ReadBoolean();
            //    Splitter = file.ReadInt32();
            //    file.Close();
            //}
            //catch { }
        }
        //Сохранение параметров программы
        public static void saveconfig()
        {
            //try
            //{
            //    Directory.CreateDirectory(ParametersFolder);
            //    BinaryWriter file = new BinaryWriter(new FileStream(ParametersFile, FileMode.Create));
            //    file.Write(X);
            //    file.Write(Y);
            //    file.Write(Width);
            //    file.Write(Heidht);
            //    file.Write(Max);
            //    file.Write(Splitter);
            //    file.Close();
            //}
            //catch { }
        }
    }
}
