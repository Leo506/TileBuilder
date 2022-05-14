using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class SeasonChanger : MonoBehaviour
{
    [SerializeField] Tilemap tilemap;

    public void ChangeSeason()
    {
        SeasonTile.ChangeSeason();
        tilemap.RefreshAllTiles();
    }

    private void OnDestroy()
    {
        SeasonTile.SetDefault();
        tilemap.RefreshAllTiles();
    }
}
