using UnityEngine;

public class MagicBall : MonoBehaviour
{
    public class MagicHitArgs
    {
        public GameObject Target;
        public int Damage;

        public MagicHitArgs(GameObject target, int damage)
        {
            Target = target; Damage = damage;
        }
    }
    public delegate void MagicHitHandler(MagicHitArgs args);
    public static event MagicHitHandler OnMagicHit;

    private float lifetime = 3.0f;
    private int damage = 2;
    private float speed = 10f;
    private Vector2 direction;


    private Rigidbody2D rb;

    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        Destroy(gameObject, lifetime);
    }

    public void SetStatus(float v, Vector2 dir)
    {
        direction = dir; speed = v;
    }

    public void Update()
    {
        transform.Translate(new Vector3(direction.x, direction.y) * speed * Time.deltaTime);
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        Debug.Log("Collided at " + collision.gameObject.ToString());
        if (!collision.gameObject.CompareTag("Player"))
        {
            if (OnMagicHit != null)
            {
                OnMagicHit.Invoke(new MagicHitArgs(collision.gameObject, damage));
            }
        }
        Destroy(gameObject);
    }
}
