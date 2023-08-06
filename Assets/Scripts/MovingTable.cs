using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovingTable : MonoBehaviour
{
    [SerializeField] Transform _pointA;
    [SerializeField] Transform _pointB;
    [SerializeField] float _speed;


    public  Transform _targetPoint; 

    private void Start()
    {
        _targetPoint = _pointA;
        transform.position = _targetPoint.position;
    }

    private void Update()
    {
        

        if (Vector3.Distance(transform.position, _targetPoint.position) != 0f)
        {
            transform.position = Vector3.MoveTowards(transform.position, _targetPoint.position, _speed * Time.deltaTime);
            Debug.Log("a");
            //_targetPoint = (_targetPoint == _pointA) ? _pointB : _pointA;
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            _targetPoint = (transform.position == _pointA.position) ? _pointB : _pointA;
            other.transform.parent = transform;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            other.transform.parent = null;
        }
    }

}
