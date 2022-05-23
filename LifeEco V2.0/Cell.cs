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
		Form1 Form;

		public PictureBox paint;

		public bool lifeCell; //Являеться ли клетка живой

		private int HP; //Сколько у неё здаровья
        private const int amountgenm = 17;

		private int[] genom = new int[amountgenm]; //Геном клетки
		// в гене храниться не значение а ссылка на это значение в геноме 
		// 00 Шанс мутации
		// 01 хп при рождении 
		// 02 максимум хп
		// 03 реенерация хп за итерацию
		// 04 максмальная продолжительность жизни
		// 05 сколько нужно еды в ход\множитель регинерации
		// 06 социальность (разность сумы гинома для того чтобы считать другом + бонус к пище за соседа)
		// 07 сколько действй за ход от 0 до 8
		// 08 1 действие
		// 09 2 действте 
		// 10 3 действие
		// 11 4 действие
		// 12 5 действие 
		// 13 6 действие 
		// 14 7 действие 
		// 15 8 действие 
		// 16 сила атки\эфективность фотосинтеза
		// действия: (0) фото синтез (1) размножение (2) атака врага (3) подобрать еду из соседний мёртвой кледки (4) перейти на соседнею кледку
		
		public int genomeSum;

		

		private int age; //Сколько итераций живёт клетка

		private Coords[] CoordsNeighbours = new Coords[8];
		// 0|3|5
		// 1| |6
		// 2|4|7

		private Cell[] neighbours = new Cell[8];

		private Coords coordsCell; //Место нахождение кледки

		private Colours ColourCell; //Цвет кледки
		private Colours oldColourCell;

		private int amoutManyLiveNeighbors;

		public int food; //Сколько у неё есть еды(ресурсов)

		public BirthData birthD = new BirthData();

		public MovingInputData movingInputD = new MovingInputData();



		public int SearchGoals(int direction, int type) //Поиск цели (0) пустая (1) враг (2) друг
		{
			int rm = direction - 1;
			if (rm < 0)
			{
				rm += 8;
			}
			switch (type)
			{
				case 0:
					//if (!neighbours[direction].lifeCell && !neighbours[direction].birthD.used && !neighbours[direction].movingInputD.used)
					//{
					//	return direction;
					//}
					//else if (!neighbours[(direction + 1) % 8].lifeCell && !neighbours[(direction + 1) % 8].birthD.used && !neighbours[(direction + 1) % 8].movingInputD.used)
					//{
					//	return (direction + 1) % 8;
					//}
					//else if (!neighbours[rm].lifeCell && !neighbours[rm].birthD.used && !neighbours[rm].movingInputD.used)
					//{
					//	return rm;
					//}
					//else
					//{
						//for (int i = 7; i >= 0; i--)
                        {
						Random rnd = new Random();
						int i = rnd.Next(0, 8);
						if (!neighbours[i].lifeCell && !neighbours[i].birthD.used && !neighbours[i].movingInputD.used)
                            {
								return i;
                            }
                        }
						return -1;
					//}
					//break;
				case 1:
					if (neighbours[direction].lifeCell && Math.Abs(genomeSum - neighbours[direction].genomeSum) > genom[genom[6] % amountgenm])
					{
						return direction;
					}
					else if (neighbours[(direction + 1) % 8].lifeCell && Math.Abs(genomeSum - neighbours[(direction + 1) % 8].genomeSum) > genom[genom[6] % amountgenm])
					{
						return (direction + 1) % 8;
					}
					else if (neighbours[rm].lifeCell && Math.Abs(genomeSum - neighbours[rm].genomeSum) > genom[genom[6] % amountgenm])
					{
						return rm;
					}
					else
					{
						return -1;
					}
					break;
				case 2:
					if (neighbours[direction].lifeCell && Math.Abs(genomeSum - neighbours[direction].genomeSum) < genom[genom[6] % amountgenm])
					{
						return direction;
					}
					else if (neighbours[(direction + 1) % 8].lifeCell && Math.Abs(genomeSum - neighbours[(direction + 1) % 8].genomeSum) < genom[genom[6] % amountgenm])
					{
						return (direction + 1) % 8;
					}
					else if (neighbours[rm].lifeCell && Math.Abs(genomeSum - neighbours[rm].genomeSum) < genom[genom[6] % amountgenm])
					{
						return rm;
					}
					else
					{
						return -1;
					}
					break;
				
			}
			return -1;
		}

		public void Rendering(int rog = 0) //Метод отрисофки кледки
		{

				if (oldColourCell.Red != ColourCell.Red || oldColourCell.Green != ColourCell.Green || oldColourCell.Blue != ColourCell.Blue)
				{


					Graphics battlefield = paint.CreateGraphics();
					SolidBrush Colour = new SolidBrush(Color.FromArgb(ColourCell.Red, ColourCell.Green, ColourCell.Blue));
					battlefield.FillRectangle(Colour, coordsCell.Y * 10 + 1, coordsCell.X * 10 + 1, 9, 9);
					oldColourCell = ColourCell;

				}
				if (rog == 1)
				{
					Graphics battlefield = paint.CreateGraphics();
					SolidBrush Colour = new SolidBrush(Color.FromArgb(ColourCell.Red, ColourCell.Green, ColourCell.Blue));
					battlefield.FillRectangle(Colour, coordsCell.Y * 10 + 1, coordsCell.X * 10 + 1, 9, 9);
				//Console.WriteLine(coordsCell.X + " " + coordsCell.Y + " " + age);
				}


		}

		public void Death() //Метод смерти кледки
		{
			lifeCell = false;
			Console.WriteLine(HP + " " + food + " " + age);
			for (int i = 0; i < amountgenm; i++)
			{
				genom[i] = 0;
			}
			food = (food + HP);
			HP = 0;
			age = 0;
			ColourCell = new Colours(255, 255, 255);
		}

		public void Eating(int amount = 0, int direction = -1) //Метод получения пищи(ресурсов)
		{
			if (amount == 0 && direction < 0)
			{
				food += (genom[genom[16] % amountgenm] * (amoutManyLiveNeighbors + 1)) * ((coordsCell.X) + 1);
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




		public void Reproduction(int direction) //Метод создание новой кледки рядом
		{
			int directionR = SearchGoals(direction, 0);
			if (directionR > -1)
			{
				neighbours[directionR].birthD.Data(food / 4, genom);
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
		
		public void MovingOutput(int direction) // Передвежение кледки (выход из старой)
		{
			int directionR = SearchGoals(direction, 0);
			if (directionR > -1)
            {
			neighbours[directionR].movingInputD.Data(HP, genom, food, age);
			lifeCell = false;
			HP = 0;
			for (int i = 0; i < amountgenm; i++)
			{
				genom[i] = 0;
			}
			food = 0;
			age = 0;
            }
		}




		public void Birth(int foodM, int[] genomM ) //Метод рождения этой клетки
		{
            if (!lifeCell)
            {
			lifeCell = true;
			Array.Copy(genomM, genom, genomM.Length);
			Random rnd = new Random();
			int mutation = rnd.Next(0, 100);
			if (mutation < genom[0] * 2)
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
			HP = genom[Math.Abs(genom[1] % amountgenm)];
			Eating(foodM);
			Console.WriteLine(coordsCell.X + " " + coordsCell.Y + " " + food);
			}

		}

		public void Damage(int damag) //Метод получения урона от другой клетки
		{
			HP -= damag;
		}

		public void MovingInput(int HPO, int[] genomO, int foodO, int ageO) // Передвежение кледки (вход в новую)
		{
			lifeCell = true;
			HP = HPO;
			Array.Copy(genomO, genom, genomO.Length);
			food += foodO;
			age = ageO;
			age++;
			food -= genom[genom[5] % amountgenm];
			HP += genom[genom[3] % amountgenm] * genom[genom[5] % amountgenm];

			if (HP > genom[genom[2] % amountgenm])
			{
				HP = genom[genom[2] % amountgenm];
			}
			ColourCell = new Colours(HP, food, genomeSum);
			if (food < 0 || HP <= 0 || age > genom[genom[4] % amountgenm])
			{
				Death();
			}
		}




		public void Initialization(int Globaly, int Globalx, Form1 localForm) //Первичная иницилизация кледки
		{
			Form = localForm; 
			lifeCell = false;
			HP = 0;
			for (int i = 0; i < amountgenm; i++) 
			{
				genom[i] = 0;
			}
			genomeSum = 0;
			food = 2;
			age = 0;
			coordsCell = new Coords(Globaly, Globalx);
			ColourCell = new Colours(255,255,255);
			amoutManyLiveNeighbors = 0;

			int i2 = 0;
			for (int y = - 1; y < 2; y++)
			{
				for (int x = -1; x < 2; x++)
				{
					if (x != 0 || y != 0)
					{
						CoordsNeighbours[i2] = new Coords(y + coordsCell.Y, x + coordsCell.X);
						neighbours[i2] = Form.cells[CoordsNeighbours[i2].Y, CoordsNeighbours[i2].X];   
						i2++;
					}
				}
			}
			Rendering();
		}

		public void Update() //Метод обновления кледки
		{            
			if (birthD.used)
            {
				Birth(birthD.food, birthD.genom);
				birthD = new BirthData();
            }
			for (int i = 0; i < 8; i++)
			{
				if (neighbours[i].lifeCell)
				{
					amoutManyLiveNeighbors++;
				}
				
			} 
			if (lifeCell)
			{
				//Random rnd = new Random();
				//int mutation = rnd.Next(0, 1000);
				//if (mutation < genom[0])
				//{
				//	int amountMutationGene = rnd.Next(0, 11);
				//	int amount = 1;
				//	if (amountMutationGene == 10)
				//	{
				//		amount = 2;
				//	}

				//	for (int i = 0; i < amount; i++)
				//	{
				//		int mutationGene = rnd.Next(0, amountgenm);
				//		int powerMutation = rnd.Next(-1, 2);
				//		genom[mutationGene] += powerMutation;
				//	}
				//}
				genomeSum = genom.Sum();
				Console.WriteLine(coordsCell.X + " " + coordsCell.Y + " " + food +" " + age);
			if(age > 0)
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
				food -= genom[Math.Abs(genom[5] % amountgenm)] * (coordsCell.Y);
				HP -= coordsCell.X;

				
				if(food <= 0 || HP <= 0 || age > genom[Math.Abs(genom[4] % amountgenm)])
                {
					Death();
                } else
                {
					HP += (genom[Math.Abs(genom[3] % amountgenm)] * genom[Math.Abs(genom[5] % amountgenm)]);
					if (HP > genom[Math.Abs(genom[2] % amountgenm)])
					{
						HP = genom[Math.Abs(genom[2] % amountgenm)];
					}
				ColourCell.Data(HP * 1, food / 10000, age * 10);
                }

				Rendering();
			}
			else
			{
				food += amoutManyLiveNeighbors / 2;
			}

   //         if (movingInputD.used) 
			//{
			//	MovingInput(movingInputD.HP, movingInputD.genom, movingInputD.food, movingInputD.age);
			//	movingInputD = new MovingInputData();
			//}

			
			amoutManyLiveNeighbors = 0;
		}

	}
}