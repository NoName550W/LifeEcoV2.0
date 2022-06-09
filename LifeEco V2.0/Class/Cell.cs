using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Windows.Forms;

namespace LifeEco_V2._0
{
	public partial class Cell
	{
		public Cell(World worldTemp, PictureBox Paint, int indexTemp, int X, int Y, int foodM, int[] genomM, Direction directionM )//Первичная иницилизация кледки
        {
			world = worldTemp;
			index = indexTemp;
			if (!world.lifeCell[X,Y])
            {
				world.lifeCell[X, Y] = true;
				world.IndexCells[X, Y] = index;
				paint = Paint;
				HP = 0;
				age = 0;
				food = world.food[X, Y];
				world.food[X, Y] = 0;
				CoordsCell = new Coords(X, Y);
				ColourCell = new Colours(255, 255, 255);

				int i2 = 0;
				int[] Numbering = { 0, 7, 6, 1, 5, 2, 3, 4 };
				for (int y = -1; y < 2; y++)
				{
					for (int x = -1; x < 2; x++)
					{
						if (x != 0 || y != 0)
						{
							CoordsNeighbours[Numbering[i2++]] = new Coords(y + CoordsCell.Y, x + CoordsCell.X);
						}
					}
				}


				Array.Copy(genomM, genom, genomM.Length);
				Random rnd = new Random();
				int mutation = rnd.Next(0, 100);
				if (mutation < (genom[Math.Abs(genom[0] % amountgenm)] + 2) * 2)
				{
					int amountMutationGene = rnd.Next(0, 11);
					int amount = 1;
					if (amountMutationGene == 10)
					{
						amount = 2;
					}

					for (int i = 0; i < amount; i++)
					{
						int mutationGene = rnd.Next(0, amountgenm);
						int powerMutation = rnd.Next(-1, 2);
						genom[mutationGene] += powerMutation;
					}
				}

				genomeSum = genom.Sum();
				CoordsCell = new Coords(Y, X);
				HP = genom[Math.Abs(genom[1] % amountgenm)];
				Eating(foodM);
				//Console.WriteLine(coordsCell.X + " " + coordsCell.Y + " " + food);
				Rendering();
			}
            else
            {
				world.cells.RemoveAt(index);
			}
			
		}

		public World world;

		private PictureBox paint;

		private int HP; //Сколько у неё здаровья
        private const int amountgenm = 17;

		private int index;

		private int[] genom = new int[amountgenm]; //Геном клетки
        /*
         в гене храниться не значение а ссылка на это значение в геноме 
		 00 Шанс мутации
		 01 хп при рождении 
		 02 максимум хп
		 03 реенерация хп за итерацию
		 04 максмальная продолжительность жизни
		 05 сколько нужно еды в ход\множитель регинерации
		 06 социальность(разность сумы гинома для того чтобы считать другом + бонус к пище за соседа)
		 07 сколько действй за ход от 0 до 8
		 08 1 действие
		 09 2 действте 
		 10 3 действие
		 11 4 действие
		 12 5 действие 
		 13 6 действие 
		 14 7 действие 
		 15 8 действие 
		 16 сила атки\эфективность фотосинтеза
         действия: (0) фото синтез(1) размножение(2) атака врага(3) подобрать еду из соседний мёртвой кледки(4) перейти на соседнею кледк 
		*/

        private Direction directionCell;

		private int age; //Сколько итераций живёт клетка

		private Coords[] CoordsNeighbours = new Coords[8];
		// 0|3|5
		// 1| |6
		// 2|4|7

		private Coords CoordsCell; //Место нахождение кледки

		private Colours ColourCell; //Цвет кледки
		private Colours oldColourCell;

		private int amoutManyLiveNeighbors;

		public int genomeSum;

		public int food; //Сколько у неё есть еды(ресурсов)

		public BirthData birthD = new BirthData();

		public MovingInputData movingInputD = new MovingInputData();



		public int SearchGoals(int direction, int type) //Поиск цели (0) пустая (1) враг (2) друг
		{
			return -1;
		}

