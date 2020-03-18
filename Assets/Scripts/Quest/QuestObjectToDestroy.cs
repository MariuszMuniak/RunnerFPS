using FPS.Attributes;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

namespace FPS.Quest
{
    public class QuestObjectToDestroy : MonoBehaviour
    {
        [SerializeField] bool activateAfterDamage = true;
        [SerializeField] float damageToActivate = 50;
        [SerializeField] UnityEvent onActivation;

        Health health;

        bool wasActivated = false;

        private void Awake()
        {
            health = GetComponent<Health>();
        }

        void Update()
        {
            if (!activateAfterDamage) { return; }

            if (!wasActivated && ReceivedRightAmountOfDamage())
            {
                wasActivated = true;
                onActivation.Invoke();
            }
        }

        private bool ReceivedRightAmountOfDamage()
        {
            return health.CurrentHealth() + damageToActivate <= health.GetMaxHealth();
        }
    } 
}
