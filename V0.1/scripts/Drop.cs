using TMPro;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.EventSystems;

public class Drop : MonoBehaviour, IDropHandler
{
    public UnityEvent OnDropEvent;

    public bool isLocked;
    public bool LockOnDrop;

    public void OnDrop(PointerEventData eventData)
    {
        if (LockOnDrop)
        {
            if (isLocked) return;
            isLocked = true;
            Drag.itemBeingDragged.GetComponent<Drag>().enabled = false;
        }
       
        Drag.itemBeingDragged.transform.SetParent(transform);
        OnDropEvent?.Invoke();
    }
}
