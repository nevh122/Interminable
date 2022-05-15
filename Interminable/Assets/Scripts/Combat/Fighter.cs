using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using RPG.Player.Movement;

namespace RPG.Core.Combat
{
    public class Fighter : MonoBehaviour
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
                GetComponent<Mover>().Stop();
            }
        }

        private bool GetRange()
        {
            return Vector3.Distance(transform.position, target.position) < weaponRange;
        }

        public void Attack(Targeting combattarget)
        {
            target = combattarget.transform;
        }
        public void Cancel()
        {
            target = null;
        }
    }
}
