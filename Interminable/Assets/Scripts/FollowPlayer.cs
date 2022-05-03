using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FollowPlayer : MonoBehaviour
{
    public Transform playerPos;
    void Start()
    {
        
    }
    void Update()
    {
        transform.position = playerPos.position;
    }
}
