using FPS.Control;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

namespace FPS.Attributes
{
    public class Health : MonoBehaviour
    {
        [SerializeField] float maxHealth = 100f;
        [SerializeField] float currentHealth;
        [SerializeField] GameObject deathEffect = null;
        [SerializeField] AudioClip deathSound = null;
        public UnityEvent onDeath;

        bool isDead = false;

        void Start()
        {
            currentHealth = maxHealth;
        }

        void Update()
        {
            if (isDead) { return; }

            if (currentHealth == 0)
            {
                Die();
            }
        }

        public void TakeDamage(GameObject instigator, float damage)
        {
            currentHealth = Mathf.Max(0, currentHealth - damage);

            if (instigator.CompareTag("Player"))
            {
                AIController controller = GetComponent<AIController>();

                if (controller != null)
                {
                    controller.ChasePlayer();
                }
            }
        }

        public void Heal(float healthToRestore)
        {
            currentHealth = Mathf.Min((currentHealth + healthToRestore), maxHealth);
        }

        public float CurrentHealth()
        {
            return currentHealth;
        }

        public float GetMaxHealth()
        {
            return maxHealth;
        }

        public float GetFraction()
        {
            return currentHealth / maxHealth;
        }

        public bool IsDead()
        {
            return isDead;
        }

        public bool CanBeHealed()
        {
            return !Mathf.Approximately(currentHealth, maxHealth);
        }

        private void Die()
        {
            isDead = true;
            onDeath.Invoke();

            if (deathEffect != null)
            {
                float timeToDestroy = 0f;

                ParticleSystem[] particles = deathEffect.GetComponentsInChildren<ParticleSystem>();

                foreach (ParticleSystem particle in particles)
                {
                    particle.Play();

                    if (timeToDestroy < particle.main.startLifetime.constant)
                    {
                        timeToDestroy = particle.main.startLifetime.constant;
                    }
                }

                if (deathSound != null)
                {
                    GetComponent<AudioSource>().PlayOneShot(deathSound);
                    timeToDestroy = timeToDestroy < deathSound.length ? deathSound.length : timeToDestroy;
                }

                Destroy(gameObject, timeToDestroy);

                return;
            }

            if (gameObject.CompareTag("Player")) { return; }

            Destroy(gameObject);
        }
    }
}
