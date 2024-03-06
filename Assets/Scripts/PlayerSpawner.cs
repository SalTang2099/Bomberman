using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class PlayerSpawner : MonoBehaviour
{
    public Tilemap destructibleTilemap;
    public Tilemap indestructibleTilemap;

    public Tile floor;
    private GameObject[] playerPrefabs;

    void Start()
    {  
        playerPrefabs = GetComponent<GameManager>().players;
        Debug.Log("Size of players: " + playerPrefabs.Length);
        //MovePlayersToSpawnPoints();
        
    }

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
            int randomIndex = Random.Range(i, list.Count);
            T temp = list[randomIndex];
            list[randomIndex] = list[i];
            list[i] = temp;
        }
        return list;
    }
}
