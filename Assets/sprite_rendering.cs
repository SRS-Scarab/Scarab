using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using System.IO;

public class TextureEncoder : MonoBehaviour
{
    public RenderTexture target;
    public int fps;

    void Start()
    {
        Time.captureFramerate = fps;
    }

    void Update()
    {
        string framePath = $"{Application.dataPath}/Sprites/playerSprite.png";
        Texture2D tex = new Texture2D(target.width, target.height, TextureFormat.RGBA32, false);
        RenderTexture.active = target;
        tex.ReadPixels(new Rect(0, 0, target.width, target.height), 0, 0);
        tex.Apply();
        byte[] data = tex.EncodeToPNG();
        UnityEngine.Object.Destroy(tex);
        File.WriteAllBytes(framePath, data);
        Debug.Log(framePath);
    }
}