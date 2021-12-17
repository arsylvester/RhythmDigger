using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChunkGenerator : MonoBehaviour
{
    [SerializeField] Chunk startChunk;
    [SerializeField] Chunk[] possibleChunks;
    [SerializeField] int minHeightToGenerate = 20;
    [SerializeField] int playerFromGeneratedDepth = 15;
    int currentDepthGenerated = 0;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void GenerateFirstChunks()
    {
        Instantiate(startChunk.gameObject);
        currentDepthGenerated = startChunk.height;
    }

    void GenerateChunks()
    {

    }
}
