using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MeshGenerator : MonoBehaviour {

    /*
     * This Class will be in charge of generating the mesh of the object through vertex.
    */

    Mesh mesh_generated;                             // Mesh var that stores the generated mesh
    List<Vector3> mesh_vertex= new List<Vector3>();  // List which stores the mesh vertex
    List<int> mesh_tris = new List<int>();           // List which stores the vertex index in order to generate triangles

    void CalculateVertex()
    {

    }
}
