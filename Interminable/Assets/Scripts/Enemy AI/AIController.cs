using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using RPG.Core.Combat;
using RPG.Player.Movement;
using RPG.Core;

namespace RPG.AI.Control
{
    public class AIController : MonoBehaviour
    {
        [SerializeField] float chaseDistance = 5f;
        [SerializeField] float suspicionTime = 3f;
        [SerializeField] PatrolPath patrolPath = null;
        [SerializeField] float waypointTollerance = 1.8f;
        [SerializeField] float waypointDwellTime = 3f;

        [Range(0,1)]
        [SerializeField] float patrolSpeedFraction = 0.2f;

        Fighter fighter;
        GameObject player;
        Mover mover;
        Health health;

        Vector3 guardPosition;
        float timeSinceLastSawPlayer = Mathf.Infinity;
        float timeSiceArrivedAtWaypoint = Mathf.Infinity;
        int currentWaypointIndex = 0;
        private void Start()
        {
            fighter = GetComponent<Fighter>();
            health = GetComponent<Health>();
            mover = GetComponent<Mover>();
            player = GameObject.FindGameObjectWithTag("Player");

            guardPosition = transform.position;  
        }
        private void Update()
        {
            if (health.IsDead()) return;
            if (InAttackRange() && fighter.CanAttack(player))
            {
                AttackBehaviour();
            }
            else if (timeSinceLastSawPlayer < suspicionTime)
            {
                SuspicionBehaviour();
            }
            else
            {
                PatrolBehaviour();
            }
            UpdateTimers();
        }

        private void UpdateTimers()
        {
            timeSinceLastSawPlayer += Time.deltaTime;
            timeSiceArrivedAtWaypoint += Time.deltaTime;
        }

        private void PatrolBehaviour()
        {
            Vector3 nextPosition = guardPosition;
            if(patrolPath != null)
            {
                if (AtWaypoint())
                {
                    timeSiceArrivedAtWaypoint = 0;
                    CycleWaypoint();
                }
                nextPosition = GetCurrentWaypoint();
            }

            if(timeSiceArrivedAtWaypoint > waypointDwellTime)
            {
                mover.StartMove(nextPosition, patrolSpeedFraction);
            }
        }

        private void SuspicionBehaviour()
        {
            GetComponent<ActionScheduler>().CancelCurrentAction();
        }

        private void AttackBehaviour()
        {
            timeSinceLastSawPlayer = 0;
            fighter.Attack(player);
        }

        private bool InAttackRange()
        {
            float distanceToPlayer = Vector3.Distance(player.transform.position, transform.position);
            return distanceToPlayer < chaseDistance;
        }
        //Called by unity
        private void OnDrawGizmosSelected()
        {
            Gizmos.color = Color.blue;
            Gizmos.DrawWireSphere(transform.position, chaseDistance);
        }

        private bool AtWaypoint()
        {
            float distanceToWaypoint = Vector3.Distance(transform.position, GetCurrentWaypoint());
            return distanceToWaypoint < waypointTollerance;
        }
        private void CycleWaypoint()
        {
            currentWaypointIndex = patrolPath.GetNextIndex(currentWaypointIndex);
        }
        private Vector3 GetCurrentWaypoint()
        {
            return patrolPath.GetWaypoint(currentWaypointIndex);
        }
    }
}

