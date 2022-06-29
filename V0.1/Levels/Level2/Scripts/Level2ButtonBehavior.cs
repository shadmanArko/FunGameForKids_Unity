using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class Level2ButtonBehavior : MonoBehaviour
{
    
    private int _amount;
  

    public void Create_UI(int amount, Sprite uiSprite)
    {
      
        _amount = amount;

        for (int i = 0; i < amount; i++)
        {
            
            var instance = new GameObject(i.ToString());
            instance.transform.parent = transform;
            instance.transform.localScale=Vector3.one;
            instance.transform.localPosition=Vector3.one;
            instance.AddComponent<Image>().sprite = uiSprite;
            
        }

    }

    public void Evaluate()
    {
        Level2GameManager.Instance.EvaluateScore(_amount);
    }
}
