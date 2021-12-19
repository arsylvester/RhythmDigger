using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace SoundSystem
{
    public class SoundPlayer : MonoBehaviour
    {
        AudioSource audioSource;

        Coroutine playSoundRoutine;
        public bool IsPlaying => (playSoundRoutine != null);

        private void Awake()
        {
            audioSource = GetComponent<AudioSource>();
        }

        /// <summary>
        /// Plays a Sound Event clip with no clipping then disables itself.
        /// </summary>
        /// <param name="sfx">SoundEvent to play</param>
        public void PlayOneShot(SoundEvent sfx)
        {
            if(sfx == null)
            {
                Debug.LogWarning("Sound Event is empty");
                return;
            }

            if(audioSource == null)
                audioSource = GetComponent<AudioSource>();

            gameObject.SetActive(true);
            audioSource.clip = sfx.ClipToPlay;
            audioSource.loop = false;
            audioSource.outputAudioMixerGroup = sfx.Mixer;
            audioSource.volume = sfx.Volume;
            audioSource.pitch = sfx.Pitch;
            playSoundRoutine = StartCoroutine(PlaySound()); 
        }

        IEnumerator PlaySound()
        {
            audioSource.Play();
            yield return new WaitForSeconds(audioSource.clip.length);
            playSoundRoutine = null;
            gameObject.SetActive(false);
        }

        //Can add other functions like stop, playWithLoop, pause, etc.
    }
}