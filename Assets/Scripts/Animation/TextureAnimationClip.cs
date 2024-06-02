#nullable enable
using System;
using UnityEngine;

[CreateAssetMenu(menuName = "Texture Animation Clip")]
public class TextureAnimationClip : ScriptableObject
{
    public float framesPerSecond = 10;
    public Texture2D[] textures = Array.Empty<Texture2D>();
}
