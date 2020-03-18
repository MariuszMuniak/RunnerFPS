using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace FPS.Items
{
    [RequireComponent(typeof(Animation))]
    public class SpinGameObject : MonoBehaviour
    {
        [SerializeField] bool playAnimationOnStart;
        [SerializeField] float speed;

        Animation anim;

        private void Awake()
        {
            anim = GetComponent<Animation>();
        }

        void Start()
        {
            if (playAnimationOnStart)
            {
                anim.Play("ShowUpSpinningAnimation");
            }
        }

        void Update()
        {
            if (anim.isPlaying) { return; }

            transform.Rotate(Vector3.up * Time.deltaTime * speed);
        }
    } 
}
