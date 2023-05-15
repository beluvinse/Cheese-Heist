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

    private void Start()
    {
        ShowHearts();
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
            SaveWithJson.Instance.SaveGame();
            SceneManager.LoadScene(SceneManager.GetActiveScene().name);
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
        Cat.OnMouseEaten += LoseCanvas;
        Cheese.OnCheeseCollected += WinCanvas;
    }

    private void OnDisable()
    {
        Cat.OnMouseEaten -= LoseCanvas;
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
