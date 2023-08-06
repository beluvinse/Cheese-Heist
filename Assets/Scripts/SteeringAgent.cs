using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SteeringAgent : MonoBehaviour
{
    protected Vector3 _velocity;

    [Header("Stearing Agent Stats")]
    [SerializeField]
    protected float _maxSpeed;
    [Range(0, 0.1f)]
    [SerializeField]
    protected float _maxForce;

    public virtual void Move()
    {
        transform.position += _velocity * Time.deltaTime;
        transform.right = _velocity;
    }

    protected void AddForce(Vector3 force)
    {
        _velocity = Vector3.ClampMagnitude(_velocity + force, _maxSpeed);
    }

    protected Vector3 Pursuit(SteeringAgent agent)
    {
        //Pos + Velocity * Time
        //Manera 1 multiplicar todo esto x Time.DeltaTime
        Vector3 futurePos = agent.transform.position + agent._velocity;

        //Manera 2 cuando estoy cerca hacer seek directamente (Manejar el tema de la distancia manual)
        if (Vector3.Distance(transform.position, futurePos) < (agent._velocity.magnitude))
        {
            Debug.DrawLine(transform.position, agent.transform.position, Color.green);
            return Seek(agent.transform.position);
        }
        Debug.DrawLine(transform.position, futurePos, Color.red);

        //Manera 3 TODO

        return Seek(futurePos);
    }

    protected Vector3 CalculateSteering(Vector3 desired)
    {
        return Vector3.ClampMagnitude(desired - _velocity, _maxForce);
    }

    protected Vector3 Seek(Vector3 target)
    {
        return CalculateSteering((target - transform.position).normalized* _maxSpeed);
    }

    protected Vector3 Flee(Vector3 target)
    {
        return -Seek(target);
    }

    public void ChangeMaxSpeed(float val)
    {
        _maxSpeed = val;
    }
}
