using TMPro;
using UnityEngine;

public class GeneralScoreManager : Singleton<GeneralScoreManager>
{
    public TextMeshProUGUI rightScoreText;
    public TextMeshProUGUI wrongScoreText;
    public TextMeshProUGUI totalScoreText;

    public void SetRightScore(int score)
    {
        rightScoreText.text = score.ToString();
    }

    public void SetWrongScore(int score)
    {
        wrongScoreText.text = score.ToString();
    }

    public void SetTotalScore(string score)
    {
        totalScoreText.text = score;
    }
}
