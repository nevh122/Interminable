using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using RPG.Player.Movement;

namespace RPG.Core.Combat
{
    public class Fighter : MonoBehaviour, IAction
    {
        [SerializeField] float weaponRange = 2f;
        [SerializeField] float timeBetweenAttacks = 1f;
        [SerializeField] float playerDamage = 5f;
        Health target;
        float timeSinceLastAttack = 0;
        private void Update()
        {
            timeSinceLastAttack = Mathf.Infinity;
            if (target == null) return;
            if (target.IsDead()) return;
            if (!GetRange())
            {
                GetComponent<Mover>().MoveTo(target.transform.position, 1f);
            }
            else
            {
                GetComponent<Mover>().Cancel();
                AttackBehavior();
            }
        }
        private void AttackBehavior()
        {
            transform.LookAt(target.transform);
            if(timeSinceLastAttack > timeBetweenAttacks)
            {
                Attack();
                timeSinceLastAttack = 0;
                //triggers Hit()
            }

        }
        private void Attack()
        {
            GetComponent<Animator>().ResetTrigger("stopAttack");
            GetComponent<Animator>().SetTrigger("attack");
        }

        private bool GetRange()
        {
            return Vector3.Distance(transform.position, target.transform.position) < weaponRange;
        }
        public bool CanAttack(GameObject combatTarget)
        {
            if(combatTarget == null)
            {
                return false;
            }
            Health targettoTest = combatTarget.GetComponent<Health>();
            return targettoTest != null && !targettoTest.IsDead();
        }
        public void Attack(GameObject combattarget)
        {
            GetComponent<ActionScheduler>().StartAction(this);
            target = combattarget.GetComponent<Health>();
        }
        public void Cancel()
        {
            StopAttack();
            target = null;
            GetComponent<Mover>().Cancel();
        }
        private void StopAttack()
        {
            GetComponent<Animator>().ResetTrigger("attack");
            GetComponent<Animator>().SetTrigger("stopAttack");
        }

        //Animation Event
        void Hit()
        {
            if (target == null) return;
            target.TakeDamage(playerDamage);
        }
    }
}
