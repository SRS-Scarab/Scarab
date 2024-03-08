#nullable enable
using UnityEngine;
using UnityEngine.InputSystem;

[CreateAssetMenu(menuName = "Items/Weapon")]
public class WeaponType : ItemType
{
    public CameraVariable? camVar;
    public AttackInfo weaponAttack;

    public override void OnItemUse(CombatEntity playerEntity, Inventory inventory, int index)
    {
        if (camVar != null && camVar.Provide() != null)
        {
            var mousePos = camVar.Provide()!.ScreenToWorldPoint(Mouse.current.position.ReadValue());
            var playerPos = playerEntity.transform.position;
            var angle = Vector2.SignedAngle(Vector2.right, mousePos - playerPos);
            weaponAttack.TryAttack(playerEntity, playerPos, angle);
        }
    }
}
