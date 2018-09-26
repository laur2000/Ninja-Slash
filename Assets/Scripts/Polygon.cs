using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Polygon {

    Vector3[] vertices;
    int[] indices;
    Mesh mesh;
    Segment[] segments;
    
    public float area;

    public Polygon(Vector3[] PolygonVertices)
    {
        vertices = PolygonVertices;
        segments = new Segment[vertices.Length];
        CalculateSegments(vertices);
    }
    private void CalculateSegments(Vector3[] verts)
    {
        segments[segments.Length - 1] = new Segment();
        segments[segments.Length - 1].SetSegment(verts[verts.Length - 1], verts[0]);
        for(int i=0;i<segments.Length-1;i++)
        {
            segments[i] = new Segment();
                segments[i].SetSegment(verts[i], verts[i + 1]);
        }
    }
    public Segment[] GetSegments()
    {
        return segments;
    }
    public void SetVertices(Vector3[] PolygonVertices)
    {
        vertices = PolygonVertices;
    }
    public void SetMesh(Mesh PolygonMesh)
    {
        mesh = PolygonMesh;
    }
    public Vector3[] GetVertices()
    {
        return vertices;
    }
    public Mesh GetMesh()
    {
        return mesh;
    }

   
    
    public void SetIndices(int[] ind)
    {
        indices = ind;
    }
    public int[] GetIndices()
    {
        return indices;
    }
}
