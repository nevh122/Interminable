using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using RPG.Core;
using RPG.Core.Combat;

namespace RPG.Player.Movement
{
    public class Mover : MonoBehaviour, IAction
    {
        [SerializeField] Transform target;
        NavMeshAgent navmeshagent;
        Health health;

        private void Start()
        {
            navmeshagent = GetComponent<NavMeshAgent>();
            health = GetComponent<Health>();
        }
        void Update()
        {
            navmeshagent.enabled = !health.IsDead();
            UpdateAnimator();
        }
        public void MoveTo(Vector3 destination)
        {
            navmeshagent.destination = destination;
            navmeshagent.isStopped = false;
        }
        public void StartMove(Vector3 destination)
        {
            GetComponent<ActionScheduler>().StartAction(this);
            MoveTo(destination);
        }
        public void Cancel()
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
