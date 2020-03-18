using FPS.Control;
using FPS.SceneManagement;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace FPS.Items
{
    public class Interaction : MonoBehaviour
    {
        [SerializeField] float range = 5f;
        [SerializeField] string[] tags;

        RaycastHit hit;

        void Update()
        {
            Ray ray = new Ray(Camera.main.transform.position, Camera.main.transform.forward);
            Physics.Raycast(ray, out hit, range);
        }

        public float GetInteractionRange()
        {
            return range;
        }

        public void Interact()
        {
            GameObject item = hit.collider.gameObject;

            switch (item.tag)
            {
                case "Key":
                    GetComponent<InventoryItems>().TakeKey();
                    item.GetComponent<Item>().OnInteraction();
                    break;
                case "EndGate":
                    if (GetComponent<InventoryItems>().HasKey)
                    {
                        item.GetComponent<Scene>().LoadScene(0);
                    }
                    break;
            }
        }

        public bool CanInteract()
        {
            if(hit.collider == null) { return false; }

            foreach (string tag in tags)
            {
                if (hit.collider.gameObject.CompareTag(tag))
                {
                    return true;
                }
            }

            return false;
        }

        private void OnDrawGizmos()
        {
            Gizmos.color = Color.yellow;
            Gizmos.DrawWireSphere(Camera.main.transform.position, range);
        }
    }
}