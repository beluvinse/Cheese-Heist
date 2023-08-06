using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using System;
using UnityEditor;

public class SaveWithJson : MonoBehaviour
{
    public static SaveWithJson Instance;
    string path;
    [SerializeField] SaveData saveData = new SaveData();
    [SerializeField] int _heartsDefault;
    [SerializeField] int _cheetosDefault;

    public static event Action OnDeletedFile;

    void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }

        CreatePath();

        Debug.LogWarning(PlayerData.Instance.GetHearts()) ;
    }

    void CreatePath()
    {
        path = Application.persistentDataPath + "/data.json";
        //path = path.Replace("/", "\\");
        
        if(!File.Exists(path))
        {
            Debug.Log("path no existe, se crea");
            saveData.lives = _heartsDefault;
            saveData.cheetos = _cheetosDefault;
            string json = JsonUtility.ToJson(saveData, true);
            File.WriteAllText(path, json);
            PlayerData.Instance.SetHearts(saveData.lives);
            PlayerData.Instance.SetCheetos(saveData.cheetos);
            PlayerPrefs.SetInt("currentStamina", saveData.lives);
            OnDeletedFile?.Invoke();
        }
    }


    public void SetHearts(int val)
    {
        saveData.lives = val;
    }

    public void SetCheetos(int val)
    {
        saveData.cheetos = val;
    }

    public int GetHearts()
    {
        LoadGame();
        return saveData.lives;
    }
    
    public int GetPotion()
    {
        LoadGame();
        return saveData.potion;
    }

    public int GetMouse()
    {
        LoadGame();
        return saveData.decoyMouse;
    }
    
    public int GetBlueHearts()
    {
        LoadGame();
        return saveData.specialHeart;
    }

    public int GetCheetos()
    {
        LoadGame();
        return saveData.cheetos;
    }

    public void SaveGame()
    {
        saveData.lives = PlayerData.Instance.GetHearts();
        saveData.cheetos = PlayerData.Instance.GetCheetos();
        saveData.potion = PlayerData.Instance.GetPotion();
        saveData.decoyMouse = PlayerData.Instance.GetMouse();
        saveData.specialHeart = PlayerData.Instance.GetBlueHearts();
        string json = JsonUtility.ToJson(saveData, true);
        File.WriteAllText(path, json);
        Debug.Log(json);
    }

    public void LoadGame()
    {
        Debug.Log(path);
        if (File.Exists(path))
        {
            Debug.Log("el path existe");
            string json = File.ReadAllText(path);
            Debug.Log(json);
            JsonUtility.FromJsonOverwrite(json, saveData);
        }
        else
            CreatePath();
    }

    public void Delete()
    {
        if (File.Exists(path))
        {
            File.Delete(path);
            Debug.Log("file deleted");
            CreatePath();
        }
    }
}