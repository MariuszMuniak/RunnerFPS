using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace FPS.Combat
{
    public class Weapon : MonoBehaviour
    {
        [SerializeField] Transform barrel = null;
        [SerializeField] WeaponConfig weaponConfig;
        [SerializeField] GameObject shotEffect = null;
        [SerializeField] Transform bulletShellSpawnPoint = null;

        int currentAmmo = 0;

        private void Awake()
        {
            if (weaponConfig.GetAnimatorOverride() != null)
            {
                Animator animator = GetComponent<Animator>();

                if (animator != null)
                {
                    GetComponent<Animator>().runtimeAnimatorController = weaponConfig.GetAnimatorOverride();
                }
            }

            currentAmmo = weaponConfig.GetMagazineSize();
        }

        public WeaponConfig GetWeaponConfig()
        {
            return weaponConfig;
        }

        public int GetCurrentAmmo()
        {
            return currentAmmo;
        }

        public Transform GetBarrel()
        {
            return barrel;
        }

        public Transform GetBulletShellSpawnPoint()
        {
            return bulletShellSpawnPoint;
        }

        public ParticleSystem[] GetShotEffect()
        {
            return shotEffect.GetComponentsInChildren<ParticleSystem>();
        }
    }
}
