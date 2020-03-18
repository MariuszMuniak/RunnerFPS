using FPS.Movement;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace FPS.Combat
{
    public class Fighter : MonoBehaviour
    {
        [SerializeField] Camera weaponCamera = null;

        WeaponConfig currentWeapon = null;
        Animator animator;
        Mover mover;
        Camera m_camera;
        Transform barrel = null;
        AudioSource audioSource = null;
        ParticleSystem[] shotEffect = null;
        Transform bulletShellSpawnPoint = null;

        float timeSinceLastShot = Mathf.Infinity;
        int currentAmmo = 0;
        bool isAiming = false;
        string fireWariant = "fire_idle";

        public bool IsAiming
        {
            get
            {
                return isAiming;
            }
        }

        private void Awake()
        {
            mover = GetComponent<Mover>();
            audioSource = GetComponent<AudioSource>();
        }

        private void Update()
        {
            if (Time.deltaTime > 0)
            {
                animator.SetFloat("speed", transform.InverseTransformDirection(mover.CurrentSpeed()).z);
            }

            timeSinceLastShot += Time.deltaTime;
        }

        public void Fire(Transform player)
        {
            audioSource.PlayOneShot(currentWeapon.GetShotSound());
            animator.Play(fireWariant, 0, 0f);

            PlayShotEffect();
            InstantiateBullet();
            InstantiateBulletShell();
            Recoil(player);

            currentAmmo--;
            print("Fire");
        }



        public void KnifeAttack()
        {
            animator.SetTrigger("knifeAttack");
        }

        public bool ReadyToFire()
        {
            if (IsReloading()) { return false; }

            if (currentAmmo == 0)
            {
                Reload(); return false;
            }

            if (timeSinceLastShot > currentWeapon.GetFireRate())
            {
                timeSinceLastShot = 0f;
                return true;
            }
            return false;
        }

        public void Reload()
        {
            if (IsShooting()) { return; }

            if (currentAmmo == 0)
            {
                animator.SetTrigger("reloadOutOfAmmo");
                audioSource.PlayOneShot(currentWeapon.GetReloadOutOfAmmoSound());
                StartCoroutine("FillUpTheMagazine");
            }
            else if (currentAmmo > 0 && currentAmmo < currentWeapon.GetMagazineSize())
            {
                animator.SetTrigger("reload");
                audioSource.PlayOneShot(currentWeapon.GetReloadSound());
                StartCoroutine("FillUpTheMagazine");
            }
        }

        public void AttachWeapon(GameObject weapon)
        {
            currentWeapon = weapon.GetComponent<Weapon>().GetWeaponConfig();
            animator = weapon.GetComponent<Animator>();
            currentAmmo = weapon.GetComponent<Weapon>().GetCurrentAmmo();
            barrel = weapon.GetComponent<Weapon>().GetBarrel();
            shotEffect = weapon.GetComponent<Weapon>().GetShotEffect();
            bulletShellSpawnPoint = weapon.GetComponent<Weapon>().GetBulletShellSpawnPoint();

            audioSource.PlayOneShot(currentWeapon.GetTakeOutSound());
        }

        public WeaponConfig GetCurrentWeapon()
        {
            return currentWeapon;
        }

        public int GetCurrenAmmo()
        {
            return currentAmmo;
        }

        public void OnAiming()
        {
            animator.SetBool("aiming", true);
            mover.WalkWithAiming();

            if (isAiming) { return; }
            isAiming = true;
            weaponCamera.GetComponent<Animation>().Play("Camera Aim In Animation");
            Camera.main.GetComponent<Animation>().Play("Main Camera Aim In Animation");
            fireWariant = "fire_aim";
            audioSource.PlayOneShot(currentWeapon.GetAimInSound());
        }

        public void OffAiming()
        {
            animator.SetBool("aiming", false);
            mover.Walk();

            if (!isAiming) { return; }
            isAiming = false;
            weaponCamera.GetComponent<Animation>().Play("Camera Aim Out Animation");
            Camera.main.GetComponent<Animation>().Play("Main Camera Aim Out Animation");
            fireWariant = "fire_idle";
            audioSource.PlayOneShot(currentWeapon.GetAimOutSound());
        }
        private void InstantiateBullet()
        {
            var bullet = Instantiate(currentWeapon.GetBullet(), barrel.position, Quaternion.identity);
            bullet.GetComponent<Bullet>().SetDamage(currentWeapon.GetDamage());
            bullet.GetComponent<Bullet>().SetInstigator(gameObject);
            bullet.GetComponent<Rigidbody>().velocity = barrel.forward * currentWeapon.GetBulletForce();
        }

        private void InstantiateBulletShell()
        {
            Instantiate(currentWeapon.GetBulletShell(), bulletShellSpawnPoint.position, Quaternion.identity);
        }

        private void PlayShotEffect()
        {
            if (shotEffect.Length == 0) { return; }

            foreach (ParticleSystem particle in shotEffect)
            {
                particle.Play();
            }
        }

        private bool IsReloading()
        {
            AnimatorStateInfo animatorStateInfo = animator.GetCurrentAnimatorStateInfo(0);

            if (animatorStateInfo.IsTag("Reloading")) { return true; }

            return false;
        }

        private bool IsShooting()
        {
            AnimatorStateInfo animatorStateInfo = animator.GetCurrentAnimatorStateInfo(0);

            if (animatorStateInfo.IsTag("Fire")) { return true; }

            return false;
        }

        private IEnumerator FillUpTheMagazine()
        {
            yield return new WaitForSeconds(1.5f);
            currentAmmo = currentWeapon.GetMagazineSize();
        }

        private void Recoil(Transform player)
        {
            if (!currentWeapon.HasWeaponRecoil()) { return; }

            Quaternion playerRotation = player.rotation;
            Camera.main.transform.rotation = Quaternion.Euler(playerRotation.eulerAngles.x - currentWeapon.GetWeaponRecoil(), playerRotation.eulerAngles.y, playerRotation.eulerAngles.z);
        }
    }
}
