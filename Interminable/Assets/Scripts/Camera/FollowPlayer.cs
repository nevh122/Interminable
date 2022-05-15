using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace RPG.Core.Camera
{
    public class FollowPlayer : MonoBehaviour
    {
        public Transform playerPos;
        void Start()
        {

        }
        void LateUpdate()
        {
            transform.position = playerPos.position;
        }
    }

}