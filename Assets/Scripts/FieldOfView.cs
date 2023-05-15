using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FieldOfView : MonoBehaviour
{

    [Header("Field of View")]
    [SerializeField] LayerMask _targetMask;
    [SerializeField] LayerMask _obstacleMask;
    [Range(0, 2)] [SerializeField] float _chaseRadius;
    float _angle = 100f;

    public bool seesEnemy;



    /*public FieldOfView(float r, LayerMask tMask, LayerMask obsMask)
    {
        _range = r;
        targetMask = tMask;
        obstructionMask = obsMask;
        _angle = 100;
    }*/

    public MouseController FieldOfViewCheck()
    {
        Collider[] RangeChecks = Physics.OverlapSphere(transform.position, _chaseRadius, _targetMask);
        if (RangeChecks.Length != 0)
        {
            Transform Target = RangeChecks[0].transform;
            Vector3 directionToTarget = (Target.position - transform.position).normalized;

            if (Vector3.Angle(transform.forward, directionToTarget) < _angle / 2)
            {
                Debug.Log("Chequeo 2");

                float distanceToTarget = Vector3.Distance(transform.position, Target.position);

                if (!Physics.Raycast(transform.position, directionToTarget, distanceToTarget, _obstacleMask))
                {
                    Debug.Log("oh un mouse");
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
        Gizmos.DrawWireSphere(transform.position, _chaseRadius);
        Vector3 viewAngleA = DirectionFromAngle(transform.eulerAngles.y, -_angle / 2);
        Vector3 viewAngleB = DirectionFromAngle(transform.eulerAngles.y, _angle / 2);
        Gizmos.color = Color.yellow;
        Gizmos.DrawLine(transform.position, transform.position + viewAngleA * _chaseRadius);
        Gizmos.DrawLine(transform.position, transform.position + viewAngleB * _chaseRadius);
        
    }

    private Vector3 DirectionFromAngle(float eulerY, float angleInDegrees)
    {
        angleInDegrees += eulerY;

        return new Vector3(Mathf.Sin(angleInDegrees * Mathf.Deg2Rad), 0, Mathf.Cos(angleInDegrees * Mathf.Deg2Rad));
    }
}
