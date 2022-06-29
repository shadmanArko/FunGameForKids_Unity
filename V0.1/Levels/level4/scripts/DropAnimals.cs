using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class DropAnimals : MonoBehaviour
{
    [SerializeField] private List<GameObject> CheckBoxes;
    private int _turn = 5;
    private AnimalPrefabBehavior _animalPrefabBehavior;
    private ValueBehaviour _valueBehaviour;
    private SceneLoader _sceneloader;
    private SFXManager _sfx_manager;

    public UnityEvent OnCorrect;
    public UnityEvent OnWrong;

    private void Start()
    {
        _sceneloader = FindObjectOfType<SceneLoader>();
        _sfx_manager = SFXManager.Instance;
    }
    public void ResultOnDrop()
    {
        _animalPrefabBehavior = Drag.itemBeingDragged.GetComponent<AnimalPrefabBehavior>();
        _valueBehaviour = Drag.itemBeingDragged.transform.parent.GetComponent<ValueBehaviour>();
        _sfx_manager?.PlaySFX(1);
        if (_animalPrefabBehavior.value.Equals(_valueBehaviour.Value)&& _animalPrefabBehavior.wentHome == false)
        {
            _sfx_manager?.PlaySFX(4);

            _animalPrefabBehavior.wentHome = true;
            CheckBoxes[_turn - Level4Manager.Instance.turn].GetComponent<CustomCheckBox>().SetState(Enums.CheckState.Correct);
            OnCorrect?.Invoke();
            Level4Manager.Instance.dropped++;
            Score.score++;
            Drag.itemBeingDragged.GetComponent<Drag>().enabled = false;
        }

        else if(_animalPrefabBehavior.value!= _valueBehaviour.Value && _animalPrefabBehavior.wentHome == false)
        {
            _sfx_manager?.PlaySFX(3);

            Drag.itemBeingDragged.transform.SetParent(_animalPrefabBehavior.ParentGameObject.transform);
            CheckBoxes[_turn - Level4Manager.Instance.turn].GetComponent<CustomCheckBox>().SetState(Enums.CheckState.Wrong);
            OnWrong?.Invoke();

        }

        if (Level4Manager.Instance.dropped == 3)
        {
            --Level4Manager.Instance.turn;
            if (Level4Manager.Instance.turn > 0)
            {
                _sfx_manager?.PlaySFX(2);
                Level4Manager.Instance.order = !Level4Manager.Instance.order;
                Level4Manager.Instance.CreateGame();
            }
            else
            {
                _sceneloader.LoadSceneAsSingle("ScoreCard");
            }
        }
    }


}
