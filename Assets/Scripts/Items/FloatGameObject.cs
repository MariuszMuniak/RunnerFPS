using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace FPS.Items
{
    public class FloatGameObject : MonoBehaviour
    {
        [SerializeField] Transform go;
        [SerializeField] float speed = 1f;
        [SerializeField] float range = 1f;

        Vector3 initialPosition;

        private void Start()
        {
            initialPosition = go.localPosition;
        }

        void Update()
        {
            go.localPosition = initialPosition + go.transform.up * Mathf.Sin(Time.time * speed) * range;
        }
    }
}
