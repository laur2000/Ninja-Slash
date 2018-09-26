using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class Shuriken
{
    Segment shurLine;
    Vector3 position;
    Vector3 intersection;
    Segment lastSegment;
    public bool isDirectionCalculated;
   
   public GameObject shurikenGO;
    float velocity = 2f;
    float initialPos;
    float passTime;
    public Shuriken()
    {
        lastSegment = new Segment();
        initialPos = 0;
        position = new Vector3();
        shurLine = new Segment();
        shurLine.SetSegment(new Vector3(0, 0, 0), new Vector3(10, 0, 0));
        isDirectionCalculated = false;
        passTime = 0f;
    }
    public Shuriken(Vector3 Position, Vector3 direction)
    {
        this.position = Position;
        initialPos = Position.x;
        passTime = 0f;
        isDirectionCalculated = false;
        shurLine = new Segment();
        shurLine.SetSegment(Position, Position + direction);
    }
    public Segment GetSegment()
    {
        return shurLine;
    }
    private void CalculatePosition()
    {
        passTime += Time.deltaTime;
        float x = 0;
        if(shurLine.dir.x>=0)
        {
            x = initialPos + velocity * passTime;
        }
        else
        {
            x = initialPos - velocity * passTime;
        }
       
        float y = 0;
        y = shurLine.Getfx(x);
        position = new Vector3(x, y, 0);
        shurikenGO.transform.position = position;
    }
    public void SetLastSegment(Segment segment)
    {
        lastSegment = segment;
    }
    public Segment GetLastSegment()
    {
        return lastSegment;
    }
    public void MoveShuriken()
    {
        CalculatePosition();
        
        //Debug.Log("Position " + position + " Intersection: " + intersection);
       if(shurLine.dir.x>=0)
        {
           // Debug.Log("Derecha");
            if (position.x>=intersection.x)
            {
                initialPos = intersection.x;
                PingPong();
                isDirectionCalculated = false;
            }
            
        }
       else
        {
           // Debug.Log("Izquierda");
            if (position.x<=intersection.x)
            {
                initialPos = intersection.x;
                PingPong();
                isDirectionCalculated = false;
            }
           
        }

    }
    private void PingPong()
    {
      //  Debug.Log(" direction: "+shurLine.dir);
        Vector3 normal = Vector3.Reflect(shurLine.dir, MathG.GetNormal(lastSegment.dir));
       // Debug.Log("Normal "+normal);
      //Debug.Log("Segment dir " + lastSegment.dir + " normal: " + MathG.GetNormal(lastSegment.dir));
        shurLine = new Segment();
       // Debug.Log("Intersection " + intersection + " second inter " + (normal + intersection));
        shurLine.SetSegment(intersection,normal*10+intersection);
       
      //  Debug.Log(shurLine.GetSegment());
        passTime = 0;
    }
    public void SetIntersection(Vector3 inter)
    {
        intersection = inter;
        shurLine = new Segment();
        shurLine.SetSegment(position, intersection);
       // Debug.Log(shurLine.dir);
    }
    public Vector3 GetIntersection()
    {
        return intersection;
    }
    public Vector3 GetPosition()
    {
        return position;
    }
    public void SetSegment(Segment segment)
    {
        shurLine = segment;
    }
}
public class ShurikenController : MonoBehaviour {
    public static Polygon poly;
    public static bool isChanging;
    [SerializeField]
    GameObject shurikenGameObject;
    Shuriken[] enemies;
    Vector3[] polySides;
	// Use this for initialization
	void Start () {
        //polySides = poly.GetVertices();

        enemies = new Shuriken[1];
        for(int i=0;i<enemies.Length;i++)
        {
            enemies[i] = new Shuriken();
            enemies[i].shurikenGO = Instantiate(shurikenGameObject);
            enemies[i].shurikenGO.transform.parent = this.transform.parent;
        }
        
    }
	
	// Update is called once per frame
	void Update () {

        CalculateObjectDirection();
	}
    void CalculateObjectDirection()
    {
        foreach (Shuriken shur in enemies)
        {
           
            if (!shur.isDirectionCalculated)
            {

                IntersectionObjectWithSide(shur);
                shur.isDirectionCalculated = true;
            }
            shur.shurikenGO.GetComponentInChildren<Transform>().transform.Rotate(0, -12 * Time.deltaTime * 20, 0);
            shur.MoveShuriken();

        }
        
    }
    void IntersectionObjectWithSide(Shuriken shur)
    {
        Segment[] polySegments;
        bool finished = false;
        Vector3 intersection = new Vector3();
        polySegments = poly.GetSegments();
      
         for(int i=0;i<polySegments.Length && !finished;i++)
            {
                intersection = MathG.IntersectionTwoSegments(shur.GetSegment(), polySegments[i]);
          
                if(intersection!=new Vector3() && shur.GetLastSegment()!=polySegments[i])
                {
                Debug.Log(intersection);
                shur.SetIntersection(intersection);
                shur.SetLastSegment(polySegments[i]);
                Instantiate(MathG.CreateSphere(intersection));
                finished = true;

                }
            }
        
    }
}
