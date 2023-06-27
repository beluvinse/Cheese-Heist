using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;

public class CanvasManager : MonoBehaviour
{
    public GameObject pauseMenuUI;
    public TMP_Text heartText;
    public TMP_Text heartText2;

    public GameObject winCanvas;
    public GameObject loseCanvas;
    public GameObject tutorialCanvas;
    public GameObject generalCanvas;

    private void Start()
    {
        ShowHearts();

        Debug.Log(PlayerPrefs.GetInt("tutorial"));

        if (PlayerPrefs.GetInt("tutorial") == 1)
        {
            tutorialCanvas.SetActive(true);
            Time.timeScale = 0f;
        }
        else
        {
            generalCanvas.SetActive(true);
            Time.timeScale = 1f;
        }
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            pauseMenuUI.SetActive(true);
        }
    }

    public void RestartLevel()
    {
        if (PlayerData.Instance.GetHearts() > 0)
        {
            Time.timeScale = 1f;
            PlayerData.Instance.AddHearts(-1);
            PlayerPrefs.SetInt("currentStamina", PlayerData.Instance.GetHearts());
            SaveWithJson.Instance.SaveGame();
            SceneManager.LoadScene(SceneManager.GetActiveScene().name);
        }
       
    }
    
    
    public void NextLevel()
    {
        if (PlayerData.Instance.GetHearts() > 0)
        {
            Time.timeScale = 1f;
            PlayerData.Instance.AddHearts(-1);
            PlayerPrefs.SetInt("currentStamina", PlayerData.Instance.GetHearts());
            SaveWithJson.Instance.SaveGame();
            SceneManager.LoadScene("Level2");
        }
       
    }

    public void ShowHearts()
    {
        var hearts = PlayerData.Instance.GetHearts();
        heartText.text = "" + hearts;
        heartText2.text = "" + hearts;
    }

    private void OnEnable()
    {
        MouseController.OnLose += LoseCanvas;
        Cheese.OnCheeseCollected += WinCanvas;
    }

    private void OnDisable()
    {
        MouseController.OnLose -= LoseCanvas;
        Cheese.OnCheeseCollected -= WinCanvas;
    }

    public void WinCanvas()
    {
        winCanvas.SetActive(true);
        Time.timeScale = 0f;
    }

    public void LoseCanvas()
    {
        loseCanvas.SetActive(true);
        Time.timeScale = 0f;
    }

}
