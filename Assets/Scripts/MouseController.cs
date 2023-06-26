using System;
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

    public static event Action OnLose;


    Vector3 input;

    public void OnDeath() { OnLose?.Invoke(); }

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
    private Renderer _renderer;

    private void Start()
    {
        _renderer = GetComponentInChildren<SkinnedMeshRenderer>();
    }

    public void EnterWallHole(Transform inPos)
    {
        _renderer.gameObject.SetActive(false);
        _lastYPos = transform.position.y;
        transform.position = new Vector3(inPos.position.x, inPos.position.y, inPos.position.z);
        _isInWallHole = true;

    }

    public void ExitWallHole(Transform outPos)
    {
        _renderer.gameObject.SetActive(true);
        transform.position = new Vector3(outPos.position.x, outPos.position.y, outPos.position.z);
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
