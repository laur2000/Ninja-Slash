using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectMeshController : MonoBehaviour {

    LineRenderer line;
    Vector3[] form;
    Segment segmentPlayer=new Segment();
    
    bool firstIntersection = true;
    private void Awake()
    {
        line = GetComponent<LineRenderer>();

    }
    private void Start()
    {
        SetLineForm(0);
        //GenerateMesh();
        cam = Camera.main;
    }
    private void Update()
    {
        /* This function is one-solution checking if one segment contains the cursor point
        if (firstIntersection)
        {
            firstSegment = DetectIntersection(GetMousePositionToWorld(), form);
            if (firstSegment.GetSegment() != new Vector3() && secondSegment.vertex1 != firstSegment.vertex1 && secondSegment.vertex1 != firstSegment.vertex2 && secondSegment.vertex2 != firstSegment.vertex1 && secondSegment.vertex2 != firstSegment.vertex2)
            {
                Debug.Log("Intersection one");
                firstIntersection = false;
            }
        }
        else
        {
            secondSegment = DetectIntersection(GetMousePositionToWorld(), form);
            if(secondSegment.GetSegment() !=new Vector3() && secondSegment.vertex1!=firstSegment.vertex1 && secondSegment.vertex1 != firstSegment.vertex2 && secondSegment.vertex2 != firstSegment.vertex1 && secondSegment.vertex2 != firstSegment.vertex2)
            {
                Debug.Log("Intersection two");
                firstIntersection = true;
            }
        }
        */

        OnPlayerMouseDown();
     
       
    }
    private Camera cam;
    Vector3 vertexButtonOne = new Vector3(), vertexButtonTwo = new Vector3();
   
    private void OnPlayerMouseDown()
    {
       
        if (Input.GetMouseButtonDown(0))
        {
           
           
           
            segmentPlayer.isSelected = false;
            vertexButtonOne = GetMousePositionToWorld();
            
        }
        if(Input.GetMouseButton(0) && !segmentPlayer.isSelected)
        {
            vertexButtonTwo = GetMousePositionToWorld();
            // Debug.Log(vertex1 + " " + vertex2);
           
                segmentPlayer.SetSegment(vertexButtonTwo, vertexButtonOne);
            /* Cast ray line
            for(float x=-3;x<3;x+=0.1f)
            {
                GameObject go = GameObject.CreatePrimitive(PrimitiveType.Sphere);
                go.transform.localScale = new Vector3(0.1f, 0.1f, 0.1f);
                go.transform.position = new Vector3(x,segmentPlayer.Getfx(x),0);

            }
            */
            //Debug.Log(segmentPlayer.GetSegment());

            DetectIntersection(segmentPlayer, form);
            Debug.Log(segmentPlayer.vertex1 + " " + segmentPlayer.vertex2);

        }
        if (Input.GetMouseButtonUp(0))
        {
            segmentPlayer.isSelected = false;
        }
       
    }
    private Vector3 GetMousePositionToWorld()
    {
        Vector3 point = new Vector3();

        Vector2 mousePos = new Vector2();

        // Get the mouse position from Event.
        // Note that the y position from Event is inverted.
        mousePos = Input.mousePosition;

        point = cam.ScreenToWorldPoint(new Vector3(mousePos.x, mousePos.y, 10));
        return point;
    }

    private Vector3 DetectIntersection(Segment segment, Vector3[] vertices)
    {
        //This function takes a segment of the space and goes through the Array of Vertex
        //creating segments and calculates the intersection 
        Vector3 line = new Vector3();
        Vector3 intersection = new Vector3();
        Segment side = new Segment();
        for (int i = vertices.Length-1; i > 0; i--)
        {
            
            line = MathG.GetLineEquation(vertices[i - 1]- vertices[i], vertices[i]);
            side = new Segment();
            side.SetSegment(line);
            side.index1 = i;
            side.index2 = i - 1;
           // Debug.Log(vertices[i] + " "+ vertices[i + 1]);
           intersection = MathG.IntersectionTwoSegments(segment, side);
         
            if (intersection!=new Vector3())
            {
                
                segment.index1 = side.index1;
                segment.index2 = side.index2;
               // Debug.Log("Intersection "+intersection+" with segment " + segment.index1 + " and " + segment.index2);
                GameObject go = GameObject.CreatePrimitive(PrimitiveType.Sphere);
                go.transform.localScale = new Vector3(0.1f, 0.1f, 0.1f);
                go.transform.position = intersection;
            }
        }
       
        return intersection;
    }

    void OnGUI()
    {
        Vector3 point = new Vector3();
        Event currentEvent = Event.current;
        Vector2 mousePos = new Vector2();

        // Get the mouse position from Event.
        // Note that the y position from Event is inverted.
        mousePos.x = currentEvent.mousePosition.x;
        mousePos.y = cam.pixelHeight - currentEvent.mousePosition.y;

        point = cam.ScreenToWorldPoint(new Vector3(mousePos.x, mousePos.y, 10));
        
        
        
        GUILayout.BeginArea(new Rect(20, 20, 250, 120));
        GUILayout.Label("Screen pixels: " + cam.pixelWidth + ":" + cam.pixelHeight);
        GUILayout.Label("Mouse position: " + mousePos);
        GUILayout.Label("World position: " + point.ToString("F3"));
        GUILayout.EndArea();
    }

    void SetLineForm(int index) //Gets an index and select the shape of the polygon from the class
    {
        HexCoor formFunction = new HexCoor(index);

        form = formFunction.GetForm(); //Stores the vertices of the polygon

        line.positionCount = form.Length;
        line.SetPositions(form); //Generates the outline of the polygon with the given vertices
       
   
    }

    void GenerateMesh() // Use the MeshGenerator Class in order to calculate the mesh given an array of vertices
    {
        MeshGenerator meshGenerator = new MeshGenerator(form);
        meshGenerator.GenerateMesh();
        GetComponent<MeshFilter>().mesh = meshGenerator.GetMesh();
       
    }

}
