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

    public string[] maps;

    //PlayerSpawner playerSpawner;

    private GameObject[] playerPrefabs;

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

        //string input = "5 5,\t33333\n,31113,\t31013,\n31113,33333";
        
        if(maps.Length != 0)
        {
            int randomIndex = UnityEngine.Random.Range(0, maps.Length);
            mapData = Parse(maps[randomIndex]);
        
        
        } else mapData = Parse(inputMap);

        Debug.Log("Number of Maps is" + maps.Length);

        GenerateTilemap();

        playerPrefabs = GetComponent<GameManager>().players;
        Debug.Log("Size of players: " + playerPrefabs.Length);



        MovePlayersToSpawnPoints();

        //GetComponent<PlayerSpawner>().MovePlayersToSpawnPoints();
        // playerSpawner = GetComponent<PlayerSpawner>();
        // if(playerSpawner != null)
        // {
        //     playerSpawner.MovePlayersToSpawnPoints();
        // } else 
        // {
        //     Debug.Log("PlayerSpawner is null!");
        // }
            
    }

    void GenerateTilemap()
    {
            Debug.Log("Generating map");

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
        string[] lines = input.Split(new char[] { ' ', ',', '\t', '\n'}, StringSplitOptions.RemoveEmptyEntries);

        // Parsing the size of the map
        int y = lines.Length; // Number of rows
        int x = lines[0].Length; // Number of columns in the first row

        int[,] map = new int[y, x]; // Note: y is rows and x is columns

        // Parsing the map
        for (int i = 0; i < y; i++) {
            string row = lines[i];
            Debug.Log("The "+i+"th row is"+row);
            for (int j = 0; j < x; j++) {
                map[i, j] = int.Parse(row[j].ToString());
            }
        }
        Debug.Log("Parse end");

        return map;
    }

    // public static int[,] Parse(string input) {
    //     string[] lines = input.Split(new char[] { ' ', ',', '\t', '\n' }, StringSplitOptions.RemoveEmptyEntries);

    //     // Parsing the size of the map
    //     int x = int.Parse(lines[0]);
    //     int y = int.Parse(lines[1]);

    //     int[,] map = new int[y, x]; // Note: y is rows and x is columns

    //     // Parsing the map
    //     for (int i = 2; i < lines.Length; i++) {
    //         string row = lines[i];
    //         for (int j = 0; j < x; j++) {
    //             map[i - 2, j] = int.Parse(row[j].ToString());
    //         }
    //     }

    //     return map;
    // }

    

    public void MovePlayersToSpawnPoints()
    {
        Debug.Log("Moving players starts");
        BoundsInt bounds = indestructibleTilemap.cellBounds;

        // Create a list to store valid spawn positions
        var validSpawnPositions = new List<Vector3Int>();

        // Iterate through the bounds of the indestructible Tilemap
        for (int x = bounds.xMin; x < bounds.xMax; x++)
        {
            for (int y = bounds.yMin; y < bounds.yMax; y++)
            {
                Vector3Int tilePosition = new Vector3Int(x, y, 0);
                if (indestructibleTilemap.GetTile(tilePosition) == floor && !destructibleTilemap.HasTile(tilePosition))
                {
                    // Add the position to the list of valid spawn positions
                    validSpawnPositions.Add(tilePosition);
                }
            }
        }

        Debug.Log("The number of possible spawn position is "+validSpawnPositions.Count);

        // Shuffle the list of valid spawn positions
        validSpawnPositions = ShuffleList(validSpawnPositions);

        // Move existing players to spawn points
        int playerCount = Mathf.Min(playerPrefabs.Length, validSpawnPositions.Count);
        for (int i = 0; i < playerCount; i++)
        {
            
            Vector3Int randomPosition = validSpawnPositions[i];
            
            Vector3 spawnPosition = indestructibleTilemap.GetCellCenterWorld(randomPosition);

            Debug.Log("Spawn Position: " + spawnPosition);

            // Move the player to the random position
            playerPrefabs[i].transform.position = spawnPosition;
        }
        Debug.Log("Moving players ends");
    }

    // Helper function to shuffle a list
    List<T> ShuffleList<T>(List<T> list)
    {
        for (int i = 0; i < list.Count; i++)
        {
            int randomIndex = UnityEngine.Random.Range(i, list.Count);
            T temp = list[randomIndex];
            list[randomIndex] = list[i];
            list[i] = temp;
        }
        return list;
    }
}
