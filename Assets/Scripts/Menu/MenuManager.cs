using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuManager : MonoBehaviour
{
    public GameObject generalPanel;
    public GameObject levelsPanel;
    public GameObject shopPanel;
    public GameObject optionsPanel;
    public UIManager uiManager;

    public void LevelPanel()
    {
        generalPanel.SetActive(false);
        levelsPanel.SetActive(true);
    }
    
    public void ShopPanel()
    {
        generalPanel.SetActive(false);
        shopPanel.SetActive(true);
    }

    public void OptionsPanel()
    {
        generalPanel.SetActive(false);
        optionsPanel.SetActive(true);
    }

    public void Atras()
    {
        shopPanel.SetActive(false);
        levelsPanel.SetActive(false);
        optionsPanel.SetActive(false);
        generalPanel.SetActive(true);
    }

    public void StartGame()
    {
        if (PlayerData.Instance.GetHearts() > 0)
        {
            PlayerData.Instance.AddHearts(-1);
            uiManager.UpdateHearts();
            SaveWithJson.Instance.SaveGame();
            SceneManager.LoadScene("SampleScene");
        }
    }

    private void OnApplicationQuit()
    {
       SaveWithJson.Instance.SaveGame();
    }

    public void ExitGame()
    {
        Application.Quit();
    }
}
