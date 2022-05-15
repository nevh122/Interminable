using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using RPG.Player.Movement;
using RPG.Core.Combat;

namespace RPG.Player.Control
{
    public class PlayerController : MonoBehaviour
    {
        private void Update()
        {
            if (InteractWithCombat()) return;
            if (InteractWithMovement()) return;
        }

        
        private bool InteractWithCombat()
        {
            RaycastHit[] hits = Physics.RaycastAll(GetRay());
            foreach(RaycastHit hit in hits)
            {
                Targeting target = hit.transform.GetComponent<Targeting>();
                if (target == null) continue;
                if (Input.GetMouseButtonDown(0))
                {
                    GetComponent<Fighter>().Attack(target);
                }
                return true;
            }
            return false;
        }
        private bool InteractWithMovement()
        {
            RaycastHit hit;
            bool hasHit = Physics.Raycast(GetRay(), out hit);
            if (hasHit)
            {
                if (Input.GetMouseButton(0))
                {
                    GetComponent<Mover>().StartMove(hit.point);
                }
                return true;
            }
            return false;
        }

        private static Ray GetRay()
        {
            return Camera.main.ScreenPointToRay(Input.mousePosition);
        }
    }
}
