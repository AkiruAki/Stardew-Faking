using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class ChangeQuantityItem : MonoBehaviour
{
    [SerializeField]
    TextMeshProUGUI textQuantity;

    public void ChangeQuantity(int i)
    {
        textQuantity.text = i.ToString();
    }
}
