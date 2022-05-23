public struct Colours
{
	public Colours(int red, int green, int blue)
	{
		if(red < 0)
		{
			Red = 0;
		} 
		else if (red > 255)
		{
			Red = 255;
		}
        else
        {
			Red = red;
        }

		if (green < 0)
		{
			Green = 0;
		}
		else if (green > 255)
		{
			Green = 255;
		}
		else
		{
			Green = green;
		}

		if (blue < 0)
		{
			Blue = 0;
		}
		else if (blue > 255)
		{
			Blue = 255;
		}
		else
		{
			Blue = blue;
		}

	}
	public void Data(int red, int green, int blue)
	{
		if (red < 0)
		{
			Red = 0;
		}
		else if (red > 255)
		{
			Red = 255;
		}
		else
		{
			Red = red;
		}

		if (green < 0)
		{
			Green = 0;
		}
		else if (green > 255)
		{
			Green = 255;
		}
		else
		{
			Green = green;
		}

		if (blue < 0)
		{
			Blue = 0;
		}
		else if (blue > 255)
		{
			Blue = 255;
		}
		else
		{
			Blue = blue;
		}

	}

	public int Red;
	public int Green;
	public int Blue;

}