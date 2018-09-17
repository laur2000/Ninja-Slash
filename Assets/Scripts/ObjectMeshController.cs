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
        SetLineForm(0);
        GenerateMesh();
    }

    void SetLineForm(int index)
    {
        HexCoor formFunction = new HexCoor(0);

        form = formFunction.GetForm();

        line.positionCount = form.Length;
        line.SetPositions(form);
       
   
    }

    void GenerateMesh()
    {
        //MeshGenerator meshGenerator = new MeshGenerator(form);
        Vector3 line1 =MathG.GetPerpendicular(form[1] - form[0], MathG.GetMiddlePoint(form[0], form[1]));
        Vector3 line2 = MathG.GetPerpendicular(form[2] - form[1], MathG.GetMiddlePoint(form[1], form[2]));
        Debug.Log(MathG.LineIntersection(line1, line2));
    }

}
