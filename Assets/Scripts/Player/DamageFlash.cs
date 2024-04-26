using System.Collections;
using UnityEngine;
using UnityEngine.UI;
using System;

/// <summary>
/// This script will flash a UI Image component with the given parameters. Useful for creating
/// quick, animated UI flashes.
/// Created by: Adam Chandler
/// Make sure that you attach this script to an Image component. You can optionally call the
/// flash remotely and pass in new flash values, or you can predefine settings in the Inspector
/// </summary>

[RequireComponent(typeof(Image))]
public class DamageFlash : MonoBehaviour
{
    // events
    public event Action OnStop = delegate { };
    public event Action OnCycleStart = delegate { };
    public event Action OnCycleComplete = delegate { };
    private static DamageFlash instance;

    Coroutine _flashRoutine = null;
    Image _flashImage;

    #region Initialization
    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
        else
        {
            Destroy(this);
        }
        _flashImage = GetComponent<Image>();
        // initial state
        SetAlphaToDefault();
    }

    #endregion

    #region Public Functions

    public static void Flash(float secondsForOneFlash, float minAlpha, float maxAlpha, Color color)
    {
        if (secondsForOneFlash <= 0) { return; }    // 0 speed wouldn't make sense

        if (instance._flashRoutine != null)
            instance.StopCoroutine(instance._flashRoutine);
        instance._flashRoutine = instance.StartCoroutine(
            instance.FlashRoutine(secondsForOneFlash, minAlpha, maxAlpha, color)
            );
    }

    public static void StopFlash()
    {
        if (instance._flashRoutine != null)
            instance.StopCoroutine(instance._flashRoutine);

        SetAlphaToDefault();

        instance.OnStop?.Invoke();
    }
    #endregion

    #region Private Functions
    IEnumerator FlashRoutine(float secondsForOneFlash, float minAlpha, float maxAlpha, Color color)
    {
        SetColor(color);
        // half the flash time should be on flash in, the other half for flash out
        float flashInDuration = secondsForOneFlash / 2;
        float flashOutDuration = secondsForOneFlash / 2;

        OnCycleStart?.Invoke();
        // flash in
        for (float t = 0f; t <= flashInDuration; t += Time.deltaTime)
        {
            Color newColor = _flashImage.color;
            newColor.a = Mathf.Lerp(minAlpha, maxAlpha, t / flashInDuration);
            _flashImage.color = newColor;
            yield return null;
        }
        // flash out
        for (float t = 0f; t <= flashOutDuration; t += Time.deltaTime)
        {
            Color newColor = _flashImage.color;
            newColor.a = Mathf.Lerp(maxAlpha, minAlpha, t / flashOutDuration);
            _flashImage.color = newColor;
            yield return null;
        }
        SetAlphaToDefault();
        OnCycleComplete?.Invoke();
    }

    private void SetColor(Color newColor)
    {
        _flashImage.color = newColor;
    }

    private static void SetAlphaToDefault()
    {
        Color newColor = instance._flashImage.color;
        newColor.a = 0;
        instance._flashImage.color = newColor;
    }

    #endregion
}