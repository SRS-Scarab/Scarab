#nullable enable
using UnityEngine;

public class FoliageGenerator : MonoBehaviour
{
    [Header("Dependencies")]
    
    [SerializeField] private CameraVariable? camVar;
    [SerializeField] private GameObject? foliagePrefab;

    [Header("Parameters")]
    
    [SerializeField] private float foliageSpacing = 5;
    
    [Header("State")]
    
    [SerializeField] private int leftBound = 0;
    [SerializeField] private int rightBound = 1;

    private void Update()
    {
        if (camVar == null || foliagePrefab == null) return;
        var cam = camVar.Provide();
        if (cam == null) return;
        var diff = cam.transform.position.x - transform.position.x;
        var left = Mathf.FloorToInt((diff - (cam.orthographicSize * Screen.width / Screen.height) * 1.5f) / foliageSpacing);
        var right = Mathf.FloorToInt((diff + (cam.orthographicSize * Screen.width / Screen.height) * 1.5f) / foliageSpacing);
        while (leftBound >= left)
        {
            var obj = Instantiate(foliagePrefab, transform);
            obj.transform.localPosition = Vector3.right * (leftBound * foliageSpacing);
            leftBound--;
        }
        while (rightBound <= right)
        {
            var obj = Instantiate(foliagePrefab, transform);
            obj.transform.localPosition = Vector3.right * (rightBound * foliageSpacing);
            rightBound++;
        }
    }
}
