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
            labelName.Text = Application.ProductName;
            labelVersion.Text = "Версия 4.0 (04.06.2021)";

            Font fontR = new Font(history.Font.FontFamily, history.Font.Size, FontStyle.Regular);
            Font fontB = new Font(history.Font.FontFamily, history.Font.Size, FontStyle.Bold);

            history.SelectionFont = fontB;
            history.AppendText("Версия 4.0 (04.06.2021)\n\n");
            history.SelectionFont = fontR;
            history.AppendText("• Использование системного буфера обмена теперь позволяет обмениваться изображением как между "+
                "разными экземплярами ZX Font, так и между другими графическими программами\n" +
                "• Прочие улучшения и исправления ошибок\n\n"); ;


        }

        private void linkLabel1_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            System.Diagnostics.Process.Start("http://www.sg-software.ru");
        }
    }
}
