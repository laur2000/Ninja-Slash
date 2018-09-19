using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MathG : MonoBehaviour {

	public static Vector3 GetMiddlePoint(Vector3 v1, Vector3 v2) //Returns the vector of the middle point between two lines
    {
        Vector3 MiddlePoint = (v1 + v2) / 2;
        return MiddlePoint;
    }
   
    public static Vector3 GetPlanePerpendicular(Vector3 normal, Vector3 Point)// Returns the parameters of the plane equation
    {
       
        float A, B, C;
        A = normal.x;
        B = normal.y;        
        C = -(A * Point.x )-(B * Point.y);


        return new Vector3(A, B, C);
    }
    public static Vector3 GetLineEquation(Vector3 vectorDir, Vector3 Point)
    {
        float A, B, C;
        A = vectorDir.y;
        B = -vectorDir.x;
        C= -(A * Point.x) - (B * Point.y);
        return new Vector3(A, B, C);
    }
    public static Vector3 TwoIcogniteEquation(Vector3 line1,Vector3 line2) //Take two plane equations and makes the intersection between them
    {
        float x, y,delta;
        //We apply cramer to resolve the two equations system
        delta = CalculateMatrix(line1,line2);
        x = (line2.z*line1.y-line1.z*line2.y) / delta;
        y = (line1.z * line2.x - line1.x * line2.z) / delta;

        return new Vector3(x, y, 0);
    }
    public static float CalculateMatrix(Vector3 line1,Vector3 line2)
    {
        float delta;
        delta = (line1.x * line2.y - line2.x * line1.y);
        return delta;
    }
   

}
