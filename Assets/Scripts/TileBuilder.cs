using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;
using UnityEngine.EventSystems;

public class TileBuilder : MonoBehaviour
{
    [SerializeField] Tilemap targetTilemap;
    [SerializeField] Tilemap planTilemap;

    Vector3Int? previousPos;

    Camera mainCamera;

    Tile currentTile;

    List<Vector3Int> usedCells;


    private void Start()
    {
        currentTile = null;
        usedCells = new List<Vector3Int>();
        mainCamera = Camera.main;
        previousPos = null;
    }

    public void CreateTile(Tile tile)
    {
        currentTile = tile;
        Debug.Log("Created tile: " + tile.name);
    }

    // Update is called once per frame
    void Update()
    {
        if (currentTile == null)
            return;

        var currentPos = mainCamera.ScreenToWorldPoint(Input.mousePosition);
        var tilePos = planTilemap.WorldToCell(currentPos);

        tilePos = ValidatePos(tilePos, false);
        
        planTilemap.SetTile(tilePos, currentTile);
        if (previousPos != null && previousPos != tilePos)
            planTilemap.SetTile(previousPos.Value, null);

        previousPos = tilePos;

        if (Input.GetMouseButtonDown(0) && !EventSystem.current.IsPointerOverGameObject())
        {
            planTilemap.SetTile(tilePos, null);

            tilePos = ValidatePos(tilePos);

            usedCells.Add(tilePos);

            targetTilemap.SetTile(tilePos, currentTile);
            currentTile = null;
            Debug.Log("Set tile on " + tilePos);
        }
    }

    private Vector3Int ValidatePos(Vector3Int pos, bool saveNewCell = true)
    {
        while (usedCells.Contains(pos))
            pos = new Vector3Int(pos.x, pos.y, pos.z + 2);

        if (saveNewCell)
            usedCells.Add(pos);
        return pos;
    }
}
