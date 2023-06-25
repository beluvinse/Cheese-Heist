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
            saveData.lives = _heartsDefault;
            saveData.cheetos = _cheetosDefault;
            string json = JsonUtility.ToJson(saveData, true);
            File.WriteAllText(path, json);
        }
    }


    public void SetHearts(int val)
    {
        saveData.lives = val;
        //SaveGame();
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
        saveData.lives = PlayerData.Instance.GetHearts();
        saveData.cheetos = PlayerData.Instance.GetCheetos();
        string json = JsonUtility.ToJson(saveData, true);
        File.WriteAllText(path, json);
        Debug.Log(json);
    }

    public void LoadGame()
    {
        if (File.Exists(path))
        {
            Debug.Log(path);
            string json = File.ReadAllText(path);
            Debug.Log(json);
            JsonUtility.FromJsonOverwrite(json, saveData);
        }
        else
            CreatePath();

    }

    public void Delete()
    {
        //FileUtil.DeleteFileOrDirectory(path);
        //saveData.cheetos = 0;
        //PlayerData.Instance.SetCheetos(saveData.cheetos);
        //saveData.lives = PlayerData.Instance.GetMaxHearts();
        //PlayerData.Instance.SetHearts(saveData.lives);

        if (File.Exists(path))
        {
            File.Delete(path);

            CreatePath();
        }
    }
}