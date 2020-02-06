using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CCTVPlayerDetection : MonoBehaviour
{
    private GameObject player;
    private LastPlayerSighting lastPlayerSighting;

    private void Start()
    {
        player = GameObject.FindGameObjectWithTag(Tags.Player);
        lastPlayerSighting = GameObject.FindGameObjectWithTag(Tags.GameController).GetComponent<LastPlayerSighting>();
    }

    private void OnTriggerStay(Collider other)
    {
        if (other.gameObject == player)
        {
            Vector3 relPlayerPos = player.transform.position - this.transform.position;
            RaycastHit hit;
            if (Physics.Raycast(transform.position, relPlayerPos, out hit))
            {
                if (hit.collider.gameObject == player)
                {
                    lastPlayerSighting.position = player.transform.position;
                }
            }
        }
    }
}
