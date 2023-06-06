using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Cat : SteeringAgent
{
    public MouseController mouse;

    [Header("Waypoints")]
    public float waypointRadius;
    public Transform Waypoints;
    public List<Transform> _waypoints = new List<Transform>();
    private int _currentWaypoint;

    FiniteStateMachine _fsm;

    public static event Action OnMouseEaten;

    [Header("Field of View")]
    FieldOfView _fov;
    [SerializeField] LayerMask _targetMask;
    [SerializeField] LayerMask _obstacleMask;
    [Range(0, 2)] [SerializeField] float _chaseRadius;
    float _destroyRadius { get { return _chaseRadius / 2; } }

    public FieldOfView FOV { get { return _fov; } }


    private float _baseSpeed;

    private bool _isChasing;

    private void Start()
    {
        _baseSpeed = _maxSpeed;
        _fsm = new FiniteStateMachine();
        _fov = GetComponent<FieldOfView>();
        //_fsm.AddState(States.Patrol, new PatrolState(this));
        //_fsm.AddState(States.Chase, new ChaseState(this));
        _fsm.AddState(States.MouseTrapped, new TrapActiveState(this));



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
        _isChasing = _fov.seesEnemy;
    }

    public void BuffSpeed(float val)
    {
        _maxSpeed *= val;
    }

    public void BaseSpeed()
    {
        _maxSpeed = _baseSpeed;
    }

    public override void Move()
    {
        transform.position += _velocity * Time.deltaTime;
        transform.forward = _velocity;
    }

    public void Chase (SteeringAgent agent)
    {
        AddForce(Pursuit(agent));
    }

    public void FollowWaypoints()
    {
        AddForce(Seek(_waypoints[_currentWaypoint].position));

        if (Vector3.Distance(_waypoints[_currentWaypoint].position, transform.position) <= waypointRadius)
            _currentWaypoint++;

        if (_currentWaypoint >= _waypoints.Count)
            _currentWaypoint = 0;
    }

    public void CheckDestroyDistance()
    {
        if (Vector3.Distance(transform.position, mouse.transform.position) < _destroyRadius)
        {
            Debug.Log("morfadium");
            OnMouseEaten?.Invoke();
            mouse.gameObject.SetActive(false);
        }
    }

    public void ObstacleAvoidance()
    {
        Vector3 desired = default;
        Debug.Log("obstacle avoidance");
        if (Physics.Raycast(transform.position - transform.right * 0.25f, viewAngleA, _chaseRadius * 0.75f, _obstacleMask))
        {
            desired = transform.right;
            Debug.Log("izq");
        }
        else if (Physics.Raycast(transform.position + transform.right * 0.25f, viewAngleB, _chaseRadius * 0.75f, _obstacleMask))
        {
            desired = -transform.right;
            Debug.Log("der");
        }


        AddForce(desired.normalized * _maxSpeed);
    }

    Vector3 viewAngleA;
    Vector3 viewAngleB;

    void OnDrawGizmos()
    {
        Gizmos.color = Color.green;

        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(transform.position, _chaseRadius);

        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, _destroyRadius);

        viewAngleA = DirectionFromAngle(transform.eulerAngles.y, -10f / 2);
        viewAngleB = DirectionFromAngle(transform.eulerAngles.y, 10f / 2);
        Gizmos.color = Color.yellow;
        Gizmos.DrawLine(transform.position - transform.right * 0.25f, transform.position - transform.right * 0.25f + viewAngleA * _chaseRadius * 0.75f);
        Gizmos.DrawLine(transform.position + transform.right * 0.25f, transform.position + transform.right * 0.25f + viewAngleB * _chaseRadius * 0.75f);
    }



    private Vector3 DirectionFromAngle(float eulerY, float angleInDegrees)
    {
        angleInDegrees += eulerY;

        return new Vector3(Mathf.Sin(angleInDegrees * Mathf.Deg2Rad), 0, Mathf.Cos(angleInDegrees * Mathf.Deg2Rad));
    }

}



