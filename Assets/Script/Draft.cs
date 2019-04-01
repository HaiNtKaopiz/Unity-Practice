using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Draft : MonoBehaviour
{
    private int numOfPoints = 100;
    public Vector3[] MakeCircle(Vector3 center)
    {
        float angleStep = 360.0f / (float)numOfPoints;
        List<Vector3> circleList = new List<Vector3>();
        Quaternion quaternion = Quaternion.Euler(0.0f, 0.0f, angleStep);

        circleList.Add(center);
        for(int i=0; i<numOfPoints-1; i++)
        {
            circleList.Add(quaternion * circleList[circleList.Count - 1]);
        }
        return circleList.ToArray();
    }
}
