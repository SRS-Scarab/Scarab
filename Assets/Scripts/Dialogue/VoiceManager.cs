using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AddressableAssets;
using UnityEngine.ResourceManagement.AsyncOperations;
using Yarn.Unity;

public class VoiceManager : MonoBehaviour
{
    private Dictionary<string, AudioClip> audioClips = new Dictionary<string, AudioClip>();

    void Start()
    {
        var dialogueRunner = FindObjectOfType<DialogueRunner>();
        dialogueRunner.AddCommandHandler<string>("play-audio", PlayAudio);
    }

    public void PlayAudio(string address)
    {
        if (audioClips.TryGetValue(address, out var clip))
        {
            // If the clip is already loaded, play it
            SoundFXManager.instance.PlaySound(clip);
        }
        else
        {
            // Load the audio clip asynchronously
            Addressables.LoadAssetAsync<AudioClip>(address).Completed += handle => OnAudioClipLoaded(handle, address);
        }
    }

    private void OnAudioClipLoaded(AsyncOperationHandle<AudioClip> handle, string address)
    {
        if (handle.Status == AsyncOperationStatus.Succeeded)
        {
            var clip = handle.Result;
            audioClips[address] = clip;

            // Play the loaded audio clip
            SoundFXManager.instance.PlaySound(clip);
        }
        else
        {
            Debug.LogError($"Failed to load audio clip from address: {address}");
        }
    }
}
