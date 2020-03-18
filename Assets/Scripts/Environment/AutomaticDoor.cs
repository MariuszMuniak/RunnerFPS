using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace FPS.Environment
{
    public class AutomaticDoor : MonoBehaviour
    {
        [SerializeField] Transform openDoor;
        [SerializeField] float triggerDistance = 5f;
        [SerializeField] float speed = 5f;
        [SerializeField] float closeDoorDwellTime = 3f;
        [SerializeField] AudioSource audioSource = null;
        [SerializeField] AudioClip openCloseSound = null;

        Transform player;
        Vector3 closeDoorPosition;
        Vector3 openDoorPosition;
        float timeSincetimeSincePlayerIsOutOfRange = Mathf.Infinity;
        bool soundWasPlaying = true;
        bool forcedToOpen = false;

        private void Awake()
        {
            player = GameObject.FindWithTag("Player").GetComponent<Transform>();
        }

        void Start()
        {
            closeDoorPosition = transform.position;
            openDoorPosition = openDoor.position;
        }

        void Update()
        {
            if (forcedToOpen)
            {
                OpenDoor();
                return;
            }

            if (DistnaceToPlayer() < triggerDistance)
            {
                timeSincetimeSincePlayerIsOutOfRange = 0;
                OpenDoor();
            }
            else
            {
                timeSincetimeSincePlayerIsOutOfRange += Time.deltaTime;
                CloseDoor();
            }
        }

        public void ForcedToOpen()
        {
            forcedToOpen = true;
        }

        public void NotForcedToOpen()
        {
            forcedToOpen = false;
        }

        private void OpenDoor()
        {
            if (transform.position.y >= openDoorPosition.y)
            {
                transform.position = openDoorPosition;
                soundWasPlaying = false;
            }
            else
            {
                PlaySoun();
                transform.Translate(Vector3.up * Time.deltaTime * speed);
            }
        }

        private void CloseDoor()
        {
            if (timeSincetimeSincePlayerIsOutOfRange < closeDoorDwellTime) { return; }

            if (transform.position.y <= closeDoorPosition.y)
            {
                transform.position = closeDoorPosition;
                soundWasPlaying = false;
            }
            else
            {
                PlaySoun();
                transform.Translate(Vector3.down * Time.deltaTime * speed);
            }
        }

        private void PlaySoun()
        {
            if(audioSource == null) { return; }
            if(openCloseSound == null) { return; }
            if(soundWasPlaying) { return; }

            soundWasPlaying = true;
            audioSource.PlayOneShot(openCloseSound);
        }

        private float DistnaceToPlayer()
        {
            return Vector3.Distance(transform.position, player.position);
        }

        private void OnDrawGizmos()
        {
            Gizmos.DrawWireSphere(transform.position, triggerDistance);
        }
    }
}
