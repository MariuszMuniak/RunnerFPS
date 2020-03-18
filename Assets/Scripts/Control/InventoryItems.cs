using FPS.Combat;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace FPS.Control
{
    public class InventoryItems : MonoBehaviour
    {
        [SerializeField] GameObject rifle = null;
        [SerializeField] GameObject handgun = null;
        [SerializeField] Image keyImage = null;

        WeaponConfig currentWeapon = null;
        int money = 0;
        bool hasKey = false;

        private void Update()
        {
            if (hasKey)
            {
                keyImage.enabled = true;
            }
            else
            {
                keyImage.enabled = false;
            }
        }

        public bool HasKey
        {
            get
            {
                return hasKey;
            }
        }

        public void TakeKey()
        {
            hasKey = true;
        }

        public int GetMoney()
        {
            return money;
        }

        public void IncreaseMoney(int value)
        {
            money += value;
        }

        public void SwitchToRifle()
        {
            handgun.SetActive(false);
            rifle.SetActive(true);
            SetCurrentWeapon(rifle);
        }

        public void SwitchToHandgun()
        {
            rifle.SetActive(false);
            handgun.SetActive(true);
            SetCurrentWeapon(handgun);
        }

        public WeaponConfig GetCurrentWeapon()
        {
            return currentWeapon;
        }

        private void SetCurrentWeapon(GameObject weapon)
        {
            GetComponent<Fighter>().AttachWeapon(weapon);
        }
    }
}
