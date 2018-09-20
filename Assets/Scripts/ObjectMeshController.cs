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
        HexCoor formFunction = new HexCoor(0);

        form = formFunction.GetForm(); //Stores the vertices of the polygon

        SetLineForm(form);//Passes the form with the vertices to the Line Renderer
        //GenerateMesh();
        cam = Camera.main;
    }
    private void Update()
    {
       
        OnPlayerMouseDown();
     
       
    }
    private Camera cam;
    Vector3 vertexButtonOne = new Vector3(), vertexButtonTwo = new Vector3();
    bool isFinished = false;
    private void OnPlayerMouseDown()
    {
        
        if (Input.GetMouseButtonDown(0))
        {
            segmentPlayer = new Segment();
            isFinished = false;
            vertexButtonOne = GetMousePositionToWorld();            
        }

        if(Input.GetMouseButton(0) && !isFinished)
        {
            
                vertexButtonTwo = GetMousePositionToWorld();
                segmentPlayer.SetSegment(vertexButtonTwo, vertexButtonOne);          
          



            Vector3 intersection=DetectIntersection(segmentPlayer, form);
            if(intersection!=new Vector3())
            {
                vertexButtonOne = intersection;
                segmentPlayer.SetSegment(vertexButtonTwo, vertexButtonOne);
                // Debug.Log(intersection + " with segments " + segmentPlayer.index1 + " and " + segmentPlayer.index2);
                // This is the representation of the player segment, use of Debug purpose
                //Debug.Log("The player segment has touched the segment at " + intersection + " of the " + segmentPlayer.index1 + " " + segmentPlayer.index2 + " segment");                

                segmentPlayer.isSelected = !segmentPlayer.isSelected; //Mark this segment as selected to exit the loop
                if (!segmentPlayer.isSelected)
                {
                    
                    for (float x = -3; x < 3; x += 0.1f)
                    {
                        if (segmentPlayer.Getfx(x) != -Mathf.Infinity && segmentPlayer.Getfx(x) != Mathf.Infinity)
                        {


                            GameObject go = GameObject.CreatePrimitive(PrimitiveType.Sphere);
                            go.transform.localScale = new Vector3(0.1f, 0.1f, 0.1f);

                            go.transform.position = new Vector3(x, segmentPlayer.Getfx(x), 0);

                        }
                    }
                    Debug.Log("We have cut the segment " + segmentPlayer.index1 + segmentPlayer.index2 + " and the segment " + segmentPlayer.index3 + segmentPlayer.index4);
                    isFinished = true;
                }             
                

            }          

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
        
        Vector3 intersection = new Vector3();
        Segment side = new Segment();
        for (int i = 0; i <vertices.Length-1; i++)
        {

            
                side = new Segment();
                side.index1 = i + 1;
                side.index2 = i;
            if (((segment.index1 != side.index1 && segment.index2 != side.index1) || (segment.index1 != side.index2 && segment.index2 != side.index2))) //Check if the segmentPlayer is not the same as the sidePlayer
            {
                side.SetSegment(vertices[i + 1], vertices[i]);

                // Debug.Log(vertices[i] + " "+ vertices[i + 1]);
                intersection = MathG.IntersectionTwoSegments(segment, side);

                if (intersection != new Vector3()  )
                {
                    if (!segment.isSelected) //If it's the first intersection it assigns the index of the vertices to index 1 and 2
                    {
                        segment.SetIndex12(side.index1, side.index2);
                    }
                    else//If it's the second intersection it assigns the index of the vertices to index 3 and 4
                    {
                        segment.SetIndex34(side.index1, side.index2);
                    }
                    // Debug.Log("Intersection "+intersection+" with segment " + segment.index1 + " and " + segment.index2);
                    return intersection;
                    /* Visual demostration of the intersection points
                     GameObject go = GameObject.CreatePrimitive(PrimitiveType.Sphere);
                     go.transform.localScale = new Vector3(0.1f, 0.1f, 0.1f);
                     go.transform.position = intersection;
                     */
                }
            }
        }
       
        return new Vector3();
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

    void SetLineForm(Vector3[] vertices) //Gets an index and select the shape of the polygon from the class
    {
       

        line.positionCount = vertices.Length;
        line.SetPositions(vertices); //Generates the outline of the polygon with the given vertices
       
   
    }
    [SerializeField]
    MeshFilter quadMesh;
    void GenerateMesh() // Use the MeshGenerator Class in order to calculate the mesh given an array of vertices
    {
        MeshGenerator meshGenerator = new MeshGenerator(form);
        meshGenerator.GenerateMesh();
        quadMesh.mesh = meshGenerator.GetMesh();
       
    }

}
