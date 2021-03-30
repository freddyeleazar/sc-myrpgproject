using Sirenix.OdinInspector;
using Sirenix.Utilities;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.Tilemaps;

public class WorldBaseGround_ : MonoBehaviour, IPointerClickHandler
{
    public Tilemap baseGroundTilemap;
    public BottomUi bottomUi;
    public Player_ player;

    public Vector3Int tilePosition;
    public Vector3Int fixedTilePosition;

    private void Awake()
    {
        bottomUi = FindObjectOfType<BottomUi>();
        player = FindObjectOfType<Player_>();
    }

    private void Start()
    {
        BuildGroundTiles();
    }

    [Button]
    private void BuildGroundTiles()
    {
        foreach (TileBase tile in baseGroundTilemap.GetTilesBlock(baseGroundTilemap.cellBounds))
        {
            //((Ground_)tile).Build();
            //Ground_ ground = ((Ground_)tile);
            //ground.sprite = ground.graphics_.ingame_.sprites[Random.Range(0, ground.graphics_.ingame_.sprites.Count)];
        }
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        switch (eventData.button)
        {
            case PointerEventData.InputButton.Left:
                Vector3 clickedWorldPoint = Camera.main.ScreenToWorldPoint(eventData.position);
                tilePosition = baseGroundTilemap.WorldToCell(clickedWorldPoint);
                fixedTilePosition = new Vector3Int(tilePosition.x - 5, tilePosition.y - 5, 0);
                Ground_ selectedTileGround = baseGroundTilemap.GetTile(fixedTilePosition) as Ground_;
                bottomUi.LoadUI(selectedTileGround);
                break;
            case PointerEventData.InputButton.Right:
                ((PlayerBehaviour_)player.mechanics_.behaviour_).DefaultAction(eventData, gameObject, "Ground");
                break;
            case PointerEventData.InputButton.Middle:
                //Desplegar menú flotante con opciones de interacción
                break;
            default:
                break;
        }
    }
}
