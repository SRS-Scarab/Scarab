using UnityEngine;

[CreateAssetMenu(menuName = "Variables/Game Object")]
public class GameObjectVariable : ScriptableVariable<GameObject>
{
    [SerializeField] private GameObject obj;

    public override GameObject Provide() => obj;

    public override void Consume(GameObject value) => obj = value;
}
