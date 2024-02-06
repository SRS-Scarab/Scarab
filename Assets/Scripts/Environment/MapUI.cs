#nullable enable
using System;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UI;

public class MapUI : MonoBehaviour
{
    [Header("Dependencies")]
    
    [SerializeField] private InputSubsystem? inputSubsystem;
    [SerializeField] private ActionsVariable? actionsVar;
    [SerializeField] private GameObjectVariable? playerVar;
    [SerializeField] private TilemapVariable? tilemapVar;
    [SerializeField] private CanvasGroup? group;
    [SerializeField] private RawImage? targetImage;
    
    [Header("Parameters")]
    
    [SerializeField] private int defaultZoomLevel = 5;
    [SerializeField] private int minZoomLevel = 2;
    [SerializeField] private int maxZoomLevel = 8;
    [SerializeField] private bool fade = true;
    
    [Header("State")]
    
    [SerializeField] private int zoomLevel;
    [NonSerialized] private Texture2D? _texture;
    
    private void Start()
    {
        if (actionsVar != null) actionsVar.Provide().Gameplay.Map.performed += Activate;
        if (group != null)
        {
            group.alpha = 0;
            group.interactable = false;
            group.blocksRaycasts = false;
        }
        zoomLevel = Mathf.Clamp(defaultZoomLevel, minZoomLevel, maxZoomLevel);;
        if (targetImage != null) CreateMapTexture();
    }

    private void Update()
    {
        if (_texture == null || _texture.width != GetTextureLength()) CreateMapTexture();
        if (playerVar != null && playerVar.Provide() != null && tilemapVar != null && tilemapVar.Provide() != null && _texture != null)
        {
            var tilemap = tilemapVar.Provide()!;
            var pos = tilemap.layoutGrid.WorldToCell(playerVar.Provide()!.transform.position);
            var centerX = _texture.width / 2;
            var centerY = _texture.height / 2;
            for (var x = 0; x < _texture.width; x++)
            {
                for (var y = 0; y < _texture.height; y++)
                {
                    var tilePos = new Vector3Int(pos.x + x - centerX, pos.y + y - centerY, 0);
                    var tile = tilemap.GetTile<MapTile>(tilePos);
                    if (tile == null) _texture.SetPixel(x, y, Color.clear);
                    else
                    {
                        var color = tile.GetMapColor();
                        var dist = new Vector2(centerX - x, centerY - y).magnitude;
                        if (fade) color.a = Mathf.Clamp(1 - 2 * dist / GetTextureLength(), 0, 1);
                        _texture.SetPixel(x, y, color);
                    }
                }
            }
            _texture.Apply();
        }
    }

    private void Activate(InputAction.CallbackContext context)
    {
        if (inputSubsystem != null) inputSubsystem.PushMap(nameof(Actions.UI));
        if (actionsVar != null)
        {
            actionsVar.Provide().Gameplay.Map.performed -= Activate;
            actionsVar.Provide().UI.CloseMap.performed += Deactivate;
            actionsVar.Provide().UI.ZoomIn.performed += ZoomIn;
            actionsVar.Provide().UI.ZoomOut.performed += ZoomOut;
        }
        if (group != null)
        {
            group.alpha = 1;
            group.interactable = true;
            group.blocksRaycasts = true;
        }
    }

    private void Deactivate(InputAction.CallbackContext context)
    {
        if (inputSubsystem != null) inputSubsystem.PopMap();
        if (actionsVar != null)
        {
            actionsVar.Provide().Gameplay.Map.performed += Activate;
            actionsVar.Provide().UI.CloseMap.performed -= Deactivate;
            actionsVar.Provide().UI.ZoomIn.performed -= ZoomIn;
            actionsVar.Provide().UI.ZoomOut.performed -= ZoomOut;
        }
        if (group != null)
        {
            group.alpha = 0;
            group.interactable = false;
            group.blocksRaycasts = false;
        }
    }

    private void ZoomIn(InputAction.CallbackContext context)
    {
        zoomLevel = Mathf.Clamp(zoomLevel - 1, minZoomLevel, maxZoomLevel);
    }
    
    private void ZoomOut(InputAction.CallbackContext context)
    {
        zoomLevel = Mathf.Clamp(zoomLevel + 1, minZoomLevel, maxZoomLevel);
    }

    private void CreateMapTexture()
    {
        if (targetImage != null)
        {
            _texture = new Texture2D(GetTextureLength(), GetTextureLength(), TextureFormat.ARGB32, false)
            {
                filterMode = FilterMode.Point,
                wrapMode = TextureWrapMode.Clamp
            };
            targetImage.texture = _texture;
        }
    }

    private int GetTextureLength() => (1 << zoomLevel) + 1;
}
