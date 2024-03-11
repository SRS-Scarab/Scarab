#nullable enable
using UnityEngine;

[CreateAssetMenu(menuName = "Attack")]
public class Attack : ScriptableObject
{
    public Sprite? attackIcon;
    public int attackValue;
}
