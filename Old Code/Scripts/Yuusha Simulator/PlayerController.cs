using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public float hpMax = 100f;
    public float healthPoint = 100f;
    public float attackPower = 15f;
    public float speed = 5.0f;
    public float jumpHeight = 530f;
    public float maxSpeed = 5f;

    public GameObject magicBall;
    public float coldDownMagic = 0.8f;
    public float coldDownAtk = 0.6f;

    [HideInInspector] public bool faceRight = true;
    [HideInInspector] public bool meleeAtking = false;

    private float coldingDownMagic = 0.0f;
    private float coldingDownAtk = 0.0f;

    private Animator anim;
    private Rigidbody2D rb;
    private SoundEffecter se;
    private GameController gameController;
    private CameraController mainCamera;
    private bool onland = false;
    private bool jump = false;
    private bool dead = false;

    public IEnumerator Dying()
    {
        dead = true;
        se.PlaySE("Death");
        anim.SetTrigger("dead");
        yield return new WaitForSecondsRealtime(1.0f);
        gameController.GameOver();
    }

    public void Hurt(float damage)
    {
        healthPoint -= Mathf.Min(healthPoint, damage);
        se.PlaySE("Damaged");

        if (healthPoint <= 0f && !dead)
        {
            StartCoroutine(Dying());
        }
    }

    public void Heal(float value)
    {
        healthPoint += Mathf.Min(hpMax - healthPoint, value);
    }

    public IEnumerator MagicAttack()
    {
        anim.SetTrigger("magic");
        se.PlaySE("Attack" + UnityEngine.Random.Range((int)0, (int)2).ToString());
        yield return new WaitForSeconds(0.5f);

        Vector3 ballPos = new Vector3(transform.position.x + (faceRight ? 0.5f : -0.5f), transform.position.y);
        Instantiate(magicBall, ballPos, Quaternion.Euler(0, 0, (faceRight ? 0f : 180f)));
    }

    public IEnumerator Attack()
    {
        anim.SetTrigger("attack");
        se.PlaySE("Attack" + UnityEngine.Random.Range((int)0, (int)2).ToString());
        meleeAtking = true;
        yield return new WaitForSeconds(0.5f);
        meleeAtking = false;
    }

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
        gameController = GameObject.FindGameObjectWithTag(Tags.GameController).GetComponent<GameController>();
    }

    private void Start()
    {
        mainCamera = GameObject.FindGameObjectWithTag(Tags.MainCamera).GetComponent<CameraController>();
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

        if (Input.GetButtonDown(Axis.Z) || Input.GetButtonDown(Axis.BtnX))
        {
            if (coldingDownMagic <= 0.0f)
            {
                StartCoroutine(MagicAttack());
                coldingDownMagic = coldDownMagic;
            }
        }

        if (Input.GetButtonDown(Axis.X) || Input.GetButtonDown(Axis.BtnY))
        {
            if (coldingDownAtk <= 0.0f)
            {
                StartCoroutine(Attack());
                coldingDownAtk = coldDownAtk;
            }
        }

        //After
        if (coldingDownAtk >= 0.0f)
        {
            coldingDownAtk -= Time.deltaTime;
        }
        if (coldingDownMagic >= 0.0f)
        {
            coldingDownMagic -= Time.deltaTime;
        }
        if (healthPoint > hpMax)
        {
            healthPoint = hpMax;
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

    private void OnTriggerEnter2D(Collider2D c)
    {
        if (c.gameObject.tag == Tags.EnemyWeapon)
        {
            Hurt(UnityEngine.Random.Range(5.0f, 30.0f));
            mainCamera.Shake();
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
