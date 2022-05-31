using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
public struct Direction
{
	public Direction(int directionM = -1)
	{
		if (directionM < 0)
        {
			if (directionM > 7)
			{
				direction = directionM - 8;
			}
			else if (directionM < 0)
			{
				direction = directionM + 8;
			}
			else
			{
				direction = directionM;
			}
		} 
		else
        {
			Random rnd = new Random();
			int directionR = rnd.Next(0, 8);
			direction = directionR;
		}
		directionBack = (direction + 4) % 8;

	}

	public void L()
	{
		direction--;

		if (direction < 0)
		{
			direction += 8;
		}

		directionBack = (direction + 4) % 8;
	}

	public void R()
	{
		direction++;

		if (direction > 7)
		{
			direction -= 8;
		}

		directionBack = (direction + 4) % 8;
	}

	public int direction;
	public int directionBack;
}