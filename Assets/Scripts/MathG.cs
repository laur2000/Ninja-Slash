using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MathG : MonoBehaviour {

	public static Vector3 GetMiddlePoint(Vector3 v1, Vector3 v2) //Returns the vector of the middle point between two lines
    {
        Vector3 MiddlePoint = (v1 + v2) / 2;
        return MiddlePoint;
    }
   
    public static Vector3 GetPerpendicular(Vector3 normal, Vector3 Point)// Returns the parameters of the plane equation
    {
       
        float A, B, C;
        A = normal.x;
        B = normal.y;
        
        C = -(A * Point.x )-(B * Point.y);


        return new Vector3(A, B, C);
    }
    public static Vector3 LineIntersection(Vector3 line1,Vector3 line2) //Take two plane equations and makes the intersection between them
    {
        float x, y;
        //We apply cramer to resolve the two equations system

        x = (line2.z*line1.y-line1.z*line2.y) / (line1.x*line2.y-line2.x*line1.y);
        y = (line1.z * line2.x - line1.x * line2.z) / (line1.x * line2.y - line2.x * line1.y);

        return new Vector3(x, y, 0);
    }
   

}
