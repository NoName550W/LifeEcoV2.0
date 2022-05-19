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
		// действия: (0) фото синтез (1) размножение (2) атака врага (3) передача пищи соседу (4) подобрать еду из соседний мёртвой кледки (5) перейти на соседнею кледку
		
		public int genomeSum;

		private int food; //Сколько у неё есть еды(ресурсов)

		private int age; //Сколько итераций живёт клетка

		private Coords[] CoordsNeighbours = new Coords[8];
		// 0|3|6
		// 1|4|7
		// 2|5|8

		private Cell[] neighbours = new Cell[8];

		private Coords coordsCell; //Место нахождение кледки

		private Colours ColourCell; //Цвет кледки
		private Colours oldColourCell;

		private int amoutManyLiveNeighbors;

		public int searchGoals(int direction, int type)
		{
			Random rnd = new Random();
			int rm = direction - 1;
			if (rm < 0)
			{
				rm += 8;
			}
			switch (type)
			{
				case 0:
					if (!Form.cells[CoordsNeighbours[direction].Y, CoordsNeighbours[direction].X].lifeCell)
					{
						return direction;
					}
					else if (!Form.cells[CoordsNeighbours[(direction + 1) % 8].Y, CoordsNeighbours[(direction + 1) % 8].X].lifeCell)
					{
						return (direction + 1) % 8;
					}
					else if (!Form.cells[CoordsNeighbours[rm].Y, CoordsNeighbours[rm].X].lifeCell)
					{
						return rm;
					}
					else
					{
						int mutation = rnd.Next(0, 8);
						return 0;
					}

					break;
				case 1:
					
					break;
				case 2:
					
					break;
				
			}
			return 0;
		}

		public async void rendering() //Метод отрисофки кледки
		{
			await Task.Run(() =>
			{
				if (oldColourCell.Red != ColourCell.Red && oldColourCell.Green != ColourCell.Green && oldColourCell.Blue != ColourCell.Blue)
				{
					Graphics battlefield = paint.CreateGraphics();
					SolidBrush Colour = new SolidBrush(Color.FromArgb(ColourCell.Red, ColourCell.Green, ColourCell.Blue));
					battlefield.FillRectangle(Colour, coordsCell.Y * 10 + 1, coordsCell.X * 10 + 1, 9, 9);
					oldColourCell = ColourCell;
				}
			});

		}

		public void death() //Метод смерти кледки
		{
			lifeCell = false;
			
			for (int i = 0; i < amountgenm; i++)
			{
				genom[i] = 0;
			}
			food = (food + HP) * 2;
			HP = 0;
			age = 0;
			ColourCell = new Colours(255, 255, 255);
		}

		public void birth(int foodM, int[] genomM ) //Метод рождения этой клетки
		{
			lifeCell = true;			
			Random rnd = new Random();
			int mutation = rnd.Next(0, 1000);
			if (mutation < genomM[0])
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
					genomM[i] += powerMutation;
				}
			
			}
			for (int i = 0; i < amountgenm; i++)
			{
				genom[i] = genomM[i];
			}
			HP = genom[genom[1] % amountgenm];
			eating();
		}

		public void attack(int direction) //Метод атаки другой клетки
		{

		}

		public void damage() //Метод получения урона от другой клетки
		{

		}

		public void reproduction(int direction) //Метод создание новой кледки рядом
		{

		}

		public void eating(int amount = 0) //Метод получения пищи(ресурсов)
		{
			if (amount == 0)
			{
				for(int i = 0; i < 8; i++)
				{

				}
			}
			else
			{

			}			
		}

		public void movingOutput(int direction) // Передвежение кледки (выход из сторой)
		{
			movingInput(HP, genom, food, age);
			lifeCell = false;
			HP = 0;
			for (int i = 0; i < amountgenm; i++)
			{
				genom[i] = 0;
			}
			food = 1;
			age = 0;

		}

		public void movingInput(int HPO, int[] genomO, int foodO, int ageO) // Передвежение кледки (вход в новую)
		{
			lifeCell = true;
			HP = HPO;
			genom = genomO;
			food = foodO;
			age = ageO;
		}

		public void initialization(int Globaly, int Globalx, Form1 localForm) //Первичная иницилизация кледки
		{
			Form = localForm; 
			lifeCell = false;
			HP = 0;
			for (int i = 0; i < amountgenm; i++) 
			{
				genom[i] = 0;
			}
			genomeSum = 0;
			food = 1;
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
						i2++;
					}
				}
			}
			rendering();
		}

		public void update() //Метод обновления кледки
		{
			for (int i = 0; i < 8; i++)
			{
					if (Form.cells[CoordsNeighbours[i].Y, CoordsNeighbours[i].X].lifeCell)
					{
						amoutManyLiveNeighbors++;
					}
				
			} 
			if (lifeCell)
			{
				genomeSum = genom.Sum();
			if(age > 1)
			{
				for(int i = 7; i < 7 + genom[genom[7] % 9];i++)
				{
					
					switch (genom[genom[i]%6])
					{
						case 0://(0) фото синтез
							eating();
							break;

						case 1://(1) размножение 
							reproduction(i);
							break;

						case 2://(2) атака врага 
							attack(i);
							break;

						case 3://(3) передача пищи соседу 

							break;

						case 4://(4) подобрать еду из соседний мёртвой кледки

							break;

						case 5://(5) перейти на соседнею кледку
							movingOutput(i);
							break;
					}
				}
			}//я люблю покетики от чая
				
				age++;
				food -= genom[genom[5] % amountgenm];
				HP += genom[genom[3] % amountgenm] * genom[genom[5] % amountgenm];

				if(HP > genom[genom[2] % amountgenm])
                {
					HP = genom[genom[2] % amountgenm];
				}
				if(food < 0 || HP <= 0 || age >= genom[genom[4] % amountgenm])
                {
					death();
                }
			}
			else
			{
				food += amoutManyLiveNeighbors / 2;
			}
			rendering();
		}

	}
}