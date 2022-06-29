using System.Collections;
using System.Collections.Generic;
using JetBrains.Annotations;
using UnityEngine;
using UnityEngine.Events;
using Random = UnityEngine.Random;


public class Level1Manger : Singleton<Level1Manger>
{
  
    [SerializeField] private Transform DragablePanel;
    [SerializeField] private GameObject CardPrefab;
    [SerializeField] private Camera cam;
    [SerializeField] public GameObject TargetObject;
    [SerializeField] private GameObject FillPanel;
    [SerializeField] private List<GameObject> CheckBoxes;
    [SerializeField] private int LowerBound;
    [SerializeField] private int UpperBound;

    private int _totalCard = 4;
    private int[] _values;
    private int _totalTurn = 5;
    private int _turn = 0;
    private float _waitTime = 0.2f;
    public UnityEvent AnimOnCorrectEvent;
    public UnityEvent AnimOnWrongEvent;
    private SceneLoader _sceneLoader;
    private ValueBehaviour _targetCard;
    private Coroutine _coroutine;
    private SFXManager _sfx_manager;
    private Coroutine _voice_loader_coroutine;
    public void Start()
    {
        Score.totalScoreToPlayFor = _totalTurn;
        _sceneLoader = FindObjectOfType<SceneLoader>();
        _targetCard = TargetObject.GetComponent<ValueBehaviour>();
        _coroutine = StartCoroutine(CreateGame());
        _sfx_manager = SFXManager.Instance;
        _sceneLoader.loadSceneAsAdditive("CommonMenu");


    }

    public void ResultOnDrop()
    {
        _waitTime = 0.1f;
        _turn++;
        int targetValue = _targetCard.Value;
        int Result = FillPanel.transform.GetChild(0).GetComponent<ValueBehaviour>().Value;
        _sfx_manager.PlaySFX(1);
        if (targetValue == Result)
        {
            Score.score++;
            AnimOnCorrectEvent?.Invoke();
            CheckBoxes[_turn - 1].GetComponent<CustomCheckBox>().SetState(Enums.CheckState.Correct);
            _sfx_manager.PlaySFX(2);
            _sfx_manager.PlaySFX(4);
        }
        else
        {
            AnimOnWrongEvent?.Invoke();
            CheckBoxes[_turn - 1].GetComponent<CustomCheckBox>().SetState(Enums.CheckState.Wrong);
            _sfx_manager.PlaySFX(3);

        }
        if (_turn == _totalTurn)
        {
            _sceneLoader.LoadSceneAsSingle("ScoreCard");
        }
        else
        {
            _coroutine = StartCoroutine(CreateGame());
        }
    }

    IEnumerator CreateGame()
    {
        DestroyObjects.Instance.DestroyObject();
        yield return new WaitForSeconds(_waitTime);
        RandomNumberGenerator random = new RandomNumberGenerator();
        _values = random.GenrateNumber(LowerBound, UpperBound, _totalCard).ToArray();
        int TargetValueInt = _values[Random.Range(0, _values.Length)];
        _targetCard.SetValue(TargetValueInt);
        _targetCard.Value = TargetValueInt;
        CreateGrid();
        if (_coroutine != null)
        {
            StopCoroutine(_coroutine);
        }
    }

    private void CreateGrid()
    {
        for (int i = 0; i < _totalCard; i++)
        {
            GameObject b = Instantiate(CardPrefab);
            DestroyObjects.Instance.ListOfGameObject.Add(b);
            ValueBehaviour card = b.GetComponentInChildren<ValueBehaviour>();
            card.transform.SetParent(DragablePanel.transform, false);
            b.GetComponent<Drag>().camera = cam;
            card.Value = _values[i];
            card.SetValue(card.Value);
        }
    }



}
