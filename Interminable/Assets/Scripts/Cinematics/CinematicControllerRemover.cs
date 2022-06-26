using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using RPG.Player.Control;
using UnityEngine.Playables;

namespace RPG.Core.Cinematics
{
    public class CinematicControllerRemover : MonoBehaviour
    {
        [SerializeField]GameObject player;
        private void Start()
        {
            player = GameObject.FindWithTag("Player");
            GetComponent<PlayableDirector>().played += DisableControl;
            GetComponent<PlayableDirector>().stopped += EnableControl;
        }
        void DisableControl(PlayableDirector pd)
        { 
            player.GetComponent<ActionScheduler>().CancelCurrentAction();
            player.GetComponent<PlayerController>().enabled = false;
        }
        void EnableControl(PlayableDirector pd)
        {
            player.GetComponent<PlayerController>().enabled = true;
        }
    }
}
