using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;


[CreateAssetMenu(fileName ="Season tile", menuName ="Tiles/Season")]
public class SeasonTile : Tile
{
    [SerializeField] Sprite commonSprite;
    [SerializeField] Sprite seasonSprite;

    static bool nowSeason = false;

    public override void GetTileData(Vector3Int position, ITilemap tilemap, ref TileData tileData)
    {
        base.GetTileData(position, tilemap, ref tileData);

        tileData.sprite = nowSeason ? seasonSprite : commonSprite;
        
    }

    public static void ChangeSeason()
    {
        nowSeason = !nowSeason;
    }

    public static void SetDefault()
    {
        nowSeason = false;
    }

    public override bool StartUp(Vector3Int position, ITilemap tilemap, GameObject go)
    {
        this.sprite = commonSprite;
        return base.StartUp(position, tilemap, go);
    }
}
