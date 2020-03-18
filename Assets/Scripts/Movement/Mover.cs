using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityStandardAssets.Characters.FirstPerson;

namespace FPS.Movement
{
    public class Mover : MonoBehaviour
    {
        [SerializeField] float crouchSpeed = 2.5f;
        [SerializeField] float walkAimSpeed = 3f;
        [SerializeField] float walkSpeed = 5f;
        [SerializeField] float runSpeed = 10f;

        private FirstPersonController firstPersonController;
        private CharacterController characterController;
        bool isCrouching = false;

        public bool IsCrouching
        {
            get { return isCrouching; }
        }

        private void Awake()
        {
            firstPersonController = GetComponent<FirstPersonController>();
            characterController = GetComponent<CharacterController>();
        }

        public Vector3 CurrentSpeed()
        {
            return characterController.velocity;
        }

        public void Crouch()
        {
            characterController.height = 0.9f;
            firstPersonController.RunSpeed = crouchSpeed;
            isCrouching = true;
        }

        public void Stand()
        {
            characterController.height = 1.8f;
            firstPersonController.RunSpeed = runSpeed;
            isCrouching = false;
        }

        public void WalkWithAiming()
        {
            firstPersonController.WalkSpeed = isCrouching ? crouchSpeed : walkAimSpeed;
        }

        public void Walk()
        {
            firstPersonController.WalkSpeed = isCrouching ? crouchSpeed : walkSpeed;
        }

        public void Run()
        {
            firstPersonController.RunSpeed = runSpeed;
        }
    } 
}
