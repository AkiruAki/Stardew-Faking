using PlayerStuff;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class Drag : MonoBehaviour
{
    ItemStats itemInStash;
    private float minDistanceEndDrag = 150f;
    [SerializeField]
    private Canvas canvas;
    public int myNumberSlot;
    [SerializeField]
    QuickInventoryScript script;

    Image imageItem;

    private void Start()
    {
        imageItem = GetComponent<Image>();
    }

    public void DragHandler(BaseEventData data)
    {
        PointerEventData pointerData = data as PointerEventData;

        Vector2 position;
        RectTransformUtility.ScreenPointToLocalPointInRectangle((RectTransform)canvas.transform, pointerData.position, canvas.worldCamera, out position);

        transform.position = canvas.transform.transform.TransformPoint(position);
    }

    // Check if the end of drag has ended with another 
    public void DragEndHandler(BaseEventData data)
    {
        for (int i = 0; i < script.NumberSlots(); i++)
        {
            if (i == myNumberSlot)
                continue;

            float distance = Vector3.Distance(transform.position, script.PositionOther(i));
            if (minDistanceEndDrag > distance)
            {
                script.ExchangeSlots(myNumberSlot, i);
                return;
            }
        }

        transform.position = script.PositionOther(myNumberSlot);
    }

    public void SetNumberSlot(int i)
    {
        myNumberSlot = i;
    }

    public void ChangeInformation(Vector3 Position)
    {
        transform.position = Position;
    }
}
