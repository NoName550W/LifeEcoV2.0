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

        bool loop = false;

        public int Time = 0;

        public Form1()
        {
            InitializeComponent();
            
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
                    cells[y, x].Initialization(y, x, this);
                }
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            //int[] gen = 
            //    { 
            //    1, //0
            //    1, 1, //1 2
            //    1, 14, //3 4
            //    1, 1, //5 6
            //    12, 11, //7 8
            //    1, 1, //9 10
            //    0, 2, //11 12
            //    1, 10, //13 14
            //    2, 15 };//15 16
    //        int[] gen =
    //{
    //            3, //0
    //            7, 4, //1 2
    //            1, 14, //3 4
    //            -1, 1, //5 6
    //            12, 10, //7 8
    //            3, 3, //9 10
    //            0, 2, //11 12
    //            1, 18, //13 14
    //            5, 15 };//15 16
                //int[] gen =
                //{
                //5, //0
                //7, 4, //1 2
                //1, 16, //3 4
                //1, 1, //5 6
                //17, 11, //7 8
                //3, -1, //9 10
                //0, 1, //11 12
                //4, 21, //13 14
                //4, 15 };//15 16
                int[] gen =
                {
                4, //0
                8, 4, //1 2
                -3, 16, //3 4
                3, 2, //5 6
                17, 14, //7 8
                4, -3, //9 10
                1, 3, //11 12
                7, 19, //13 14
                5, 40};//15 16

            cells[0, 0].Birth(5, gen);
            cells[1, 0].Birth(5, gen);
            cells[2, 0].Birth(5, gen);
            for (int y = 0; y < 100; y++)
            {
                for (int x = 0; x < 100; x++)
                {
                    cells[y, x].Update();
                }
            }
        }

        private async void button3_Click(object sender, EventArgs e)
        {
            await Task.Run(() =>
            {
                loop = true;
            while(loop)
            {
                Console.WriteLine(Time);
                for (int y = 0; y < 100; y++)
                {
                    for (int x = 0; x < 100; x++)
                    {
                        cells[y, x].Update();
                        
                    }
                }
                Time++;

            }
            });

        }

        private void button4_Click(object sender, EventArgs e)
        {
            loop = false;
            for (int y = 0; y < 100; y++)
            {
                for (int x = 0; x < 100; x++)
                {
                    cells[y, x].Rendering(1);

                }
            }
        }
    }
}
