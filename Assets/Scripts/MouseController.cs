using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MouseController : SteeringAgent
{
    [SerializeField] Controller controller;
    [SerializeField] float speed;

    [SerializeField] bool _isRooted;

    public bool isRooted { get { return _isRooted; } }

    public Animator myAnim;

    Vector3 input;

    void FixedUpdate()
    {

        if (!_isRooted) 
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
