using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class Level5Manager : MonoBehaviour
{
    public enum GameMode
    {
        Plus, Minus
    }

    [SerializeField] private Transform DragablePanel;
    [SerializeField] private GameObject CardPrefab;
    [SerializeField] private ValueBehaviour Target1;
    [SerializeField] private ValueBehaviour Target2;


    public int TargetValue
    {
        get
        {
            switch (mode)
            {
                case GameMode.Plus:
                    return Target1.Value + Target2.Value;
                case GameMode.Minus:
                    return Target1.Value - Target2.Value;
                default:
                    throw new ArgumentOutOfRangeException();
            }
        }
    }

    [SerializeField] public GameObject FillPanel;

    [SerializeField] private int _totalCard = 5;
    private int[] _values;

    [SerializeField] private int LowerRange;
    [SerializeField] private int UpperRange;
    private Camera camera;
    private int turn;
    private SceneLoader _sceneLoader;
    private SFXManager _sfx_manager;
    private Coroutine _voice_loader_coroutine;

    public GameMode mode = GameMode.Plus;




    public void Start()
    {

        turn = 5;
        Score.totalScoreToPlayFor = turn;
        _sfx_manager = SFXManager.Instance;
        _sceneLoader = FindObjectOfType<SceneLoader>();
        _sceneLoader.loadSceneAsAdditive("CommonMenu");
        camera = Camera.main;
        CreateGame();
    }
    public void Update()
    {
        if (Input.GetButtonDown("Cancel"))
        {
            FindObjectOfType<SceneLoader>().LoadSceneAsSingle("Map");
        }
    }
    public void CreateGame()
    {
        RandomNumberGenerator random = new RandomNumberGenerator();
        List<int> randList = random.GenrateNumber(LowerRange, UpperRange, _totalCard);

        _values = randList.ToArray();


        int targetValue;
        int oneNumber;
        int twoNumber;

        switch (mode)
        {
            case GameMode.Plus:
                {
                    targetValue = _values[Random.Range(0, _values.Length)];
                    oneNumber = Random.Range(1, targetValue - 1);
                    twoNumber = targetValue - oneNumber;
                }
                break;
            case GameMode.Minus:
                {
                    targetValue = _values[Random.Range(0, _values.Length)];
                    twoNumber = Random.Range(1, targetValue - 1);
                    oneNumber = targetValue + twoNumber;
                }
                break;
            default:
                throw new ArgumentOutOfRangeException();
        }



        Target1.SetValue(oneNumber);

        Target2.SetValue(twoNumber);
        CreateGrid();
    }
    private void CreateGrid()
    {

        DestroyObjects.Instance.DestroyObject();
        for (int i = 0; i < _totalCard; i++)
        {
            GameObject b = Instantiate(CardPrefab);
            DestroyObjects.Instance.ListOfGameObject.Add(b);
            ValueBehaviour card = b.GetComponentInChildren<ValueBehaviour>();
            b.GetComponent<Drag>().camera = camera;
            card.transform.SetParent(DragablePanel.transform, false);
            card.Value = _values[i];
            card.SetValue(card.Value);
        }
    }
    IEnumerator PlayVoice(float wait, int voice_index)
    {
        yield return new WaitForSeconds(wait);
        if (_voice_loader_coroutine != null)
        {
            StopCoroutine(_voice_loader_coroutine);
        }
    }
}
