using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class Boundary
{
    public float xMin, xMax, zMin, zMax;
}

public class PlayerController : MonoBehaviour
{
    [Range(0.0f, 10.0f)]
    public float speed = 5.0f;
    [Range(0.0f, 1.0f)]
    public float touchSpeed = 0.5f;
    [Range(0.0f, 8.0f)]
    public float tilt = 4.0f;
    public Boundary boundary;

    public float fireColdDown = 0.5f;
    public GameObject bolt;
    public Transform shoter;

    private float fireColding = 0.0f;
    private Rigidbody rb;

    private void Start()
    {
        rb = this.gameObject.GetComponent<Rigidbody>();
    }

    private void FixedUpdate()
    {
        float moveHorizontal = Input.GetAxis("Horizontal");
        float moveVertical = Input.GetAxis("Vertical");

        if (Input.touchCount > 0)
        {
            if (Input.touches[0].phase == TouchPhase.Moved)
            {
                moveHorizontal += Input.GetAxis("Mouse X") * touchSpeed;
                moveVertical += Input.GetAxis("Mouse Y") * touchSpeed;
            }
        }

        Vector3 movement = new Vector3(moveHorizontal, 0.0f, moveVertical);
        rb.velocity = movement * speed;
        rb.position = new Vector3(Mathf.Clamp(rb.position.x, boundary.xMin, boundary.xMax), 0.0f, Mathf.Clamp(rb.position.z, boundary.zMin, boundary.zMax));
        rb.rotation = Quaternion.Euler(0.0f, 0.0f, rb.velocity.x * -tilt);
    }

    private void Update()
    {
        if ((Input.GetButton("Fire1") || Input.touchCount > 0) && fireColding > fireColdDown)
        {
            fireColding = 0.0f;
            Instantiate(bolt, shoter.position, shoter.rotation);
            this.GetComponent<AudioSource>().Play();
        }
        if (fireColding < fireColdDown)
        {
            fireColding += Time.deltaTime;
        }
    }
}
