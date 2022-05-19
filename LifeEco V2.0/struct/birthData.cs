public struct birthData
{
	public birthData(int a = 0)
	{
		used = false;
		food = 0;
		genom = new int[1];
		genom[0] = 0;
	}
	public void data(int foodM, int[] genomM)
	{
		used = true;
		food = foodM;
		genom = new int[genomM.Length];
		genom = genomM;
	}
	public bool used;
	public int food;
	public int[] genom;
}