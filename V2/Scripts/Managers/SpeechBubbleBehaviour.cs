using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class SpeechBubbleBehaviour : MonoBehaviour
{
    public string LevelInstruction="Drag the correct answer to the empty container";
    public string CorrectAnswerResponse = "Correct Answer!";
    public string WrongAnswerResponse = "Uhh! Try Again";

    public Color InstructionColor=Color.black;
    public Color CorrectAnswerColor=Color.green;
    public Color WrongAnswerColor=Color.red;

    public TextMeshProUGUI textMesh;

    public float characterSpawnDelay = 0f;


    void Start()
    {
        ShowInstruction();
    }


    public void ShowInstruction()
    {
        textMesh.color = InstructionColor;
        StopAllCoroutines();
        StartCoroutine(ShowText(LevelInstruction,false));
    }

    public  void ShowCorrect()
    {
        textMesh.color = CorrectAnswerColor;
        StopAllCoroutines();
        StartCoroutine(ShowText(CorrectAnswerResponse));
    }

    public  void ShowWrong()
    {
        textMesh.color = WrongAnswerColor;
        StopAllCoroutines();
        StartCoroutine(ShowText(WrongAnswerResponse));
    }


    private IEnumerator ShowText(string message,bool showInstruction=true)
    {
     
        textMesh.text = "";
        yield return StartCoroutine(ShowCharacters(message));
        yield return new WaitForSeconds(1);
        if (showInstruction)
        {
            ShowInstruction();
        }
    }


    private IEnumerator ShowCharacters(string message)
    {
        var chars=message.ToCharArray();

        foreach (var character in chars)
        {
            yield return new WaitForSeconds(characterSpawnDelay);
            textMesh.text += character;
        }
    }



}
