using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Tutorial : MonoBehaviour
{
    public GameObject tutorialPanel;
    public GameObject generalCanvas;

    public GameObject tutorial;
    public GameObject tutorial2;
    public GameObject tutorial3;
    public GameObject tutorial4;

    private void Awake()
    {
        Time.timeScale = 0f;
    }

    public void WatchTutorial()
    {
        tutorial.SetActive(false);
        tutorial2.SetActive(true);
    }

    public void SkipTutorial()
    {
        tutorialPanel.SetActive(false);
        generalCanvas.SetActive(true);
        Time.timeScale = 1f;
    }

    public void Flechita1()
    {
        tutorial2.SetActive(false);
        tutorial3.SetActive(true);
    }
    
    public void Flechita2()
    {
        tutorial3.SetActive(false);
        tutorial4.SetActive(true);
    }
    
    public void Flechita3()
    {
        tutorialPanel.SetActive(false);
        generalCanvas.SetActive(true);
        Time.timeScale = 1f;
    }





}
