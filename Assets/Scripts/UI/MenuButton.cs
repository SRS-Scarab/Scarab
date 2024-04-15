using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UI;

public class MenuButton : MonoBehaviour
{
    [SerializeField] private MenuManager menuManager;
    [SerializeField] private string? menuName;
    private Button menuButton;
    private bool active;
    
    void Start()
    {
        menuButton = GetComponent<Button>();
        menuButton.onClick.AddListener(openPauseMenu);
    }

    void openPauseMenu()
    {
        menuManager.setActiveMenu(menuName);
    }
}
