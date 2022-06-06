public struct Coords
{
	public Coords(int x, int y)
	{
		if(x > 99)
		{
			X = x - 100;
		} 
		else if(x < 0)
        {
			X = x + 100;
        }
        else
        {
			X = x;
        }
		if (y > 99)
		{
			Y = y - 100;
		}
		else if (y < 0)
		{
			Y = y + 100;
		}
		else
		{
			Y = y;
		}
		
	}
	public int X;
	public int Y;
}