using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChunkGenerator : MonoBehaviour
{
    [SerializeField] Chunk startChunk;
    [SerializeField] DifficultyGroup[] difficultyGroups;
    [SerializeField] int minDepthToGenerate = 20;
    [SerializeField] int playerFromGeneratedDepth = 15;
    int currentDepthGenerated = 0, currentDifficultyGroup = 0;
    PlayerController player;
    Chunk lastChunk;

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
        lastChunk = startChunk;
        currentDepthGenerated = startChunk.height;
        GenerateChunks();
    }

    void GenerateChunks()
    {
        if (difficultyGroups.Length - 1 > currentDifficultyGroup && currentDepthGenerated >= difficultyGroups[currentDifficultyGroup + 1].depth)
            currentDifficultyGroup++;

        Chunk[] possibleChunks = difficultyGroups[currentDifficultyGroup].possibleChunks;

        int goalDepth = currentDepthGenerated + minDepthToGenerate;
        while(currentDepthGenerated < goalDepth)
        {
            int randomInt = Random.Range(0, possibleChunks.Length);

            //Check if nextChunk to generate is on the last chunk's blacklist
            if(lastChunk.chunkBlackList.Length != 0)
            {
                bool skipChunk = false;
                foreach(Chunk blackListedChunk in lastChunk.chunkBlackList)
                {
                    if(possibleChunks[randomInt] == blackListedChunk)
                    {
                        skipChunk = true;
                        break;
                    }
                }

                if (skipChunk)
                    continue;
            }
            Instantiate(possibleChunks[randomInt].gameObject, new Vector3(0, -currentDepthGenerated), Quaternion.identity).GetComponent<Chunk>().SetPlayer(player);
            lastChunk = possibleChunks[randomInt];
            currentDepthGenerated += possibleChunks[randomInt].height;
        }
    }
}
