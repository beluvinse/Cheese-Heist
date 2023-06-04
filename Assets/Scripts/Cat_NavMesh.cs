using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

[RequireComponent(typeof(NavMeshAgent))]
public class Cat_NavMesh : MonoBehaviour
{
    [Header("Agent")]
    [SerializeField] float _speed;
    [SerializeField] float _angularSpeed;
    [SerializeField] float _acceleration;

    [Header("Waypoints")]
    public List<Transform> waypoints;
    private Transform _currentTarget;
    private int _index = 1;
    private bool _inReverse = false;
    private bool _atEnd = false;
    private bool _moving = true;




    NavMeshAgent _agent;
    FiniteStateMachine _fsm;
    FieldOfView _fov;

    private void Start()
    {
        _agent = GetComponent<NavMeshAgent>();
        _agent.speed = _speed;
        _agent.angularSpeed = _angularSpeed;
        _agent.acceleration = _acceleration;

        if(waypoints.Count > 0 && waypoints[0] != null)
        {
            _currentTarget = waypoints[_index];
            _agent.SetDestination(_currentTarget.position);
        }

        _fsm = new FiniteStateMachine();
        _fsm.AddState(States.Patrol, new PatrolState(this));

        _fsm.ChangeState(States.Patrol);
    }

    private void Update()
    {
        _agent.speed = _speed;
        _agent.angularSpeed = _angularSpeed;
        _agent.acceleration = _acceleration;

        _fsm.Update();
    }

    public void WaypointSystem()
    {
        if(_currentTarget != null)
        {
            Debug.Log("a");
            if((Vector3.Distance(transform.position, _currentTarget.position) <= 0.1f) && _moving)
            {
                Debug.Log("aaa");
                _moving = false;
                StartCoroutine("MoveToNextWaypoint");
            }
        }
    }


    IEnumerator MoveToNextWaypoint()
    {
        Debug.Log("aa");
        if (!_inReverse)
        {
            _index++;
        }

        if(_index < waypoints.Count && !_inReverse)
        {
            if (_index == 1)
                yield return new WaitForSeconds(Random.Range(1f, 2f));

            _currentTarget = waypoints[_index];
        }
        else
        {
            if (!_atEnd)
            {
                _atEnd = true;
                yield return new WaitForSeconds(Random.Range(1f, 2f));
            }

            _index--;
            _inReverse = true;


            if(_index == 0)
            {
                _inReverse = false;
                _atEnd = false;
            }

            _currentTarget = waypoints[_index];
        }

        _agent.SetDestination(_currentTarget.position);
        _moving = true;
    }
}

public enum States
{
    Patrol,
    Chase,
    MouseTrapped
}