using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    [SerializeField]MouseController target;

    [Range(1, 5)] [SerializeField] float height;

    float _startingPlayerHeight;

    // Start is called before the first frame update
    void Start()
    {
        _startingPlayerHeight = target.transform.position.y;
    }

    // Update is called once per frame
    void Update()
    {
        transform.position = new Vector3(target.transform.position.x, height + (target.transform.position.y - _startingPlayerHeight), target.transform.position.z);
    }
}