using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TimerDeActivateObject : MonoBehaviour
{
    // Script to activate or deactivate based on time
    [SerializeField]
    bool callOnStart, activateQuestion;
    [SerializeField]
    GameObject DeActivation;

    [SerializeField]
    float timer;
    // Start is called before the first frame update
    void Start()
    {
        if (callOnStart)
            StartCoroutine(TimerDeActivation());
    }
    
    public void AnotherObjectDeActivate(GameObject gameObject, float time, bool tipeDeactivation)
    {
        StartCoroutine(TimerDeActivation(gameObject, time, tipeDeactivation));
    }

    public void CallDeActivation()
    {
        StartCoroutine(TimerDeActivation());
    }

    IEnumerator TimerDeActivation()
    {
        yield return new WaitForSeconds(timer);
        if (!activateQuestion)
            DeActivation.SetActive(false);
        else
            DeActivation.SetActive(true);
    }

    IEnumerator TimerDeActivation(GameObject c, float t, bool d)
    {
        yield return new WaitForSeconds(t);
        if (d)
            c.SetActive(true);
        else
            c.SetActive(false);
    }
}
