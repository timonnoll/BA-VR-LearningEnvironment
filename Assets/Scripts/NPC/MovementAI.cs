using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

namespace TN
{
    /// <summary>
    /// Handle Movement from NPC to given waypoints.
    /// </summary>
    public class MovementAI : MonoBehaviour
    {
        [Header("Transform Settings")]
        public Transform[] waypoints;
        public Transform facing;


        [Header("Kinematic Properties")]
        public float speed;
        public float rotationSpeed;

        private int waypointIndex;
        private bool canMove;

        private Vector3 target;
        private NavMeshAgent agent;
        private LookAtController lookAtController;
        private Animator animator;

        // Get components of the object to be moved (NPC).
        private void Awake()
        {
            agent = GetComponent<NavMeshAgent>();
            lookAtController = GetComponent<LookAtController>();
            animator = GetComponent<Animator>();
        }

        // Start with first waypoint.
        private void Start()
        {
            waypointIndex = 0;
            UpdateTarget();
        }

        // Start moving to destination if object can move, otherwise rotate towards facing position.
        private void Update()
        {
            if (canMove)
            {
                // Set target destination, so NavMeshAgent can calculate fastest route.
                agent.destination = target;

                lookAtController.SetStatus(false);
                animator.SetFloat("Speed", speed);

                // Move until object reaches destination. 
                if (Vector3.Distance(transform.position, target) > 0.1f)
                {
                    transform.position = Vector3.MoveTowards(transform.position, target, speed * Time.deltaTime);
                }
                else
                {
                    // Deactivate NavMeshAgent and stop moving towards destination.
                    agent.isStopped = true;
                    canMove = false;
                    lookAtController.SetStatus(true);
                }
            }
            else
            {
                // Rotate towards facing position.
                Vector3 relativePos = facing.position - transform.position;
                Quaternion rotation = Quaternion.LookRotation(relativePos, Vector3.up);
                transform.rotation = Quaternion.Lerp(transform.rotation, rotation, rotationSpeed * Time.deltaTime);
            }
        }

        // Update destination waypoint.
        public void UpdateTarget()
        {
            target = waypoints[waypointIndex].position;
            agent.isStopped = false;
            canMove = true;
        }

        // Set index of new Waypoint.
        public void SetWaypointIndex(int index)
        {
            waypointIndex = index;
            UpdateTarget();
        }
    }

}
