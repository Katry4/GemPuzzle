using UnityEngine;
using System.Collections;

public class IntVector2
{

	#region Basic vectors
	public static IntVector2 up = new  IntVector2(1, 0);
	public static IntVector2 down = new IntVector2(-1, 0);
	public static IntVector2 right = new IntVector2(0, 1);
	public static IntVector2 left = new IntVector2(0, -1);
	#endregion

	public int x, y;

	public IntVector2(int x, int y)
	{
		this.x = x;
		this.y = y;
	}

	public override string ToString()
	{
		return "("+x+";"+y+")";
	}

	public static IntVector2 operator +(IntVector2 v1, IntVector2 v2)
	{
		return new IntVector2(v1.x + v2.x, v1.y + v2.y);
	}

	public static IntVector2 operator -(IntVector2 v1, IntVector2 v2)
	{
		return new IntVector2(v1.x - v2.x, v1.y - v2.y);
	}

}
