using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LaserPlayerDetection : MonoBehaviour
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
        if (this.GetComponent<Renderer>().enabled)
        {
            if (other.gameObject == player)
            {
                lastPlayerSighting.position = other.transform.position;
            }
        }
    }
}
