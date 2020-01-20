using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Linq;
using System.Reflection;
using System.Windows.Forms;

namespace ZXFont
{
    partial class FormAbout : Form
    {
        public FormAbout()
        {
            InitializeComponent();
            this.Text = "О " + Program.Name;
            label1.Text = Program.Name;
            label2.Text = "Версия " + Program.Version;
            label3.Text = "Автор программы: " + Program.Autor;
        }

        private void linkLabel1_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            System.Diagnostics.Process.Start("http://www.sg-software.ru");
        }
    }
}
