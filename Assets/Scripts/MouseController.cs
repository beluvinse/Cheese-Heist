using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MouseController : SteeringAgent
{
    [SerializeField] Controller controller;
    [SerializeField] float speed;

    [SerializeField] bool _isRooted;

    public bool isRooted { get { return _isRooted; } }

    Vector3 input;

    public Animator anim;

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
                anim.SetBool("isMoving", true);
            }
            else
            {
                Debug.Log("esta quieto");
                anim.SetBool("isMoving", false);
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
