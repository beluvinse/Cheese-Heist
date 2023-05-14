using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Hunter : SteeringAgent
{
    [Header("Waypoints")]
    public float waypointRadius;
    [SerializeField] float patrolCost;
    public Transform[] waypoints;
    public LayerMask obstacleLayer;
    public float avoidanceWeight;

    [Header("Stats")]
    public bool FullEnergy;
    public float energy;
    [SerializeField] float maxEnergy;
    [SerializeField] float restRegen;

    [Header("Chase")]
    [SerializeField] float chaseRadius;
    [SerializeField] float destroyRadius;
    [SerializeField] float chaseCost;

    public SpriteRenderer sprite;


    private int _currentWaypoint;

    FiniteStateMachine _fsm;
    //HashSet<Boid> allBoids;


    private void Start()
    {
        energy = maxEnergy;
        //allBoids = BoidManager.instance.allBoids;

        _fsm = new FiniteStateMachine();

        //_fsm.AddState(HunterStates.Rest, new RestState(this, restRegen));
        //_fsm.AddState(HunterStates.Patrol, new PatrolState(this, patrolCost));
        //_fsm.AddState(HunterStates.Chase, new ChaseState(this, chaseCost));

        //_fsm.ChangeState(HunterStates.Patrol);
    }

    private void Update()
    {
        if (energy >= maxEnergy)
            FullEnergy = true;

        if (energy <= 0)
            FullEnergy = false;


        Vector3 obstacleForce = ObstacleAvoidance();
        //Vector3 force = obstacleForce == Vector3.zero ? CalculateSteering(transform.right * _maxSpeed) : obstacleForce;
        AddForce(obstacleForce * avoidanceWeight);
        
        Move();
        _fsm.Update();




    }

    public void FollowWaypoints()
    {
        AddForce(Seek(waypoints[_currentWaypoint].position));

        if (Vector3.Distance(waypoints[_currentWaypoint].position, transform.position) <= waypointRadius)
            _currentWaypoint++;

        if (_currentWaypoint >= waypoints.Length)
            _currentWaypoint = 0;
    }

    public void Rest()
    {
        AddForce(-_velocity);
        sprite.color = Color.yellow;
    }

    public void Chase(SteeringAgent t)
    {
        AddForce(Pursuit(t));
    }

    public SteeringAgent CheckPursuit()
    {
        /*foreach (Boid b in allBoids)
        {
            if ((Vector3.Distance(b.transform.position, transform.position)) <= chaseRadius)
            {
                return b;
            }
        }*/
        return null;
    }

    public void DestroyBoid()
    {
        /*var b = CheckPursuit().GetComponent<Boid>();
        if (Vector3.Distance(b.transform.position, transform.position) <= destroyRadius)
        {
            b.DestroyThis();
        }*/
    }

    public void EnergyDrain(float x)
    {
        if (energy > 0)
        {
            energy -= x * Time.deltaTime;

        }
        else
            energy = 0;
    }

    public void EnergyRegen(float x)
    {
        if (energy < maxEnergy)
        {
            energy += x * Time.deltaTime;

        }
        else
            energy = maxEnergy;
    }


    void OnDrawGizmos()
    {
        Gizmos.color = Color.green;
        Gizmos.DrawWireSphere(waypoints[_currentWaypoint].position, waypointRadius);

        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(transform.position, chaseRadius);

        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, destroyRadius);

        Vector3 origin1 = transform.position + transform.up / 1.5f;
        Vector3 origin2 = transform.position - transform.up / 1.5f;
        Gizmos.DrawLine(origin1, origin1 + transform.right * chaseRadius);
        Gizmos.DrawLine(origin2, origin2 + transform.right * chaseRadius);
    }

    Vector3 ObstacleAvoidance()
    {
        Vector3 desired = default;
        Debug.Log("obstacle avoidance");
        if (Physics.Raycast(transform.position + transform.up / 1.5f, _velocity, chaseRadius, obstacleLayer))
        {
            desired = -transform.up;
            Debug.Log("if");
        }
        else if (Physics.Raycast(transform.position - transform.up / 1.5f, _velocity, chaseRadius, obstacleLayer))
        {
            desired = transform.up;
            Debug.Log("else if");
        }
        else return desired;

        return CalculateSteering(desired.normalized * _maxSpeed);
    }

}

public enum HunterStates
{
    Rest,
    Patrol,
    Chase
}
