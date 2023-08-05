using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class SwipeManager : MonoBehaviour
{
    public static SwipeManager instance { get; private set;}

    SwipeData data = new SwipeData();
    [SerializeField] int maxPointsCapacity = 10; // 1 punto cada 0.1f, 10 puntos en 1 segundo
    [SerializeField] float refreshTimer = 0.1f, timer;

    public Action<SwipeData> OnSwipe;

    private void Awake()
    {
        instance = this;
    }

    void Update()
    {
        if (Input.touchCount < 1) return; //Debug no tener dedos en pantalla

        var touch = Input.touches[0];

        if (touch.phase == TouchPhase.Began)
        {
            data.isSwipe = true;
            data.points.Add(touch.position);
            timer = 0;
            return;
        }

        if (touch.phase == TouchPhase.Ended || touch.phase == TouchPhase.Canceled)
        {
            data.isSwipe = false;
            data.points.Clear();
            timer = 0;
            return;
        }

        timer += Time.deltaTime;

        if(timer >= refreshTimer)
        {
            data.points.Add(touch.position);

            if(maxPointsCapacity < data.points.Count)
            {
                data.points.RemoveAt(0);
            }
            timer = 0;
            OnSwipe(data);
        }
    }
}
