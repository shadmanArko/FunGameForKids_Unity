using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class Level2GameManager : Singleton<Level2GameManager>
{
    [SerializeField] private GameObject ButtonPrefab;
    [SerializeField] private int Value;
    [SerializeField] private TextMeshProUGUI TargetValue; 
    private int[] values;
    [SerializeField] private Sprite[] ButtonSprites;
    [SerializeField] private Transform GridRoot;
    [SerializeField] private List<GameObject> CheckBoxes;
    [SerializeField]private int _turn;
    private List<GameObject> _listOfGameObject;
    private SceneLoader _sceneloader;
    private Coroutine _coroutine;

    public UnityEvent OnCorrectAnswer;
    public UnityEvent OnWrongAnswer;


    private SFXManager _sfx_manager;
    void Start()
    {
        _turn = 5;
        Score.totalScoreToPlayFor = _turn;
        _sceneloader = gameObject.GetComponent<SceneLoader>();
        _sfx_manager = SFXManager.Instance;
        _sceneloader.loadSceneAsAdditive("CommonMenu");
        CreateGame();
        _coroutine= StartCoroutine(CreateUi());
    }
    public void Update()
    {
        if (Input.GetButtonDown("Cancel"))
        {
            _sceneloader.LoadSceneAsSingle("Map");
        }
    }
    private void CreateGame()
    {
        
        RandomNumberGenerator random = new RandomNumberGenerator();
        values = random.GenrateNumber(1, 9, 4).ToArray();

        Value = values[Random.Range(0, values.Length)];
        TargetValue.text = Value.ToString();
    }

    IEnumerator CreateUi()
    {
        yield return null;
        yield return new WaitForSeconds(.1f);
        _listOfGameObject = new List<GameObject>();
        DestroyObjects.Instance.DestroyObject();
        for (int i = 0; i < 4; i++)
        {
            yield return new WaitForSeconds(0.3f);
            GameObject button = Instantiate(ButtonPrefab, GridRoot);
            _listOfGameObject.Add(button);
            DestroyObjects.Instance.ListOfGameObject.Add(button);
        }

        for (int i = 0; i < 4; i++)
        {
            _listOfGameObject[i].GetComponent<Level2ButtonBehavior>().Create_UI(values[i], ButtonSprites[i]);
        }
        if (_coroutine!=null)
        {
            StopCoroutine(_coroutine);
        }
    }
    
    public void EvaluateScore(int value)
    {
        _sfx_manager.PlaySFX(1);

        if (value == Value)
        {
            
            Score.score++;
            _sfx_manager.PlaySFX(2);
            _sfx_manager.PlaySFX(4);

            CheckBoxes[CheckBoxes.Count - _turn].GetComponent<CustomCheckBox>().SetState(Enums.CheckState.Correct);
            OnCorrectAnswer?.Invoke();
        }
        else
        {
            
            _sfx_manager.PlaySFX(3);

            CheckBoxes[CheckBoxes.Count - _turn].GetComponent<CustomCheckBox>().SetState(Enums.CheckState.Wrong);
            OnWrongAnswer?.Invoke();

        }
        _turn--;

        if (_turn > 0)
        {
            CreateGame();
            _coroutine=StartCoroutine(CreateUi());
        }
        else
        {
           _sceneloader.LoadSceneAsSingle("ScoreCard");
        }
    }

}
