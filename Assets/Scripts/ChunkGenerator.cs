using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChunkGenerator : MonoBehaviour
{
    [SerializeField] Chunk startChunk;
    [SerializeField] Chunk[] possibleChunks;
    [SerializeField] int minDepthToGenerate = 20;
    [SerializeField] int playerFromGeneratedDepth = 15;
    int currentDepthGenerated = 0;
    PlayerController player;

    // Start is called before the first frame update
    void Start()
    {
        player = FindObjectOfType<PlayerController>();
        GenerateFirstChunks();
    }

    // Update is called once per frame
    void Update()
    {
        if(player.transform.position.y < -(currentDepthGenerated - playerFromGeneratedDepth))
        {
            GenerateChunks();
        }
    }

    void GenerateFirstChunks()
    {
        Instantiate(startChunk.gameObject).GetComponent<Chunk>().SetPlayer(player);
        currentDepthGenerated = startChunk.height;
        GenerateChunks();
    }

    void GenerateChunks()
    {
        int goalDepth = currentDepthGenerated + minDepthToGenerate;
        while(currentDepthGenerated < goalDepth)
        {
            int randomInt = Random.Range(0, possibleChunks.Length);
            Instantiate(possibleChunks[randomInt].gameObject, new Vector3(0, -currentDepthGenerated), Quaternion.identity).GetComponent<Chunk>().SetPlayer(player);
            currentDepthGenerated += possibleChunks[randomInt].height;
        }
    }
}
