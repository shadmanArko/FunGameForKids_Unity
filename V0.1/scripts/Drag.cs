using TMPro;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.EventSystems;

public class Drag : MonoBehaviour, IBeginDragHandler, IDragHandler, IEndDragHandler
{
    public static GameObject itemBeingDragged;
    Vector3 startPosition;
    Transform startParent;
    public Camera camera;
    public UnityEvent OnBeginDragEvent;
    public UnityEvent OnEndDragEvent;
    private CanvasGroup _canvasGroup;
    private SFXManager _sfx_manager;

    
    private void Start()
    {
        _canvasGroup = GetComponent<CanvasGroup>();
        _sfx_manager = SFXManager.Instance;
        if (_sfx_manager != null)
        {
            OnBeginDragEvent.AddListener(() => _sfx_manager.PlaySFX(0));

        }
    }
    public void OnBeginDrag(PointerEventData eventData)
    {

        itemBeingDragged = gameObject;
        startPosition = transform.position;
        startParent = transform.parent;
        _canvasGroup.blocksRaycasts = false;
        OnBeginDragEvent?.Invoke();
    }

    public void OnEndDrag(PointerEventData eventData)
    {

        itemBeingDragged = null;
        _canvasGroup.blocksRaycasts = true;
        if (transform.parent == startParent)
        {
            transform.position = startPosition;
        }
        OnEndDragEvent?.Invoke();
    }

    public void OnDrag(PointerEventData eventData)
    {

        Vector3 position = camera.ScreenToWorldPoint(Input.mousePosition);
        transform.position = new Vector3(position.x, position.y);
    }

    private void OnDestroy()
    {
        OnBeginDragEvent.RemoveListener(() => _sfx_manager.PlaySFX(0));

    }


}
