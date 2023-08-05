using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class BonusLevel : MonoBehaviour
{

    [SerializeField] float _timer;
    [SerializeField] float _cuts;
    [SerializeField] GameObject _trail;
    [SerializeField] GameObject explosion;
    [SerializeField] GameObject Panel;
    bool isTimerRunning;
    public float timer;

    private void Update()
    {
        
        if(timer < _timer)
        {
            timer += Time.deltaTime;
            isTimerRunning = true;
        }
        else
        {
            isTimerRunning = false;
            GetComponentInChildren<SkinnedMeshRenderer>().enabled = false;
            explosion.SetActive(true);
            explosion.transform.GetChild(0).GetComponent<TMP_Text>().text = "Ganaste " + _cuts + " Chetos!!";
            Debug.Log("Ganaste " + _cuts + "Chetos!!");
        }
    }

    private void Start()
    {
        SwipeManager.instance.OnSwipe += CalculateSwipe;
    }

    void CalculateSwipe(SwipeData data)
    {
        if (isTimerRunning)
        {
            float z = Camera.main.WorldToScreenPoint(transform.position).z;

            Vector3[] points = new Vector3[data.points.Count];

            for (int i = 0; i < data.points.Count; i++)
            {
                Vector3 initPos = Camera.main.ScreenToWorldPoint(new Vector3(data.points[i].x, data.points[i].y, z));

                points[i] = initPos;

                if (i == data.points.Count - 1) break;

                Vector3 finalPos = Camera.main.ScreenToWorldPoint(new Vector3(data.points[i + 1].x, data.points[i + 1].y, z));

                Vector3 dir = finalPos - initPos;

                _trail.transform.position = new Vector3(Mathf.Lerp(initPos.x, finalPos.x, 0.5f), Mathf.Lerp(initPos.y, finalPos.y, 0.5f), Mathf.Lerp(initPos.z, finalPos.z, 0.5f));
                if (Physics.Raycast(initPos, dir.normalized, dir.magnitude))
                {
                    _cuts++;
                    Debug.Log(_cuts);
                    transform.localScale += new Vector3(0.1f, 0.05f, 0.1f);
                }
            }
        }
    }

   
}
