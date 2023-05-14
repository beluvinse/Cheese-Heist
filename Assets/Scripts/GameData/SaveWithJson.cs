using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using System;
using UnityEditor;

public class SaveWithJson : MonoBehaviour
{
    string path;
    [SerializeField] SaveData saveData = new SaveData();

    void Awake()
    {
        path = Application.persistentDataPath + "/data.json";

        Debug.Log(path);
    }

    public void SaveGame()
    {
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
        FileUtil.DeleteFileOrDirectory(path);
    }
}