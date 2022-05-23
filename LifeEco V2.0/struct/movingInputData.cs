using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
public struct MovingInputData
{
	public MovingInputData(int a = 0)
	{
		time = a;
		used = false;
		HP = a;
		food = a;
		genom = new int[1];
		genom[0] = a;
		age = a;
	}
	public void Data(int timeG, int HPO, int[] genomO, int foodO, int ageO)
    {
		time = timeG;
		used = true;
		HP = HPO;
		food = foodO;
		genom = new int[genomO.Length];
		Array.Copy(genomO, genom, genomO.Length);
		age = ageO;
	}
	public int time;
	public int HP;
	public bool used;
	public int food;
	public int[] genom;
	public int age;
}