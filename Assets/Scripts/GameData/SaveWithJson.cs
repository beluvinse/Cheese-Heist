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
    public PlayerData playerData;

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
        path = Application.persistentDataPath + "/data.json";
        Debug.Log(path);
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
    
    public int GetCheetos()
    {
        LoadGame();
        return saveData.cheetos;
    }

    public void SaveGame()
    {   
        saveData.lives = playerData.GetHearts();
        saveData.cheetos = playerData.GetCheetos();
        string json = JsonUtility.ToJson(saveData, true);
        File.WriteAllText(path, json);
        Debug.Log(json);
    }

    public void LoadGame()
    {
        string json = File.ReadAllText(path);
        JsonUtility.FromJsonOverwrite(json, saveData);
        Debug.Log(json);

    }

    public void Delete()
    {
        //FileUtil.DeleteFileOrDirectory(path);
        saveData.cheetos = 0;
        PlayerData.Instance.SetCheetos(saveData.cheetos);
        saveData.lives = PlayerData.Instance.GetMaxHearts();
        PlayerData.Instance.SetHearts(saveData.lives);
    }
}