using FPS.Attributes;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace FPS.Items
{
    public class HealthPotion : MonoBehaviour
    {
        [SerializeField] float value = 10;
        [SerializeField] AudioClip takeSound;

        private void OnTriggerEnter(Collider other)
        {
            if (other.gameObject.tag == "Player")
            {
                Health health = other.GetComponent<Health>();

                if (health.CanBeHealed())
                {
                    GetComponent<AudioSource>().PlayOneShot(takeSound);
                    health.Heal(value);
                    Destroy(gameObject,takeSound.length);
                }
            }
        }
    }
}
