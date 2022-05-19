using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace LifeEco_V2._0
{
    public partial class Form1 : Form
    {
        public Cell[,] cells = new Cell[100,100];

        public Form1()
        {
            InitializeComponent();
            
        }

        public void Neigh(int x, int y, int type, int g)
        {

        }

        private void Form1_Load(object sender, EventArgs e)
        {
        }

        private void button1_Click(object sender, EventArgs e)
        {

        }

        private void button1_Click_1(object sender, EventArgs e)
        {
            for(int y = 0; y < 100; y++)
            {
                for (int x = 0; x < 100; x++)
                {
                    cells[y, x] = new Cell();
                }
            }
            for (int y = 0; y < 100; y++)
            {
                for (int x = 0; x < 100; x++)
                {
                    cells[y, x].paint = this.pictureBox1;
                    cells[y, x].initialization(y, x, this);
                }
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            if(1==1)
            {

            }
        }
    }
}
