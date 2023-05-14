using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Cat : SteeringAgent
{
    [Header("Waypoints")]
    public float waypointRadius;
    public Transform Waypoints;
    public List<Transform> _waypoints = new List<Transform>();
    private int _currentWaypoint;

    FiniteStateMachine _fsm;

    [Header("Field of View")]
    FieldOfView _fov;
    [SerializeField] LayerMask _targetMask;
    [SerializeField] LayerMask _obstacleMask;
    [Range(0, 2)] [SerializeField] float _chaseRadius;
    float _destroyRadius { get { return _chaseRadius / 2; } }

    public FieldOfView FOV { get { return _fov; } }


    private void Start()
    {
        _fsm = new FiniteStateMachine();
        _fov = GetComponent<FieldOfView>();
        _fsm.AddState(States.Patrol, new PatrolState(this));
        _fsm.AddState(States.Chase, new ChaseState(this));



        _fsm.ChangeState(States.Patrol);

        for (int i = 0; i < Waypoints.childCount; i++)
        {
            _waypoints.Add(Waypoints.GetChild(i));
        }
    }


    private void Update()
    {
        _fsm.Update();
        //ObstacleAvoidance();
    }

    public override void Move()
    {
        transform.position += _velocity * Time.deltaTime;
        transform.forward = _velocity;
    }

    public void Chase (SteeringAgent agent)
    {
        AddForce( Pursuit(agent));
    }

    public void FollowWaypoints()
    {
        AddForce(Seek(_waypoints[_currentWaypoint].position));

        if (Vector3.Distance(_waypoints[_currentWaypoint].position, transform.position) <= waypointRadius)
            _currentWaypoint++;

        if (_currentWaypoint >= _waypoints.Count)
            _currentWaypoint = 0;
    }

    public Vector3 ObstacleAvoidance()
    {
        Vector3 desired = default;
        Debug.Log("obstacle avoidance");
        if (Physics.Raycast(transform.position + transform.forward / 1.5f, _velocity, _chaseRadius, _obstacleMask))
        {
            desired = -transform.up;
            Debug.Log("if");
        }
        else if (Physics.Raycast(transform.position - transform.forward / 1.5f, _velocity, _chaseRadius, _obstacleMask))
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
        //Gizmos.DrawLine(transform.position,transform.position + transform.forward / 1.5f);
        //Gizmos.DrawLine(transform.position,transform.position - transform.forward / 1.5f);

        
    }

}

public enum States
{
    Patrol,
    Chase
}

