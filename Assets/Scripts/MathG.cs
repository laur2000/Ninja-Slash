using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class Segment
{
    Vector3 segment;
    public Vector3 vertex1, vertex2;
    public int index1, index2;
    public bool isSelected;
    public Segment()
    {
        segment = new Vector3();
        vertex1 = new Vector3(); 
        vertex2 = new Vector3(); 
        isSelected = false;
    }

    public Segment(Vector3 line, int indexOne, int IndexTwo)
    {
        segment = line;
        index1 = indexOne;
        index2 = IndexTwo;
        isSelected = false;
    }
    public Vector3 GetSegment()
    {
        return segment;
    }
    public void SetSegment(Vector3 line)
    {
        segment = line;
    }
    public void SetSegment(Vector3 v1, Vector3 v2)
    {
        Vector3 aux = new Vector3();
        if (v1.x > v2.x)
        {
            aux = v2;
            v2 = v1;
            v1 = aux;
        }
        vertex1 = v1;
        vertex2 = v2;
        segment = MathG.GetLineEquation(v2 - v1, v1);
    }
    public void SetVertexOne(Vector3 vertex)
    {
        vertex1 = vertex;
    }
    public void SetVertexTwo(Vector3 vertex)
    {
        vertex2 = vertex;
        Vector3 aux = new Vector3();
        if (vertex1.x > vertex2.x)
        {
            aux = vertex2;
            vertex2 = vertex1;
            vertex1 = aux;
        }
    }
    public float Getfx(float x)
    {
        float y = 0;
        y=(-segment.x*x-segment.z)/segment.y;
        return y;
    }



}
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
        C = vectorDir.x * Point.y - vectorDir.y * Point.x;
        
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
    public static Vector3 IntersectionTwoSegments(Segment segment1, Segment segment2) //Take two plane equations and makes the intersection between them
    {
        float x, y, delta;
        Vector3 line1=segment1.GetSegment();
        Vector3 line2=segment2.GetSegment();
        //We apply cramer to resolve the two equations system
        delta = CalculateMatrix(line1, line2);
        x = ((line1.z*line2.y)-(line2.z*line1.y))/ delta;
        y = ((line1.x*line2.z)-(line2.x*line1.z))/ delta;
        if(-x>=segment1.vertex1.x && -x<=segment1.vertex2.x)
        {
            
            return new Vector3(-x, -y, 0);
        }
        
       
        return new Vector3();
    }
    public static float CalculateMatrix(Vector3 line1,Vector3 line2)
    {
        //Uses the general method of calculating the matrix
        float delta;
        delta = ((line1.x * line2.y) - (line2.x * line1.y));
        return delta;
    }
   
    public static bool LineContains(Vector3 line,Vector3 Point)
    {
        //
        bool isContained = false;
        if((line.x*Point.x+line.y*Point.y+line.z)>-0.2f && (line.x * Point.x + line.y * Point.y + line.z)< 0.2f)
        {
            isContained = true;
        }
        return isContained;
    }
    public static bool SegmentContains(Vector3 line, Vector3 Vertex1, Vector3 Vertex2, Vector3 Point)
    {
        bool isContained = false;
        Vector3 aux = new Vector3();
        if(Vertex1.x>Vertex2.x)
        {
            aux = Vertex2;
            Vertex2 = Vertex1;
            Vertex1 = aux;
        }

        if (Point.x<Vertex1.x && Point.x>Vertex2.x)
        {
            if ((line.x * Point.x + line.y * Point.y + line.z) > -0.2f && (line.x * Point.x + line.y * Point.y + line.z) < 0.2f)
            {
                isContained = true;
            }
        }
        return isContained;
    }

}
