using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MouseController : SteeringAgent
{
    [SerializeField] Controller controller;
    [SerializeField] float _speed;

    [SerializeField] bool _isRooted;
    [SerializeField] bool _isInWallHole;

    [Header("SpeedBoost")]
    [SerializeField] GameObject _particles;
    [SerializeField] float _speedBoostMulti;
    [SerializeField] float _speedBoostDuration;

    [Header("Decoy")]
    [SerializeField] GameObject _decoy;
    [SerializeField] float _decoyDuration;
    [SerializeField] Transform _decoySpawn;

    [SerializeField] GameObject _inmune;

    private SpeedBoost _speedBoost;
    private DecoyMouse _decoyMouse;

    public bool IsRooted { get { return _isRooted; } }
    public bool IsInWallHole { get { return _isInWallHole; } }
    public float speed { 
        get { return _speed; }
        set { _speed = value; } 
    }


    public Animator myAnim;

    public static event Action OnLose;

    Vector3 input;

    public void OnDeath() { 
        OnLose?.Invoke();
        AudioManager.Instance.PlayMouseSound();
        gameObject.SetActive(false);

    }

    public IEnumerator Revive()
    {
        input = Vector3.zero;
        gameObject.SetActive(true);
        gameObject.layer = LayerMask.NameToLayer("Inmune");
        _inmune.SetActive(true);
        yield return new WaitForSeconds(3);
        _inmune.SetActive(false);
        gameObject.layer = LayerMask.NameToLayer("Mouse");

    }

    void FixedUpdate()
    {

        if (!_isRooted && !_isInWallHole) 
        {
            input = controller.GetMovementInput();
            if (input != Vector3.zero)
            {
                AddForce(input * _speed);
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
        _speedBoost = new SpeedBoost(_speedBoostMulti, _speedBoostDuration, _particles, this);
        _decoyMouse = new DecoyMouse(_decoyDuration, _decoy, this, _decoySpawn);
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


    public void SpeedBoost()
    {
        StartCoroutine(_speedBoost.Boost());
    }

    public void SpawnDecoy()
    {
        StartCoroutine(_decoyMouse.PlantDecoy());
    }
}
