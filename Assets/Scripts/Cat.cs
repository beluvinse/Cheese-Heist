using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Cat : SteeringAgent
{
    [Header("Waypoints")]
    public float waypointRadius;
    public Transform Waypoints;
    public List<Transform> _waypoints = new List<Transform>();
    public LayerMask obstacleLayer;

    [Header("Chase")]
    [SerializeField] float _chaseRadius;
    [SerializeField] float _destroyRadius;

    private int _currentWaypoint;

    private void Start()
    {
        for(int i = 0; i < Waypoints.childCount; i++)
        {
            _waypoints.Add(Waypoints.GetChild(i));
        }
    }


    private void Update()
    {
        Move();
        FollowWaypoints();
        //ObstacleAvoidance();
    }

    protected override void Move()
    {
        transform.position += _velocity * Time.deltaTime;
        transform.forward = _velocity;
    }


    public void FollowWaypoints()
    {
        AddForce(Seek(_waypoints[_currentWaypoint].position));

        if (Vector3.Distance(_waypoints[_currentWaypoint].position, transform.position) <= waypointRadius)
            _currentWaypoint++;

        if (_currentWaypoint >= _waypoints.Count)
            _currentWaypoint = 0;
    }

    Vector3 ObstacleAvoidance()
    {
        Vector3 desired = default;
        Debug.Log("obstacle avoidance");
        if (Physics.Raycast(transform.position + transform.up / 1.5f, _velocity, _chaseRadius, obstacleLayer))
        {
            desired = -transform.up;
            Debug.Log("if");
        }
        else if (Physics.Raycast(transform.position - transform.up / 1.5f, _velocity, _chaseRadius, obstacleLayer))
        {
            desired = transform.up;
            Debug.Log("else if");
        }
        else return desired;

        return CalculateSteering(desired.normalized * _maxSpeed);
    }

    void OnDrawGizmos()
    {
        Gizmos.color = Color.green;
        //Gizmos.DrawWireSphere(_waypoints[_currentWaypoint].position, waypointRadius);

        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(transform.position, _chaseRadius);

        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, _destroyRadius);

        Vector3 origin1 = transform.position + transform.up / 1.5f;
        Vector3 origin2 = transform.position - transform.up / 1.5f;
        Gizmos.DrawLine(origin1, origin1 + transform.right * _chaseRadius);
        Gizmos.DrawLine(origin2, origin2 + transform.right * _chaseRadius);
    }

}

public enum States
{
    Patrol,
    Chase
}

