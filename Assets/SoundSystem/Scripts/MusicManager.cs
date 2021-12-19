using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace SoundSystem
{
    public class MusicManager : MonoBehaviour
    {
        int activeLayerIndex = 1;
        public int ActiveLayerIndex => activeLayerIndex;

        MusicPlayer musicPlayer1;
        MusicPlayer musicPlayer2;

        bool isMusicPlayer1Playing = false;

        public MusicPlayer ActivePlayer => (isMusicPlayer1Playing) ? musicPlayer1 : musicPlayer2;
        public MusicPlayer InActivePlayer => (isMusicPlayer1Playing) ? musicPlayer2 : musicPlayer1;

        MusicEvent activeMusicEvent;

        public const int MaxLayerCount = 4;

        float volume = 1;
        public float Volume
        {
            get => volume;
            private set
            {
                value = Mathf.Clamp(value, 0, 1);
                volume = value;
            }
        }


        private static MusicManager _instance;
        public static MusicManager Instance
        {
            get
            {
                if (_instance == null)
                {
                    _instance = FindObjectOfType<MusicManager>();
                    if (_instance == null)
                    {
                        GameObject singletonGO = new GameObject("MusicManager_singleton");
                        _instance = singletonGO.AddComponent<MusicManager>();
                        DontDestroyOnLoad(singletonGO);
                    }
                }
                return _instance;
            }
        }

        private void Awake()
        {
            if (_instance != null && _instance != this)
                Destroy(gameObject);
            else
                _instance = this;

            SetupMusicPlayers();
        }

        public void PlayMusic(MusicEvent musicEvent, float fadeTime)
        {
            if (musicEvent == null)
                return;
            if (musicEvent == activeMusicEvent)
                return;

            ActivePlayer.Stop(fadeTime);
            activeMusicEvent = musicEvent;
            isMusicPlayer1Playing = !isMusicPlayer1Playing;

            ActivePlayer.Play(musicEvent, fadeTime);
        }

        public void StopMusic(float fadeTime)
        {
            if (activeMusicEvent == null)
                return;

            activeMusicEvent = null;
            ActivePlayer.Stop(fadeTime);
        }

        private void SetupMusicPlayers()
        {
            musicPlayer1 = gameObject.AddComponent<MusicPlayer>();
            musicPlayer2 = gameObject.AddComponent<MusicPlayer>();
        }

        public void IncreaseLayerIndex(float fadeTime)
        {
            int newLayerIndex = activeLayerIndex + 1;
            newLayerIndex = Mathf.Clamp(newLayerIndex, 0, MaxLayerCount - 1);

            if (newLayerIndex == activeLayerIndex)
                return;

            activeLayerIndex = newLayerIndex;
            ActivePlayer.FadeVolume(Volume, fadeTime);
        }

        public void DecreaseLayerIndex(float fadeTime)
        {
            int newLayerIndex = activeLayerIndex - 1;
            newLayerIndex = Mathf.Clamp(newLayerIndex, 0, MaxLayerCount - 1);

            if (newLayerIndex == activeLayerIndex)
                return;

            activeLayerIndex = newLayerIndex;
            ActivePlayer.FadeVolume(Volume, fadeTime);
        }
    }
}
