using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace PrintApp
{
    public partial class Form1 : Form
    {
        int counter = 0;
        int curPages;
        public Form1()
        {
            InitializeComponent();
        }

        private void label1_Click(object sender, EventArgs e)
        {
            
        }

        // кнопка Print
        private void button2_Click(object sender, EventArgs e)
        {
            if (printDialog1.ShowDialog() == DialogResult.OK)
            {
                printDocument1.Print();
            }
        }

        private void printDocument1_PrintPage(object sender, System.Drawing.Printing.PrintPageEventArgs e)
        {
            Font myFont = new Font("Arial", 14, FontStyle.Regular, GraphicsUnit.Pixel);
            string curLine;

            float leftMargin = e.MarginBounds.Left; // отступ слева документа
            float topMargin = e.MarginBounds.Top;   // отступ сверху документа
            float yPos = 0;                         // текущая позиция для вывода строки

            int nPages; // кол-во страниц
            int nLines; // максимально-возможное количество строк на странице
            int i;      // номер текущей строки для вывода на странице

            // считаем максимально возможное кол-во строк на странице
            nLines = (int)(e.MarginBounds.Height / myFont.GetHeight(e.Graphics));

            // считаем кол-во страниц для печати
            nPages = (richTextBox1.Lines.Length - 1) / nLines + 1;

            // цикл печати/вывода одной страницы
            i = 0;
            while ((i < nLines) && (counter < richTextBox1.Lines.Length))
            {
                // взять строку для вывода из ricTextBox1
                curLine = richTextBox1.Lines[counter];

                // взять текущую позицию по Y
                yPos = topMargin + i * myFont.GetHeight(e.Graphics);

                // вывести строку в документ
                e.Graphics.DrawString(curLine, myFont, Brushes.Blue, leftMargin, yPos, new StringFormat());

                counter++;
                i++;
            }

            // если текста больше, чем на одну страницу,
            // добавляем доп страницу для печати
            e.HasMorePages = false;

            if (curPages < nPages)
            {
                curPages++;
                e.HasMorePages = true;
            }
        }

        private void printDocument1_BeginPrint(object sender, System.Drawing.Printing.PrintEventArgs e)
        {
            counter = 0;
            curPages = 1;
        }

        // кнопка Page Setup
        private void button1_Click(object sender, EventArgs e)
        {
            pageSetupDialog1.ShowDialog();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            printPreviewDialog1.ShowDialog();
        }
    }
}
