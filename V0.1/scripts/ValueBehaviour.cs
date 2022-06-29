using TMPro;
using UnityEngine;

public class ValueBehaviour : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI text;
    public int Value;

    public void SetValue(int i)
    {
        Value = i;
        text.text = i.ToString();
    }
}
