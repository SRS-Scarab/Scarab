#nullable enable
using UnityEngine;
using UnityEngine.InputSystem;

[CreateAssetMenu(menuName = "Items/Weapon")]
public class WeaponType : ItemType
{
    public CameraVariable? camVar;
    public AttackPrefab? attack;

    public override void OnItemUse(CombatEntity playerEntity, Inventory inventory, int index)
    {
        if (camVar != null && camVar.Provide() != null)
        {
            var mousePos = Mouse.current.position.ReadValue();
            var center = new Vector2(Screen.width / 2f, Screen.height / 2f);
            var angle = -Vector2.SignedAngle(Vector2.right, mousePos - center);
            // todo somehow get attack position
            if (attack != null)
            {
                attack.TryInstantiate(playerEntity, playerEntity.transform.position + Vector3.up, angle);
            }
        }
    }
}
