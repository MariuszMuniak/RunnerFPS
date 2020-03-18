using FPS.Movement;
using FPS.Combat;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace FPS.Control
{
    [RequireComponent(typeof(AIMover))]
    public class AIController : MonoBehaviour
    {
        [SerializeField] float chaseDistance = 10f;
        [SerializeField] float shoutDistance = 5f;
        [SerializeField] Transform hand;
        [SerializeField] PatrolPath patrolPath = null;
        [SerializeField] float waypointTolerance = 1f;
        [SerializeField] float waypointDwellTime = 3f;
        [Range(0, 1)]
        [SerializeField] float patrolSpeedFraction = 0.5f;

        GameObject player;
        AIMover aiMover;
        AIFighter aiFighter;

        bool sawPlayer = false;
        float maxRaycastDistance;
        float timeSinceArrivedAtWaypoint = Mathf.Infinity;
        int currentWaypointIndex = 0;

        private void Awake()
        {
            player = GameObject.FindWithTag("Player");
            aiMover = GetComponent<AIMover>();
            aiFighter = GetComponent<AIFighter>();
        }

        void Start()
        {
            maxRaycastDistance = chaseDistance * 2;

            if (patrolPath != null)
            {
                MoveToNextWaypoint();
            }
        }

        void Update()
        {
            if (IsAggravated() || SeesPlayer())
            {
                aiMover.SetStoppingDistance(shoutDistance);
                aiMover.MoveTo(player.transform.position, 1);
            }
            else
            {
                Patrol();
            }

            if (SeesPlayer() && IsInWeaponRange())
            {
                FaceToPlayer();

                if (aiFighter.ReadyToFire())
                {
                    aiFighter.Fire();
                }
            }

            if(sawPlayer && !SeesPlayer())
            {
                aiMover.SetStoppingDistance(1);
                aiMover.MoveTo(player.transform.position, 1);
            }

            timeSinceArrivedAtWaypoint += Time.deltaTime;
        }

        public void ChasePlayer()
        {
            if (!IsAggravated())
            {
                aiMover.MoveTo(player.transform.position, 1.25f);
            }
        }

        private void Patrol()
        {
            if (patrolPath == null) { return; }

            aiMover.SetStoppingDistance(0.1f);

            if (AtWaypoint())
            {
                timeSinceArrivedAtWaypoint = 0;
                CycleWaypoint();
            }

            if (timeSinceArrivedAtWaypoint > waypointDwellTime)
            {
                MoveToNextWaypoint();
            }
        }

        private void MoveToNextWaypoint()
        {
            aiMover.MoveTo(GetCurrentWayppoint(), patrolSpeedFraction);
        }

        private Vector3 GetCurrentWayppoint()
        {
            return patrolPath.GetWaypoint(currentWaypointIndex);
        }

        private void CycleWaypoint()
        {
            currentWaypointIndex = patrolPath.GetNextIndex(currentWaypointIndex);
        }

        private bool AtWaypoint()
        {
            float distanceToWaypoint = Vector3.Distance(transform.position, GetCurrentWayppoint());

            return distanceToWaypoint < waypointTolerance;
        }

        private bool IsAggravated()
        {
            return DistanceToPlayer() < chaseDistance;
        }

        private bool IsInWeaponRange()
        {
            return DistanceToPlayer() < shoutDistance;
        }

        private bool SeesPlayer()
        {
            Ray ray = new Ray(hand.position, hand.forward);

            if (Physics.Raycast(ray, out RaycastHit hit, maxRaycastDistance))
            {
                if (hit.collider.gameObject.CompareTag("Player")) 
                {
                    sawPlayer = true;
                    return true; 
                }
            }

            return false;
        }

        private float DistanceToPlayer()
        {
            return Vector3.Distance(player.transform.position, transform.position);
        }

        private void FaceToPlayer()
        {
            Vector3 target = new Vector3(player.transform.position.x, transform.position.y, player.transform.position.z);

            transform.LookAt(target);
        }

        private void OnDrawGizmos()
        {
            Gizmos.color = Color.black;
            Gizmos.DrawWireSphere(transform.position, chaseDistance);
            Gizmos.color = Color.red;
            Gizmos.DrawWireSphere(transform.position, maxRaycastDistance);

            Gizmos.color = Color.red;
            Gizmos.DrawRay(hand.position, hand.forward * 100);
        }
    }
}
