using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CircleGenerator : MonoBehaviour , ISquardPositionGenerator
{
    public Vector3[] GetPosition(int count)
    {
        float step = 360f / count;
        List<Vector3> result = new List<Vector3>();

        for(int i = 0; i < count; i++)
        {
            result.Add(new Vector3(Mathf.Sin(i * step), 0, Mathf.Cos(i * step)));
        }
        return result.ToArray();
    }

}
