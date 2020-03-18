using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace FPS.Control
{
    public class FacingToThePlayer : MonoBehaviour
    {
        Transform player;

        private void Awake()
        {
            player = GameObject.FindWithTag("Player").GetComponent<Transform>();
        }

        private void LateUpdate()
        {
            transform.LookAt(player);
        }
    } 
}
