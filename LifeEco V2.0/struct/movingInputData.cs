public struct movingInputData
{
	public movingInputData(int a = 0)
	{
		used = false;
		HP = 0;
		food = 0;
		genom = new int[1];
		genom[0] = 0;
		age = 0;
	}
	public void data(int HPO, int[] genomO, int foodO, int ageO)
    {
		used = true;
		HP = HPO;
		food = foodO;
		genom = new int[genomO.Length];
		genom = genomO;
		age = ageO;
	}
	public int HP;
	public bool used;
	public int food;
	public int[] genom;
	public int age;
}