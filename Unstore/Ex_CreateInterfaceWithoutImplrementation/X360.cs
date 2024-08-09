

using UnityEngine;
using System.Collections;

public class X360  {

	public static I_X360Data x360 = new X360ControllerData();

	public static  I_X360Data GetData()  
	{
		return x360;
	}

	public static bool IsA ()
	{
		return GetData().IsA();
	}

	public static bool IsB ()
	{
		return GetData().IsB();
	}

	public static bool IsX ()
	{
		
		return GetData().IsX();
	}

	public static bool IsY ()
	{
		return GetData().IsY();
	}

	public static bool IsLb ()
	{
		return GetData().IsLb();
	}

	public static bool IsRb ()
	{
		return GetData().IsRb();
	}

	public static bool IsMenu ()
	{
		return GetData().IsMenu();
	}

	public static bool IsStart ()
	{
		return GetData().IsStart();
	}

	public static float GetLt ()
	{
		return GetData().GetLt();
	}

	public static bool IsLt ()
	{
		return GetData().IsLt();
	}

	public static float GetRt ()
	{
		return GetData().GetRt();
	}

	public static bool IsRt ()
	{
		return GetData().IsRt();
	}

	public static bool IsLeftPull ()
	{
		return GetData().IsLeftPull();
	}

	public static bool IsRightPull ()
	{
		return GetData().IsRightPull();
	}

	//public static Direction GetLeft ()
	//{
	//	x360.Refresh_LEFT();
	//	return GetData().GetLeft();
	//}
	//public static Vector3 GetVectorLeft ()
	//{
	//	x360.Refresh_LEFT();
	//	return  ConvertToVector3( GetData().GetLeft());
	//}

	//public static Direction GetRight ()
	//{
	//	x360.Refresh_RIGHT();
	//	return GetData().GetRight();
	//}
	//public static Vector3 GetVectorRight ()
	//{
	//	x360.Refresh_RIGHT();
	//	return ConvertToVector3( GetData().GetRight());
	//}
	
	//public static Direction GetArrows ()
	//{
	//	x360.Refresh_ARROWS();
	//	return GetData().GetArrows();
	//}
	//public static Vector3 GetVectorArrows ()
	//{
	//	x360.Refresh_ARROWS();
	//	return ConvertToVector3( GetData().GetArrows());
	//}

	//public static Vector3 ConvertToVector3(Direction d)
	//{
	//	return new Vector3(d.GetX(),d.GetY(),0);
	//}
	//public static Vector2 ConvertToVector2(Direction d)
	//{
	//	return new Vector2(d.GetX(),d.GetY());
	//}

}
