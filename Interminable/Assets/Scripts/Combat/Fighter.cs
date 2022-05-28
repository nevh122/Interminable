using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using RPG.Player.Movement;

namespace RPG.Core.Combat
{
    public class Fighter : MonoBehaviour, IAction
    {
        [SerializeField] float weaponRange = 2f;
        Transform target;
        private void Update()
        {
            if (target == null) return;
            if (!GetRange())
            {
                GetComponent<Mover>().MoveTo(target.position);
            }
            else
            {
                GetComponent<Mover>().Cancel();
                AttackBehavior();
            }
        }

        private void AttackBehavior()
        {
            GetComponent<Animator>().SetTrigger("attack");
        }

        private bool GetRange()
        {
            return Vector3.Distance(transform.position, target.position) < weaponRange;
        }

        public void Attack(Targeting combattarget)
        {
            GetComponent<ActionScheduler>().StartAction(this);
            target = combattarget.transform;
        }
        public void Cancel()
        {
            target = null;
        }

        //Animation Event
        void Hit()
        {

        }
    }
}
