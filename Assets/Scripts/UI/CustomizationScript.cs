using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEditor;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.SceneManagement;

public class CustomizationScript : MonoBehaviour
{
    public UnityEvent OnCharacterGenderChange;
    [System.Serializable]
    public class OnCharacterSkinChange: UnityEvent<Color>
    {

    }
    public OnCharacterSkinChange onCharacterSkinChange;
    [SerializeField]
    Color[] posibleColors;

    [SerializeField]
    Animator canvasController;
    [SerializeField]
    AudioSource audioSource;

    TimerDeActivateObject timerDeActivateObject;

    private void Start()
    {
        timerDeActivateObject = GetComponent<TimerDeActivateObject>();
    }

    public void InvokeChangeGender()
    {
        OnCharacterGenderChange?.Invoke();
    }

    public void InvokeChangeSkinColor(int color)
    {
        onCharacterSkinChange?.Invoke(posibleColors[color]);
    }

    public void NewGameClick(GameObject c)
    {
        canvasController.SetTrigger("ClickOnMainMenu");
        canvasController.SetBool("NewGame", true);
    }

    public void StartNewGame()
    {
        StartCoroutine(LoadAsyncScene());
    }

    public void GoBackNewGame()
    {
        canvasController.SetTrigger("ClickOnMainMenu");
        canvasController.SetBool("NewGame", false);
    }

    public void LoadGame()
    {
        canvasController.SetTrigger("ClickOnMainMenu");
        canvasController.SetBool("LoadGame", true);

        StartCoroutine(LoadAsyncScene());
        // Add stuff loading
    }

    public void StartSettings()
    {
        canvasController.SetTrigger("ClickOnMainMenu");
        canvasController.SetBool("Settings", true);

    }

    public void GoBackSettings()
    {
        canvasController.SetTrigger("ClickOnMainMenu");
        canvasController.SetBool("Settings", false);
    }

    IEnumerator LoadAsyncScene()
    {
        yield return new WaitForSeconds(10f);

        AsyncOperation asyncLoad = SceneManager.LoadSceneAsync("MainGame");

        while (!asyncLoad.isDone)
        {
            yield return null;
        }

    }

    public void ChangeFullScreen()
    {
        Screen.fullScreen = !Screen.fullScreen;
    }

    public void ChangeMuteOrNot()
    {
        audioSource.mute = !audioSource.mute;
    }
}
