/*
 * This .cs file is used in Ball-Game.
 * A game that moves the plate to prevent the ball from falling out of the screen.
 */

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    public GameObject plate;
    private Vector3 offset;

    private void Start()
    {
        offset = transform.position - plate.transform.position;
    }

    private void LateUpdate()
    {
        transform.position = plate.transform.position + offset;
    }
}
