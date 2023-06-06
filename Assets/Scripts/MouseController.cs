using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MouseController : SteeringAgent
{
    [SerializeField] Controller controller;
    [SerializeField] float speed;

    [SerializeField] bool _isRooted;
    [SerializeField] bool _isInWallHole;

    public bool IsRooted { get { return _isRooted; } }
    public bool IsInWallHole { get { return _isInWallHole; } }

    public Animator myAnim;

    Vector3 input;

    void FixedUpdate()
    {

        if (!_isRooted && !_isInWallHole) 
        {
            input = controller.GetMovementInput();
            //transform.position += controller.GetMovementInput() * speed * Time.deltaTime;
            if (input != Vector3.zero)
            {
                AddForce(controller.GetMovementInput() * speed);
                Move();
                myAnim.SetBool("isMoving", true);
            }
            else
            {
                myAnim.SetBool("isMoving", false);
            }
        }
        else
        {
            input = Vector3.zero;
        }
            
    }

    float _lastYPos;

    public void EnterWallHole(Transform inPos)
    {
        _lastYPos = transform.position.y;
        transform.position = new Vector3(inPos.position.x, _lastYPos, inPos.position.z);
        _isInWallHole = true;

    }

    public void ExitWallHole(Transform outPos)
    {
        transform.position = new Vector3(outPos.position.x, _lastYPos, outPos.position.z);
        _isInWallHole = false;

    }

    public void Trapped(float time)
    {
        StartCoroutine(MouseTrapped(time));
    }

    IEnumerator MouseTrapped(float time)
    {
        _isRooted = true;
        yield return new WaitForSeconds(time);
        _isRooted = false;
    }
}
