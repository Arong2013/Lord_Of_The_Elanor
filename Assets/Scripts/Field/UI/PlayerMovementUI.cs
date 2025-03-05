using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
public class PlayerMovementUI : MonoBehaviour, IPointerDownHandler,IPlayerUesableUI
{
    PlayerUnit playerUnit;
    public void Initialize(PlayerUnit playerUnit)
    {
        this.playerUnit = playerUnit;
    }
    public void OnPointerDown(PointerEventData eventData)
    {
        var playerDIR = GetPlayerDir(eventData);
        playerUnit.InputMove(playerDIR); 
    }
    public Vector2Int GetPlayerDir(PointerEventData eventData)
    {
        Vector2 touchPosition = eventData.position;
        Vector2 playerScreenPosition = Camera.main.WorldToScreenPoint(playerUnit.transform.position);

        Vector2 direction = Vector2.zero;

        float deltaX = touchPosition.x - playerScreenPosition.x;
        float deltaY = touchPosition.y - playerScreenPosition.y;

        if (Mathf.Abs(deltaX) > Mathf.Abs(deltaY))
        {
            if (deltaX > 0)
                direction = Vector2.right;
            else
                direction = Vector2.left;
        }
        else
        {
            if (deltaY > 0)
                direction = Vector2.up;
            else
                direction = Vector2.down;
        }
        Debug.Log($"터치 방향: {direction}");
        return  Utils.ToVector2Int(direction);
    }

}
