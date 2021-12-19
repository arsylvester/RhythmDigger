using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;

namespace SoundSystem
{
    [CreateAssetMenu(menuName = "SoundSystem/Sound Event", fileName = "SFX_")]
    public class SoundEvent : ScriptableObject
    {
        //Sound clips
        [SerializeField] AudioClip[] soundClips;
        public AudioClip[] SoundClips => soundClips;
        AudioClip clipToPlay;
        public AudioClip ClipToPlay => clipToPlay;

        //Can add more (Volume, Pitch, etc.)
        [Range(0, 1)]
        [SerializeField] float volume = 1;
        public float Volume
        {
            get => volume;
            private set
            {
                value = Mathf.Clamp(value, 0, 1);
                volume = value;
            }
        }

        [Range(-3, 3)]
        [SerializeField] float minPitch = 1f;
        [Range(-3, 3)]
        [SerializeField] float maxPitch = 1f;
        public float Pitch => Random.Range(minPitch, maxPitch);

        //Mixer
        [SerializeField] AudioMixerGroup mixer;
        public AudioMixerGroup Mixer => mixer;

        public void PlayOneShot(int clipIndex)
        {
            clipToPlay = soundClips[clipIndex];
            SFXManager.Instance.PlayOneShot(this);
        }

        public void PlayRandomClip()
        {
            clipToPlay = soundClips[Random.Range(0, soundClips.Length - 1)];
            SFXManager.Instance.PlayOneShot(this);
        }
    }

    /*
    [CustomEditor(typeof(SoundEvent))]
    public class MyScriptEditor : Editor
    {
        override public void OnInspectorGUI()
        {
            var myScript = target as SoundEvent;

            myScript.randomPitch = GUILayout.Toggle(myScript.randomPitch, "Flag");

            if (myScript.randomPitch)
                myScript.minPitch = EditorGUILayout.FloatField("Min Pitch:", myScript.minPitch);

        }
    }
    */
}
