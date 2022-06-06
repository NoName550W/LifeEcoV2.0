using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LifeEco_V2._0
{
    public class World
    {
        public List<Cell> cells = new List<Cell>();
        public bool[,] lifeCell = new bool[100,100];
        public int[,] NamberCells = new int[100, 100];
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
