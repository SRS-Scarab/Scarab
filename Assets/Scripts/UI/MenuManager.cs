using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UI;

public class MenuManager : MonoBehaviour
{
    [SerializeField] private InputSubsystem inputSubsystem;
    [SerializeField] private ActionsVariable actions;
    [SerializeField] private GameObject pauseMenu;
    [SerializeField] private GameObject settingsMenu;
    private string activeMenu = "pause"; // only have settings and pause as options
    private Dictionary<string, GameObject> menuMap = new Dictionary<string, GameObject>();
    private bool active;
    
    void Start()
    {
        active = false;
        pauseMenu.SetActive(active);
        settingsMenu.SetActive(active);
        actions.Provide().Gameplay.Menu.performed += openMenuCallback;
        menuMap.Add("pause", pauseMenu);
        menuMap.Add("settings", settingsMenu);
    }
    public void setActiveMenu(string menu){
        if (menu == activeMenu) openMenu();
        else {
            if (activeMenu != null) menuMap[activeMenu].SetActive(false);
            activeMenu = menu;
            if (activeMenu != null) menuMap[activeMenu].SetActive(true);
        }
    }

    void openMenuCallback(InputAction.CallbackContext context)
    {
        openMenu();
    }

    void openMenu()
    {
        active = !active;
        if (active)
        {
            inputSubsystem.PushMap(nameof(Actions.UI));
            actions.Provide().Gameplay.Menu.performed -= openMenuCallback;
            actions.Provide().UI.CloseMenu.performed += openMenuCallback;
            Time.timeScale = 0;
        }
        else
        {
            inputSubsystem.PopMap();
            actions.Provide().Gameplay.Menu.performed += openMenuCallback;
            actions.Provide().UI.CloseMenu.performed -= openMenuCallback;
            Time.timeScale = 1;
        }
        if (activeMenu != null) menuMap[activeMenu].SetActive(active);
    }
}
