using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AddressableAssets;
using UnityEngine.ResourceManagement.AsyncOperations;
using Yarn.Unity;

public class VoiceManager : MonoBehaviour
{
    private Dictionary<string, AudioClip> audioClips = new Dictionary<string, AudioClip>();
    [SerializeField] private AudioSource voiceLineObject;
    [SerializeField] private Camera mainCamera;
    private AudioSource audioSource;

    void Start()
    {
        var dialogueRunner = FindObjectOfType<DialogueRunner>();
        dialogueRunner.AddCommandHandler<string, float>("play-audio", PlayAudio);
        dialogueRunner.onDialogueComplete.AddListener(OnCompleteDialogue);
    }

    public void PlayAudio(string address, float volume = 1f)
    {
        if (audioClips.TryGetValue(address, out var clip))
        {
            // If the clip is already loaded, play it
            PlayVoiceLine(clip, volume);
        }
        else
        {
            // Load the audio clip asynchronously
            Addressables.LoadAssetAsync<AudioClip>(address).Completed += handle => OnAudioClipLoaded(handle, address, volume);
        }
    }
    public void PlayVoiceLine(AudioClip audioClip, float volume)
    {
        if(audioSource != null) {
            audioSource.Stop();
            Destroy(audioSource.gameObject, audioSource.clip.length);
        }
        audioSource = Instantiate(voiceLineObject, mainCamera.transform.position, Quaternion.identity);
        audioSource.clip = audioClip;
        audioSource.volume = volume;
        audioSource.Play();
        Destroy(audioSource.gameObject, audioSource.clip.length);
    }
    
    public void OnCompleteDialogue(){
        if(audioSource != null) {
            audioSource.Stop();
            Destroy(audioSource.gameObject, audioSource.clip.length);
        }
    }

    private void OnAudioClipLoaded(AsyncOperationHandle<AudioClip> handle, string address, float volume)
    {
        if (handle.Status == AsyncOperationStatus.Succeeded)
        {
            var clip = handle.Result;
            audioClips[address] = clip;

            // Play the loaded audio clip
            PlayVoiceLine(clip, volume);
        }
        else
        {
            Debug.LogError($"Failed to load audio clip from address: {address}");
        }
    }
}
