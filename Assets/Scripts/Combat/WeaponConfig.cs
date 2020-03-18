using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace FPS.Combat
{
    [CreateAssetMenu(fileName = "Weapon", menuName = "Weapons/Make New Weapon", order = 0)]
    public class WeaponConfig : ScriptableObject
    {
        [SerializeField] string weaponName = null;
        [SerializeField] AnimatorOverrideController animatorOverride = null;
        [SerializeField] GameObject weapon = null;
        [SerializeField] float damage = 0f;
        [SerializeField] int magazineSize = 0;
        [SerializeField] float fireRate = 0f;
        [SerializeField] GameObject bullet = null;
        [SerializeField] float bulletForce = 0f;
        [SerializeField] bool hasWeaponRecoil = false;
        [SerializeField] float weaponRecoil = 0f;
        [SerializeField] GameObject bulletShell = null;
        [SerializeField] AudioClip shotSound = null;
        [SerializeField] AudioClip takeOutSound = null;
        [SerializeField] AudioClip reloadSound = null;
        [SerializeField] AudioClip reloadOutOfAmmoSound = null;
        [SerializeField] AudioClip aimInSound = null;
        [SerializeField] AudioClip aimOutSound = null;

        public AnimatorOverrideController GetAnimatorOverride()
        {
            return animatorOverride;
        }

        public float GetFireRate()
        {
            return 1 / fireRate;
        }

        public float GetFireRateForEnemy()
        {
            return fireRate;
        }

        public int GetMagazineSize()
        {
            return magazineSize;
        }

        public string GetWeaponName()
        {
            return weaponName;
        }

        public GameObject GetBullet()
        {
            return bullet;
        }

        public float GetBulletForce()
        {
            return bulletForce;
        }

        public GameObject GetBulletShell()
        {
            return bulletShell;
        }

        public AudioClip GetShotSound()
        {
            return shotSound;
        }

        public AudioClip GetTakeOutSound()
        {
            return takeOutSound;
        }

        public AudioClip GetReloadSound()
        {
            return reloadSound;
        }

        public AudioClip GetReloadOutOfAmmoSound()
        {
            return reloadOutOfAmmoSound;
        }

        public AudioClip GetAimInSound()
        {
            return aimInSound;
        }

        public AudioClip GetAimOutSound()
        {
            return aimOutSound;
        }

        public float GetDamage()
        {
            return damage;
        }

        public bool HasWeaponRecoil()
        {
            return hasWeaponRecoil;
        }

        public float GetWeaponRecoil()
        {
            return weaponRecoil;
        }
    }
}
