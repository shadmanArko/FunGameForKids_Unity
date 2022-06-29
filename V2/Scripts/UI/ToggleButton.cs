using System;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class ToggleButton : MonoBehaviour
{
    Image image;

    public bool On;

    public Sprite OnSprite;
    public Sprite OffSprite;

    public BoolEvent OnToggle;
    

    private void Start()
    {
        image = GetComponent<Image>();
        On = SFXManager.Instance.isOn;
        image.sprite = On ? OnSprite : OffSprite;
    }

    public void ToggleState()
    {
        On = !On;
        image.sprite = On ? OnSprite : OffSprite;
        OnToggle?.Invoke(On);
    }

}

[Serializable]
public class BoolEvent:UnityEvent<bool>{ }
