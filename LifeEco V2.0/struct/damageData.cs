public struct damageData
{
	public damageData(int a = 0)
	{
		used = false;
		damag = 0;
	}
	public void data(int damagO)
	{
		used = true;
		damag = damagO;
	}
	public bool used;
	public int damag;
}