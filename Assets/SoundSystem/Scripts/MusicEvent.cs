using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;

namespace SoundSystem
{
    public enum LayerType
    {
        Additive,
        Single
    }

    [CreateAssetMenu(menuName = "SoundSystem/Music Event", fileName = "MUS_")]
    public class MusicEvent : ScriptableObject
    {
        //Music Tracks
        [SerializeField] AudioClip[] musicLayers;
        public AudioClip[] MusicLayers => musicLayers;
        //Blending type
        [SerializeField] LayerType layerType = LayerType.Additive;
        public LayerType LayerType => layerType;
        //Mixer
        [SerializeField] AudioMixerGroup mixer;
        public AudioMixerGroup Mixer => mixer;

        public void Play(float fadeTime)
        {
            MusicManager.Instance.PlayMusic(this, fadeTime);
        }
    }
}
