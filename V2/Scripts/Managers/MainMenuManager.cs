using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class MainMenuManager : MonoBehaviour
{

    public GameObject GameOverLabel;
  
    void Start()
    {
        SFXManager.Instance.PlayBGM(0);
        CheckGameOver();
    }

    public void ToggleSound(bool state)
    {
        SFXManager.Instance.ToggleSound(state);
    }

    //todo hardcoded ending logic
    private void CheckGameOver()
    {
       var gameOver= SaveLoadManager.Instance.Datas.FirstOrDefault(r => r.LevelIndex == 10 && r.Stars > 0);
       GameOverLabel.SetActive(gameOver != null);
    }

    public void QuitGame()
    {
        Application.Quit();
    }


}
