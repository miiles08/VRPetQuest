using UnityEngine;
using System.Collections.Generic;

public class UIScreenManager : MonoBehaviour
{
    public GameObject mainMenu;
    public GameObject shopScreen;
    public GameObject statsScreen;

    public void OpenShop()
    {
        mainMenu.SetActive(false);
        shopScreen.SetActive(true);
        statsScreen.SetActive(false);
    }

    public void OpenStats()
    {
        mainMenu.SetActive(false);
        shopScreen.SetActive(false);
        statsScreen.SetActive(true);
    }

    public void BackToMenu()
    {
        mainMenu.SetActive(true);
        shopScreen.SetActive(false);
        statsScreen.SetActive(false);
    }
}