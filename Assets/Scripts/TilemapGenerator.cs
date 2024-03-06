using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class TilemapGenerator : MonoBehaviour
{
    public Tilemap destructibleTilemap;
    public Tilemap indestructibleTilemap;
    public Tile[] destructibleTiles; // Array of destructible tiles (e.g., brick)
    public Tile[] indestructibleTiles; // Array of indestructible tiles (e.g., wall, floor)
    public int[,] mapData; // 2D array representing the map

    void Start()
    {
        mapData = new int[10, 10] {
            {2, 2, 2, 2, 2, 2, 2, 2, 2, 2},
            {2, 0, 0, 1, 0, 0, 0, 0, 0, 2},
            {2, 1, 2, 1, 2, 1, 2, 1, 2, 2},
            {2, 0, 0, 0, 0, 0, 0, 0, 0, 2},
            {2, 3, 2, 1, 2, 1, 2, 3, 2, 2},
            {2, 0, 0, 1, 0, 0, 0, 0, 0, 2},
            {2, 1, 2, 1, 2, 1, 2, 1, 2, 2},
            {2, 0, 0, 0, 0, 0, 0, 0, 0, 2},
            {2, 1, 2, 1, 2, 1, 2, 1, 2, 2},
            {2, 2, 2, 2, 2, 2, 2, 2, 2, 2}
        };
        GenerateTilemap();
    }

    void GenerateTilemap()
    {

        destructibleTilemap.ClearAllTiles();
        indestructibleTilemap.ClearAllTiles();
        for (int x = 0; x < mapData.GetLength(0); x++)
        {
            for (int y = 0; y < mapData.GetLength(1); y++)
            {
                int tileIndex = mapData[x, y]; // Value in your array at position x, y

                // Check if the tile is destructible
                if (tileIndex >= 0 && tileIndex < destructibleTiles.Length)
                {
                    destructibleTilemap.SetTile(new Vector3Int(x, y, 0), destructibleTiles[tileIndex]); // Set destructible tile
                }
                // Check if the tile is indestructible
                else if (tileIndex >= destructibleTiles.Length && tileIndex < destructibleTiles.Length + indestructibleTiles.Length)
                {
                    indestructibleTilemap.SetTile(new Vector3Int(x, y, 0), indestructibleTiles[tileIndex - destructibleTiles.Length]); // Set indestructible tile
                }
            }
        }
    }
}
