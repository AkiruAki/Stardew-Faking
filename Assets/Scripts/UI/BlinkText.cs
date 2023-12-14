using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
public class BlinkText : MonoBehaviour
{
    TextMeshProUGUI textToBlink;
    [SerializeField]
    Color initialColor, finalColor;
    [SerializeField]
    float timeOneBlink;
    bool initial = true;
    // Start is called before the first frame update
    void Start()
    {
        textToBlink = GetComponent<TextMeshProUGUI>();
        StartCoroutine(Blinking());
    }

    IEnumerator Blinking()
    {
        yield return new WaitForSeconds(timeOneBlink/2);
        if(initial)
            textToBlink.color = finalColor;
        else
            textToBlink.color = initialColor;
        initial = !initial;
        StartCoroutine(Blinking());
    }
}
