using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

[RequireComponent(typeof(NavMeshAgent))]
public class Cat_NavMesh : MonoBehaviour
{
    public MouseController mouse;

    [Header("Agent")]
    [SerializeField] float _speed;
    private float _baseSpeed;
    [SerializeField] float _angularSpeed;
    [SerializeField] float _acceleration;

    [Header("Waypoints")]
    public List<Transform> waypoints;
    private Transform _currentTarget;
    private int _index = 1;
    private bool _inReverse = false;
    private bool _atEnd = false;
    private bool _moving = true;

    [Header("Field of View")]
    [Range(0f, 4f)] [SerializeField] float _chaseRadius;
    [Range(0f, 4f)] [SerializeField] float _destroyRadius;
    [Range(0f,180f)][SerializeField] float _angleView;
    [SerializeField] LayerMask _targetMask;
    [SerializeField] LayerMask _obstacleMask;




    NavMeshAgent _agent;
    FiniteStateMachine _fsm;
    FieldOfView _fov;

    public FieldOfView FOV {get{return _fov;}}

    private void Start()
    {
        _agent = GetComponent<NavMeshAgent>();
        _agent.speed = _speed;
        _baseSpeed = _speed;
        _agent.angularSpeed = _angularSpeed;
        _agent.acceleration = _acceleration;

        if(waypoints.Count > 0 && waypoints[0] != null)
        {
            _currentTarget = waypoints[_index];
            _agent.SetDestination(_currentTarget.position);
        }

        _fov = new FieldOfView(this.transform, _chaseRadius, _angleView, _targetMask, _obstacleMask);

        _fsm = new FiniteStateMachine();
        _fsm.AddState(States.Patrol, new PatrolState(this));
        _fsm.AddState(States.Chase,new ChaseState(this));

        _fsm.ChangeState(States.Patrol);
    }

    private void Update()
    {
        _agent.speed = _speed;
        _agent.angularSpeed = _angularSpeed;
        _agent.acceleration = _acceleration;

        _fsm.Update();
    }

    public void WaypointSystem(bool stateChanged)
    {
        if(_currentTarget != null)
        {
            if (stateChanged)
            {
                _moving = false;
                StartCoroutine("MoveToNextWaypoint");
            }
            if((Vector3.Distance(transform.position, _currentTarget.position) <= 0.1f) && _moving)
            {
                _moving = false;
                StartCoroutine("MoveToNextWaypoint");
            }
        }
    }

    public void ChaseMouse(){
        _agent.SetDestination(mouse.transform.position);
    }

    public void BuffSpeed(float val){_speed *= val;}
    
    public void SetBaseSpeed(){_speed = _baseSpeed;}

    public static event Action OnMouseEaten;

    public void CheckDestroyDistance()
    {
        if (Vector3.Distance(transform.position, mouse.transform.position) < _destroyRadius)
        {
            Debug.Log("morfadium");
            OnMouseEaten?.Invoke();
            mouse.gameObject.SetActive(false);
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
                yield return new WaitForSeconds(UnityEngine.Random.Range(3f, 6f));

            _currentTarget = waypoints[_index];
        }
        else
        {
            if (!_atEnd)
            {
                _atEnd = true;
                yield return new WaitForSeconds(UnityEngine.Random.Range(3f, 6f));
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

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.gray;
        Gizmos.DrawWireSphere(transform.position, _chaseRadius);
        Vector3 viewAngleA = DirectionFromAngle(transform.eulerAngles.y, -_angleView / 2);
        Vector3 viewAngleB = DirectionFromAngle(transform.eulerAngles.y, _angleView / 2);
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, _destroyRadius);
        Gizmos.DrawLine(transform.position, transform.position + viewAngleA * _chaseRadius);
        Gizmos.DrawLine(transform.position, transform.position + viewAngleB * _chaseRadius);

        
        
    }

    private Vector3 DirectionFromAngle(float eulerY, float angleInDegrees)
    {
        angleInDegrees += eulerY;

        return new Vector3(Mathf.Sin(angleInDegrees * Mathf.Deg2Rad), 0, Mathf.Cos(angleInDegrees * Mathf.Deg2Rad));
    }
}



public enum States
{
    Patrol,
    Chase,
    MouseTrapped
}