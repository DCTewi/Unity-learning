using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EnemyController : MonoBehaviour
{
    public float health = 15f;
    public Slider healthBar;

    public float speed = 1f;
    public float actionCD = 0.5f;

    [HideInInspector] public bool faceRight = false;
    [HideInInspector] public bool dirRight = false;

    private Animator anim;
    private SoundEffecter se;
    private Rigidbody2D rb;
    private PlayerController player;
    private CameraController mainCamera;

    private IEnumerator Dying()
    {
        anim.SetTrigger("dead");
        se.PlaySE("Death");
        yield return new WaitForSeconds(1.35f);

        Destroy(gameObject);
    }

    private void Hurt()
    {
        health -= UnityEngine.Random.Range(5.0f, 10.0f);

        if (health <= 0.0f)
        {
            StartCoroutine(Dying());
        }
    }

    private void Hurt(float damage)
    {
        health -= damage;

        if (health <= 0.0f)
        {
            StartCoroutine(Dying());
        }
    }

    private void Flip()
    {
        faceRight = !faceRight;
        transform.localScale = new Vector3(transform.localScale.x * -1, transform.localScale.y);
    }

    private IEnumerator Attack()
    {
        Vector2 oldV = rb.velocity;
        rb.velocity = new Vector2(0f, 0f);

        anim.SetTrigger("attack");
        se.PlaySE("Attack");

        yield return new WaitForSeconds(0.6f);
        rb.velocity = oldV;
    }

    private void Awake()
    {
        anim = GetComponent<Animator>();
        se = transform.Find("SoundEffecterEnemy").gameObject.GetComponent<SoundEffecter>();
    }

    private void Start()
    {
        player = GameObject.FindGameObjectWithTag(Tags.Player).GetComponent<PlayerController>();
        mainCamera = GameObject.FindGameObjectWithTag(Tags.MainCamera).GetComponent<CameraController>();
    }

    private void FixedUpdate()
    {
        dirRight = transform.position.x - player.transform.position.x < 0.0f;

        if (dirRight != faceRight)
        {
            Flip();
        }
    }

    private void Update()
    {
        healthBar.value = health;
    }

    private void OnTriggerEnter2D(Collider2D c)
    {
        if (c.gameObject.tag == Tags.PlayerWeapon)
        {
            Hurt(player.attackPower);
            mainCamera.Shake();
        }

        if (c.gameObject.tag == Tags.MagicBall)
        {
            Hurt();
            Destroy(c.gameObject);
            mainCamera.Shake();
        }
    }
}
