using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MeshGenerator : MonoBehaviour {

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
    }
   public Mesh GetMesh()
    {
        return mesh_generated;
    }
    public void GenerateMesh()
    {
        //Triangulate()
    }
    private void Triangulate(Vector3 center)
    {
        for(int i=0;i<mesh_vertex.Length;i++) //Generates a triangle foreach vertex of the polygon
        {

        }
    }
    private void GetCircumcenter()
    {
        Vector3 OA = mesh_vertex[0];
        Vector3 OB = mesh_vertex[1];
        Vector3 OC = mesh_vertex[2];


    }
    
}
