using TMPro;
using UnityEngine;

public class Level7Buttonbehavior : MonoBehaviour
{
    
    [SerializeField] private TextMeshProUGUI buttonvalue;

    private int _amount;

    void Awake()
    {
        buttonvalue = GetComponentInChildren<TextMeshProUGUI>();
    }


    public void CreateUI(int amount)
    {
        _amount = amount;
        buttonvalue.text = _amount.ToString();
    }

   
}