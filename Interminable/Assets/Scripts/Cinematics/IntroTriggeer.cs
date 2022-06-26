using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Playables;

public class IntroTriggeer : MonoBehaviour
{
    bool hasPlayed = false;
    private void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.tag == "Player" && !hasPlayed)
        {
            GetComponent<PlayableDirector>().Play();
            hasPlayed = true;
        }
    }
}
