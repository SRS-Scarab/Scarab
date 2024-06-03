using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Video;
using UnityEngine.SceneManagement;
using TMPro;

public class OpeningDialogue : MonoBehaviour
{
    public TextMeshProUGUI textComponent;
    [SerializeField] private string[] lines;
    [SerializeField] private float textSpeed;
    [SerializeField] private string firstScene;
    private VideoPlayer videoPlayer;

    private int index;

    // Start is called before the first frame update
    void Start()
    {
        videoPlayer = GetComponent<VideoPlayer>();
        textComponent.text = string.Empty;
        videoPlayer.url = $"Assets/Videos/Dream_{index}.mov";
        StartDialogue();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            if (textComponent.text == lines[index])
            {
                NextLine();
            }
            else
            {
                StopAllCoroutines();
                textComponent.text = lines[index];
            }
        }
    }

    void StartDialogue()
    {
        index = 0;
        StartCoroutine(TypeLine());
    }

    IEnumerator TypeLine()
    {
        foreach (char c in lines[index].ToCharArray())
        {
            textComponent.text += c;
            yield return new WaitForSeconds(textSpeed);
        }
    }

    void NextLine()
    {
        if (index < lines.Length - 1)
        {
            index++;
            if (index != lines.Length -1) videoPlayer.url = $"Assets/Videos/Dream_{index}.mov";
            textComponent.text = string.Empty;
            StartCoroutine(TypeLine());
        }
        else
        {
            // Load the next scene here
            LoadNextScene();
        }
    }

    void LoadNextScene()
    {
        // Assuming the next scene is named "NextScene"
        SceneManager.LoadScene(firstScene);
    }
}
