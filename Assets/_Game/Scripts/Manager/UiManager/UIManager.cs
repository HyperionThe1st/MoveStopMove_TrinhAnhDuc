using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIManager : MonoBehaviour
{
    public static UIManager UIInstance;
    private GameObject currentScreen;
    Stack<GameObject> stackScreen;
    [SerializeField] private GameObject gamePlayScreen;
    [SerializeField] private GameObject mainMenuScreen;

    public void ChangeUIState(GameObject _screen)
    {
        if (currentScreen != _screen && currentScreen != null)
        {
            currentScreen.SetActive(false);
            currentScreen = _screen;
            currentScreen.SetActive(true);
        }
        else if (currentScreen == null)
        {
            currentScreen = _screen;
            currentScreen.SetActive(true);
        }
    }

    private void Start()
    {
        UIInstance = this;
        ShowMainMenu(true);
        ShowPlayScreen(false);
        stackScreen = new Stack<GameObject>();
        stackScreen.Push(mainMenuScreen);
    }
    public void ShowPlayScreen(bool isTrue)
    {
        gamePlayScreen.SetActive(isTrue);
    }
    public void ShowMainMenu(bool isTrue)
    {
        mainMenuScreen.SetActive(isTrue);
    }

    public void SetScreen()
    {

    }
}
