#nullable enable
using UnityEngine;

public class TextureAnimator : MonoBehaviour
{
    public Renderer? target;
    public TextureAnimationClip? current;

    [SerializeField]
    private float time;
    
    private void Update()
    {
        if (target != null)
        {
            if (current == null)
            {
                time = 0;
            }
            else
            {
                var timePerFrame = 1 / current.framesPerSecond;
                var frame = (int)(time / timePerFrame) % current.textures.Length;
                target.material.mainTexture = current.textures[frame];
                time += Time.deltaTime;
            }
        }
    }
}
