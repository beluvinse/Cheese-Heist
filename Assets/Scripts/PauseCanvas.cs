using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;

public class PauseCanvas : MonoBehaviour
{
    public GameObject pauseMenuUI;
    public GameObject pauseButton;

    public TMP_Text cheetosText;

    private void Awake()
    {
        UpdateCheetos();
    }

    public void BackToMenu()
    {
        SaveWithJson.Instance.SaveGame();
        Time.timeScale = 1f;
        SceneManager.LoadScene("Menu");
    }

    public void BackToGame()
    {
        pauseMenuUI.SetActive(false);
        pauseButton.SetActive(true);
        Time.timeScale = 1f;
    }

    public void Pause()
    {
        pauseMenuUI.SetActive(true);
        pauseButton.SetActive(false);
        Time.timeScale = 0f;
    }

    public void UpdateCheetos()
    {
        var _cheetos = PlayerData.Instance.GetCheetos();
        cheetosText.text = "" + _cheetos;
    }

    private void OnEnable()
    {
        Cheto.OnChetoCollected += UpdateCheetos;
    }

    private void OnDisable()
    {
        Cheto.OnChetoCollected -= UpdateCheetos;
    }


}
