using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using ExcelScaleLib;
namespace TestForm
{
    public partial class Form1 : Form
    {
        ExcelScaleLib.Port ExcelScaleLib_port = new Port();
        public Form1()
        {         
            InitializeComponent();
            ExcelScaleLib.Communication.ConsoleWrite = true;
            this.button_初始化.Click += Button_初始化_Click;
            this.button_讀取_g.Click += Button_讀取_g_Click;
            this.button_扣重.Click += Button_扣重_Click;
            this.button_讀取_錢.Click += Button_讀取_錢_Click;
        }

    
        private void Button_初始化_Click(object sender, EventArgs e)
        {
          
            ExcelScaleLib_port.Init("COM5", 9600);
        }

        private void Button_讀取_g_Click(object sender, EventArgs e)
        {
            ExcelScaleLib_port.get_weight(Port.enum_unit_type.g);
        }
        private void Button_讀取_錢_Click(object sender, EventArgs e)
        {
            ExcelScaleLib_port.get_weight(Port.enum_unit_type.dwt);
        }
        private void Button_扣重_Click(object sender, EventArgs e)
        {
            ExcelScaleLib_port.set_sub_current_weight();
        }
    }
}
