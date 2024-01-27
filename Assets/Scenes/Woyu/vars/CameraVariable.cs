#nullable enable
using UnityEngine;

[CreateAssetMenu(menuName = "Variables/Camera")]
public class CameraVariable : ScriptableVariable<Camera?>
{
    [SerializeField] private Camera? camera;

    public override Camera? Provide() => camera;

    public override void Consume(Camera? value) => camera = value;
}
