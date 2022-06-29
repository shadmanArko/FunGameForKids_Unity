using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;


[RequireComponent(typeof(Level5Manager))]
public class Target : MonoBehaviour
{
    
    [SerializeField] private List<GameObject> CheckBoxes;
    private Level5Manager Manager;

    private SFXManager _sfx_manager;

    public int Turn = 0;


    public UnityEvent OnCorrect;
    public UnityEvent OnWrong;

    private void Start()
    {
        Manager = GetComponent<Level5Manager>();
        _sfx_manager = SFXManager.Instance;
    }
    public void OnSubmitPressed()
    {
        Turn++;
        _sfx_manager.PlaySFX(1);
        int targetValue = Manager.TargetValue;

        int result = Manager.FillPanel.transform.GetChild(0).GetComponent<ValueBehaviour>().Value;
        if (targetValue == result)
        {
            Score.score++;
            CheckBoxes[Turn - 1].GetComponent<CustomCheckBox>().SetState(Enums.CheckState.Correct);
            OnCorrect?.Invoke();
            _sfx_manager.PlaySFX(2);
            _sfx_manager.PlaySFX(4);
   
        }

        else
        {
            CheckBoxes[Turn - 1].GetComponent<CustomCheckBox>().SetState(Enums.CheckState.Wrong);
            OnWrong?.Invoke();
            _sfx_manager.PlaySFX(3);
            
        }
       

        if (Turn == Score.totalScoreToPlayFor)
        {
            FindObjectOfType<SceneLoader>().LoadSceneAsSingle("ScoreCard");
        }
        else if(Turn< Score.totalScoreToPlayFor)
        {
           Manager.CreateGame();
        }
    }
   
}
