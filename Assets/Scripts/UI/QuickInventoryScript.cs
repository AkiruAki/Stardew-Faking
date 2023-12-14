using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class QuickInventoryScript : MonoBehaviour
{
    [SerializeField]
    ChangeQuantityItem[] quantityItem;
    [SerializeField]
    Color standardColor, selectedColor;
    [SerializeField]
    Image[] imagesSlots;
    [SerializeField]
    GameObject allInventory;
    InventoryUpdateUI inventoryUpdateUI;
    [SerializeField]
    int numberSlots = 8;
    [SerializeField]
    Drag[] dragSlots;
    [SerializeField]
    Vector3[] initialPositions;
    [SerializeField]
    Sprite[] bucketStates; 

    int actualSelected = 0;

    void Start()
    {
        imagesSlots[actualSelected].color = selectedColor;
        initialPositions = new Vector3[numberSlots];
        dragSlots = new Drag[numberSlots];
        for (int i = 0; i < numberSlots; i++)
        {
            initialPositions[i] = allInventory.transform.GetChild(i).position;
            dragSlots[i] = allInventory.transform.GetChild(i).GetComponent<Drag>();
            dragSlots[i].SetNumberSlot(i);
        }
        inventoryUpdateUI = allInventory.GetComponent<InventoryUpdateUI>();
    }
    public void SelectSlot(int i)
    {
        imagesSlots[actualSelected].color = standardColor;
        imagesSlots[i].color = selectedColor;
        actualSelected = i;
    }

    public void OpenCloseAllInventory(bool close)
    {
        if (close)
        {
            ManagerCharacterAndObjectives.instance.AllowControls();
            allInventory.SetActive(false);
        }
        else
            allInventory.SetActive(true);
    }

    public int NumberSlots()
    {
        return numberSlots;
    }

    public Vector3 PositionOther(int i)
    {
        return initialPositions[i];
    }

    public void ExchangeSlots(int a, int b)
    {
        dragSlots[a].ChangeInformation(initialPositions[b]);
        dragSlots[b].ChangeInformation(initialPositions[a]);
        dragSlots[a].SetNumberSlot(b);
        dragSlots[b].SetNumberSlot(a);

        Drag t = dragSlots[a];
        dragSlots[a] = dragSlots[b];
        dragSlots[b] = t;

        ManagerCharacterAndObjectives.instance.ExchangeInventorySlots(a, b);
    }

    public void FillBucket()
    {
        imagesSlots[0].sprite = bucketStates[1];
    }

    public void EmptyBucket()
    {
        imagesSlots[0].sprite = bucketStates[0];
    }

    public void SetQuantityItem(int i, int q)
    {
        quantityItem[i].ChangeQuantity(q);
    }

    // clean this code to a event
    public void AddVisualInventory(int index, Sprite sprite, int quantity)
    {
        inventoryUpdateUI.ChangeNewElement(index, sprite, quantity);
    }
    public void AddVisualInventory(int index, int quantity)
    {
        inventoryUpdateUI.ChangeQuantity(index, quantity);
    }
}
