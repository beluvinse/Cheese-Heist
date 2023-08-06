using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpeedBoost 
{
    float _speedMultiplier;
    float _duration;

    GameObject _particles;
    MouseController _mouse;

    public SpeedBoost(float speedMulti, float duration, GameObject particles, MouseController mouse)
    {
        _speedMultiplier = speedMulti;
        _duration = duration;
        _particles = particles;
        _mouse = mouse;
    }


    public IEnumerator Boost()
    {
        var previousSpeed = _mouse.speed;
        _mouse.speed = previousSpeed * _speedMultiplier;
        _mouse.ChangeMaxSpeed(_mouse.speed);
        _particles.SetActive(true);
        yield return new WaitForSeconds(_duration);
        _particles.SetActive(false);
        _mouse.speed = previousSpeed;
        _mouse.ChangeMaxSpeed(1.5f);

    }

}
