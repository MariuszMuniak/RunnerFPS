using FPS.Attributes;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace FPS.Combat
{
    public class KnifeAttack : MonoBehaviour
    {
        private void OnTriggerEnter(Collider other)
        {
            if (other.gameObject.CompareTag("Enemy"))
            {
                other.GetComponent<Health>().TakeDamage(gameObject, 50);
            }
        }
    } 
}
