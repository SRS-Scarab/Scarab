#nullable enable
using System;
using UnityEngine;

[CreateAssetMenu(menuName = "Stage/Actor")]
public class StageActor : ScriptableObject
{
    public string actorName = string.Empty;
    public Sprite? defaultSprite;
    public StageActorExpression[] expressions = Array.Empty<StageActorExpression>();
}
