using Cinemachine;
using System.Collections;
using UnityEngine;

public class Player : MonoBehaviour
{
    public static Player Instance;

    public delegate void PlayerHealthChangedHandler(float percent);
    public static PlayerHealthChangedHandler OnPlayerHealthChanged;

    [Header("System")]
    public Texture2D CursorTexture;

    [Header("Status")]
    public MagicBall MagicBall;
    public Transform ShootingPort;

    private Animator animator;
    private CinemachineImpulseSource impulse;

    [SerializeField] private int health;
    [SerializeField] private int maxHealth;
    [SerializeField] private float speed;

    private Vector2 direction;
    private LineRenderer aimLine;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        else Destroy(gameObject);

        animator = GetComponent<Animator>();
        impulse = GetComponent<CinemachineImpulseSource>();

        aimLine = gameObject.AddComponent<LineRenderer>();
        aimLine.material = new Material(Shader.Find("Legacy Shaders/Particles/Additive"));
        aimLine.positionCount = 2;
        aimLine.startColor = aimLine.endColor = new Color(200, 200, 200, 50);
        aimLine.startWidth = aimLine.endWidth = 0.1f;

        Cursor.SetCursor(CursorTexture, Vector2.zero, CursorMode.Auto);

        speed = 25f;
        maxHealth = health = 10;
    }

    private void Update()
    {
        HandleMove();
        HandleAttack();
        HandleDirection();
    }

    private void HandleDirection()
    {
        var pos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        direction = (pos - transform.position).normalized;

        // Debug.Log(direction.ToString());

        // transform.LookAt(pos);

        aimLine.SetPosition(0, transform.position);
        aimLine.SetPosition(1, pos);

        if (direction.x > 0 == transform.localScale.x > 0)
        {
            Flip();
        }
    }

    private void HandleAttack()
    {
        if (health == 0) return;

        if (Input.GetMouseButtonDown(0))
        {
            animator.SetTrigger("Attack");
            impulse.GenerateImpulse();

            StartCoroutine(ShootingBall());
        }
    }

    private IEnumerator ShootingBall()
    {
        yield return new WaitForSecondsRealtime(25f / 60f);
        Instantiate(MagicBall, ShootingPort.position, Quaternion.identity).SetStatus(40, direction);
    }

    private void HandleMove()
    {
        if (health == 0) return;

        float h = Input.GetAxis("Horizontal"),
            v = Input.GetAxis("Vertical");
        Vector2 move = new Vector2(h, v);

        //if (!Mathf.Approximately(h, 0) || !Mathf.Approximately(v, 0))
        //{
        //    Debug.Log(string.Format("Move: ({0}, {1}) Magnitude of move = {2}", move.x, move.y, move.magnitude));
        //}

        animator.SetFloat("Speed", move.magnitude);

        // Vector2 pos = rb.position + move * speed * Time.deltaTime;
        // rb.MovePosition(pos);
        transform.Translate(move * speed * Time.deltaTime);
    }

    public void ChangeHealth(int delta)
    {
        health = Mathf.Clamp(health + delta, 0, maxHealth);
        OnPlayerHealthChanged.Invoke(health / (float)maxHealth);
        if (health == 0)
        {
            animator.SetTrigger("Dead");
        }
    }

    private void Flip()
    {
        var scale = transform.localScale;
        scale.x *= -1;

        transform.localScale = scale;
    }
}
