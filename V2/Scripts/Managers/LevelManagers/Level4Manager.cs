using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Level4Manager : Singleton<Level4Manager>
{
    [SerializeField] private GameObject itemsPanel;
    [SerializeField] private GameObject BoxPrefab;
    [SerializeField] private GameObject BarnPanel;

    [SerializeField] private List<Sprite> icons;
    [SerializeField] private GameObject PlacingCard;
    [SerializeField] public bool order;

    public int turn;
    public int dropped;
    private List<int> _animalPrefabsIndices;
    private int _totalObjects;
    private int _totalAnimals;
    private List<int> _values;
    private List<int> _animalValues;
    private SceneLoader _sceneLoader;


    void Start()
    { 
        _totalObjects = 3;
        _totalAnimals = 5;
        turn = 5;
        Score.totalScoreToPlayFor = turn * _totalObjects;
        _sceneLoader = FindObjectOfType<SceneLoader>();

        _sceneLoader.loadSceneAsAdditive("CommonMenu");
        CreateGame();
    }
    public void Update()
    {
        if (Input.GetButtonDown("Cancel"))
        {
            _sceneLoader.LoadSceneAsSingle("Map");
        }
    }
    public void CreateGame()
    {
        dropped = 0;
        RandomNumberGenerator random = new RandomNumberGenerator();
        List<int> randList = random.GenrateNumber(1,10,_totalObjects);
        _values = randList;
        _animalValues = randList;
        _values.Sort();

        DestroyObjects.Instance.DestroyObject();
        SpawnBarns();
        SpawnAnimals();
    }

    private void SpawnBarns()
    {
            for (int i = 0; i < _values.Count; ++i)
            {
                GameObject placingCard = Instantiate(PlacingCard, BarnPanel.transform);
                DestroyObjects.Instance.ListOfGameObject.Add(placingCard);
                Drop drop = placingCard.GetComponentInChildren<Drop>();
                drop.OnDropEvent.AddListener(gameObject.transform.GetComponent<DropAnimals>().ResultOnDrop);
                ValueBehaviour valueBehaviour = placingCard.GetComponentInChildren<ValueBehaviour>();
            if (order)
                {
                    
                    valueBehaviour.SetValue(_values[i]);
                    valueBehaviour.Value = _values[i];
                }
                else
                {
                    
                    valueBehaviour.SetValue(_values[_values.Count - 1 - i]);
                    valueBehaviour.Value = _values[_values.Count - 1 - i];
                }
            } 
    }

    private void SpawnAnimals()
    {
        _animalPrefabsIndices = new List<int>();
        while(_animalPrefabsIndices.Count < 3)
        {
            int number = Random.Range(0, _totalAnimals);
            if (_animalPrefabsIndices.Contains(number)) continue;
            _animalPrefabsIndices.Add(number);
        }

        for(int i = 0; i < _animalValues.Count; i++)
        {
            GameObject mainAnimalPanel = itemsPanel;
            GameObject boxPrefab = Instantiate(BoxPrefab, itemsPanel.transform);
            boxPrefab.GetComponent<Level2ButtonBehavior>().Create_UI(_animalValues[i], icons[_animalPrefabsIndices[i]]);
            boxPrefab.GetComponent<Drag>().camera = Camera.main;

            AnimalPrefabBehavior animalPanel = boxPrefab.GetComponent<AnimalPrefabBehavior>();
            animalPanel.ParentGameObject = mainAnimalPanel;
            animalPanel.value = _animalValues[i];

           
        }
    }

   
}
