﻿using System;
using System.IO;
using System.Windows.Forms;

class Editor
{
    //Константы программы
    public const string ProgramName = "ZX Font";
    public const string ProgramVersion = "3.1 - 9 июля 2015 года";
    public const string ProgramAutor = "Сергей Гордеев";
    public const string FileUnnamed = "Безымянный";
    public const string FileType = "Spectrum-шрифт (*.SpecCHR)|*.SpecCHR|Все файлы (*.*)|*.*";
    static string ParametersFolder = Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData) + "\\SG\\ZX Font"; //Папка с конфигурацией программы
    static string ParametersFile = Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData) + "\\SG\\ZX Font\\config.cfg"; //Файл конфигурации программы
    //Инициализация параметров программы
    public static void init()
    {
        try
        {
            //Пробуем загрузить настройки, если они были сохранены
            BinaryReader file = new BinaryReader(new FileStream(ParametersFile, FileMode.Open));
            WindowsPosition.X = file.ReadInt32();
            WindowsPosition.Y = file.ReadInt32();
            WindowsPosition.Width = file.ReadInt32();
            WindowsPosition.Heidht = file.ReadInt32();
            WindowsPosition.Max = file.ReadBoolean();
            WindowsPosition.Splitter = file.ReadInt32();
            file.Close();
        }
        catch { }
    }
    //Сохранение параметров программы
    public static void saveconfig()
    {
        try
        {
            Directory.CreateDirectory(ParametersFolder);
            BinaryWriter file = new BinaryWriter(new FileStream(ParametersFile, FileMode.Create));
            file.Write(WindowsPosition.X);
            file.Write(WindowsPosition.Y);
            file.Write(WindowsPosition.Width);
            file.Write(WindowsPosition.Heidht);
            file.Write(WindowsPosition.Max);
            file.Write(WindowsPosition.Splitter);
            file.Close();
        }
        catch { }
    }
    //Злобное сообщение об ошибке
    public static void Error(string message)
    {
        MessageBox.Show(message, "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
    }
    //Не злобное сообщение
    public static void Message(string message)
    {
        MessageBox.Show(message, ProgramName);
    }
}

