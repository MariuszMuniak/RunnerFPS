using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace FPS.Combat
{
    public class AIFighter : MonoBehaviour
    {
        GameObject player;
        Weapon weapon;
        WeaponConfig weaponConfig;
        Animator animator;

        float timeSinceLastShot = Mathf.Infinity;

        private void Awake()
        {
            player = GameObject.FindWithTag("Player");
            weapon = GetComponentInChildren<Weapon>();
            animator = GetComponent<Animator>();
            weaponConfig = weapon.GetWeaponConfig();
        }

        void Start()
        {
            if (weaponConfig.GetAnimatorOverride() != null)
            {
                animator.runtimeAnimatorController = weaponConfig.GetAnimatorOverride();
            }

            animator.SetFloat("fireRate", Mathf.Max(1, weaponConfig.GetFireRateForEnemy()));
        }

        void Update()
        {
            timeSinceLastShot += Time.deltaTime;
        }

        public void Fire()
        {
            GetComponent<AudioSource>().PlayOneShot(weaponConfig.GetShotSound());

            transform.LookAt(new Vector3(player.transform.position.x, transform.position.y, player.transform.position.z));

            var bullet = Instantiate(weaponConfig.GetBullet(), weapon.GetBarrel().position, Quaternion.identity);
            bullet.GetComponent<AIBullet>().Initialization(gameObject, player.transform, weaponConfig.GetDamage());

            animator.Play("fire", 1);
        }

        public bool ReadyToFire()
        {
            if (timeSinceLastShot > weaponConfig.GetFireRate())
            {
                timeSinceLastShot = 0;
                return true;
            }

            return false;
        }
    }
}
