using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FieldOfView
{
    public bool seesEnemy;

    private Transform _agent;
    private float _range;
    private float _angle;
    private LayerMask _targetMask;
    private LayerMask _obstructionMask;


    public FieldOfView(Transform agent, float r, float angle, LayerMask tMask, LayerMask obsMask)
    {
        _agent = agent;
        _range = r;
        _targetMask = tMask;
        _obstructionMask = obsMask;
        _angle = angle;
    }

    public MouseController FieldOfViewCheck()
    {
        Collider[] RangeChecks = Physics.OverlapSphere(_agent.transform.position, _range, _targetMask);
        if (RangeChecks.Length != 0)
        {
            Transform Target = RangeChecks[0].transform;
            Vector3 directionToTarget = (Target.position - _agent.transform.position).normalized;

            if (Vector3.Angle(_agent.transform.forward, directionToTarget) < _angle / 2)
            {

                float distanceToTarget = Vector3.Distance(_agent.transform.position, Target.position);

                if (!Physics.Raycast(_agent.transform.position, directionToTarget, distanceToTarget, _obstructionMask))
                {
                    seesEnemy = true;
                    return Target.GetComponent<MouseController>();
                }

                else
                    seesEnemy = false; return null;
            }
            else
                seesEnemy = false; return null;
        }
        else if (seesEnemy)
            seesEnemy = false; return null;

    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.gray;
        Gizmos.DrawWireSphere(_agent.transform.position, _range);
        Vector3 viewAngleA = DirectionFromAngle(_agent.transform.eulerAngles.y, -_angle / 2);
        Vector3 viewAngleB = DirectionFromAngle(_agent.transform.eulerAngles.y, _angle / 2);
        Gizmos.color = Color.red;
        Gizmos.DrawLine(_agent.transform.position, _agent.transform.position + viewAngleA * _range);
        Gizmos.DrawLine(_agent.transform.position, _agent.transform.position + viewAngleB * _range);

        
        
    }

    private Vector3 DirectionFromAngle(float eulerY, float angleInDegrees)
    {
        angleInDegrees += eulerY;

        return new Vector3(Mathf.Sin(angleInDegrees * Mathf.Deg2Rad), 0, Mathf.Cos(angleInDegrees * Mathf.Deg2Rad));
    }
}
