using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
public struct BirthData
{
	public BirthData(int a = 0)
	{
		used = false;
		food = a;
		genom = new int[1];
		genom[0] = 0;
	}
	public void Data(int foodM, int[] genomM)
	{
		used = true;
		food = foodM;
		genom = new int[genomM.Length];
		Array.Copy(genomM, genom, genomM.Length);
	}
	public bool used;
	public int food;
	public int[] genom;
}