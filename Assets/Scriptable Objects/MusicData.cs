using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "MusicData", menuName = "ScriptableObjects/MusicDataObject", order = 1)]
public class MusicData : ScriptableObject
{
    public AudioClip audioClip;
    public float musicBPM = 60f;
    public float firstBeatOffset = 0f, validBeatOffset = 0.25f;
    [Range(1, 10)]
    public int beatsOnScreen = 1;
}
