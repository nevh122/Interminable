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
        [SerializeField] float maxSpeed = 6f;
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
        public void MoveTo(Vector3 destination, float speedFraction)
        {
            navmeshagent.destination = destination;
            navmeshagent.speed = maxSpeed * Mathf.Clamp01(speedFraction);
            navmeshagent.isStopped = false;
        }
        public void StartMove(Vector3 destination, float speedFraction)
        {
            GetComponent<ActionScheduler>().StartAction(this);
            MoveTo(destination, speedFraction);
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
