using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    [SerializeField]MouseController target;

    [Range(1, 5)] [SerializeField] float height;
    [Range(0, 5)] [SerializeField] float center;

    [SerializeField] Vector3 minPos, maxPos;

    float _startingPlayerHeight;

    // Start is called before the first frame update
    void Start()
    {
        _startingPlayerHeight = target.transform.position.y;
    }

    // Update is called once per frame
    void Update()
    {
        //if(!target.IsInWallHole)
            transform.position = new Vector3(Mathf.Clamp(target.transform.position.x, minPos.x, maxPos.x), 
                height + (target.transform.position.y - _startingPlayerHeight),
                Mathf.Clamp( target.transform.position.z - center, minPos.z, maxPos.z));

    }
}