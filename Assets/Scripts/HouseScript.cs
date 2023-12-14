using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HouseScript : MonoBehaviour
{
    [SerializeField]
    GameObject canvasLoad;

    Animator animatorCanvas;
    private void Start()
    {
        animatorCanvas = canvasLoad.GetComponent<Animator>();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        canvasLoad.SetActive(true);
        animatorCanvas.SetTrigger("ShowMessageSleep");
        ManagerCharacterAndObjectives.instance.BlockControlls();
        ManagerCharacterAndObjectives.instance.PassADay();
    }
}
