using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;
using System;

public class TilemapGenerator : MonoBehaviour
{
    public Tilemap destructibleTilemap;
    public Tilemap indestructibleTilemap;
    //public Tile[] destructibleTiles; // Array of destructible tiles (e.g., brick)
    //public Tile[] indestructibleTiles; // Array of indestructible tiles (e.g., wall, floor)

    public Tile floor; // Array of indestructible tiles (e.g., wall, floor)

    public Tile shadowedFloor; // Array of indestructible tiles (e.g., wall, floor)

    public Tile wall; // Array of indestructible tiles (e.g., wall, floor)

    public Tile brick; // Array of indestructible tiles (e.g., wall, floor)
    public int[,] mapData; // 2D array representing the map

    public string inputMap;

    void Start()
    {
        // mapData = new int[10, 10] {
        // {3, 3, 3, 3, 3, 3, 3, 3, 3, 3},
        // {3, 1, 2, 1, 1, 1, 1, 2, 1, 3},
        // {3, 0, 3, 3, 3, 0, 3, 3, 0, 3},
        // {3, 1, 1, 1, 1, 1, 1, 1, 1, 3},
        // {3, 0, 3, 0, 3, 3, 0, 3, 0, 3},
        // {3, 1, 1, 1, 1, 1, 1, 1, 1, 3},
        // {3, 0, 3, 3, 3, 0, 3, 3, 0, 3},
        // {3, 1, 2, 1, 1, 1, 1, 2, 1, 3},
        // {3, 3, 0, 3, 3, 3, 3, 0, 3, 3},
        // {3, 3, 3, 3, 3, 3, 3, 3, 3, 3}
        // };

//         mapData= new int[10, 10] {
//     {3, 3, 3, 3, 3, 3, 3, 3, 3, 3},
//     {3, 1, 2, 1, 1, 1, 1, 1, 1, 3},
//     {3, 0, 3, 3, 0, 3, 3, 3, 0, 3},
//     {3, 1, 1, 1, 1, 1, 1, 1, 1, 3},
//     {3, 3, 3, 3, 0, 3, 3, 3, 0, 3},
//     {3, 1, 1, 1, 1, 1, 1, 1, 1, 3},
//     {3, 0, 3, 3, 0, 3, 3, 3, 0, 3},
//     {3, 1, 1, 1, 1, 1, 1, 1, 1, 3},
//     {3, 3, 3, 0, 3, 3, 3, 2, 3, 3},
//     {3, 3, 3, 3, 3, 3, 3, 3, 3, 3}
// };

        string input = "5 5,\t33333\n,31113,\t31013,\n31113,33333";
        mapData = Parse(inputMap);

        GenerateTilemap();
        GetComponent<PlayerSpawner>().MovePlayersToSpawnPoints();
    }

    void GenerateTilemap()
    {

            //         Generate a 10 by 10 map with the following specifications:
            // - 0 represents destructible brick tiles
            // - 1 represents floor tiles 
            // - 2 represents floor tiles in shadow
            // - 3 represents wall tiles

            // The map should have a mixture of floor tiles, destructible walls, and indestructible walls. Ensure that there are pathways from one side of the map to the other side, with walls strategically placed to create interesting gameplay.

            // The format should be code that assigns to variable of type int[,] in unity, so i can copy paste to my code.

        destructibleTilemap.ClearAllTiles();
        indestructibleTilemap.ClearAllTiles();
        for (int x = 0; x < mapData.GetLength(0); x++)
        {
            for (int y = 0; y < mapData.GetLength(1); y++)
            {
                int tileIndex = mapData[x, y]; // Value in your array at position x, y

                if(tileIndex == 0)
                {
                    destructibleTilemap.SetTile(new Vector3Int(x, y, 0), brick);
                    indestructibleTilemap.SetTile(new Vector3Int(x, y, 0), floor);
                } else if(tileIndex == 1)
                {
                    indestructibleTilemap.SetTile(new Vector3Int(x, y, 0), floor);
                } else if(tileIndex == 2)
                {
                    indestructibleTilemap.SetTile(new Vector3Int(x, y, 0), shadowedFloor);
                } else if(tileIndex == 3)
                {
                    indestructibleTilemap.SetTile(new Vector3Int(x, y, 0), wall);
                }

                // Check if the tile is destructible
                // if (tileIndex >= 0 && tileIndex < destructibleTiles.Length)
                // {
                //     destructibleTilemap.SetTile(new Vector3Int(x, y, 0), destructibleTiles[tileIndex]); // Set destructible tile
                // }
                // // Check if the tile is indestructible
                // else if (tileIndex >= destructibleTiles.Length && tileIndex < destructibleTiles.Length + indestructibleTiles.Length)
                // {
                //     indestructibleTilemap.SetTile(new Vector3Int(x, y, 0), indestructibleTiles[tileIndex - destructibleTiles.Length]); // Set indestructible tile
                // }
            }
        }
    }

    public static int[,] Parse(string input) {
        string[] lines = input.Split(new char[] { ' ', ',', '\t', '\n' }, StringSplitOptions.RemoveEmptyEntries);

        // Parsing the size of the map
        int x = int.Parse(lines[0]);
        int y = int.Parse(lines[1]);

        int[,] map = new int[y, x]; // Note: y is rows and x is columns

        // Parsing the map
        for (int i = 2; i < lines.Length; i++) {
            string row = lines[i];
            for (int j = 0; j < x; j++) {
                map[i - 2, j] = int.Parse(row[j].ToString());
            }
        }

        return map;
    }
}
