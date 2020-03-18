using FPS.Attributes;
using FPS.UI;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace FPS.Combat
{
    public class Bullet : MonoBehaviour
    {
        [SerializeField] [Range(5, 100)] float destroyAfter = 0f;

        GameObject instigator;
        float damage = 0;

        void Start()
        {
            Destroy(gameObject, destroyAfter);
        }

        public void SetInstigator(GameObject instigator)
        {
            this.instigator = instigator;
        }

        public void SetDamage(float value)
        {
            damage = value;
        }

        private void OnCollisionEnter(Collision collision)
        {
            if (collision.gameObject.CompareTag("Enemy"))
            {
                if (instigator.CompareTag("Player"))
                {
                    instigator.GetComponent<Crosshair>().ShowCrosshairHit();
                }

                collision.gameObject.GetComponent<Health>().TakeDamage(instigator, damage);
                print("Hit");
            }

            Destroy(gameObject);
        }
    }
}
