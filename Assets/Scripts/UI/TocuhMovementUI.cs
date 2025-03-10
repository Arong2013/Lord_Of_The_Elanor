using System;
using UnityEngine;
using UnityEngine.EventSystems;
public class TouchMovementUI : MonoBehaviour,IPointerDownHandler, IPointerUpHandler, IDragHandler
{
    [SerializeField] private LayerMask groundLayer;
    private Vector3Int touchPos;
    private bool isTouching;

    private event Action<Vector3Int> OnPointerUpEvent;
    public void AddListener(Action<Vector3Int> listener)
    {
        OnPointerUpEvent += listener;
    }
    public void RemoveListener(Action<Vector3Int> listener)
    {
        OnPointerUpEvent -= listener;
    }

    public void OnPointerDown(PointerEventData eventData)
    {
        touchPos = Vector3Int.zero;
        isTouching = true;
        touchPos =  GetTouchPosition(eventData.position);
    }
    public void OnPointerUp(PointerEventData eventData)
    {
        isTouching = false;
        OnPointerUpEvent?.Invoke(touchPos);
    }
    public void OnDrag(PointerEventData eventData)
    {
        if (isTouching)
        {
           touchPos =   GetTouchPosition(eventData.position);
        }
    }
    public Vector3Int GetTouchPosition(Vector2 screenPosition)
    {
        Ray ray = Camera.main.ScreenPointToRay(screenPosition);
        if (Physics.Raycast(ray, out RaycastHit hit, Mathf.Infinity, groundLayer))
        {
            return Utils.ToVector3Int(hit.point);
        }
        return Vector3Int.zero;
    }
}
