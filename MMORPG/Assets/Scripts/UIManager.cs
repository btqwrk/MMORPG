using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class UIManager : MonoBehaviour
{
    [Header("Menu GameObjects")]
    public GameObject loginScreen;
    public GameObject settingsScreen;
    public GameObject loadingScreen;
    public GameObject characterSelectionScreen;


    public static UIManager instance;
    public UIs _uis;

    public enum UIs
    {
        LoginScreen = 1,
        SettingsScreen = 2,
        LoadingScreen = 3,
        CharacterSelectionScreen = 4
    }

    void Awake()
    {
        instance = this;
        _uis = UIs.LoginScreen;
    }

    // Update is called once per frame
    void Update()
    {
        switch(_uis)
        {
            case UIs.LoginScreen:
                loginScreen.SetActive(true);
                loadingScreen.SetActive(false);
                settingsScreen.SetActive(false);
                characterSelectionScreen.SetActive(false);
                
                break;
            case UIs.SettingsScreen:
                loginScreen.SetActive(false);
                loadingScreen.SetActive(false);
                settingsScreen.SetActive(true);
                characterSelectionScreen.SetActive(false);
                break;
            case UIs.LoadingScreen:
                loginScreen.SetActive(false);
                loadingScreen.SetActive(true);
                settingsScreen.SetActive(false);
                characterSelectionScreen.SetActive(false);
                break;
            case UIs.CharacterSelectionScreen:
                loginScreen.SetActive(false);   
                loadingScreen.SetActive(false);
                settingsScreen.SetActive(false);
                characterSelectionScreen.SetActive(true);
                break;
        }
    }

    public void ChangeMenu(int ui)
    {
        _uis = (UIs)ui;
    }

    public void DoExitGame()
    {
        Application.Quit();
    }
}
