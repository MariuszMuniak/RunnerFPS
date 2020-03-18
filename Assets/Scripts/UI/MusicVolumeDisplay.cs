using FPS.Settings;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace FPS.UI
{
    public class MusicVolumeDisplay : MonoBehaviour
    {
        [SerializeField] AudioManager audioManager;

        Text textValue;

        private void Awake()
        {
            textValue = GetComponent<Text>();
        }

        void Update()
        {
            textValue.text = string.Format("{0:00}", audioManager.GetMusicVolumeValue());
        }
    } 
}
