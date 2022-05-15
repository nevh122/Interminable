using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using RPG.Core.Combat;

namespace RPG.Player.Movement
{
    public class Mover : MonoBehaviour
    {
        [SerializeField] Transform target;
        NavMeshAgent navmeshagent;

        private void Start()
        {
            navmeshagent = GetComponent<NavMeshAgent>();
        }
        void Update()
        {
            UpdateAnimator();
        }
        public void MoveTo(Vector3 destination)
        {
            navmeshagent.destination = destination;
            navmeshagent.isStopped = false;
        }
        public void StartMove(Vector3 destination)
        {
            GetComponent<Fighter>().Cancel();
            MoveTo(destination);
        }
        public void Stop()
        {
            navmeshagent.isStopped = true;
        }
        private void UpdateAnimator()
        {
            Vector3 velocity = navmeshagent.velocity;
            Vector3 localVelocity = transform.InverseTransformDirection(velocity);
            float speed = localVelocity.z;
            GetComponent<Animator>().SetFloat("forwardSpeed", speed);
        }
    }
}