		public void Rendering(int rog = 0) //Метод отрисофки кледки
		{

				if (oldColourCell.Red != ColourCell.Red || oldColourCell.Green != ColourCell.Green || oldColourCell.Blue != ColourCell.Blue)
				{


					Graphics battlefield = paint.CreateGraphics();
					SolidBrush Colour = new SolidBrush(Color.FromArgb(ColourCell.Red, ColourCell.Green, ColourCell.Blue));
					battlefield.FillRectangle(Colour, CoordsCell.Y * 10 + 1, CoordsCell.X * 10 + 1, 9, 9);
					oldColourCell = ColourCell;

				}
				if (rog == 1)
				{
					Graphics battlefield = paint.CreateGraphics();
					SolidBrush Colour = new SolidBrush(Color.FromArgb(ColourCell.Red, ColourCell.Green, ColourCell.Blue));
					battlefield.FillRectangle(Colour, CoordsCell.Y * 10 + 1, CoordsCell.X * 10 + 1, 9, 9);
				//Console.WriteLine(coordsCell.X + " " + coordsCell.Y + " " + age);
				}


		}

		public void Death() //Метод смерти кледки
		{
			world.lifeCell[CoordsCell.X, CoordsCell.Y] = false;
			world.IndexCells[CoordsCell.X, CoordsCell.Y] = -1;
			world.food[CoordsCell.X, CoordsCell.Y] = (food + HP);
			world.cells.RemoveAt(index);
		}

		public void Eating(int amount = 0, int direction = -1) //Метод получения пищи(ресурсов)
		{
			if (amount == 0 && direction < 0)
			{
				food += (genom[genom[16] % amountgenm] * ((amoutManyLiveNeighbors + 1) / 2)) * ((CoordsCell.X) + 1);
			}
			else if (amount != 0 && direction < 0)
			{
				food += amount;
			} 
			else if(direction > -1)
            {
				int directionR = SearchGoals(direction, 0);
				if (directionR > -1)
				{
					food += neighbours[directionR].food;
					neighbours[directionR].food = 0;
				}

			}
		}




		public void Reproduction() //Метод создание новой кледки рядом
		{
			

			if (!world.lifeCell[CoordsNeighbours[directionCell.directionBack].X, CoordsNeighbours[directionCell.directionBack].Y])
			{
				world.cells.Add(Cell())
				(time, food / 4, genom);
				food /= 2;
			}
		}
		
		public void Attack(int direction) //Метод атаки другой клетки
		{
			int directionR = SearchGoals(direction, 1);
			if (directionR > -1)
            {
				neighbours[directionR].Damage(genom[genom[16] % amountgenm]);

			}
		}
		
		public void Moving(int direction) // Передвежение кледки (выход из старой)
		{

        
		}




		public void Damage(int damag) //Метод получения урона от другой клетки
		{
			HP -= damag;
		}

		public void Update() //Метод обновления кледки
		{
			if(age >= 0)
			{
				int f = 8 + genom[genom[7] % amountgenm] % 3;
				for (int i = 8; i < f;i++)
				{
					int f2 = genom[Math.Abs(genom[i] % amountgenm)] % 2;
					switch (f2)
					{
						case 0://(0) фото синтез
							Eating();
							break;

						case 1://(1) размножение 
							if(food > 10)
                            {
								Reproduction(i);
                            }
							
							break;

						//case 2://(2) атака врага 
						//	Attack(i);
						//	break;

						//case 3://(3) подобрать еду из соседний мёртвой кледки
						//	Eating(0, i);
						//	break;

						//case 4://(4) перейти на соседнею кледку
						//	MovingOutput(i);
						//		i = 7 + genom[genom[7] % 9] + 1;
						//	break;
					}
				}
			}
				
			age++;
			//MessageBox.Show(genom[Math.Abs(genom[5] % amountgenm)] * (coordsCell.Y / 2)+" ");
			food -= genom[Math.Abs(genom[5] % amountgenm)] * (CoordsCell.Y);
			HP -= CoordsCell.X;

			if (food <= 0 || HP <= 0 || age > genom[Math.Abs(genom[4] % amountgenm)])
			{
				Death();
			}
			else
			{
				HP += (genom[Math.Abs(genom[3] % amountgenm)] * genom[Math.Abs(genom[5] % amountgenm)]);
				if (HP > genom[Math.Abs(genom[2] % amountgenm)])
				{
					HP = genom[Math.Abs(genom[2] % amountgenm)];
				}
				ColourCell.Data(HP * 1, food / 1000, age * 10);
			}

			Rendering();



			//       if (movingInputD.used) 
			//{
			//	MovingInput(movingInputD.HP, movingInputD.genom, movingInputD.food, movingInputD.age);
			//	movingInputD = new MovingInputData();
			//}
		}

	}
}