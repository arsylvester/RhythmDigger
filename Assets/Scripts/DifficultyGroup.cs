using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "DifficultyGroup")]
public class DifficultyGroup : ScriptableObject
{
    public Chunk[] possibleChunks;
    public MusicData musicData;
}
