using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BonusLevel : MonoBehaviour
{
    private void Start()
    {
        SwipeManager.instance.OnSwipe += CalculateSwipe;
    }

    void CalculateSwipe(SwipeData data)
    {
        float z = Camera.main.WorldToScreenPoint(transform.position).z;

        Vector3[] points = new Vector3[data.points.Count];

        for(int i = 0; i < data.points.Count; i++)
        {
            Vector3 initPos = Camera.main.ScreenToWorldPoint(new Vector3(data.points[i].x, data.points[i].y, z));

            points[i] = initPos;

            if (i == data.points.Count - 1) break;

            Vector3 finalPos = Camera.main.ScreenToWorldPoint(new Vector3(data.points[i + 1].x, data.points[i + 1].y, z));

            Vector3 dir = finalPos - initPos;

            if(Physics.Raycast(initPos, dir.normalized, dir.magnitude))
            {
                Debug.Log("me cortaron");
            }
        }
    }
}
