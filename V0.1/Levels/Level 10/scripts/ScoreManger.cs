using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScoreManger : MonoBehaviour
{
    [SerializeField] private GameObject Panel1;
    [SerializeField] private GameObject Panel2;
    [SerializeField] private GameObject Panel3;
    [SerializeField] private GameObject Panel4;
    [SerializeField] private List<GameObject> CheckBoxes;
    
    private int _totalTurns = 5;
    private int _turn = 5;
    private Level10AnimationController _level10AnimationController;
    private SceneLoader _sceneloader;
    private ValueBehaviour _valueBehaviour_panel1;
    private ValueBehaviour _valueBehaviour_panel2;
    private ValueBehaviour _valueBehaviour_panel3;
    private ValueBehaviour _valueBehaviour_panel4;
    private SFXManager _sfx_manager;
    private Coroutine _voice_loader_coroutine;
    private void Start()
    {
        Score.totalScoreToPlayFor = _turn;
        _sceneloader = FindObjectOfType<SceneLoader>();
        _sfx_manager = SFXManager.Instance;
        _level10AnimationController = FindObjectOfType<Level10AnimationController>();
    }

    void Update()
    {
        if (AllFilled())
        {
            _turn--;
            if (AddCheck() && SubCheck())
            {
                Score.score++;
                CheckBoxes[_totalTurns - _turn - 1].GetComponent<CustomCheckBox>().SetState(Enums.CheckState.Correct);
                _sfx_manager.PlaySFX(2);
                _sfx_manager.PlaySFX(4);
                
            }
            else
            {
  
                CheckBoxes[_totalTurns - _turn - 1].GetComponent<CustomCheckBox>().SetState(Enums.CheckState.Wrong);
                _sfx_manager.PlaySFX(3);
         
            }
        
            Level10Manager.Instance.CreateGame();

            if (_turn<=0)
            {
                _sceneloader.LoadSceneAsSingle("ScoreCard");
            }
            UnlockDroppableBoxes();
        }
    }


    private void UnlockDroppableBoxes()
    {
        Panel1.GetComponent<Drop>().isLocked = false;
        Panel2.GetComponent<Drop>().isLocked = false;
        Panel3.GetComponent<Drop>().isLocked = false;
        Panel4.GetComponent<Drop>().isLocked = false;
    }

    #region Checking

    bool AllFilled()
    {
        if (Panel1.transform.childCount != 0 && Panel2.transform.childCount != 0 && Panel3.transform.childCount != 0 &&
            Panel4.transform.childCount != 0)
        {
            return true;
        }
        return false;
    }


    bool AddCheck()
    {
        _valueBehaviour_panel1 = Panel1.transform.GetChild(0).GetComponent<ValueBehaviour>();
        _valueBehaviour_panel2 = Panel2.transform.GetChild(0).GetComponent<ValueBehaviour>();
        _valueBehaviour_panel3 = Panel3.transform.GetChild(0).GetComponent<ValueBehaviour>();
        _valueBehaviour_panel4 = Panel4.transform.GetChild(0).GetComponent<ValueBehaviour>();
        if (_valueBehaviour_panel1.Value + _valueBehaviour_panel2.Value == Level10Manager.Instance.Add1Value)
        {
            if (_valueBehaviour_panel3.Value +_valueBehaviour_panel4.Value == Level10Manager.Instance.Add2Value)
            {
                return true;
            }
        }
        return false;
    }

    bool SubCheck()
    {
        _valueBehaviour_panel1 = Panel1.transform.GetChild(0).GetComponent<ValueBehaviour>();
        _valueBehaviour_panel2 = Panel2.transform.GetChild(0).GetComponent<ValueBehaviour>();
        _valueBehaviour_panel3 = Panel3.transform.GetChild(0).GetComponent<ValueBehaviour>();
        _valueBehaviour_panel4 = Panel4.transform.GetChild(0).GetComponent<ValueBehaviour>();
        if (_valueBehaviour_panel1.Value -_valueBehaviour_panel3.Value == Level10Manager.Instance.Sub1Value)
        {
            if (_valueBehaviour_panel2.Value -_valueBehaviour_panel4.Value == Level10Manager.Instance.Sub2Value)
            {
                return true;
            }
        }
        return false;
    }

    #endregion
    
}
