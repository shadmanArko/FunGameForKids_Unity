using UnityEngine.SceneManagement;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class ScoreCardManager : MonoBehaviour
{
    public static ScoreCardManager Instance;
    [SerializeField] public TextMeshProUGUI score;
    [SerializeField] public TextMeshProUGUI congratsText;
    [SerializeField] public GameObject starPanel;
    [SerializeField] public GameObject starRef;
    [SerializeField] public AudioClip winSFX;
    [SerializeField] public AudioClip loseSFX;

    private int totalStars;
    private int starsRecieved;
    private int playerScore;
    private int totalScoreToPlayFor;
    private AudioSource audioSource;

    private void Awake()
    {
        if(Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
    }

    private void Start()
    {
        audioSource = GetComponent<AudioSource>();
        totalStars = 3;
        CreateScoreCard();
    }

    private void CreateScoreCard()
    {
        
        score.text = Score.score.ToString();
        playerScore = Score.score;
        totalScoreToPlayFor = Score.totalScoreToPlayFor;

        if (playerScore < totalScoreToPlayFor * 0.3)
            starsRecieved = 0;
        else if (playerScore > (totalScoreToPlayFor * 0.3) && playerScore <= (totalScoreToPlayFor * 0.6))
            starsRecieved = 1;
        else if (playerScore > (totalScoreToPlayFor * 0.6) && playerScore <= (totalScoreToPlayFor * 0.8))
            starsRecieved = 2;
        else
            starsRecieved = 3;

        congratsText.gameObject.SetActive(starsRecieved > 0);
        AudioClip gameOverAudio = starsRecieved > 0 ? winSFX : loseSFX;
        audioSource.clip = gameOverAudio;
        audioSource.Play();
        GameObject[] stars = new GameObject[totalStars];

        for(int i = 0; i < totalStars;i++)
        {
            GameObject star = Instantiate(starRef, starPanel.transform);
            stars[i] = star;
        }

        for(int i = starsRecieved; i < totalStars;i++)
        {
            GameObject star = stars[i];
            star.GetComponent<Image>().color = Color.black;
        }

        LevelData leveldata=SaveLoadManager.Instance.Datas.Find(r => r.LevelIndex == Score.LastPlayedMapIndex);
        if (leveldata.Stars < starsRecieved)
        {
            leveldata.Stars = starsRecieved;
        }

        LevelData nextleveldata = SaveLoadManager.Instance.Datas.Find(r => r.LevelIndex == Score.LastPlayedMapIndex+1);
        if (nextleveldata != null && starsRecieved>0) nextleveldata.Unlocked = true;
        SaveLoadManager.Instance.SaveData();

    }

    public void GotoMainMenu()
    {
        SceneManager.LoadScene("Map", LoadSceneMode.Single);
        Score.score = 0;
        Score.totalScoreToPlayFor = 0;
    }
}
