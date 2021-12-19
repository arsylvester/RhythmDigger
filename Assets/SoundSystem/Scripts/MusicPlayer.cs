using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace SoundSystem
{
    public class MusicPlayer : MonoBehaviour
    {
        List<AudioSource> layerSources = new List<AudioSource>();
        List<float> sourceStartVolumes = new List<float>();
        MusicEvent musicEvent = null;
        Coroutine fadeVolumeRoutine = null;
        Coroutine stopRoutine = null;

        private void Awake()
        {
            // Setup audiosources
            CreateLayerSources();
        }

        private void CreateLayerSources()
        {
            for (int i = 0; i < MusicManager.MaxLayerCount; i++)
            {
                layerSources.Add(gameObject.AddComponent<AudioSource>());
                // Set up audiosource
                layerSources[i].playOnAwake = false;
                layerSources[i].loop = true;
            }
        }

        public void Play(MusicEvent musicEventToPlay, float fadeTime)
        {
            musicEvent = musicEventToPlay;
            if (musicEvent == null)
            {
                Debug.LogWarning("Music event is null");
                return;
            }

            for (int i = 0; i < layerSources.Count && (i < musicEventToPlay.MusicLayers.Length); i++)
            {
                // if there is content in music layer
                if(musicEventToPlay.MusicLayers[i] != null)
                {
                    layerSources[i].volume = 0;
                    layerSources[i].clip = musicEventToPlay.MusicLayers[i];
                    layerSources[i].outputAudioMixerGroup = musicEvent.Mixer;
                    layerSources[i].Play();
                }
            }

            // fade up the volume
            FadeVolume(MusicManager.Instance.Volume, fadeTime);
        }

        public void Stop(float fadeTime)
        {
            if (stopRoutine != null)
                StopCoroutine(stopRoutine);
            stopRoutine = StartCoroutine(StopRoutine(fadeTime));
        }

        IEnumerator StopRoutine(float fadeTime)
        {
            if(fadeVolumeRoutine != null)
            {
                StopCoroutine(fadeVolumeRoutine);
            }
            // blend the volume to 0
            if(musicEvent.LayerType == LayerType.Additive)
            {
                fadeVolumeRoutine = StartCoroutine(LerpSourceAdditiveRoutine(0, fadeTime));
            }
            else if(musicEvent.LayerType == LayerType.Single)
            {
                fadeVolumeRoutine = StartCoroutine(LerpSourceSingleRoutine(0, fadeTime));
            }

            // wait for blend to finish
            yield return fadeVolumeRoutine;
            // stop all audio sources
            foreach (AudioSource source in layerSources)
            {
                source.Stop();
            }
        }

        public void FadeVolume(float targetVolume, float fadeTime)
        {
            targetVolume = Mathf.Clamp(targetVolume, 0, 1);
            if (fadeTime < 0)
                fadeTime = 0;

            if (fadeVolumeRoutine != null)
                StopCoroutine(fadeVolumeRoutine);

            //animate Additive layer
            if(musicEvent.LayerType == LayerType.Additive)
            {
                fadeVolumeRoutine = StartCoroutine(LerpSourceAdditiveRoutine(targetVolume, fadeTime));
            }
            else if(musicEvent.LayerType == LayerType.Single)
            {
                fadeVolumeRoutine = StartCoroutine(LerpSourceSingleRoutine(targetVolume, fadeTime));
            }
        }

        IEnumerator LerpSourceAdditiveRoutine(float targetVolume, float fadeTime)
        {
            SaveSourceStartVolumes();

            float startVolume;
            float newVolume;

            for (float elapsedTime = 0; elapsedTime <= fadeTime; elapsedTime += Time.deltaTime)
            {
                // Go through each layer, and adjust volume this frame
                for (int i = 0; i < layerSources.Count; i++)
                {
                    // if an active layer fate to target
                    if (i <= MusicManager.Instance.ActiveLayerIndex)
                    {
                        //adjust volume
                        startVolume = sourceStartVolumes[i];
                        newVolume = Mathf.Lerp(startVolume, targetVolume, elapsedTime / fadeTime);
                        layerSources[i].volume = newVolume;
                    }
                    // fade to 0 if not
                    else
                    {
                        startVolume = sourceStartVolumes[i];
                        newVolume = Mathf.Lerp(startVolume, 0, elapsedTime / fadeTime);
                        layerSources[i].volume = newVolume;
                    }
                }

                yield return new WaitForEndOfFrame();
            }

            for (int i = 0; i < layerSources.Count; i++)
            {
                if (i <= MusicManager.Instance.ActiveLayerIndex)
                {
                    layerSources[i].volume = targetVolume;
                }
                else
                {
                    layerSources[i].volume = 0;
                }
            }
        }

        private void SaveSourceStartVolumes()
        {
            sourceStartVolumes.Clear();
            for (int i = 0; i < layerSources.Count; i++)
            {
                sourceStartVolumes.Add(layerSources[i].volume);
            }
        }

        IEnumerator LerpSourceSingleRoutine(float targetVolume, float fadeTime)
        {
            SaveSourceStartVolumes();

            float startVolume;
            float newVolume;

            for (float elapsedTime = 0; elapsedTime <= fadeTime; elapsedTime += Time.deltaTime)
            {
                // Go through each layer, and adjust volume this frame
                for (int i = 0; i < layerSources.Count; i++)
                {
                    // if an active layer fate to target
                    if (i == MusicManager.Instance.ActiveLayerIndex)
                    {
                        //adjust volume
                        startVolume = sourceStartVolumes[i];
                        newVolume = Mathf.Lerp(startVolume, targetVolume, elapsedTime / fadeTime);
                        layerSources[i].volume = newVolume;
                    }
                    // fade to 0 if not
                    else
                    {
                        startVolume = sourceStartVolumes[i];
                        newVolume = Mathf.Lerp(startVolume, 0, elapsedTime / fadeTime);
                        layerSources[i].volume = newVolume;
                    }
                }

                yield return new WaitForEndOfFrame();
            }

            for (int i = 0; i < layerSources.Count; i++)
            {
                if (i == MusicManager.Instance.ActiveLayerIndex)
                {
                    layerSources[i].volume = targetVolume;
                }
                else
                {
                    layerSources[i].volume = 0;
                }
            }
        }
    }
}