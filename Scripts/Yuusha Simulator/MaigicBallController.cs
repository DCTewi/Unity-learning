using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MaigicBallController : MonoBehaviour
{
    public float speed = 5.0f;
    public float lifetime = 1.5f;

    private void Start()
    {
        GetComponent<Rigidbody2D>().velocity = new Vector2(transform.rotation.z == 0 ? speed : -speed, 0);
        Destroy(gameObject, lifetime);
    }
}
