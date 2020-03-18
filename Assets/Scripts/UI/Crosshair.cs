using FPS.Combat;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace FPS.UI
{
    public class Crosshair : MonoBehaviour
    {
        [SerializeField] Image crosshair;
        [SerializeField] Image crosshairHit;
        [SerializeField] float crosshairHitShowTime = 0.3f;

        private Fighter fighter;
        private float timeSinceLastShowCrosshairHit = Mathf.Infinity;

        private void Awake()
        {
            fighter = GetComponent<Fighter>();
        }

        private void Start()
        {
            crosshairHit.color = crosshair.color;
        }

        void Update()
        {
            if (fighter.IsAiming)
            {
                crosshair.enabled = false;
            }
            else
            {
                crosshair.enabled = true;
            }

            if(timeSinceLastShowCrosshairHit > crosshairHitShowTime)
            {
                crosshairHit.enabled = false;
            }

            timeSinceLastShowCrosshairHit += Time.deltaTime;
        }

        public void ShowCrosshairHit()
        {
            timeSinceLastShowCrosshairHit = 0;
            crosshairHit.enabled = true;
        }
    } 
}
