using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class OptionsPanel : MonoBehaviour
{

    [SerializeField] Slider _volumeSlider;
    [SerializeField] Toggle _toggle;
    [SerializeField] GameObject _deletePanel;

    private void Start()
    {
        if (!PlayerPrefs.HasKey("volume"))
        {
            PlayerPrefs.SetFloat("volume", 1);
            LoadVolume();
        }
        else
            LoadVolume();
    }

    private void Awake()
    {
        if(PlayerPrefs.GetInt("tutorial") == 1)
        {
            _toggle.isOn = true;
        }
        else
            _toggle.isOn = false;
    }


    public void Load()
    {
        SaveWithJson.Instance.LoadGame();
    }

    public void Save()
    {
        SaveWithJson.Instance.SaveGame();
    }

    public void Delete()
    {
        SaveWithJson.Instance.Delete();
        _deletePanel.SetActive(false);
    }

    public void DeletePanelOn()
    {
        _deletePanel.SetActive(true);
    }
    
    public void DeletePanelOff()
    {
        _deletePanel.SetActive(false);
    }

    public void ChangeVolume()
    {
        AudioListener.volume = _volumeSlider.value;
        SaveVolume();
    }

    private void LoadVolume()
    {
        _volumeSlider.value = PlayerPrefs.GetFloat("volume");
    }


    private void SaveVolume()
    {
        PlayerPrefs.SetFloat("volume", _volumeSlider.value);
    }

    public void ToggleInteraction()
    {
        PlayerPrefs.SetInt("tutorial", _toggle.isOn ? 1 : 0);
    }




}
