using UnityEngine;
using System.Collections;

public class Lib  {

	public static Color RColor (int r , int g , int b , int a ){
		float rr = (float)r/255f;
		float gg = (float)g/255f;
		float bb = (float)b/255f;
		float aa = (float)a/255f;
		return new Color (rr,gg,bb,aa);
	}
}
