using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class OptionsPanel : MonoBehaviour
{

    [SerializeField] Slider _volumeSlider;

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






}
