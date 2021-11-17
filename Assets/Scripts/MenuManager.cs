using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MenuManager : MonoBehaviour
{
    [SerializeField] private GameObject menu;
    [SerializeField] private PlayerInput[] players;

    public void QuitGame()
    {
        Application.Quit();
    }

    public void Restart() 
    {
        menu.SetActive(false);
    }
}
