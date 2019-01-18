/*
 * This .cs file is used in Ball-Game.
 * A game that moves the plate to prevent the ball from falling out of the screen.
 */

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlateController : MonoBehaviour
{
    //Plate's Speed
    [Range (0.0f, 10.0f)]
    public float speed = 5.0f;

    private void Update()
    {
        float h = Input.GetAxis("Horizontal");
        transform.Translate(Vector3.right * h * speed * Time.deltaTime);
    }
}
