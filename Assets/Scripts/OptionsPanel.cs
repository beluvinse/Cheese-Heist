using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OptionsPanel : MonoBehaviour
{
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
}
