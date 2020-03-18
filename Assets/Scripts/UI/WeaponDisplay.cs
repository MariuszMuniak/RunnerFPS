using FPS.Combat;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace FPS.UI
{
    public class WeaponDisplay : MonoBehaviour
    {
        [SerializeField] WeaponImage[] weaponImages;

        Fighter fighter;
        WeaponImage currentWeaponImage = null;

        private void Awake()
        {
            fighter = GameObject.FindWithTag("Player").GetComponent<Fighter>();
        }

        private void Start()
        {
            currentWeaponImage = weaponImages[0];
        }

        private void Update()
        {
            if(currentWeaponImage.weaponName == fighter.GetCurrentWeapon().GetWeaponName()) { return; }

            SetWeaponImage(fighter.GetCurrentWeapon().GetWeaponName());
        }

        private void SetWeaponImage(string weaponName)
        {
            foreach (WeaponImage weaponImage in weaponImages)
            {
                if (weaponName != weaponImage.weaponName) { continue; }

                GetComponent<Image>().sprite = weaponImage.sprite;
                currentWeaponImage = weaponImage;
            }
        }

        [System.Serializable]
        private class WeaponImage
        {
            public string weaponName;
            public Sprite sprite;
        }
    } 
}
