using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace FPS.Settings
{
    public class AudioManager : MonoBehaviour
    {
        AudioSource[] audioSources;
        AudioSource backgroundMusic;
        Settings settings;

        private void Awake()
        {
            settings = FindObjectOfType<Settings>();
        }

        void Start()
        {
            audioSources = FindObjectsOfType<AudioSource>();

            foreach (AudioSource audioSource in audioSources)
            {
                if (audioSource.gameObject.name == "BackgroundMusic")
                {
                    backgroundMusic = audioSource;
                    break;
                }
            }

            MasterVolume(settings.MasterVolume);
            MusicVolume(settings.MusicVolume);
        }

        public void MasterVolume(float value)
        {
            settings.MasterVolume = value;

            foreach (AudioSource audioSource in audioSources)
            {
                if (audioSource.gameObject.name == "BackgroundMusic")
                {
                    MusicVolume(settings.MusicVolume);
                }
                else
                {
                    audioSource.volume = value;
                }
            }
        }

        public void MusicVolume(float value)
        {
            settings.MusicVolume = value;

            if (backgroundMusic != null)
            {
                backgroundMusic.volume = settings.MasterVolume * settings.MusicVolume;
            }
        }

        public float GetMasterVolumeValue()
        {
            return settings.MasterVolume * 100;
        }

        public float GetMusicVolumeValue()
        {
            return settings.MusicVolume * 100;
        }
    }
}
