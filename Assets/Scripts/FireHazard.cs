using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FireHazard : MonoBehaviour
{
    [SerializeField] float _durationTime;
    [SerializeField] float _waitingTime;

    public ParticleSystem fuegoo;

    private bool isTurnedOn = false;

    private float _count = 0;

    private void OnTriggerEnter(Collider other)
    {

        var m = other.GetComponent<MouseController>();
        if(m && isTurnedOn) {
            Debug.LogWarning("AAAAAAAAAAAAA");
            m.OnDeath(); 
        }
    }

    private void Start()
    {
        fuegoo.gameObject.SetActive(false);
    }

    void Update()
    {
        if(_count < _waitingTime)
        {
            _count += Time.deltaTime;
        }
        else
        {
            _count = 0;
            StartCoroutine(Activate());
        }
    }

   
   

    IEnumerator Activate()
    {
        var collider = GetComponent<BoxCollider>();
        fuegoo.gameObject.SetActive(true);
        collider.enabled = true;
        isTurnedOn = true;
        yield return new WaitForSeconds(_durationTime);
        collider.enabled = false;
        fuegoo.gameObject.SetActive(false);
        isTurnedOn = false;

    }
}
