using System.Collections.Generic;
using UnityEngine;

public class MapController : MonoBehaviour
{
    public List<GameObject> terrainChunks;
    public GameObject player;
    public float checkerRadius;
    Vector3 noTerrainPosition;
    public LayerMask terrainMask;
    public GameObject currnetChunk;
    PlayerMovement playerMovement;

    [Header("Optimization")]
    public List<GameObject> spawnedChunks;
    GameObject latestChunk;
    public float maxOptimizationDistance; //Must be greater than the length and width of the tilemap
    float optimizationDistance;
    float optimizerColldown;
    public float optimizerColldownDuration;

    // Start is called before the first frame update
    void Start()
    {
        playerMovement = FindAnyObjectByType<PlayerMovement>();
        spawnedChunks = new List<GameObject>();
    }

    // Update is called once per frame
    void Update()
    {
        ChunkChecker();
        ChunkOptimizer();
    }

    void ChunkChecker()
    {
        if (!currnetChunk)
            return;

        string tileChunkPosition = "";

        if (playerMovement.moveDirection.x > 0) // right
        {
            tileChunkPosition += "Right";
        }
        else if (playerMovement.moveDirection.x < 0) // left
        {
            tileChunkPosition += "Left";
        }

        if (playerMovement.moveDirection.y > 0) // up
        {
            tileChunkPosition += "Up";
        }
        else if (playerMovement.moveDirection.y < 0) // down
        {
            tileChunkPosition += "Down";
        }

        var playerPos = currnetChunk.transform.Find(tileChunkPosition).position;

        if (!Physics2D.OverlapCircle(playerPos, checkerRadius, terrainMask))
        {
            noTerrainPosition = playerPos;
            SpawnChunk();
        }
    }

    void SpawnChunk()
    {
        int random = Random.Range(0, terrainChunks.Count);
        latestChunk = Instantiate(terrainChunks[random], noTerrainPosition, Quaternion.identity);
        spawnedChunks.Add(latestChunk);
    }

    void ChunkOptimizer()
    {
        optimizerColldown -= Time.deltaTime;

        if (optimizerColldown <= 0)
        {
            optimizerColldown = optimizerColldownDuration;
        }
        else
            return;


        foreach (var chunk in spawnedChunks)
        {
            optimizationDistance = Vector3.Distance(player.transform.position, chunk.transform.position);

            bool optimizationAllowed = optimizationDistance > maxOptimizationDistance;

            chunk.SetActive(!optimizationAllowed);
        }
    }
}
