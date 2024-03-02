#nullable enable
using UnityEngine;
using UnityEngine.Rendering.Universal;

public class TilemapShadowCaster : MonoBehaviour
{
    [SerializeField]
    private CompositeCollider2D tilemapCollider = null!;
    
    private void Start()
    {
        for (var i = 0; i < tilemapCollider.pathCount; i++)
        {
            var vertices = new Vector2[tilemapCollider.GetPathPointCount(i)];
            tilemapCollider.GetPath(i, vertices);
            var obj = new GameObject("Shadow Caster");
            obj.transform.SetParent(transform);
            var polygon = obj.AddComponent<PolygonCollider2D>();
            polygon.points = vertices;
            polygon.enabled = false;
            var caster = obj.AddComponent<ShadowCaster2D>();
            caster.selfShadows = true;
            obj.AddComponent<PolygonShadowCaster>();
        }
    }
}
