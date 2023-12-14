using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class InventoryUpdateUI : MonoBehaviour
{
    [SerializeField]
    Image[] imagesInventory;

    public void ChangeNewElement(int index, Sprite spriteItem, int quantity)
    {
        imagesInventory[index].sprite = spriteItem;
        imagesInventory[index].GetComponentInChildren<TextMeshProUGUI>().text = quantity.ToString();
    }
    public void ChangeQuantity(int index, int quantity)
    {
        imagesInventory[index].GetComponentInChildren<TextMeshProUGUI>().text = quantity.ToString();
    }
}
