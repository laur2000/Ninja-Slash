using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HexCoor{

    public const float outerRadius = 2f;
  
    public const float innerRadius = outerRadius * 0.866025404f;



    private Vector3[] corners;
    
    public HexCoor(int index)
    {
        switch(index)
        {
            case 0:
                {
                    corners = new Vector3[7];

                    corners[0] = new Vector3(outerRadius*2, 0, 0);
                    corners[1] = new Vector3(outerRadius * 0.5f, -innerRadius, 0);
                    corners[2] = new Vector3(-outerRadius * 0.5f, -innerRadius, 0);
                    corners[3] = new Vector3(-outerRadius, 0, 0);
                    corners[4] = new Vector3(-outerRadius * 0.5f, innerRadius, 0);
                    corners[5] = new Vector3(outerRadius * 0.5f, innerRadius, 0);
                    corners[6] = new Vector3(outerRadius*2, 0, 0);
                }
                break;
            case 1:
                {
                    corners = new Vector3[7];

                    corners[0] = new Vector3(1 * 2, 0, 0);
                    corners[1] = new Vector3(2 * 0.5f, -2, 0);
                    corners[2] = new Vector3(-5 * 0.5f, -6, 0);
                    corners[3] = new Vector3(-outerRadius, 4, 0);
                    corners[4] = new Vector3(-2 * 0.5f, innerRadius, 0);
                    corners[5] = new Vector3(3 * 0.5f, 8, 0);
                    corners[6] = new Vector3(1 * 2, 0, 0);
                }
                break;
        }
    }
    public Vector3[] GetForm()
    {
        return corners;
    }
   
   
}
