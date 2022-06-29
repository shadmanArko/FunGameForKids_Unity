using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class Level10Manager : Singleton<Level10Manager>
{
    #region Variables
    [SerializeField] private GameObject Panel;
    [SerializeField] private GameObject CardPrefab;
    [SerializeField] private TextMeshProUGUI Add1;
    [SerializeField] private TextMeshProUGUI Add2;
    [SerializeField] private TextMeshProUGUI Sub1;
    [SerializeField] private TextMeshProUGUI Sub2;
    [SerializeField] private int UpperLimit;
    [SerializeField] private int LowerLimit;

    [SerializeField]private int _totalOptions = 8;
    public int Add1Value, Add2Value, Sub1Value, Sub2Value;
    private List<int> _values;
    private List<int> _resultValues;
    private Camera camera;
    private SceneLoader _sceneloader;
    private Coroutine _coroutine;
    private SFXManager _sfx_manager;
    private Coroutine _voice_loader_coroutine;
    #endregion
    void Start()
    {
        camera = Camera.main;
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

    private void ValueGeneration()
    {
        RandomNumberGenerator random = new RandomNumberGenerator();
        _values = random.GenrateNumber(LowerLimit, UpperLimit, _totalOptions);
        PlaceOutput();
    }

    private void PlaceOutput()
    {
        
        _resultValues = new List<int>(4);
        while (_resultValues.Count < 4)
        {
            int number = _values[Random.Range(1, _totalOptions)];
            if (_resultValues.Contains(number)) continue;
            _resultValues.Add(number);
        }
        _resultValues.Sort();
        Add1Value = _resultValues[2] + _resultValues[3];
        Add2Value = _resultValues[0] + _resultValues[1];
        Sub1Value = _resultValues[3] - _resultValues[0];
        Sub2Value = _resultValues[2] - _resultValues[1];
        Add1.text = Add1Value.ToString();
        Add2.text = Add2Value.ToString();
        Sub1.text = Sub1Value.ToString();
        Sub2.text = Sub2Value.ToString();
    }

    public void CreateGame()
    {
        ValueGeneration();
        DestroyObjects.Instance.DestroyObject();
        _coroutine=StartCoroutine(StartGeneratingGrid());
    }

    public void OnDrop()
    {
        _sfx_manager.PlaySFX(1);
       
    }
    IEnumerator StartGeneratingGrid()
    {
        yield return null;
        
        for (int i = 0; i < _totalOptions; i++)
        {
            yield return new WaitForSeconds(0.2f);
            GameObject bear = Instantiate(CardPrefab,Panel.transform);
            DestroyObjects.Instance.ListOfGameObject.Add(bear);
            bear.GetComponent<ValueBehaviour>().Value = _values[_totalOptions - i - 1];
            bear.GetComponent<ValueBehaviour>().SetValue(_values[_totalOptions - i - 1]);
            bear.GetComponent<Drag>().camera = camera;
        }

        if (_coroutine!=null)
        {
            StopCoroutine(_coroutine);
        }
    }
    IEnumerator PlayVoice(float wait, int voice_index)
    {
        yield return new WaitForSeconds(wait);
    }

}
