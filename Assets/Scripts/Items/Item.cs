using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace FPS.Items
{
    public class Item : MonoBehaviour
    {
        [SerializeField] GameObject textHelper;

        GameObject player;
        Camera m_Camera;
        float interactionDistance;

        private void Awake()
        {
            player = GameObject.FindWithTag("Player");
            m_Camera = Camera.main;
        }

        void Start()
        {
            interactionDistance = player.GetComponent<Interaction>().GetInteractionRange();
        }

        void Update()
        {
            if(textHelper == null) { return; }

            if (DistanceToPlayer() < interactionDistance)
            {
                textHelper.SetActive(true);
            }
            else
            {
                textHelper.SetActive(false);
            }
        }

        public void OnInteraction()
        {
            Destroy(gameObject);
        }

        private float DistanceToPlayer()
        {
            return Vector3.Distance(transform.position, m_Camera.transform.position);
        }
    } 
}
