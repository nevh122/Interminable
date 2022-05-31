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
            timeSinceLastAttack += Time.deltaTime;
            if (target == null) return;
            if (target.IsDead()) return;
            if (!GetRange())
            {
                GetComponent<Mover>().MoveTo(target.transform.position);
            }
            else
            {
                GetComponent<Mover>().Cancel();
                AttackBehavior();
            }
        }

        private void AttackBehavior()
        {
            if(timeSinceLastAttack > timeBetweenAttacks)
            {
                GetComponent<Animator>().SetTrigger("attack");
                timeSinceLastAttack = 0;
                //triggers Hit()
            }
            
        }

        private bool GetRange()
        {
            return Vector3.Distance(transform.position, target.transform.position) < weaponRange;
        }

        public void Attack(Targeting combattarget)
        {
            GetComponent<ActionScheduler>().StartAction(this);
            target = combattarget.GetComponent<Health>();
        }
        public void Cancel()
        {
            GetComponent<Animator>().SetTrigger("stopAttack");
            target = null;
        }

        //Animation Event
        void Hit()
        {
            target.TakeDamage(playerDamage);
        }
    }
}
