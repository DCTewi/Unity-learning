using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoltController : MonoBehaviour
{
    [Range (0.0f, 20.0f)]
    public float speed = 10.0f;

    private Rigidbody rb;

    private void Start()
    {
        rb = this.gameObject.GetComponent<Rigidbody>();
        if (rb != null)
        {
            rb.velocity = new Vector3(0.0f, 0.0f, speed);
        }
    }
}
