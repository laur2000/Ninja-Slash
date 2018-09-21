using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MeshGenerator  {

    /*
     * This Class will be in charge of generating the mesh of the object through vertex.
    */

    private Mesh mesh_generated;                             // Mesh var that stores the generated mesh
    private Vector3[] mesh_vertex;                       // List which stores the mesh vertex
    private int[] mesh_tris;                             // List which stores the vertex index in order to generate triangles
    
    public MeshGenerator(Vector3[] vertex) //This constructor requires a list of vertex
    {
        mesh_vertex = vertex;
        mesh_tris = new int[vertex.Length * 3];
        mesh_generated = new Mesh();
        mesh_generated.name = "Mesh";
    }
   public Mesh GetMesh()
    {
        return mesh_generated;
    }
    public void GenerateMesh()
    {
        AddCenter(GetCenter(mesh_vertex));
        Triangulate();
        mesh_generated.RecalculateNormals();
        mesh_generated.RecalculateTangents();
        
    }

    private void AddCenter(Vector3 center) //Given a vertex center, It will be added to the last position of the vertices Array
    {
        Vector3[] vertices = new Vector3[mesh_vertex.Length + 1];
        int i = 0;
       for(;i<mesh_vertex.Length;i++)
        {
            vertices[i] = mesh_vertex[i];
        }
        vertices[i] = center;
        mesh_vertex = new Vector3[vertices.Length];
        mesh_vertex = vertices;
        mesh_generated.vertices = mesh_vertex;
    }
    private Vector3 GetCenter(Vector3[] vertices) // Get the center of a form given a number of vertices
    {
        float vertex1x=0, vertex2x=0;
        float vertex1y=0, vertex2y=0;
        float maxLenghtx=0, maxLenghty = 0;
        for (int y=0;y<vertices.Length;y++)
        {
            for (int x = 0; x < vertices.Length; x++)
            {
                //Debug.Log("v1x: " + vertices[y].x + " v2x: " + vertices[x].x);
                //Debug.Log(Mathf.Abs(vertices[y].x - vertices[x].x));
                if(Mathf.Abs(vertices[y].x-vertices[x].x)>maxLenghtx) //Calculates the furthesh horizontal distance between two vertices
                {
                    maxLenghtx = Mathf.Abs(vertices[y].x - vertices[x].x);
                    vertex1x = vertices[y].x;
                    vertex2x = vertices[x].x;
                }
                //Debug.Log("v1y: " + vertices[y].y + " v2y: " + vertices[x].y);
               // Debug.Log(Mathf.Abs(vertices[y].y - vertices[x].y));
                if (Mathf.Abs(vertices[y].y - vertices[x].y) > maxLenghty)//Calculates the furthesh vertical distance between two vertices
                {
                    maxLenghty = Mathf.Abs(vertices[y].y - vertices[x].y);
                    vertex1y = vertices[y].y;
                    vertex2y = vertices[x].y;
                }
            }
        }
        Vector3 middley, middlex;
        middley = MathG.GetMiddlePoint(new Vector3(vertex1y, 0), new Vector3(vertex2y, 0));
        middlex = MathG.GetMiddlePoint(new Vector3(vertex1x, 0, 0), new Vector3(vertex2x, 0, 0));

        Vector3 center = middlex + middley; //The center vertex is the result of the Middle Point of the four vertices
        return center;
    }
    private void Triangulate()
    {
        for (int i = 0,j = 0; j < mesh_vertex.Length*3-3; i ++,j+=3) //Generates a triangle foreach vertex of the polygon
        {
            mesh_tris[j] = i ;
           // Debug.Log("Triangle " + j + " is vertex " + (i + 1));
            mesh_tris[j+1] = i+1;
            //Debug.Log("Triangle " + (j+1) + " is vertex " + (i));
            mesh_tris[j+2] = mesh_vertex.Length-1;
           // Debug.Log("Triangle " + (j+2) + " is vertex " + (mesh_vertex.Length-1));
        }
        mesh_generated.triangles = mesh_tris;
    }
   
    
}
