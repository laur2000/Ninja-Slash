using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HexCoor{

    public const float outerRadius = 2f;
  
    public const float innerRadius = outerRadius * 0.866025404f;
    
    
    
    private Vector3[] corners =
    {
        //First hexagon
       
       
       
       
       
     
       

    };
    public HexCoor(int index)
    {
        switch(index)
        {
            case 0:
                {
                    corners = new Vector3[7];

                    corners[0] = new Vector3(outerRadius, 0, 0);
                    corners[1] = new Vector3(outerRadius * 0.5f, -innerRadius, 0);
                    corners[2] = new Vector3(-outerRadius * 0.5f, -innerRadius, 0);
                    corners[3] = new Vector3(-outerRadius, 0, 0);
                    corners[4] = new Vector3(-outerRadius * 0.5f, innerRadius, 0);
                    corners[5] = new Vector3(outerRadius * 0.5f, innerRadius, 0);
                    corners[6] = new Vector3(outerRadius, 0, 0);
                }
                break;
        }
    }
    public Vector3[] GetForm()
    {
        return corners;
    }
   
   
}
