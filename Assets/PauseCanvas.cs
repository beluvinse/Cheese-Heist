using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PauseCanvas : MonoBehaviour
{
    public GameObject pauseMenuUI;
    public GameObject pauseButton;

    public void BackToMenu()
    {
        SceneManager.LoadScene("Menu");
    }

    public void BackToGame()
    {
        pauseMenuUI.SetActive(false);
        pauseButton.SetActive(true);
    }

    public void Pause()
    {
        pauseMenuUI.SetActive(true);
        pauseButton.SetActive(false);
    }

}
