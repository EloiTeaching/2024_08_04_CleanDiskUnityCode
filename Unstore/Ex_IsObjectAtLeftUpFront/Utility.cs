using UnityEngine;
using System.Collections;
using System;
public class Utility  {


    public static class Converter {
        public static RYP QuaternionToRYP(Quaternion value) { throw new NotImplementedException(); }
    }
    public static class Axes
    {
        public static Vector3 GetDirection(Vector3 axesPosition, Quaternion axeRotation, Vector3 point) { throw new NotImplementedException(); }
    }

    public static class BasedOnPosition 
    {
        /**To complete*/

       public static Vector3 GetDirection(Vector3 left, Vector3 right, Vector3 thirdEdge) {
            Vector3 direction = Vector3.zero;
            Vector3 triCenter = (left + right + thirdEdge * 2f) / 4f;

			Vector3 vect1 = left-triCenter;
			Vector3 vect2 = right-triCenter;
			direction = Vector3.Cross( vect2,vect1).normalized;

            return direction;
		}
		public static Vector3 GetDirection(Vector3 originePoint, Vector3 directionalPoint) {
			
			return directionalPoint - originePoint;
		}
		public static Quaternion GetQuadDirection(Vector3 originePoint, Vector3 directionalPoint) {

			return 		Quaternion.LookRotation( (directionalPoint-originePoint).normalized, Vector3.forward);

		}

		public static bool IsAtRight(Vector3 point,Vector3 rootPosition ,Quaternion rootRotation){
			Vector3 relocatedPoint = Relocated (point, rootPosition, rootRotation);
			if (relocatedPoint.x >= 0)
				return true;
			return false;
		}
		public static bool IsAtLeft(Vector3 point,Vector3 rootPosition ,Quaternion rootRotation){
			Vector3 relocatedPoint = Relocated (point, rootPosition, rootRotation);
			if (relocatedPoint.x < 0)
				return true;
			return false;
		}
	
		public static bool IsAtTop(Vector3 point,Vector3 rootPosition ,Quaternion rootRotation){
			Vector3 relocatedPoint = Relocated (point, rootPosition, rootRotation);
			if (relocatedPoint.y >= 0)
				return true;
			return false;
		}
		public static bool IsAtDown(Vector3 point,Vector3 rootPosition ,Quaternion rootRotation){
			Vector3 relocatedPoint = Relocated (point, rootPosition, rootRotation);
			if (relocatedPoint.y < 0)
				return true;
			return false;
		}
		public static bool IsAtFront(Vector3 point,Vector3 rootPosition ,Quaternion rootRotation){
			Vector3 relocatedPoint = Relocated (point, rootPosition, rootRotation);
			if (relocatedPoint.z >= 0)
				return true;
			return false;
		}
		public static bool IsAtBack(Vector3 point,Vector3 rootPosition ,Quaternion rootRotation){
			Vector3 relocatedPoint = Relocated (point, rootPosition, rootRotation);
			if (relocatedPoint.z < 0)
				return true;
			return false;
		}

		
		public static Vector3 Relocated(Vector3 point,Vector3 rootPosition ,Quaternion rootRotation, bool withIgnoreX=false, bool withIgnoreY=false, bool withIgnoreZ=false){
			//Recenter the point on cartesions axes
			Vector3 p = point- rootPosition;
			
			//Rotate points, based on the root rotation, in aim to be front of cartesians axes
			//No need to be rotate if the root is already in front of cartesians axes
			if (rootRotation.eulerAngles != Vector3.zero) {
				Quaternion rootRotationInverse = Quaternion.Inverse (rootRotation);
				p = RotateAroundPoint (p, Vector3.zero, rootRotationInverse);
			}
			if (withIgnoreX)
				p.x = 0;
			if (withIgnoreY)
				p.y = 0;
			if (withIgnoreZ)
				p.z = 0;

			//Debug.Log (p);
			//Debug.DrawLine (rootPosition, rootPosition+p ,  Color.red);
			return p;
		}
		
		public static Vector3 RotateAroundPoint(Vector3 point, Vector3 pivot, Quaternion angle) {
			return angle * ( point - pivot) + pivot;
		}


		public static float GetAngleBetween(Vector3 first, Vector3 pivotCenter , Vector3 second)
		{
					Vector3 ptOne = first - pivotCenter;
					Vector3 ptTwo = second - pivotCenter;
					return Vector3.Angle (ptOne, ptTwo);
		}

		
		public static float GetAngleBetween(Vector3 point, Vector3 rootPos, Quaternion rootRot, bool withIgnoreX=false, bool withIgnoreY=false, bool withIgnoreZ=false )
		{
			Vector3 relocPoint = Relocated (point, rootPos, rootRot, withIgnoreX,withIgnoreY,withIgnoreZ);
			return GetAngleBetween (relocPoint, Vector3.zero, Vector3.forward);
		}


        public static Vector2 GetDirection(Vector2 lineA, Vector2 lineB, Vector2 directionalPoint) { throw new NotImplementedException(); }
        public static Vector2 GetDirection(Vector2 Left, Vector2 right) { throw new NotImplementedException(); }


    }

	public static class  BasedOnRotation
    {

		public static Vector3 GetForwardPoint( Vector3 position, Vector3 rotation,float distance =5f){
			return GetForwardPoint (position, Quaternion.Euler (rotation), distance);
		}

		public static Vector3 GetForwardPoint( Vector3 position, Quaternion rotation,float distance =5f){
			Vector3 point = rotation * Vector3.forward * distance;
			return position + point;
		}

    }


	public static class Drawer  {
		
		public static readonly Color defaultColor = Color.white;
		
		public static void DrawLine(Vector3 start, Vector3 end, Color Color, float time = 0.02f) { }
		public static void DrawArrow(Vector3 start, Vector3 end, Color Color, float time = 0.02f) { }
		
		public static void DraweLine(Vector3[] points, bool displayStart, bool displayEnd, Color Color, float time = 0.03f) { }
		
		public static void DrawPoint(Vector3 point) { }
		public static void DrawPoint(Vector3 point, Quaternion rotation,PointWithRotationType  type) { }
		
		public static void DrawAxes(Vector3 point, Quaternion direction) { }
		
		// public static void DrawRectangle(...){}
		// public static void DrawCircle(... ,int numberSection){}
		// public static void DrawTriangle(...){}
		// public static void DrawTriangulation(...){}
		public enum PointWithRotationType{Cross, Arrow, Axes}
	}



    /**Roll, Yaw, Pitch*/
    public class RYP
    {

    }
}
