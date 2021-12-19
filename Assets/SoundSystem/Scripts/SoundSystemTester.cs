using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using SoundSystem;

public class SoundSystemTester : MonoBehaviour
{
    [SerializeField] MusicEvent SongA;
    [SerializeField] MusicEvent SongB;
    [SerializeField] SoundEvent SFXA;
    [SerializeField] SoundEvent SFXB;

    // Update is called once per frame
    void Update()
    {/*
        if(Keyboard.current.qKey.wasReleasedThisFrame)
        {
            SongA.Play(2.5f);
        }
        if (Keyboard.current.eKey.wasReleasedThisFrame)
        {
            SongB.Play(2.5f);
        }
        if (Keyboard.current.leftArrowKey.wasReleasedThisFrame)
        {
            MusicManager.Instance.DecreaseLayerIndex(5);
        }
        if (Keyboard.current.rightArrowKey.wasReleasedThisFrame)
        {
            MusicManager.Instance.IncreaseLayerIndex(5);
        }
        if (Keyboard.current.xKey.wasReleasedThisFrame)
        {
            MusicManager.Instance.StopMusic(1);
        }
        if (Keyboard.current.oKey.wasReleasedThisFrame)
        {
            SFXA.PlayOneShot(0);
        }
        if (Keyboard.current.pKey.wasReleasedThisFrame)
        {
            SFXB.PlayRandomClip();
        }
        */
    }
}
