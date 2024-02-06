using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

namespace TN
{
    public class MovementAI : MonoBehaviour
    {
        public Transform[] waypoints;

        public Transform facing;
        public float speed;
        public float rotationSpeed;
        int waypointIndex;
        bool canMove = true;
        Vector3 target;

        private NavMeshAgent agent;
        private LookAtController lookAtController;
        private Animator animator;
        // Start is called before the first frame update

        private void Awake()
        {
            agent = GetComponent<NavMeshAgent>();
            lookAtController = GetComponent<LookAtController>();
            animator = GetComponent<Animator>();
        }
        void Start()
        {
            UpdateTarget();
        }

        // Update is called once per frame
        void Update()
        {
            if (canMove)
            {
                agent.destination = target;
                lookAtController.SetStatus(false);
                animator.SetFloat("Speed", speed);
                if (Vector3.Distance(transform.position, target) > 0.1f)
                {
                    transform.position = Vector3.MoveTowards(transform.position, target, speed * Time.deltaTime);
                }
                else
                {
                    lookAtController.SetStatus(true);
                    agent.isStopped = true;
                    canMove = false;
                }
            }
            else
            {
                Vector3 relativePos = facing.position - transform.position;
                Quaternion rotation = Quaternion.LookRotation(relativePos, Vector3.up);
                transform.rotation = Quaternion.Lerp(transform.rotation, rotation, rotationSpeed * Time.deltaTime);
            }
        }

        public void UpdateTarget()
        {
            target = waypoints[waypointIndex].position;
        }

        public void SetWaypointIndex(int index)
        {
            waypointIndex = index;
        }
    }

}
