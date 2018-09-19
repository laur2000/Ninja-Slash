using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectMeshController : MonoBehaviour {

    LineRenderer line;
    Vector3[] form;
    private void Awake()
    {
        line = GetComponent<LineRenderer>();

    }
    private void Start()
    {
        SetLineForm(1);
        GenerateMesh();
    }

    void SetLineForm(int index)
    {
        HexCoor formFunction = new HexCoor(index);

        form = formFunction.GetForm();

        line.positionCount = form.Length;
        line.SetPositions(form);
       
   
    }

    void GenerateMesh()
    {
        MeshGenerator meshGenerator = new MeshGenerator(form);
        meshGenerator.GenerateMesh();
        GetComponent<MeshFilter>().mesh = meshGenerator.GetMesh();
    }

}
