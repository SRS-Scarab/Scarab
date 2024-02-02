using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UI;

public class Settings : MonoBehaviour
{
    public InputSubsystem inputSubsystem;
    public ActionsVariable actions;
    public GameObject settings;
    private Button settingsButton;
    private bool active;
    // Start is called before the first frame update
    void Start()
    {
        settingsButton = GetComponent<Button>();
        active = false;
        settings.SetActive(active);
        settingsButton.onClick.AddListener(openSettings);
        actions.Provide().Gameplay.Menu.performed += openSettingsCallback;
    }

    void openSettingsCallback(InputAction.CallbackContext context)
    {
        openSettings();
    }

    void openSettings()
    {
        active = !active;
        if (active)
        {
            inputSubsystem.PushMap(nameof(Actions.UI));
            actions.Provide().Gameplay.Menu.performed -= openSettingsCallback;
            actions.Provide().UI.CloseMenu.performed += openSettingsCallback;
        }
        else
        {
            inputSubsystem.PopMap();
            actions.Provide().Gameplay.Menu.performed += openSettingsCallback;
            actions.Provide().UI.CloseMenu.performed -= openSettingsCallback;
        }
        settings.SetActive(active);
    }
}
