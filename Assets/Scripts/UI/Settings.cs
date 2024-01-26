using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Settings : MonoBehaviour
{
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
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown("escape"))
        {
            openSettings();
        }
    }
    void openSettings()
    {
        active = !active;
        settings.SetActive(active);
    }
}
