using System;
using System.Collections.Generic;
using System.IO;
using UnityEngine;


[Serializable]
public struct SaveData
{
    public int Highscore;
}

public class SaveManager : MonoBehaviour
{
    [SerializeField] private SaveData data;
    [SerializeField] string fileName;
    public int GetHighscore => data.Highscore;
    string GetPath()
    {
        return Application.persistentDataPath + "/" + fileName + ".json";
    }
    public void LoadData()
    {
        if (!File.Exists(GetPath()))
        {
            SaveGameFile();
            return;
        }
        string jsonfile = File.ReadAllText(GetPath());
        data = JsonUtility.FromJson<SaveData>(jsonfile);
    }
    public void SaveGameFile()
    {
        string jsonfile = JsonUtility.ToJson(data, true);
        File.WriteAllText(GetPath(), jsonfile);
    }
    public void SetHighscore(int someScore)
    {
        if (data.Highscore < someScore)
        {
            data.Highscore = someScore;
            SaveGameFile();
        }
    }
}