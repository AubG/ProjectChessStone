using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public static class Extensions {

	// Note that Color32 and Color implictly convert to each other. You may pass a Color object to this method without first casting it.
	public static string ColorToHex(this Color color)
	{
		string hex = color.r.ToString("X2") + color.g.ToString("X2") + color.b.ToString("X2");
		return hex;
	}
	
	public static Color HexToColor(this Color color, string hex)
	{
		byte r = byte.Parse(hex.Substring(0,2), System.Globalization.NumberStyles.HexNumber);
		byte g = byte.Parse(hex.Substring(2,2), System.Globalization.NumberStyles.HexNumber);
		byte b = byte.Parse(hex.Substring(4,2), System.Globalization.NumberStyles.HexNumber);
		return new Color32(r,g,b, 255);
	}
}