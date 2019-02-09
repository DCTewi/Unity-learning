using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public float healthPoint = 100f;
    public float attackPower = 10f;
    public float speed = 5.0f;
    public float jumpHeight = 1000f;
    public float maxSpeed = 5f;

    [HideInInspector] public bool faceRight = true;

    private Animator anim;
    private Rigidbody2D rb;
    private SoundEffecter se;
    private bool onland = false;
    private bool jump = false;

    private void Flip()
    {
        faceRight = !faceRight;
        var newScale = transform.localScale;
        newScale.x *= -1;
        transform.localScale = newScale;
    }

    private void Awake()
    {
        anim = GetComponent<Animator>();
        rb = GetComponent<Rigidbody2D>();
        se = transform.Find("SoundEffecter").gameObject.GetComponent<SoundEffecter>();
    }

    private void Update()
    {
        if (Input.GetButtonDown(Axis.C) || Input.GetButtonDown(Axis.BtnA))
        {
            if (onland)
            {
                jump = true;
                onland = false;
            }
        }
    }

    private void FixedUpdate()
    {
        //Move
        float h = Input.GetAxis(Axis.Horizontal) * speed;

        if (rb.velocity.x * h < maxSpeed)
        {
            rb.AddForce(Vector2.right * h * speed);
        }
        if (rb.velocity.x > maxSpeed)
        {
            rb.velocity = new Vector2(Mathf.Sign(rb.velocity.x) * maxSpeed, rb.velocity.y);
        }
        anim.SetFloat("speed", rb.velocity.x);

        if ((h < 0 && faceRight) || (h > 0 && !faceRight))
        {
            Debug.Log("Fliped");
            Flip();
        }

        //Jump
        if (jump)
        {
            se.PlaySE("Jump");
            rb.AddForce(Vector2.up * jumpHeight);
            jump = false;

        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == Tags.Midground)
        {
            onland = true;
        }
    }
}