public class WindowsPosition
{
    static public int X = 100;
    static public int Y = 100;
    static public int Width = 800;
    static public int Heidht = 800;
    static public bool Max = false;
    static public int Splitter = 500;
}
//Класс проекта
public class Project
{
    public const byte Maximumsize = 16;
    public static string FileName;
    public static string EditName;
    public static bool Changed;
    public static byte XL;
    public static byte XR;
    public static byte YT;
    public static byte Yt;
    public static byte YB;
    //Данные проекта
    public int Symbols; //96, 224 или 256
    public byte SizeX; //4 - 16
    public byte SizeY; //8 или 16
    public byte[, ,] Font = new byte[256, Maximumsize, Maximumsize];
    public byte ADD;
    //Копирование объекта
    public void Copy(Project Copy)
    {
        //Text = Copy.Text;
        Symbols = Copy.Symbols;
        SizeX = Copy.SizeX;
        SizeY = Copy.SizeY;
        for (int i = 0;i<256;i++)
            for(int j=0;j<Maximumsize;j++)
                for(int k = 0;k<Maximumsize;k++)
                    Font[i,j,k] = Copy.Font[i,j,k];
        ADD = Copy.ADD;
    }
    //Новый проект
    public void NewProject()
    {
        FileName = "";
        EditName = Editor.FileUnnamed;
        Changed = false;
        //Создание нового документа
        Symbols = 96;
        SizeX = 8;
        SizeY = 8;
        Font = new byte[256, Maximumsize, Maximumsize];
        ADD = 32;
        XL = 0;
        XR = 0;
        YT = 0;
        Yt = 0;
        YB = 0;
    }
    //Сохранение проекта
    public bool Save()
    {
        try
        {
            BinaryWriter file = new BinaryWriter(new FileStream(EditName, FileMode.Create));
            if (SizeX != 8 | SizeY != 8)
            {
                file.Write('Z');
                file.Write('F');
                file.Write('1');
                file.Write((short)Symbols);
                file.Write(SizeX);
                file.Write(SizeY);
                file.Write(XL);
                file.Write(XR);
                file.Write(YT);
                file.Write(Yt);
                file.Write(YB);
                file.Write((byte)0); //Зарезервированное место
                file.Write((byte)0);
                file.Write((byte)0);
                file.Write((byte)0);
            }
            for (int s = 0; s < Symbols; s++)
                for (int l = 0; l < SizeY; l++)
                    for (int b = 0; b < SizeX; b += 8)
                    {
                        byte bt = 0;
                        if (Font[s + ADD, l, b] == 1) bt += 128;
                        if (Font[s + ADD, l, b + 1] == 1) bt += 64;
                        if (Font[s + ADD, l, b + 2] == 1) bt += 32;
                        if (Font[s + ADD, l, b + 3] == 1) bt += 16;
                        if (Font[s + ADD, l, b + 4] == 1) bt += 8;
                        if (Font[s + ADD, l, b + 5] == 1) bt += 4;
                        if (Font[s + ADD, l, b + 6] == 1) bt += 2;
                        if (Font[s + ADD, l, b + 7] == 1) bt += 1;
                        file.Write(bt);
                    }
            file.Close();
            return true;
        }
        catch
        {
            Editor.Error("Произошла ошибка во время сохранения файла. Файл не сохранён.");
            return false;
        }
    }
    //Загрузка проекта
    public bool Open(string OpenFile)
    {
        try
        {
            bool open = false;
            FileInfo info = new FileInfo(OpenFile);
            long size = info.Length;
            BinaryReader file = new BinaryReader(new FileStream(OpenFile, FileMode.Open));
            //Стандартный шрифт
            if (size == 768 | size == 1792 | size == 2048)
            {
                Symbols = (int)size / 8;
                SizeX = 8;
                SizeY = 8;
                Font = new byte[256, Maximumsize, Maximumsize];
                ADD = 32;
                if (Symbols == 256) ADD = 0;
                for (int s = 0; s < Symbols; s++)
                    for (int l = 0; l < 8; l++)
                        LoadByte(file.ReadByte(), s + ADD, l, 0);
                open = true;
                XL = 0; //Эта штука будет сохраняться только в нестандартном шрифте
                XR = 0; //А по умолчанию они будут убраны
                YT = 0;
                Yt = 0;
                YB = 0;
            }
            //Шрифт, надо полагать, не стандартный
            if (!open)
            {
                string Format = "" + file.ReadChar() + file.ReadChar() + file.ReadChar();
                if (Format == "ZF1")
                {
                    open = true;
                    Symbols = file.ReadInt16();
                    SizeX = file.ReadByte();
                    SizeY = file.ReadByte();
                    XL = file.ReadByte();
                    XR = file.ReadByte();
                    YT = file.ReadByte();
                    Yt = file.ReadByte();
                    YB = file.ReadByte();
                    file.ReadBytes(4);
                    Font = new byte[256, Maximumsize, Maximumsize];
                    ADD = 32;
                    if (Symbols == 256) ADD = 0;
                    for (int s = 0; s < Symbols; s++)
                        for (int l = 0; l < SizeY; l++)
                            for (int b = 0; b < SizeX; b += 8)
                                LoadByte(file.ReadByte(), s + ADD, l, b);
                }
            }
            file.Close();
            if (open) return true;
            {
                Editor.Error("Файл не является файлом шрифта, или его версия пока не поддерживается.");
                return false;
            }
        }
        catch
        {
            Editor.Error("Произошла ошибка при открытии файла.");
            return false;
        }
    }
    void LoadByte(byte b, int s, int l,int bb)
    {
        if ((b & 128) != 0) Font[s, l, bb + 0] = 1;
        if ((b & 64) != 0) Font[s, l, bb + 1] = 1;
        if ((b & 32) != 0) Font[s, l, bb + 2] = 1;
        if ((b & 16) != 0) Font[s, l, bb + 3] = 1;
        if ((b & 8) != 0) Font[s, l, bb + 4] = 1;
        if ((b & 4) != 0) Font[s, l, bb + 5] = 1;
        if ((b & 2) != 0) Font[s, l, bb + 6] = 1;
        if ((b & 1) != 0) Font[s, l, bb + 7] = 1;
    }
}