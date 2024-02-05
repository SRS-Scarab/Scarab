#nullable enable
using System;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.Tilemaps;
using UnityEngine.UI;

public class MapUI : MonoBehaviour
{
    [Header("Dependencies")]
    
    [SerializeField] private InputSubsystem? inputSubsystem;
    [SerializeField] private ActionsVariable? actionsVar;
    [SerializeField] private GameObjectVariable? playerVar;
    [SerializeField] private Tilemap? tilemap;
    [SerializeField] private CanvasGroup? group;
    [SerializeField] private RawImage? targetImage;
    
    [Header("State")]
    
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

        if (targetImage != null)
        {
            _texture = new Texture2D(64, 32, TextureFormat.ARGB32, false)
            {
                filterMode = FilterMode.Point,
                wrapMode = TextureWrapMode.Clamp
            };
            targetImage.texture = _texture;
        }
    }

    private void Update()
    {
        if (playerVar != null && playerVar.Provide() != null && tilemap != null && _texture != null)
        {
            var pos = tilemap.layoutGrid.WorldToCell(playerVar.Provide()!.transform.position);
            var centerX = _texture.width / 2;
            var centerY = _texture.height / 2;
            for (var x = 0; x < _texture.width; x++)
            {
                for (var y = 0; y < _texture.height; y++)
                {
                    var tilePos = new Vector3Int(pos.x + x - centerX, pos.y + y - centerY, 0);
                    var tile = tilemap.GetTile<MapTile>(tilePos);
                    _texture.SetPixel(x, y, tile == null ? Color.black : tile.GetMapColor());
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
        }
        if (group != null)
        {
            group.alpha = 0;
            group.interactable = false;
            group.blocksRaycasts = false;
        }
    }
}
