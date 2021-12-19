using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "SongData", menuName = "ScriptableObjects/SongDataObject", order = 1)]
public class SongData : ScriptableObject
{
    public AudioClip musicSource;
    public float musicBPM = 60f;
    public float firstBeatOffset = 0f;
    [Range(1, 10)]
    public int beatsOnScreen = 1;
}
