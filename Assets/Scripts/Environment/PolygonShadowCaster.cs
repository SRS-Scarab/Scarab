#nullable enable
using System.Linq;
using System.Reflection;
using UnityEngine;
using UnityEngine.Rendering.Universal;

public class PolygonShadowCaster : MonoBehaviour
{
    private void Start()
    {
        var polygon = GetComponent<PolygonCollider2D>();
        var caster = GetComponent<ShadowCaster2D>();
        var type = caster.GetType();
        var shapeField = type.GetField("m_ShapePath", BindingFlags.NonPublic | BindingFlags.Instance);
        var shapeHashField = type.GetField("m_ShapePathHash", BindingFlags.NonPublic | BindingFlags.Instance);
        var meshField = type.GetField("m_Mesh", BindingFlags.NonPublic | BindingFlags.Instance);
        var generateMethod = type.Assembly.GetType("UnityEngine.Rendering.Universal.ShadowUtility").GetMethod("GenerateShadowMesh", BindingFlags.Public | BindingFlags.Static);
        shapeField!.SetValue(caster, polygon.GetPath(0).ToList().ConvertAll(e => new Vector3(e.x, e.y)).ToArray());
        shapeHashField!.SetValue(caster, Random.Range(int.MinValue, int.MaxValue));
        meshField!.SetValue(caster, new Mesh());
        generateMethod!.Invoke(caster, new[] { meshField.GetValue(caster), shapeField.GetValue(caster) });
    }
}
