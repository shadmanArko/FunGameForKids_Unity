using System.Collections;
using System.Collections.Generic;
using System.IO;
using Newtonsoft.Json;
using UnityEngine;

public class SaveLoadManager : MonoBehaviour
{
    public static SaveLoadManager Instance;


    public List<LevelData> Datas;

    private string Path => System.IO.Path.Combine(Application.persistentDataPath, "Data.json");

    void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
            LoadData();
        }
        else
        {
            Destroy(gameObject);
        }
    }

    public void SaveData()
    {
        string data = JsonConvert.SerializeObject(Datas);
        File.WriteAllText(Path,data);
    }


    public void LoadData()
    {
        if (File.Exists(Path))
        {
            string data = File.ReadAllText(Path);
            Datas = JsonConvert.DeserializeObject<List<LevelData>>(data);
        }
    }

   

  
}
