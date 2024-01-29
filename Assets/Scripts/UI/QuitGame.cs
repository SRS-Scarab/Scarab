using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SceneChange : MonoBehaviour
{
    private Button exitButton;
    // Start is called before the first frame update
    void Start()
    {
        exitButton = GetComponent<Button>();
        exitButton.onClick.AddListener(ExitGame);
    }
    public void ExitGame()
    {
        Debug.Log("Quit");
        PlayerPrefs.DeleteAll();
        Application.Quit();
    }
}
