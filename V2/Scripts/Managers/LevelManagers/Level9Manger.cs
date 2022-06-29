using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class Level9Manger : Singleton<Level9Manger>
{
    [SerializeField] private List<GameObject> ShapePrefabs;
    [SerializeField] private GameObject SpawnPanel;
    [SerializeField] private List<GameObject> CheckBoxes;
    
    [SerializeField]private int _totalShapes = 12;
    public int turn = 0;
    private SceneLoader _sceneloader;
    private Coroutine _coroutine;
    private ShapeBehaviour _shapeBehaviour_parent;
    private ShapeBehaviour _shapeBehaviour_child;
    private SFXManager _sfx_manager;


    public UnityEvent OnCorrect;
    public UnityEvent OnWrong;

    public void Start()
    {
        Score.totalScoreToPlayFor = _totalShapes;
        GameObject b = Instantiate(ShapePrefabs[Random.Range(0, ShapePrefabs.Count)]);
        ShapeBehaviour card = b.GetComponentInChildren<ShapeBehaviour>();
        b.GetComponent<Drag>().camera = Camera.main;
        card.transform.SetParent(SpawnPanel.transform, false);
        _sceneloader = FindObjectOfType<SceneLoader>();
        _sfx_manager = SFXManager.Instance;
        _sceneloader.loadSceneAsAdditive("CommonMenu");
        CreateGame();
    }
    public void Update()
    {
        if (Input.GetButtonDown("Cancel"))
        {
            _sceneloader.LoadSceneAsSingle("Map");
        }
    }

    public void CreateGame()
    {
        _coroutine = StartCoroutine(GenerateRoutine());
    }

    IEnumerator GenerateRoutine()
    {
        yield return null;
        for (int i = 0; i < _totalShapes - 1; i++)
        {
            yield return new WaitForSeconds(0.2f);
            GameObject b = Instantiate(ShapePrefabs[Random.Range(0, ShapePrefabs.Count)]);
            ShapeBehaviour card = b.GetComponentInChildren<ShapeBehaviour>();
            b.GetComponent<Drag>().camera = Camera.main;
            card.transform.SetParent(SpawnPanel.transform, false);
        }
        if (_coroutine!=null)
        {
            StopCoroutine(_coroutine);
        }
    }



    public void ResultOnDrop()
    {

        _sfx_manager.PlaySFX(1);
        turn++;
        _shapeBehaviour_parent = Drag.itemBeingDragged.gameObject.transform.parent.GetComponent<ShapeBehaviour>();
        _shapeBehaviour_child = Drag.itemBeingDragged.GetComponent<ShapeBehaviour>();
        if (_shapeBehaviour_parent.shapeId ==_shapeBehaviour_child.shapeId)
        {
            Score.score++;
            CheckBoxes[turn - 1].GetComponent<CustomCheckBox>().SetState(Enums.CheckState.Correct);
            OnCorrect?.Invoke();
            _sfx_manager.PlaySFX(2);
            _sfx_manager.PlaySFX(4);

        }
        else
        {
            CheckBoxes[turn - 1].GetComponent<CustomCheckBox>().SetState(Enums.CheckState.Wrong);
            OnWrong?.Invoke();
            _sfx_manager.PlaySFX(3);

        }

        if (turn == _totalShapes)
        {
            _sceneloader.LoadSceneAsSingle("ScoreCard");
        }
        Destroy(Drag.itemBeingDragged);
    }

}
