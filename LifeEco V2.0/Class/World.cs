using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LifeEco_V2._0
{
	public class World
	{
		public World()
		{
			for (int x = 0; x < 99; x++)
			{
				for (int y = 0; y < 99; y++)
				{
					lifeCell[x, y] = false;
					IndexCells[x, y] = -1;
					food[x, y] = 1;
					temperature[x, y] = 0;
					toxicity[x, y] = 0;
				}
			}
		}		
	

		public List<Cell> cells = new List<Cell>();
		public bool[,] lifeCell = new bool[100,100];
		public int[,] IndexCells = new int[100, 100];
		public int[,] food = new int[100, 100];
		public int[,] temperature = new int[100, 100];
		public int[,] toxicity = new int[100, 100];
		public int Time = 0;

		public void passageOfTime()
		{
			Time++;
		}
	}
}
