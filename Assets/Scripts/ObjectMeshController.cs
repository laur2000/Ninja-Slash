using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectMeshController : MonoBehaviour {
    Polygon form;
    LineRenderer line;
    
    Segment segmentPlayer=new Segment();
    [SerializeField]
    Transform trail;
    public Polygon GetPoly()
    {
        return form;
    }
    private void Awake()
    {
        line = GetComponent<LineRenderer>();

    }
    private void Start()
    {
       
        HexCoor formFunction = new HexCoor(2);
        trail.gameObject.SetActive(false);
        form = new Polygon(formFunction.GetForm());//Stores the vertices of the polygon
        GetComponent<MeshFilter>().mesh = GenerateMesh(form).GetMesh();
        SetLineForm(form);//Passes the form with the vertices to the Line Renderer
        ShurikenController.poly = form;
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
        if (Input.GetMouseButtonUp(0))
        {
            trail.gameObject.SetActive(false);
            
           
        }
        if (Input.GetMouseButtonDown(0))
        {
            segmentPlayer = new Segment();
            isFinished = false;
            vertexButtonOne = GetMousePositionToWorld();
            vertexButtonTwo = vertexButtonOne;
            trail.transform.position = vertexButtonOne;
            trail.gameObject.SetActive(true);
            
        }
        
        if(Input.GetMouseButton(0))
        {
            vertexButtonTwo = GetMousePositionToWorld();
            trail.transform.position = vertexButtonTwo - new Vector3(0, 0, +0.1f);
            if (!isFinished)
            {
                
                segmentPlayer.SetSegment(vertexButtonOne,vertexButtonTwo );

                
                Vector3 intersection = DetectIntersection(segmentPlayer, form.GetVertices());
                if (intersection != new Vector3() && (Mathf.Infinity!=segmentPlayer.Getfx(intersection.x) && Mathf.NegativeInfinity != segmentPlayer.Getfx(intersection.x)))
                {
                    if(!segmentPlayer.isSelected)
                    {

                        vertexButtonOne = intersection;
                        segmentPlayer.SetVertexOne(intersection);
                    }
                    else
                    {
                        
                        segmentPlayer.SetSegment(segmentPlayer.vertex1, segmentPlayer.vertex2);
                        segmentPlayer.SetVertexTwo(intersection);

                    }                    
                    // Debug.Log(intersection + " with segments " + segmentPlayer.index1 + " and " + segmentPlayer.index2);
                    // This is the representation of the player segment, use of Debug purpose
                    // Debug.Log("The player segment has touched the segment at " + intersection + " of the " + segmentPlayer.index1 + " " + segmentPlayer.index2 + " segment");                

                    segmentPlayer.isSelected = !segmentPlayer.isSelected; //Mark this segment as selected to exit the loop
                    if (!segmentPlayer.isSelected)                    {
                        /*
                         * This draw the line of the segment cut. Use for debug purpose
                        for (float x = -3; x < 3; x += 0.1f)
                        {
                            if (segmentPlayer.Getfx(x) != -Mathf.Infinity && segmentPlayer.Getfx(x) != Mathf.Infinity)
                            {


                                GameObject go = GameObject.CreatePrimitive(PrimitiveType.Sphere);
                                go.transform.localScale = new Vector3(0.1f, 0.1f, 0.1f);

                                go.transform.position = new Vector3(x, segmentPlayer.Getfx(x), 0);

                            }
                        }
                        */
                        // Debug.Log("We have cut the segment " + segmentPlayer.index1 + segmentPlayer.index2 + " and the segment " + segmentPlayer.index3 + segmentPlayer.index4);


                        if (IsInsidePolygon(MathG.GetMiddlePoint(segmentPlayer.vec1, segmentPlayer.vec2), form)) //Checks if the segment cuts the figure from inside or outside
                        {
                            List<Vector3[]> v = new List<Vector3[]>();
                            v = RecalculateVerticesWithSegment(segmentPlayer, form.GetVertices());
                            Polygon poly1 = new Polygon(v[0]);
                            Polygon poly2 = new Polygon(v[1]);

                            GenerateFallingObject(poly1, poly2);
                        }
                        isFinished = true;

                    }


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

    
    private List<Vector3[]> RecalculateVerticesWithSegment(Segment segment, Vector3[] vertices)
    {
        int longitud = 0;
        List<Vector3[]> vertArray = new List<Vector3[]>();

        Vector3[] array1 = new Vector3[0], array2 = new Vector3[0];
        Vector3 dir = segment.dir;
       // Debug.Log(dir + " v1: " + segment.vertex1 + " v2: " + segment.vertex2);
       // Debug.Log("Vert1: " + segment.index1 + segment.index2 + " Vert2: " + segment.index3 + segment.index4);
       
            if (MathG.PointNotContainedInLine(segment.GetSegment(), vertices[0]) >= 0) //Check if the vertex[0] is at the left or at the right of the segment
            {
                if (dir.x >= 0) //Checks if the director vector is positive or negative
                {
                    for (int i = segment.index2; i <= segment.index3; i++, longitud++)
                    {

                    }
                }
                else
                {
                    for (int i = segment.index1; i >= segment.index4; i--, longitud++)
                    {

                    }
                }

                if (dir.x >= 0)
                {
                  
                 
                 //   Debug.Log(" Pendiente positiva, 0 derecha, vector positivo: ");
                    array1 = new Vector3[longitud + 2];
                    array1[0] = segment.vec1;
                    array1[array1.Length - 1] = segment.vec2;
                    for (int i = segment.index2, j = 1; j < (array1.Length - 1); i++, j++)
                    {
                        array1[j] = vertices[i];
                    }

                array2 = new Vector3[(vertices.Length - longitud) + 2];
                array2[0] = segment.vec2;
                array2[array2.Length - 1] = segment.vec1;
                int h = 1;
                for(int i=segment.index4; i<vertices.Length;)
                {
                    if (i == 0)
                    {

                        i = vertices.Length;

                    }
                    else
                    {
                        array2[h] = vertices[i];
                        i++;
                        h++;
                    }
                }
                
                for(int i=0;i<=segment.index1;)
                {
                    array2[h] = vertices[i];
                    i++;
                    h++;
                }

                }
                else
                {
                    //Debug.Log(" Pendiente positiva, 0 derecha, vector negativo: ");
                    array1 = new Vector3[longitud + 2];
                    array1[0] = segment.vec2;
                    array1[array1.Length - 1] = segment.vec1;
                    for (int i = segment.index4, j = 1; j < (array1.Length - 1); i++, j++)
                    {
                        array1[j] = vertices[i];
                    }

                array2 = new Vector3[(vertices.Length - longitud) + 2];
                array2[0] = segment.vec1;
                array2[array2.Length - 1] = segment.vec2;
                int h = 1;
                for (int i = segment.index2; i < vertices.Length;)
                {
                    if (i == 0)
                    {

                        i = vertices.Length;

                    }
                    else
                    {
                        array2[h] = vertices[i];
                        i++;
                        h++;
                    }
                }
               
                for (int i = 0; i <= segment.index3;)
                {
                    array2[h] = vertices[i];
                    i++;
                    h++;
                }
            }



            }
            else
            {

                if (dir.x < 0)
                {
                    for (int i = segment.index2; i <= segment.index3; i++, longitud++)
                    {

                    }
                }
                else
                {
                    for (int i = segment.index1; i >= segment.index4; i--, longitud++)
                    {

                    }
                }
                if (dir.x >= 0)
                {
                   // Debug.Log(" Pendiente Positiva, 0 izquierda, vector positivo: ");
                    array1 = new Vector3[longitud + 2];
                    array1[0] = segment.vec2;
                    array1[array1.Length - 1] = segment.vec1;
                    for (int i = segment.index4, j = 1; j < (array1.Length - 1); i++, j++)
                    {
                        array1[j] = vertices[i];
                    }
                array2 = new Vector3[(vertices.Length - longitud) + 2];
                array2[0] = segment.vec1;
                array2[array2.Length - 1] = segment.vec2;
                int h = 1;
                for (int i = segment.index2; i < vertices.Length;)
                {
                    if (i == 0)
                    {

                        i = vertices.Length;

                    }
                    else
                    {
                        array2[h] = vertices[i];
                        i++;
                        h++;
                    }
                }
                
                for (int i = 0; i <= segment.index3;)
                {
                    array2[h] = vertices[i];
                    i++;
                    h++;
                }
            }
                else
                {
                    //Debug.Log(" Pendiente Positiva, 0 izquierda, vector negativo: ");
                    array1 = new Vector3[longitud + 2];
                    array1[0] = segment.vec1;
                    array1[array1.Length - 1] = segment.vec2;
                    for (int i = segment.index2, j = 1; j < (array1.Length - 1); i++, j++)
                    {
                        array1[j] = vertices[i];
                    }

                array2 = new Vector3[(vertices.Length - longitud) + 2];
                array2[0] = segment.vec2;
                array2[array2.Length - 1] = segment.vec1;
                int h = 1;
                for (int i = segment.index4; i < vertices.Length;)
                {
                    if (i == 0)
                    {

                        i = vertices.Length;

                    }
                    else
                    {
                        array2[h] = vertices[i];
                        i++;
                        h++;
                    }
                }
               
                for (int i = 0; i <= segment.index1;)
                {
                    array2[h] = vertices[i];
                    i++;
                    h++;
                }



            }
            }
        
        vertArray.Add(array1);
        vertArray.Add(array2);

      

        //Debug.Log("Vertices: " + longitud);
        return vertArray;
    }
    private Vector3 DetectIntersection(Segment segment, Vector3[] vertices)
    {
        //This function takes a segment of the space and goes through the Array of Vertex
        //creating segments and calculates the intersection 
        
        Vector3 intersection = new Vector3();
        Segment side = new Segment();
        int index1 = 0, index2 = 0;
        for (int i = 0; i <vertices.Length; i++)
        {
          if(i==vertices.Length-1)//Checks if it's the last vertex form the array, and assigns to the second index the initial vertex 
            {
                index1 = i;
                index2 = 0;
            }
          else
            {
                
                index1 = i;
                index2 = i+1;
                
            }
            
                side = new Segment();
                side.index1 = index1;
                side.index2 = index2;
            //Check if the segmentPlayer is not the same as the sidePlayer
            if (((segment.index1 != side.index1 && segment.index2 != side.index1) || (segment.index1 != side.index2 && segment.index2 != side.index2))) 
            {
                side.SetSegment(vertices[index1], vertices[index2]);

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
    [SerializeField]
    GameObject fallObject;
    void SetLineForm(Polygon poly) //Gets an index and select the shape of the polygon from the class
    {
        Vector3[] vertices = poly.GetVertices();
       
        line.positionCount = vertices.Length;
        line.SetPositions(vertices); //Generates the outline of the polygon with the given vertices
        
   
    }
   void GenerateFallingObject(Polygon poly1, Polygon poly2)
    {
        
        poly1 = GenerateMesh(poly1);
        poly2 = GenerateMesh(poly2);
        
            if (poly1.area >= poly2.area)
            {
                this.GetComponent<MeshFilter>().mesh = poly2.GetMesh();
                form = poly2;
                SetLineForm(poly2);

                GameObject go = Instantiate(fallObject);
                go.GetComponent<MeshFilter>().mesh = poly1.GetMesh();
                AddForce(go, segmentPlayer.dir);
                Destroy(go, 5);

            }
            else
            {
                this.GetComponent<MeshFilter>().mesh = poly1.GetMesh();
                form = poly1;
                SetLineForm(poly1);
                GameObject go = Instantiate(fallObject);
                go.GetComponent<MeshFilter>().mesh = poly2.GetMesh();
                AddForce(go, segmentPlayer.dir);
                Destroy(go, 5);
            }

        


      

    }
    private void AddForce(GameObject go,Vector3 dir)
    {
        Rigidbody rb;
        rb = go.GetComponent<Rigidbody>();
        rb.AddForce(dir*100);
    }
    
    Polygon GenerateMesh(Polygon poly) // Use the MeshGenerator Class in order to calculate the mesh given an array of vertices
    {
        // MeshGenerator meshGenerator = new MeshGenerator(form);
        // meshGenerator.GenerateMesh();
        // quadMesh.mesh = meshGenerator.GetMesh();
        
        Vector3[] vertex = poly.GetVertices();
        Vector2[] vertices2D = new Vector2[vertex.Length];
        for(int i=0;i<vertex.Length;i++)
        {
            vertices2D[i] = new Vector2(vertex[i].x, vertex[i].y);
            
            //Debug.Log(vertices2D[i]+" "+i);
        }
        Triangulator tr = new Triangulator(vertices2D);
      
        int[] indices = tr.Triangulate();
        
        // Create the Vector3 vertices
        Vector3[] vertices = new Vector3[vertices2D.Length];
        for (int i = 0; i < vertices.Length; i++)
        {
            vertices[i] = new Vector3(vertices2D[i].x, vertices2D[i].y, 0);
            //Debug.Log(vertices[i]+" "+i);
        }
       
        
     
      
        poly.SetIndices(indices);
        poly.SetVertices(vertices);
      
        
        // Create the mesh
        Mesh msh = new Mesh();
        
            msh.vertices = vertices;
            msh.triangles = indices;
            msh.RecalculateNormals();
            msh.RecalculateBounds();
            poly.SetMesh(msh);
            poly.area = tr.Area();
        
        return poly;
    }
    bool IsInsidePolygon(Vector3 point, Polygon poly)
    {
        bool isInside = false;
        int[] indices = poly.GetIndices();
        Vector3[] vertices = poly.GetVertices();
        for (int i = 0; i < indices.Length && !isInside; i += 3)
        {
            //Debug.Log(vertices2D[indices[i]]+" "+indices[i]+" "+ vertices2D[indices[i+1]] + " " + indices[i+1] +" "+ vertices2D[indices[i+2]] + " " + indices[i+2]);
            isInside = MathG.Trilateration(vertices[indices[i]], vertices[indices[i + 1]], vertices[indices[i + 2]], point);
        }
        return isInside;
    }
}
