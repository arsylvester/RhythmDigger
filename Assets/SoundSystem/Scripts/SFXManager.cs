using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace SoundSystem
{
    public class SFXManager : MonoBehaviour
    {
        //Object Pooling
        List<SoundPlayer> soundPool = new List<SoundPlayer>();
        [SerializeField] int poolMinimum = 3;
        [SerializeField] int poolMaximum = 6;
        int currentPoolSize = 0;

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

        //Singleton instance, if doesn't exist create it
        private static SFXManager _instance;
        public static SFXManager Instance
        {
            get
            {
                if (_instance == null)
                {
                    _instance = FindObjectOfType<SFXManager>();
                    if (_instance == null)
                    {
                        GameObject singletonGO = new GameObject("SFXManager_singleton");
                        _instance = singletonGO.AddComponent<SFXManager>();
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

            SetupPool();
        }

        //Activate an sound player and play the sound event
        public void PlayOneShot(SoundEvent soundEvent)
        {
            if (soundEvent == null)
                return;

            SoundPlayer player = GetAvailableSoundPlayer();
            if (player != null)
                player.PlayOneShot(soundEvent);
        }

        /// <summary>
        /// Find a sound player in the pool or add one if there is space.
        /// </summary>
        /// <returns>a sound player from the pool</returns>
        public SoundPlayer GetAvailableSoundPlayer()
        {
            //Find available Sound Player in pool
            for (int i = 0; i < currentPoolSize; i++)
            {
                if (!soundPool[i].IsPlaying)
                    return soundPool[i];
            }

            //If all active in pool add to pool
            if (currentPoolSize < poolMaximum)
                return AddToPool();

            return null;
        }

        //Set up pool with minimum players needed
        private void SetupPool()
        {
            for (int i = 0; i < poolMinimum; i++)
            {
                AddToPool();
            }
        }

        /// <summary>
        /// Creates a sound player and adds it to the pool.
        /// </summary>
        /// <returns>The newly created SoundPlayer</returns>
        private SoundPlayer AddToPool()
        {
            //Create object and set as a child of the manager
            GameObject soundPlayer = new GameObject("Sound Player " + (currentPoolSize + 1));
            soundPlayer.transform.parent = transform;

            //Add components
            SoundPlayer player = soundPlayer.AddComponent<SoundPlayer>();
            soundPlayer.AddComponent<AudioSource>();

            //Other
            soundPlayer.SetActive(false);
            soundPool.Add(player);
            currentPoolSize++;
            return player;
        }
    }
}
