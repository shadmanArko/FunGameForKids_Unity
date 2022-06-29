using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class LevelButtonData : MonoBehaviour
{
    [SerializeField]private LevelData LevelData;
    [SerializeField]private Image[] Stars;
    [SerializeField] private TextMeshProUGUI LevelIndexText; 
    private Button Button;
    void Start()
    {
        Button = GetComponent<Button>();
        LevelData LoadData = SaveLoadManager.Instance.Datas.Find(r => r.LevelIndex == LevelData.LevelIndex);
        if (LoadData != null)
        {
            LevelData = LoadData;
        }
        else
        {
            SaveLoadManager.Instance.Datas.Add(LevelData);
            SaveLoadManager.Instance.SaveData();
        }
        InitializeData(LevelData);
    }

    public void InitializeData(LevelData levelData)
    {
        LevelData = levelData;
        Button.interactable = LevelData.Unlocked;
        LevelIndexText.text = LevelData.LevelIndex.ToString();
        Button.onClick.AddListener(() =>
        {
            SceneManager.LoadScene(LevelData.LevelName + LevelData.LevelIndex);
            Score.LastPlayedMapIndex = LevelData.LevelIndex;
            SFXManager.Instance.StopBGM(0);//REPLACE With levelBGM later
        });

        for (int i = 0; i < LevelData.Stars; i++)
        {
            Stars[i].color = Color.white;
        }
    }


}

[Serializable]
public class LevelData
{
    public string LevelName;
    public int LevelIndex;
    public bool Unlocked;
    public int Stars;
}
