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
		//public Cell[,] cells = new Cell[100,100];

		World world = new World();

		bool loop = false;

		

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
			for(int x = 0; x < 100; x++)
			{
				for (int y = 0; y < 100; y++)
				{
					Graphics battlefield = pictureBox1.CreateGraphics();
					SolidBrush Colour = new SolidBrush(Color.FromArgb(255, 255, 255));
					battlefield.FillRectangle(Colour, y * 10 + 1, x * 10 + 1, 9, 9);
				}
			}
		}

		private void button2_Click(object sender, EventArgs e)
		{
			int[] gen =
				{
				1, //0
				1, 1, //1 2
				1, 16, //3 4
				1, 1, //5 6
				12, 11, //7 8
				1, 15, //9 10
				0, 3, //11 12
				1, 9, //13 14
				2, 15 };//15 16

			//cells[0, 0].Birth(10, gen);
			//cells[0, 1].Birth(10, gen);
			//cells[0, 2].Birth(10, gen);
			//cells[1, 2].Birth(10, gen);
			//cells[2, 0].Birth(10, gen);
			//for (int y = 0; y < 100; y++)
			//{
			//    for (int x = 0; x < 100; x++)
			//    {
			//        cells[y, x].Update();
			//    }
			//}
			world.cells.Add(new Cell(world, pictureBox1, world.cells.Count, 10, 10, 20, gen, 1));
			//world.cells.Add(new Cell(world, pictureBox1, world.cells.Count, 50, 10, 5, gen, 1));
		}

		private async void button3_Click(object sender, EventArgs e)
		{
			await Task.Run(() =>
			{
				loop = true;
				while(loop)
				{
					for(int i = world.cells.Count - 1; i >= 0; i--)
                    {
						world.cells[i].Update(i);
					}
					world.passageOfTime();
					
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
					Graphics battlefield = pictureBox1.CreateGraphics();
					SolidBrush Colour = new SolidBrush(Color.FromArgb(255, 255, 255));
					battlefield.FillRectangle(Colour, y * 10 + 1, x * 10 + 1, 9, 9);
					
					label1.Text = world.Time.ToString();
				}
			}
			
		}

		private void Form1_Paint(object sender, PaintEventArgs e)
		{

		}
	}
}
