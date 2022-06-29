using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;

public class CustomCheckBox : MonoBehaviour
{
    public Enums.CheckState State;
    public List<StateImage> Icons;


    [SerializeField] private Image StateUI;

    public void SetState(Enums.CheckState state)
    {
        State = state;
        SetIcon();
    }

    private void SetIcon()
    {
        StateUI.sprite = Icons.First(r => r.State == State).Icon;
    }

}

[Serializable]
public class StateImage
{
    public Sprite Icon;
    public Enums.CheckState State;
}
