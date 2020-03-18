using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace FPS.UI
{
    public class ClickSound : MonoBehaviour
    {
        AudioSource audioSource;

        private void Awake()
        {
            AudioSource[] objects = GameObject.FindObjectsOfType<AudioSource>();

            foreach (AudioSource source in objects)
            {
                if (source.gameObject.name == "Click Sound")
                {
                    audioSource = source;
                    break;
                }
            }
        }

        void Start()
        {
            GetComponent<Button>().onClick.AddListener(audioSource.Play);
        }
    }
}
