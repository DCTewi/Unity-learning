using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    //Move and Jump Variables.
    public float moveForce = 365f;
    public float maxSpeed = 5f;
    public float jumpForce = 1000f;
    //Shoot Variables.
    public Rigidbody2D rocket;
    public float rocketSpeed = 20f;

    //Face Directions.
    [HideInInspector] public bool facingRight = true;
    [HideInInspector] public bool jump = false;

    //Jump Boolean.
    private Transform groundCheck;
    private bool grounded = false;
    //Player Animator.
    private Animator anim;

    private void Flip()
    {
        facingRight = !facingRight;
        Vector3 theScale = transform.localScale;
        theScale.x *= -1;
        transform.localScale = theScale;
    }

    private void Start()
    {
        groundCheck = transform.Find("groundCheck");
        anim = GetComponent<Animator>();
    }

    private void Update()
    {
        //Move and Jump.
            // Another version:
            // grounded = Physics2D.Linecast(transform.position, groundCheck.position, 1 << LayerMask.NameToLayer("Ground"));
        grounded = Physics2D.Linecast(transform.position, groundCheck.position, LayerMask.GetMask("Ground"));
        
        if (Input.GetButtonDown("Jump"))
        {
            Debug.Log("Jumping.");
            if (grounded)
            {
                jump = true;
                Debug.Log("Jumped.");
            }
        }
        //Shoot.
        if (Input.GetButtonDown("Fire1"))
        {
            anim.SetTrigger("shoot");
            if (facingRight)
            {
                Rigidbody2D rocketInstance = Instantiate(rocket, transform.position, Quaternion.Euler(new Vector3(0f, 0f, 0f))) as Rigidbody2D;
                rocketInstance.velocity = new Vector2(rocketSpeed, 0);
            }
            else
            {
                Rigidbody2D rocketInstance = Instantiate(rocket, transform.position, Quaternion.Euler(new Vector3(0f, 0f, 180f))) as Rigidbody2D;
                rocketInstance.velocity = new Vector2(-rocketSpeed, 0);
            }
        }
    }

    private void FixedUpdate()
    {
        //Move.
        float h = Input.GetAxis("Horizontal");
        anim.SetFloat("speed", Mathf.Abs(h));
        Rigidbody2D rb = GetComponent<Rigidbody2D>();
        if (h * rb.velocity.x < maxSpeed)
        {
            rb.AddForce(Vector2.right * h * moveForce);
        }
        if (Mathf.Abs(rb.velocity.x) > maxSpeed)
        {
            rb.velocity = new Vector2(Mathf.Sign(rb.velocity.x) * maxSpeed, rb.velocity.y);
        }

        //Flip.
        if (h > 0 && !facingRight) Flip();
        else if (h < 0 && facingRight) Flip();

        //Jump.
        if (jump)
        {
            anim.SetTrigger("jump");
            rb.AddForce(new Vector2(0f, jumpForce));
            jump = false;
        }
    }
}
