using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;

public class SoundFXManager : MonoBehaviour
{
    public static SoundFXManager instance;
    [SerializeField] private AudioSource soundFXObject;
    private void Awake()
    {
        if (instance ==null) instance = this;
    }

    public void PlaySound(AudioClip audioClip, Transform transform, float volume)
    {
        AudioSource audioSource = Instantiate(soundFXObject, transform.position, Quaternion.identity);
        audioSource.clip = audioClip;
        audioSource.volume = volume;
        audioSource.Play();
        Destroy(audioSource, audioSource.clip.length);
    }
    
    public void PlayRandomSound(AudioClip[] audioClip, Transform transform, float volume)
    {
        int rand = Random.Range(0, audioClip.Length);
        AudioSource audioSource = Instantiate(soundFXObject, transform.position, Quaternion.identity);
        audioSource.clip = audioClip[rand];
        audioSource.volume = volume;
        audioSource.Play();
        Destroy(audioSource, audioSource.clip.length);
    }
}
