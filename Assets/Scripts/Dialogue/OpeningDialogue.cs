using System.Collections;
using SlimUI.ModernMenu;
using UnityEngine;
using UnityEngine.Video;
using TMPro;

public class OpeningDialogue : MonoBehaviour
{
    public TextMeshProUGUI textComponent;

    [SerializeField]
    private VideoClip[] clips;
    
    [SerializeField] private string[] lines;
    [SerializeField] private float textSpeed;
    [SerializeField] private string firstScene;

    [SerializeField]
    private UIMenuManager manager;

    [SerializeField]
    private bool loaded;
    
    private VideoPlayer videoPlayer;

    private int index;

    // Start is called before the first frame update
    void Start()
    {
        videoPlayer = GetComponent<VideoPlayer>();
        textComponent.text = string.Empty;
        videoPlayer.clip = clips[index];
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
            videoPlayer.clip = clips[index];
            if (lines[index] != lines[index - 1])
            {
                textComponent.text = string.Empty;
                StopAllCoroutines();
                StartCoroutine(TypeLine());
            }
        }
        else
        {
            // Load the next scene here
            LoadNextScene();
        }
    }

    void LoadNextScene()
    {
        if (loaded) return;
        // Assuming the next scene is named "NextScene"
        manager.LoadScene(firstScene);
        loaded = true;
    }
}
